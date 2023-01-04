using Newtonsoft.Json;

namespace XRPL.TrustlineService.Domain.Statistics;

public class MarketPlaces: NFTsMarket
{
    [JsonProperty("mp_account")]
    public string MarketPlaceAccount { get; set; }
}