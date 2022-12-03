using Newtonsoft.Json;

using System.Globalization;

namespace XRPL.TrustlineService.Domain;

public class TrustlinesServerResponse : ServerInfo, IResponse
{ 
    public Dictionary<string, IssuerRoot> issuers { get; set; }

    #region Implementation of IResponse

    public HttpResponseMessage Response { get; set; }

    #endregion
}
