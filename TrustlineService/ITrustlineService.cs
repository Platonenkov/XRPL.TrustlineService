using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XRPL.TrustlineService.Domain.NFTsOffers;
using XRPL.TrustlineService.Domain.Statistics;
using XRPL.TrustlineService.Domain.Xls20;
using XRPL.TrustlineService.Domain;

namespace XRPL.TrustlineService
{
    public interface ITrustlineService
    {
        /// <summary> get all trustlines info </summary>
        /// <returns><seealso cref="TrustlinesServerResponse"/></returns>
        Task<TrustlinesServerResponse> GetKnownTrustlines(CancellationToken Cancel = default);

        #region NFT methods

        /// <summary> Returns an array of XRPL accounts which have currently issued NFTs on the XRP Ledger. </summary>
        Task<BaseServerResponse<Xls20NftIssuers>> GetAllNFTIssuer(CancellationToken Cancel = default);

        /// <summary> get all Issuer NFT info </summary>
        Task<BaseServerResponse<IssuerXls20Nft>> GetIssuerNFT(string Issuer, uint? Taxon = null, CancellationToken Cancel = default);

        /// <summary> get all Issuer taxon (collections) </summary>
        Task<BaseServerResponse<Xls20TaxonResponse>> GetIssuerTaxon(string Issuer, CancellationToken Cancel = default);

        /// <summary> get NFT info </summary>
        Task<BaseServerResponse<Xls20NftResponse>> GetNftInfoById(string NftId, CancellationToken Cancel = default);

        /// <summary> get AccountNFTs </summary>
        Task<BaseServerResponse<AccountXls20Nft>> GetAccountNFTs(string account, CancellationToken Cancel = default);
        #endregion

        #region Offer methods

        /// <summary> get offers for nft by issuer  and taxon</summary>
        Task<BaseServerResponse<IssuerOffers>> GetIssuerNFTsOffers(string Issuer, uint? Taxon = null, CancellationToken Cancel = default);

        /// <summary> Get all offers for NFTs owned by specific account</summary>
        Task<BaseServerResponse<AccountNFTsOffers>> GetNFTsOffersForAccount(string Account, CancellationToken Cancel = default);

        /// <summary> Get all NFT offers owned by specific account</summary>
        Task<BaseServerResponse<AccountOffersForNFTs>> GetAccountNFTsOffers(string Account, CancellationToken Cancel = default);

        /// <summary> The Offer Destination Account which you want to get the Offers for.</summary>
        Task<BaseServerResponse<DestinationNFTsOffers>> GetDestinationNFTsOffers(string Account, CancellationToken Cancel = default);

        /// <summary> get all relevant offers for a specific account</summary>
        Task<BaseServerResponse<AllAccountNFTsOffers>> GetAllAccountNFTsOffers(string account, CancellationToken Cancel = default);

        /// <summary> get a specific offer by its ID</summary>
        Task<BaseServerResponse<NFTOfferInfo>> GetNFTOfferInfo(string NFTOfferID, CancellationToken Cancel = default);

        /// <summary> Get all offers for a single NFT</summary>
        Task<BaseServerResponse<NFTokenOffers>> GetNFTokenIDOffers(string NFTokenID, CancellationToken Cancel = default);

        #endregion

        #region Statistic methods

        /// <summary>
        /// Get stats about all NFTs from specific issuer.<br/>
        /// THIS API ENDPOINT EXCLUDES:<br/>
        ///- EXPIRED BUY OR SELL OFFERS<br/>
        ///- BUY OR SELL OFFERS WITH AMOUNT = 0<br/>
        ///- SELL OFFERS WHERE THE OFFER OWNER IS *NOT* THE NFT OWNER<br/>
        /// Returns an object containing live statistics about the issuer's NFTs<br/> 
        /// floor holds values of the lowest sell offers for various currencies with an amount > 0 which are placed across ALL Market Places or without Destination(open market)<br/>
        /// open_market holds stats about all offers WITHOUT a Destination set<br/>
        /// market_places holds stats for NFTs which have one of the MarketPlaces set as Destination
        /// </summary>
        Task<BaseServerResponse<CollectionNFTsStatats>> GetIssuerNFTsStats(string issuer, CancellationToken Cancel = default);

        /// <summary>
        /// Get stats about specific NFTs from specific issuer with the given Taxon.<br/>
        /// THIS API ENDPOINT EXCLUDES:<br/>
        ///- EXPIRED BUY OR SELL OFFERS<br/>
        ///- BUY OR SELL OFFERS WITH AMOUNT = 0<br/>
        ///- SELL OFFERS WHERE THE OFFER OWNER IS *NOT* THE NFT OWNER<br/>
        /// Returns an object containing live statistics about the issuer's NFTs<br/> 
        /// floor holds values of the lowest sell offers for various currencies with an amount > 0 which are placed across ALL Market Places or without Destination(open market)<br/>
        /// open_market holds stats about all offers WITHOUT a Destination set<br/>
        /// market_places holds stats for NFTs which have one of the MarketPlaces set as Destination
        /// </summary>
        Task<BaseServerResponse<CollectionNFTsStatats>> GetCollectionStats(string issuer, long Taxon, CancellationToken Cancel = default);



        #endregion

    }
}
