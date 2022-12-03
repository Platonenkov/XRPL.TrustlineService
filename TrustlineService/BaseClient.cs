using System.Net;
using System.Net.Http.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using XRPL.TrustlineService.Domain;

namespace XRPL.TrustlineService
{
    /// <summary>
    /// Базовый клиент с реализациями
    /// </summary>
    public abstract class BaseClient
    {
        public readonly string ServiceAddress;

        /// <summary> Http клиент </summary>
        protected readonly HttpClient _Client;
        JsonSerializerSettings serializerSettings;

        /// <summary>
        /// Базовый конструктор
        /// </summary>
        /// <param name="serviceAddress">адрес сервиса</param>
        protected BaseClient(string serviceAddress)
        {
            ServiceAddress = serviceAddress;
            _Client = new HttpClient
            {
                BaseAddress = new Uri(serviceAddress)
            };

            _Client.DefaultRequestHeaders.Accept.Clear();

            serializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc
            };
        }

        /// <summary> Get </summary>
        /// <typeparam name="TEntity">Тип нужных данных</typeparam>
        /// <param name="url">адрес</param>
        /// <param name="Cancel">Признак отмены асинхронной операции</param>
        /// <returns></returns>
        protected async Task<TEntity> GetAsync<TEntity>(string url, CancellationToken Cancel = default) where TEntity : IResponse, new()
        {
            var response = await _Client.GetAsync(url, Cancel);
            if (response.StatusCode == HttpStatusCode.NotFound || !response.IsSuccessStatusCode) return new TEntity(){Response = response};
            
            var data = await response.Content.ReadAsStringAsync(cancellationToken: Cancel);
            var result = string.IsNullOrWhiteSpace(data) ? new TEntity() : JsonConvert.DeserializeObject<TEntity>(data, serializerSettings);
            result.Response = response;
            return result;
        }

        /// <summary> Post </summary>
        /// <typeparam name="TItem">Тип нужных данных</typeparam>
        /// <param name="url">адрес</param>
        /// <param name="item">данные</param>
        /// <param name="Cancel">Признак отмены асинхронной операции</param>
        /// <returns></returns>
        protected async Task<HttpResponseMessage> PostAsync<TItem>(string url, TItem item, CancellationToken Cancel = default)
        {
            var response = await _Client.PostAsJsonAsync(url, item, Cancel);
            return response.EnsureSuccessStatusCode();
        }
    }
}
