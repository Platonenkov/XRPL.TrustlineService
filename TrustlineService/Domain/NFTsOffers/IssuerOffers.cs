using Newtonsoft.Json;

namespace XRPL.TrustlineService.Domain.NFTsOffers;

public class IssuerOffers
{
    [JsonProperty("issuer")]
    public string Issuer { get; set; }
    [JsonProperty("taxon")]
    public uint? Taxon { get; set; }
    [JsonProperty("offers")]
    public List<NFTOffers> Offers { get; set; }
}