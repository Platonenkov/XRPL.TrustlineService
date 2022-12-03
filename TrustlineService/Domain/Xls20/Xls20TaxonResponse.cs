using Newtonsoft.Json;

namespace XRPL.TrustlineService.Domain.Xls20;

public class Xls20TaxonResponse/* : BaseServerResponse*/
{
    /// <summary>
    /// NFT issuer
    /// </summary>
    [JsonProperty("issuer")]
    public string Issuer { get; set; }
    /// <summary>
    /// NFT Taxon (collection)
    /// </summary>
    [JsonProperty("taxons")]
    public List<uint> Taxons { get; set; }
}