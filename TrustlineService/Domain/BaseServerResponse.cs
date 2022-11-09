using Newtonsoft.Json;

using System.Globalization;

namespace XRPL.TrustlineService.Domain
{
    public class BaseServerResponse
    {
        [JsonProperty("ledger_hash")]
        public string LedgerHash { get; set; }

        [JsonProperty("ledger_index")]
        public long LedgerIndex { get; set; }

        [JsonProperty("ledger_close")]
        public string LedgerClose { get; set; }
        [JsonProperty("ledger_close_ms")]
        public long LedgerCloseMs { get; set; }

        public DateTime LedgerDate => DateTime.ParseExact(LedgerClose, "yyyy-MMM-dd HH:mm:ss.000000000 UTC", CultureInfo.InvariantCulture);

    }
}
