using Newtonsoft.Json;

using XRPL.TrustlineService.Domain;
using XRPL.TrustlineService.Domain.Xls20;

namespace XRPL.TrustlineService
{
    /// <summary> client to connect to <a>https://api.xrpldata.com/</a></summary>
    public class XrplTrustlineClient : BaseClient
    {
        public XrplTrustlineClient():base("https://api.xrpldata.com/")
        {
        }
        /// <summary> get all trustlines info </summary>
        /// <returns><seealso cref="TrustlinesServerResponse"/></returns>
        public async Task<TrustlinesServerResponse> GetKnownTrustlines(CancellationToken Cancel = default)
        {
            var response = await GetAsync<TrustlinesServerResponse>("api/v1/tokens", Cancel);
            return response;
        }
        /// <summary> get all XLS20 NFT info </summary>
        /// <returns><seealso cref="Xls20NftServiceResponse"/></returns>
        public async Task<Xls20NftServiceResponse> GetXls20NFT(CancellationToken Cancel = default)
        {
            var response = await GetAsync<Xls20NftServiceResponse>("api/v1/xls20-nfts", Cancel);
            return response;
        }
        /// <summary> get all Issuer NFT info </summary>
        /// <returns><seealso cref="IssuerXls20NftResponse"/></returns>
        public async Task<IssuerXls20NftResponse> GetIssuerNFT(string Issuer,uint? Taxon = null, CancellationToken Cancel = default)
        {
            var address = Issuer;
            if (Taxon is not null)
            {
                address += $"/taxon/{Taxon}";
            }
            var response = await GetAsync<Dictionary<string,dynamic>>($"api/v1/xls20-nfts/issuer/{address}", Cancel);
            var IssuerInfo = new IssuerXls20NftResponse();
            if (response.Count > 5)
                throw new ArgumentException("Unknown field type");

            foreach (var key in response.Keys)
            {
                switch (key)
                {
                    case "ledger_index":
                        IssuerInfo.LedgerIndex = response[key];
                        break;
                    case "ledger_hash":
                        IssuerInfo.LedgerHash = response[key];
                        break;
                    case "ledger_close":
                        IssuerInfo.LedgerClose = response[key];
                        break;
                    case "ledger_close_ms":
                        IssuerInfo.LedgerCloseMs = response[key];
                        break;
                    default:
                        IssuerInfo.IssuerInfo = new IssuerXls20Nft() { Issuer = key, NFTs = JsonConvert.DeserializeObject<List<Xls20Nft>>($"{response[key]}") };
                        break;

                }
            }

            return IssuerInfo;
        }
        /// <summary> get all Issuer taxon (collections) </summary>
        /// <returns><seealso cref="Xls20TaxonResponse"/></returns>
        public async Task<Xls20TaxonResponse> GetIssuerTaxon(string Issuer, CancellationToken Cancel = default)
        {
            var response = await GetAsync<Dictionary<string,dynamic>>($"api/v1/xls20-nfts/taxon/{Issuer}", Cancel);
            var IssuerInfo = new Xls20TaxonResponse();
            if (response.Count > 5)
                throw new ArgumentException("Unknown field type");

            foreach (var key in response.Keys)
            {
                switch (key)
                {
                    case "ledger_index":
                        IssuerInfo.LedgerIndex = response[key];
                        break;
                    case "ledger_hash":
                        IssuerInfo.LedgerHash = response[key];
                        break;
                    case "ledger_close":
                        IssuerInfo.LedgerClose = response[key];
                        break;
                    case "ledger_close_ms":
                        IssuerInfo.LedgerCloseMs = response[key];
                        break;
                    default:
                        IssuerInfo.Issuer = key;
                        IssuerInfo.Taxon = JsonConvert.DeserializeObject<List<uint>>($"{response[key]}");
                        break;

                }
            }

            return IssuerInfo;
        }
        /// <summary> get NFT info </summary>
        /// <returns><seealso cref="Xls20NftResponse"/></returns>
        public async Task<Xls20NftResponse> GetNftInfoById(string NftId, CancellationToken Cancel = default)
        {
            var response = await GetAsync<Xls20NftResponse>($"api/v1/xls20-nfts/nft/{NftId}", Cancel);
            return response;
        }
    }


}
