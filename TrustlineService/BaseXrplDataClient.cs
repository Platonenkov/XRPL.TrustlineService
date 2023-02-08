using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;
using Newtonsoft.Json;
using XRPL.TrustlineService.Domain;

namespace XRPL.TrustlineService;

public abstract class BaseXrplDataClient
{
    #region Base

    protected DateTime LastRequestDateTime { get; private set; }

    /// <summary> Http клиент </summary>
    protected readonly HttpClient _Client;
    JsonSerializerSettings serializerSettings;

    /// <summary> Get </summary>
    /// <typeparam name="TEntity">Тип нужных данных</typeparam>
    /// <param name="url">адрес</param>
    /// <param name="Cancel">Признак отмены асинхронной операции</param>
    /// <returns></returns>
    protected async Task<TEntity> GetAsync<TEntity>(string url, CancellationToken Cancel = default) where TEntity : IResponse, new()
    {
        Remaining -= 1;

        LastRequestDateTime = DateTime.Now;
        var response = await _Client.GetAsync(url, Cancel);

        if (response.Headers.TryGetValues("x-ratelimit-reset", out var reset))
        {
            int.TryParse(reset.FirstOrDefault(), out var reset_sec);
            SetReset(reset_sec);
        }
        if (response.Headers.TryGetValues("x-ratelimit-remaining", out var remaining))
        {
            int.TryParse(remaining.FirstOrDefault(), out var remaining_count);
            Remaining = remaining_count;

        }
        if (response.Headers.TryGetValues("x-ratelimit-limit", out var limit))
        {
            int.TryParse(limit.FirstOrDefault(), out var limit_count);
            Limit = limit_count;
        }
        if (Limit == 0 && response.StatusCode.ToString() == "TooManyRequests")
        {
            return new TEntity() { Response = response };
        }

        if (response.StatusCode.ToString() == "TooManyRequests")
        {
            if (!await CheckLimit(Cancel))
                return new TEntity() { Response = response };

            return await GetAsync<TEntity>(url, Cancel);
        }
        if (response.StatusCode == HttpStatusCode.NotFound || !response.IsSuccessStatusCode) return new TEntity() { Response = response };

        var data = await response.Content.ReadAsStringAsync();
        var result = string.IsNullOrWhiteSpace(data) ? new TEntity() : JsonConvert.DeserializeObject<TEntity>(data, serializerSettings);
        result.Response = response;
        return result;
    }
    protected async Task<bool> CheckLimit(CancellationToken Cancel)
    {
        if (Limit is { } and 0)
            return false;
        if (Remaining <= 0)
        {
            if (WaitWhenLimit && Reset is { } sec and > 0)
            {
                OnWaitAction?.Invoke($"Limit: {Limit} per Minute;{Environment.NewLine}"
                                     + $"Available number of requests: {Remaining};{Environment.NewLine}"
                                     + $"Reset through {Reset} sec.");
                Debug.WriteLine("Wait");
                await Task.Delay(TimeSpan.FromSeconds(sec + 3), Cancel);
                await CheckLimit(Cancel);
            }
            else
                return false;
        }

        return true;
    }

    /// <summary> Post </summary>
    /// <typeparam name="TItem">Тип нужных данных</typeparam>
    /// <typeparam name="TEntity">тип данных ответа</typeparam>
    /// <param name="url">адрес</param>
    /// <param name="item">данные</param>
    /// <param name="Cancel">Признак отмены асинхронной операции</param>
    /// <returns></returns>
    protected async Task<TEntity> PostAsync<TItem, TEntity>(string url, TItem item, CancellationToken Cancel = default) where TEntity : IResponse, new()
    {
        Remaining -= 1;

        LastRequestDateTime = DateTime.Now;
        var response = await _Client.PostAsJsonAsync(url, item, Cancel);
        if (response.Headers.TryGetValues("x-ratelimit-reset", out var reset))
        {
            int.TryParse(reset.FirstOrDefault(), out var reset_sec);
            SetReset(reset_sec);
        }
        if (response.Headers.TryGetValues("x-ratelimit-remaining", out var remaining))
        {
            int.TryParse(remaining.FirstOrDefault(), out var remaining_count);
            Remaining = remaining_count;

        }
        if (response.Headers.TryGetValues("x-ratelimit-limit", out var limit))
        {
            int.TryParse(limit.FirstOrDefault(), out var limit_count);
            Limit = limit_count;
        }

        if (Limit == 0 && response.StatusCode.ToString() == "TooManyRequests")
        {
            return new TEntity() { Response = response };
        }
        if (response.StatusCode.ToString() == "TooManyRequests")
        {
            if (!await CheckLimit(Cancel))
                return new TEntity() { Response = response };

            return await PostAsync<TItem, TEntity>(url, item, Cancel);
        }
        if (response.StatusCode == HttpStatusCode.NotFound || !response.IsSuccessStatusCode) return new TEntity() { Response = response };

        var data = await response.Content.ReadAsStringAsync();
        var result = string.IsNullOrWhiteSpace(data) ? new TEntity() : JsonConvert.DeserializeObject<TEntity>(data, serializerSettings);
        result.Response = response;
        return result;
    }


    #endregion

    #region Limit

    public Action<string> OnWaitAction;
    /// <summary>
    /// Maximum number of requests per minute.
    /// </summary>
    public int? Limit { get; private set; }

    /// <summary>
    /// Number of remaining requests per minute.
    /// </summary>
    public int? Remaining { get; private set; }
    /// <summary>
    /// Number of seconds until the current rate limit window resets.
    /// </summary>
    public int? Reset { get; private set; }

    protected void SetReset(int val)
    {
        Reset = val;
        if (val >= 0)
        {
            timer?.Dispose();
            timer = null;
            timer = new Timer(
                State =>
                {
                    Reset -= 1;
                    Debug.WriteLine($"{Reset}");
                    if (Reset <= 0)
                    {
                        timer = null;
                        Reset = null;
                        Remaining = null;
                        Limit = null;

                    }
                }, val, 0, 1000);
        }
    }


    private static Timer timer;
    /// <summary>
    /// If the maximum number of requests per minute has been exceeded, either waits for the limit to be reset or returns null
    /// </summary>
    public bool WaitWhenLimit { get; set; }

    #endregion

    public readonly string ApiServerAddress;
    public int ApiVersion { get; set; } = 1;
    /// <summary>
    /// api key
    /// </summary>
    public string ApiKey { get; set; }

    /// <summary>
    /// https://api.xrpldata.com/
    /// Api Client
    /// </summary>
    /// <param name="waitWhenLimit">If the maximum number of requests per minute has been exceeded, either waits for the limit to be reset or returns null</param>
    /// <param name="apiKey">api key</param>
    /// <param name="BaseServiceAddress">server address</param>
    protected BaseXrplDataClient(bool waitWhenLimit, string apiKey, string BaseServiceAddress = "https://api.xrpldata.com/")
    {
        ApiServerAddress = BaseServiceAddress;
        WaitWhenLimit = waitWhenLimit;
        _Client = new HttpClient
        {
            BaseAddress = new Uri(ApiServerAddress)
        };

        _Client.DefaultRequestHeaders.Accept.Clear();
        if (!string.IsNullOrWhiteSpace(apiKey))
        {
            _Client.DefaultRequestHeaders.Add("x-api-key", apiKey);
            ApiKey = apiKey;
        }

        serializerSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            DateTimeZoneHandling = DateTimeZoneHandling.Utc
        };

    }

}