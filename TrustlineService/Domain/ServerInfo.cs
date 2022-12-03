using System.Globalization;
using Newtonsoft.Json;

namespace XRPL.TrustlineService.Domain;

public class ServerInfo
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