using Newtonsoft.Json;

namespace XRPL.TrustlineService.Domain.NFTsOffers;

public class AccountNFTsOffers
{
    [JsonProperty("owner")]
    public string Owner { get; set; }
    [JsonProperty("offers")]
    public List<NFTOffers> Offers { get; set; }
}