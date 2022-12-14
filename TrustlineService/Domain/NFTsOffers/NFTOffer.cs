using Newtonsoft.Json;

using XRPL.TrustlineService.Converters;

namespace XRPL.TrustlineService.Domain.NFTsOffers
{
    public class NFTOffer
    {
        [JsonConverter(typeof(CurrencyConverter))]
        public ServiceCurrency Amount { get; set; }
        public int Flags { get; set; }
        public string NFTokenID { get; set; }
        public string OfferID { get; set; }
        public string Owner { get; set; }
        public string Destination { get; set; }
        public ulong Expiration { get; set; }
    }
}
