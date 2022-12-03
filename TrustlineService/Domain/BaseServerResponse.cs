using Newtonsoft.Json;

namespace XRPL.TrustlineService.Domain
{
    public class BaseServerResponse<T>: IResponse
    {
        public HttpResponseMessage Response { get; set; }
        [JsonProperty("info")]
        public ServerInfo Info { get; set; }
        [JsonProperty("data")]
        public T Data { get; set; }
    }

    public interface IResponse
    {
        public HttpResponseMessage Response { get; set; }
    }
}
