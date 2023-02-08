using XRPL.TrustlineService.Domain;
using XRPL.TrustlineService.Domain.NFTsOffers;

namespace XRPL.TrustlineService;

/// <summary> client to connect to <a>https://api.xrpldata.com/</a></summary>
public class XrplFundedClient : BaseXrplDataClient, IFundedStatus
{
    public XrplFundedClient(bool waitWhenLimit, string apiKey, string BaseServiceAddress = "https://api.xrpldata.com/") : base(waitWhenLimit, apiKey, BaseServiceAddress)
    {
    }

    #region Implementation of IFundedStatus

    /// <inheritdoc />
    public async Task<BaseServerResponse<FundedOffer>> CheckFundedNFTsOffer(string OfferID, CancellationToken Cancel = default)
    {
        if (!await CheckLimit(Cancel))
            return null;

        var response = await GetAsync<BaseServerResponse<FundedOffer>>($"api/v{ApiVersion}/xls20-nfts/funded/offer/{OfferID}", Cancel);
        return response;
    }

    /// <inheritdoc />
    public async Task<BaseServerResponse<FundedOffers>> CheckFundedNFTsOffer(FundedOffersRequest offers, CancellationToken Cancel = default)
    {
        if (!await CheckLimit(Cancel))
            return null;
        if (offers.Offers is null)
            throw new ArgumentNullException($"You can send a maximum of 20 OfferIDs in the array at a time! Your value is null");
        if (offers.Offers.Count is 0 or > 20)
            throw new ArgumentException($"You can send a maximum of 20 OfferIDs in the array at a time! Your value = {offers.Offers.Count}");

        var response = await PostAsync<FundedOffersRequest, BaseServerResponse<FundedOffers>>($"api/v{ApiVersion}/xls20-nfts/funded/offers", offers, Cancel);
        return response;
    }

    #endregion
}