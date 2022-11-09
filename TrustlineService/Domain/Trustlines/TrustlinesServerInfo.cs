using Newtonsoft.Json;

using System.Globalization;

namespace XRPL.TrustlineService.Domain;

public class TrustlinesServerResponse : BaseServerResponse
{ 
    public Dictionary<string, IssuerRoot> issuers { get; set; }
}
