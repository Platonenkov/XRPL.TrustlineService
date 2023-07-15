using Newtonsoft.Json;

namespace XRPL.TrustlineService.Domain.NFTsOffers;

public class NFTOffers
{
    public string NFTokenID { get; set; }
    public string NFTokenOwner { get; set; }
    public string URI { get; set; }
    public string URI_valid => URI.FromHexString();
    [JsonProperty("buy")]
    public List<NFTOffer> Buy { get; set; }
    [JsonProperty("sell")]
    public List<NFTOffer> Sell { get; set; }
}