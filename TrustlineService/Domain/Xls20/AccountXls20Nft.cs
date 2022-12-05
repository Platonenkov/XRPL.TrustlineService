using Newtonsoft.Json;

namespace XRPL.TrustlineService.Domain.Xls20;

public class AccountXls20Nft
{
    [JsonProperty("owner")]
    public string Owner { get; set; }
    [JsonProperty("nfts")]
    public List<Xls20Nft> NFTs { get; set; }
}