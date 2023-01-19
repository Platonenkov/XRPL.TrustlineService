using Newtonsoft.Json;

namespace XRPL.TrustlineService.Domain.NFTsOffers;

public class AllAccountNFTsOffers
{
    [JsonProperty("xrplaccount")]
    public string Account { get; set; }
    [JsonProperty("offers_owned")]
    public List<NFTOffer> AccountOffers { get; set; }
    [JsonProperty("offers_for_own_nfts")]
    public List<NFTOffers> NFTOffers { get; set; }
    [JsonProperty("offers_as_destination")]
    public List<NFTOffer> DestinationOffers { get; set; }
}