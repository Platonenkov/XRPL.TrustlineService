using Newtonsoft.Json;

using XRPL.TrustlineService.Domain;

namespace XRPL.TrustlineService
{
    /// <summary> client to connect to <a>https://api.xrpldata.com/</a></summary>
    public class XrplTrustlineClient : BaseClient
    {
        public XrplTrustlineClient():base("https://api.xrpldata.com/")
        {
        }
        /// <summary> get all trustlines info </summary>
        /// <returns><seealso cref="TrustlinesServerInfo"/></returns>
        public async Task<TrustlinesServerInfo> GetKnownTrustlines()
        {
            var response = await GetAsync<TrustlinesServerInfo>("api/v1/tokens");
            return response;
        }
    }


}
