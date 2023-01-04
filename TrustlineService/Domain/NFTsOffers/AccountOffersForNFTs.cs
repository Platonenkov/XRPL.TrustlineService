using Newtonsoft.Json;

namespace XRPL.TrustlineService.Domain.NFTsOffers;

public class AccountOffersForNFTs
{
    [JsonProperty("offerowner")]
    public string OfferOwner { get; set; }
    [JsonProperty("offers")]
    public List<NFTOffer> Offers { get; set; }
}