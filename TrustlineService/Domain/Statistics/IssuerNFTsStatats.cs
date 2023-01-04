using Newtonsoft.Json;

namespace XRPL.TrustlineService.Domain.Statistics
{
    public class NfTsCollectionStatats
    {
        [JsonProperty("issuer")]
        public string Issuer { get; set; }
        [JsonProperty("taxon")]
        public string Taxon { get; set; }
        [JsonProperty("nfts")]
        public uint NFTsCount { get; set; }
        [JsonProperty("unique_owners")]
        public uint UniqueOwners { get; set; }
        [JsonProperty("nfts_for_sale")]
        public uint NFTsForSale { get; set; }
        [JsonProperty("buy_offers")]
        public uint BuyOffers { get; set; }
        [JsonProperty("sell_offers")]
        public uint SellOffers { get; set; }
        [JsonProperty("floor")]
        public List<FloorCurrency> Floor { get; set; }
        [JsonProperty("open_market")]
        public NFTsMarket OpenMarket { get; set; }
        [JsonProperty("market_places")]
        public List<MarketPlaces> MarketPlaces { get; set; }

    }
}
