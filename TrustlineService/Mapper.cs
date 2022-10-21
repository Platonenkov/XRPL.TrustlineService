using XRPL.TrustlineService.Domain;

namespace XRPL.TrustlineService
{
    public static class Mapper
    {
        public static IEnumerable<TrustlineInfo> Map(this TrustlinesServerInfo response)
            => response.issuers.Values.SelectMany(
            i => i.tokens?.Select(
                t => new TrustlineInfo()
                {
                    Issuer = i.data?.account,
                    CurrencyCode = t.currency.CurrencyValidCode(),
                    Amount = t.amount,
                    Created = t.created?.date is null ? null : DateTime.TryParse(t.created.date,  out var date) ? date : null,
                    Domain = i.data?.domain,
                    Holders = t.holders ?? 0,
                    Kyc = i.data?.kyc,
                    Offers = t.offers ?? 0,
                    ResolvedBy = i.data?.resolvedBy,
                    TrustlinesCount = t.trustlines ?? 0,
                    Twitter = i.data?.twitter,
                    Username = i.data?.username,
                    Verified = i.data?.verified
                })!).ToArray();
        /// <summary> get readable token code </summary>
        /// <param name="CurrencyCode">token code</param>
        /// <returns>readable token code</returns>
        static string CurrencyValidCode(this string CurrencyCode) => CurrencyCode is { Length: > 0 } row ? row.Length > 3 ? row.FromHexString().Trim('\0') : row : string.Empty;

    }
}

