namespace XRPL.TrustlineService.Domain;

public class IssuerRoot
{
    public TokenIssuerInfo? data { get; set; }
    public List<Token>? tokens { get; set; }
}