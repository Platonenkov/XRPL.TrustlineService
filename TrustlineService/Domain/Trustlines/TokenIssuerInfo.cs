namespace XRPL.TrustlineService.Domain;

public class TokenIssuerInfo 
{
    public string? resolvedBy { get; set; }
    public string? account { get; set; }
    public string? username { get; set; }
    public string? domain { get; set; }
    public string? twitter { get; set; }
    public bool? verified { get; set; }
    public bool? kyc { get; set; }
}