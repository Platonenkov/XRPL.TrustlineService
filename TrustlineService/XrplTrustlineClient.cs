using XRPL.TrustlineService.Domain;
using XRPL.TrustlineService.Domain.NFTsOffers;
using XRPL.TrustlineService.Domain.Xls20;

namespace XRPL.TrustlineService
{

    /// <summary> client to connect to <a>https://api.xrpldata.com/</a></summary>
    public class XrplTrustlineClient : BaseClient
    {
        public int ApiVersion { get; set; } = 1;
        public XrplTrustlineClient() : base("https://api.xrpldata.com/")
        {
        }
        /// <summary> get all trustlines info </summary>
        /// <returns><seealso cref="TrustlinesServerResponse"/></returns>
        public async Task<TrustlinesServerResponse> GetKnownTrustlines(CancellationToken Cancel = default)
        {
            var response = await GetAsync<TrustlinesServerResponse>($"api/v{ApiVersion}/tokens", Cancel);
            return response;
        }
        ///// <summary> get all XLS20 NFT info </summary>
        //public async Task<BaseServerResponse<List<IssuerXls20Nft>>> GetXls20NFT(CancellationToken Cancel = default)
        //{
        //    var response = await GetAsync<BaseServerResponse<List<IssuerXls20Nft>>>("api/v{ApiVersion}/xls20-nfts", Cancel);
        //    return response;
        //}
        /// <summary> Returns an array of XRPL accounts which have currently issued NFTs on the XRP Ledger. </summary>
        public async Task<BaseServerResponse<Xls20NftIssuers>> GetAllNFTIssuer(CancellationToken Cancel = default)
        {
            var response = await GetAsync<BaseServerResponse<Xls20NftIssuers>>($"api/v{ApiVersion}/xls20-nfts/all/issuers", Cancel);

            return response;
        }
        /// <summary> get all Issuer NFT info </summary>
        public async Task<BaseServerResponse<IssuerXls20Nft>> GetIssuerNFT(string Issuer, uint? Taxon = null, CancellationToken Cancel = default)
        {
            var address = Issuer;
            if (Taxon is not null)
            {
                address += $"/taxon/{Taxon}";
            }
            var response = await GetAsync<BaseServerResponse<IssuerXls20Nft>>($"api/v{ApiVersion}/xls20-nfts/issuer/{address}", Cancel);
            return response;
        }
        /// <summary> get all Issuer taxon (collections) </summary>
        public async Task<BaseServerResponse<Xls20TaxonResponse>> GetIssuerTaxon(string Issuer, CancellationToken Cancel = default)
        {
            var response = await GetAsync<BaseServerResponse<Xls20TaxonResponse>>($"api/v{ApiVersion}/xls20-nfts/taxon/{Issuer}", Cancel);

            return response;
        }
        /// <summary> get NFT info </summary>
        public async Task<BaseServerResponse<Xls20NftResponse>> GetNftInfoById(string NftId, CancellationToken Cancel = default)
        {
            var response = await GetAsync<BaseServerResponse<Xls20NftResponse>>($"api/v{ApiVersion}/xls20-nfts/nft/{NftId}", Cancel);
            return response;
        }
        /// <summary> get AccountNFTs </summary>
        public async Task<BaseServerResponse<AccountXls20Nft>> GetAccountNFTs(string account, CancellationToken Cancel = default)
        {
            var response = await GetAsync<BaseServerResponse<AccountXls20Nft>>($"api/v{ApiVersion}/xls20-nfts/owner/{account}", Cancel);
            return response;
        }
        /// <summary> get offers for nft by issuer  and taxon</summary>
        public async Task<BaseServerResponse<IssuerOffers>> GetIssuerNFTsOffers(string Issuer, uint? Taxon = null, CancellationToken Cancel = default)
        {
            var address = Issuer;
            if (Taxon is not null)
            {
                address += $"/taxon/{Taxon}";
            }
            var response = await GetAsync<BaseServerResponse<IssuerOffers>>($"api/v{ApiVersion}/xls20-nfts/offers/issuer/{address}", Cancel);
            return response;
        }
        /// <summary> Get all offers for NFTs owned by specific account</summary>
        public async Task<BaseServerResponse<AccountNFTsOffers>> GetAccountNFTsOffers(string Account, CancellationToken Cancel = default)
        {
            var response = await GetAsync<BaseServerResponse<AccountNFTsOffers>>($"api/v{ApiVersion}/xls20-nfts/offers/owner/{Account}", Cancel);
            return response;
        }
        /// <summary> Get all offers for a single NFT</summary>
        public async Task<BaseServerResponse<NFTokenOffers>> GetNFTokenIDOffers(string NFTokenID, CancellationToken Cancel = default)
        {
            var response = await GetAsync<BaseServerResponse<NFTokenOffers>>($"api/v{ApiVersion}/xls20-nfts/offers/nft/{NFTokenID}", Cancel);
            return response;
        }
        /// <summary> get a specific offer by its ID</summary>
        public async Task<BaseServerResponse<NFTOfferInfo>> GetNFTOfferInfo(string NFTOfferID, CancellationToken Cancel = default)
        {
            var response = await GetAsync<BaseServerResponse<NFTOfferInfo>>($"api/v{ApiVersion}/xls20-nfts/offer/id/{NFTOfferID}", Cancel);
            return response;
        }
    }


}
