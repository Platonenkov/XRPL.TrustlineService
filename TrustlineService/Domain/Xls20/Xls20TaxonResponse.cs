namespace XRPL.TrustlineService.Domain.Xls20;

public class Xls20TaxonResponse : BaseServerResponse
{
    /// <summary>
    /// NFT issuer
    /// </summary>
    public string Issuer { get; set; }
    /// <summary>
    /// NFT Taxon (collection)
    /// </summary>
    public List<uint> Taxon { get; set; }
}