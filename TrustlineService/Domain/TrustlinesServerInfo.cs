using Newtonsoft.Json;

using System.Globalization;

namespace XRPL.TrustlineService.Domain;

public class TrustlinesServerInfo 
{
    [JsonProperty("ledger_hash")]
    public string LedgerHash { get; set; }

    [JsonProperty("ledger_index")]
    public uint LedgerIndex { get; set; }

    [JsonProperty("ledger_close")]
    public string LedgerClose { get; set; }
    [JsonProperty("ledger_close_ms")]
    public uint LedgerCloseMs { get; set; }
    public Dictionary<string, IssuerRoot> issuers { get; set; }

    public DateTime LedgerDate => DateTime.ParseExact(LedgerClose, "yyyy-MMM-dd HH:mm:ss.000000000 UTC", CultureInfo.InvariantCulture);

    #region Implementation of IEntity<out int>

    public int Id { get; set; }

    #endregion
}