using Newtonsoft.Json;

namespace XRPL.TrustlineService.Domain;

public class Token
{
    public string currency { get; set; }
    public double amount { get; set; }
    public uint? trustlines { get; set; }
    public uint? holders { get; set; }
    public uint? offers { get; set; }
    public TokenCreatedInfo? created { get; set; }
    [JsonProperty("self_assessment")]
    public SelfAssesmentInfo? SelfAssessment { get; set; }
}