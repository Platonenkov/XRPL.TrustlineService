using System.Diagnostics;
using Newtonsoft.Json;

using System.Net.Http.Json;
using System.Net;
using System.Runtime.ConstrainedExecution;

using XRPL.TrustlineService.Domain;
using XRPL.TrustlineService.Domain.NFTsOffers;
using XRPL.TrustlineService.Domain.Statistics;
using XRPL.TrustlineService.Domain.Xls20;

namespace XRPL.TrustlineService
{

    /// <summary> client to connect to <a>https://api.xrpldata.com/</a></summary>
    public class XrplTrustlineClient: ITrustlineService
    {
        #region Base

        public DateTime LastRequestDateTime { get; private set; }
        public readonly string ServiceAddress;

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


        #endregion
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

        void SetReset(int val)
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
        public static string ApiServerAddress => "https://api.xrpldata.com/";
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
        public XrplTrustlineClient(bool waitWhenLimit, string apiKey)
        {
            WaitWhenLimit = waitWhenLimit;
            ServiceAddress = ApiServerAddress;
            _Client = new HttpClient
            {
                BaseAddress = new Uri(ApiServerAddress)
            };

            _Client.DefaultRequestHeaders.Accept.Clear();
            if (!string.IsNullOrWhiteSpace(apiKey))
            {
                _Client.DefaultRequestHeaders.Add("x-api-key", ApiKey);
                ApiKey = apiKey;
            }

            serializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc
            };

        }

        async Task<bool> CheckLimit(CancellationToken Cancel)
        {
            if (Remaining <= 0)
            {
                if (WaitWhenLimit && Reset is { } sec and > 0)
                {
                    OnWaitAction?.Invoke($"Limit: {Limit} per Minute;{Environment.NewLine}"
                                         + $"Available number of requests: {Remaining};{Environment.NewLine}"
                                         + $"Reset through {Reset} sec.");
                    Debug.WriteLine("Wait");
                    await Task.Delay(TimeSpan.FromSeconds(sec + 1), Cancel);
                    await CheckLimit(Cancel);
                }
                else
                    return false;
            }

            return true;
        }
        /// <summary> get all trustlines info </summary>
        /// <returns><seealso cref="TrustlinesServerResponse"/></returns>
        public async Task<TrustlinesServerResponse> GetKnownTrustlines(CancellationToken Cancel = default)
        {
            if (!await CheckLimit(Cancel))
                return null;
            var response = await GetAsync<TrustlinesServerResponse>($"api/v{ApiVersion}/tokens", Cancel);
            return response;
        }

        #region NFT methods

        /// <summary> Returns an array of XRPL accounts which have currently issued NFTs on the XRP Ledger. </summary>
        public async Task<BaseServerResponse<Xls20NftIssuers>> GetAllNFTIssuer(CancellationToken Cancel = default)
        {
            if (!await CheckLimit(Cancel))
                return null;

            var response = await GetAsync<BaseServerResponse<Xls20NftIssuers>>($"api/v{ApiVersion}/xls20-nfts/all/issuers", Cancel);
            return response;
        }
        /// <summary> get all Issuer NFT info </summary>
        public async Task<BaseServerResponse<IssuerXls20Nft>> GetIssuerNFT(string Issuer, uint? Taxon = null, CancellationToken Cancel = default)
        {
            if (!await CheckLimit(Cancel))
                return null;

            var address = Issuer;
            if (Taxon is not null)
            {
                address += $"/taxon/{Taxon}";
            }
            var response = await GetAsync<BaseServerResponse<IssuerXls20Nft>>($"api/v{ApiVersion}/xls20-nfts/issuer/{address}", Cancel);
            return response;
        }
        /// <summary> get all Issuer taxon (collections) </summary>
        public async Task<BaseServerResponse<Xls20TaxonResponse>> GetIssuerTaxon(string Issuer, CancellationToken Cancel = default)
        {
            if (!await CheckLimit(Cancel))
                return null;

            var response = await GetAsync<BaseServerResponse<Xls20TaxonResponse>>($"api/v{ApiVersion}/xls20-nfts/taxon/{Issuer}", Cancel);

            return response;
        }
        /// <summary> get NFT info </summary>
        public async Task<BaseServerResponse<Xls20NftResponse>> GetNftInfoById(string NftId, CancellationToken Cancel = default)
        {
            if (!await CheckLimit(Cancel))
                return null;

            var response = await GetAsync<BaseServerResponse<Xls20NftResponse>>($"api/v{ApiVersion}/xls20-nfts/nft/{NftId}", Cancel);
            return response;
        }
        /// <summary> get AccountNFTs </summary>
        public async Task<BaseServerResponse<AccountXls20Nft>> GetAccountNFTs(string account, CancellationToken Cancel = default)
        {
            if (!await CheckLimit(Cancel))
                return null;

            var response = await GetAsync<BaseServerResponse<AccountXls20Nft>>($"api/v{ApiVersion}/xls20-nfts/owner/{account}", Cancel);
            return response;
        }

        #endregion

        #region Offer methods

