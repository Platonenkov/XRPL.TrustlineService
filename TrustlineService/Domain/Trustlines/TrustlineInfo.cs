namespace XRPL.TrustlineService.Domain
{

    public class TrustlineInfo
    {
        public string? ResolvedBy { get; set; }
        public string? Issuer { get; set; }
        public string? Username { get; set; }
        public string? Domain { get; set; }
        public string? Twitter { get; set; }
        public bool? Verified { get; set; }
        public bool? Kyc { get; set; }


        public string CurrencyCode { get; set; }
        public double Amount { get; set; }
        public uint TrustlinesCount { get; set; }
        public uint Holders { get; set; }
        public uint Offers { get; set; }
        public DateTime? Created { get; set; }

    }
}
