using Newtonsoft.Json;

namespace XRPL.TrustlineService.Domain.NFTsOffers;

public class AccountNFTsOffers
{
    [JsonProperty("nftowner")]
    public string NFTOwner { get; set; }
    [JsonProperty("offers")]
    public List<NFTOffers> Offers { get; set; }
}