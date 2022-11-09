using Newtonsoft.Json;

namespace XRPL.TrustlineService.Domain;

public class SelfAssesmentInfo
{
    public string issuer { get; set; }
    [JsonProperty("currency_code")]
    public string CurrencyCode { get; set; }
    [JsonProperty("self_assessment")]
    public bool? SelfAssessment { get; set; }
    public bool? verification { get; set; }
    public string? information { get; set; }
}