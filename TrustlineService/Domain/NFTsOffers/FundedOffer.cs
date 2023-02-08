using Newtonsoft.Json;

namespace XRPL.TrustlineService.Domain.NFTsOffers
{
    public class FundedOffer
    {
        [JsonProperty("offerid")]
        public string OfferID { get; set; }
        [JsonProperty("funded")]
        public bool Funded { get; set; }
        [JsonProperty("exists")]
        public bool Exist { get; set; }
    }
    public class FundedOffers
    {
        [JsonProperty("offers")]
        public IEnumerable<FundedOffer> Offers { get; set; }
    }
    public class FundedOffersRequest
    {
        [JsonProperty("offers")]
        public List<string> Offers { get; set; }

        public FundedOffersRequest(List<string> offers)
        {
            Offers = offers;
        }
    }

}
