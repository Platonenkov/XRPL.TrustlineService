using Newtonsoft.Json;
using XRPL.TrustlineService.Converters;

namespace XRPL.TrustlineService.Domain.Statistics;

public class NFTsMarket
{
    [JsonProperty("nfts_for_sale")]
    public uint NFTsForSale { get; set; }
    [JsonProperty("buy_offers")]
    public uint BuyOffers { get; set; }
    [JsonProperty("sell_offers")]
    public uint SellOffers { get; set; }
    [JsonProperty("floor")]
    public List<FloorCurrency> Floor { get; set; }
}