        /// <summary> get offers for nft by issuer  and taxon</summary>
        public async Task<BaseServerResponse<IssuerOffers>> GetIssuerNFTsOffers(string Issuer, uint? Taxon = null, CancellationToken Cancel = default)
        {
            if (!await CheckLimit(Cancel))
                return null;

            var address = Issuer;
            if (Taxon is not null)
            {
                address += $"/taxon/{Taxon}";
            }
            var response = await GetAsync<BaseServerResponse<IssuerOffers>>($"api/v{ApiVersion}/xls20-nfts/offers/issuer/{address}", Cancel);
            return response;
        }
        /// <summary> Get all offers for NFTs owned by specific account</summary>
        public async Task<BaseServerResponse<AccountNFTsOffers>> GetNFTsOffersForAccount(string Account, CancellationToken Cancel = default)
        {
            if (!await CheckLimit(Cancel))
                return null;

            var response = await GetAsync<BaseServerResponse<AccountNFTsOffers>>($"api/v{ApiVersion}/xls20-nfts/offers/nftowner/{Account}", Cancel);
            return response;
        }
        /// <summary> Get all NFT offers owned by specific account</summary>
        public async Task<BaseServerResponse<AccountOffersForNFTs>> GetAccountNFTsOffers(string Account, CancellationToken Cancel = default)
        {
            if (!await CheckLimit(Cancel))
                return null;

            var response = await GetAsync<BaseServerResponse<AccountOffersForNFTs>>($"api/v{ApiVersion}/xls20-nfts/offers/offerowner/{Account}", Cancel);
            return response;
        }
        /// <summary> The Offer Destination Account which you want to get the Offers for.</summary>
        public async Task<BaseServerResponse<DestinationNFTsOffers>> GetDestinationNFTsOffers(string Account, CancellationToken Cancel = default)
        {
            if (!await CheckLimit(Cancel))
                return null;

            var response = await GetAsync<BaseServerResponse<DestinationNFTsOffers>>($"api/v{ApiVersion}/xls20-nfts/offers/offerdestination/{Account}", Cancel);
            return response;
        }
        /// <summary> get all relevant offers for a specific account</summary>
        public async Task<BaseServerResponse<AllAccountNFTsOffers>> GetAllAccountNFTsOffers(string account, CancellationToken Cancel = default)
        {
            if (!await CheckLimit(Cancel))
                return null;

            var response = await GetAsync<BaseServerResponse<AllAccountNFTsOffers>>($"api/v{ApiVersion}/xls20-nfts/offers/all/account/{account}", Cancel);
            return response;
        }
        /// <summary> get a specific offer by its ID</summary>
        public async Task<BaseServerResponse<NFTOfferInfo>> GetNFTOfferInfo(string NFTOfferID, CancellationToken Cancel = default)
        {
            if (!await CheckLimit(Cancel))
                return null;

            var response = await GetAsync<BaseServerResponse<NFTOfferInfo>>($"api/v{ApiVersion}/xls20-nfts/offer/id/{NFTOfferID}", Cancel);
            return response;
        }
        /// <summary> Get all offers for a single NFT</summary>
        public async Task<BaseServerResponse<NFTokenOffers>> GetNFTokenIDOffers(string NFTokenID, CancellationToken Cancel = default)
        {
            if (!await CheckLimit(Cancel))
                return null;

            var response = await GetAsync<BaseServerResponse<NFTokenOffers>>($"api/v{ApiVersion}/xls20-nfts/offers/nft/{NFTokenID}", Cancel);
            return response;
        }

        #endregion

        #region Statistic methods
        /// <summary>
        /// Get stats about all NFTs from specific issuer.<br/>
        /// THIS API ENDPOINT EXCLUDES:<br/>
        ///- EXPIRED BUY OR SELL OFFERS<br/>
        ///- BUY OR SELL OFFERS WITH AMOUNT = 0<br/>
        ///- SELL OFFERS WHERE THE OFFER OWNER IS *NOT* THE NFT OWNER<br/>
        /// Returns an object containing live statistics about the issuer's NFTs<br/> 
        /// floor holds values of the lowest sell offers for various currencies with an amount > 0 which are placed across ALL Market Places or without Destination(open market)<br/>
        /// open_market holds stats about all offers WITHOUT a Destination set<br/>
        /// market_places holds stats for NFTs which have one of the MarketPlaces set as Destination
        /// </summary>
        public async Task<BaseServerResponse<CollectionNFTsStatats>> GetIssuerNFTsStats(string issuer, CancellationToken Cancel = default)
        {
            if (!await CheckLimit(Cancel))
                return null;

            var response = await GetAsync<BaseServerResponse<CollectionNFTsStatats>>($"api/v{ApiVersion}/xls20-nfts/stats/issuer/{issuer}", Cancel);
            return response;
        }

        /// <summary>
        /// Get stats about specific NFTs from specific issuer with the given Taxon.<br/>
        /// THIS API ENDPOINT EXCLUDES:<br/>
        ///- EXPIRED BUY OR SELL OFFERS<br/>
        ///- BUY OR SELL OFFERS WITH AMOUNT = 0<br/>
        ///- SELL OFFERS WHERE THE OFFER OWNER IS *NOT* THE NFT OWNER<br/>
        /// Returns an object containing live statistics about the issuer's NFTs<br/> 
        /// floor holds values of the lowest sell offers for various currencies with an amount > 0 which are placed across ALL Market Places or without Destination(open market)<br/>
        /// open_market holds stats about all offers WITHOUT a Destination set<br/>
        /// market_places holds stats for NFTs which have one of the MarketPlaces set as Destination
        /// </summary>
        public async Task<BaseServerResponse<CollectionNFTsStatats>> GetCollectionStats(string issuer, long Taxon, CancellationToken Cancel = default)
        {
            if (!await CheckLimit(Cancel))
                return null;

            var response = await GetAsync<BaseServerResponse<CollectionNFTsStatats>>($"api/v{ApiVersion}/xls20-nfts/stats/issuer/{issuer}/taxon/{Taxon}", Cancel);
            return response;
        }



        #endregion
    }


}
