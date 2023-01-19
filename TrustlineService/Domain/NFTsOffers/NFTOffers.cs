using Newtonsoft.Json;

namespace XRPL.TrustlineService.Domain.NFTsOffers;

public class NFTOffers
{
    public string NFTokenID { get; set; }
    public string NFTokenOwner { get; set; }
    [JsonProperty("buy")]
    public List<NFTOffer> Buy { get; set; }
    [JsonProperty("sell")]
    public List<NFTOffer> Sell { get; set; }
}