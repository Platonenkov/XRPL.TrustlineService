using Newtonsoft.Json;

namespace XRPL.TrustlineService.Domain.Statistics;

public class CollectionNFTsStatats
{
    [JsonProperty("issuer")]
    public string Issuer { get; set; }
    [JsonProperty("taxon")]
    public string Taxon { get; set; }
    [JsonProperty("collection_info")]
    public NfTsCollectionStatats CollectionInfo { get; set; }
}