using Newtonsoft.Json;

namespace XRPL.TrustlineService.Domain.NFTsOffers;

public class NFTOfferInfo
{
    [JsonProperty("offerid")]
    public string OfferID { get; set; }
    [JsonProperty("offer")]
    public NFTOffer Offer { get; set; }
}