using Newtonsoft.Json;

namespace XRPL.TrustlineService.Domain.NFTsOffers;

public class DestinationNFTsOffers
{
    [JsonProperty("offerdestination")]
    public string Destination { get; set; }
    [JsonProperty("offers")]
    public List<NFTOffer> Offers { get; set; }
}