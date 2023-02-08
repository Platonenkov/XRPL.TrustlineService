using XRPL.TrustlineService.Domain;
using XRPL.TrustlineService.Domain.NFTsOffers;

namespace XRPL.TrustlineService;

public interface IFundedStatus
{
    /// <summary> Check if a single offer is actually funded or not</summary>
    /// <returns>Returns the funded status for a specific offer.</returns>
    Task<BaseServerResponse<FundedOffer>> CheckFundedNFTsOffer(string OfferID, CancellationToken Cancel = default);
    /// <summary>
    /// Check a batch of offers for its funded status<br/>
    /// You can send a maximum of 20 OfferIDs in the array at a time!
    /// </summary>
    /// <returns>Returns an array of the funded status for a the given offers.</returns>
    Task<BaseServerResponse<FundedOffers>> CheckFundedNFTsOffer(FundedOffersRequest offers, CancellationToken Cancel = default);

}