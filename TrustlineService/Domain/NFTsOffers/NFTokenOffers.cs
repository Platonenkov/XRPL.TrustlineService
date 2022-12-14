using Newtonsoft.Json;

namespace XRPL.TrustlineService.Domain.NFTsOffers;

public class NFTokenOffers
{
    [JsonProperty("nftokenid")]
    public string NFTokenid { get; set; }
    [JsonProperty("offers")]
    public NFTOffers Offers { get; set; }
}