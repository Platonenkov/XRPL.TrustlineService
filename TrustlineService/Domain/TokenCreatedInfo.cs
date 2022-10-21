namespace XRPL.TrustlineService.Domain;

public class TokenCreatedInfo
{
    public string date { get; set; }
    public string hash { get; set; }
    public bool? fallback { get; set; }
}