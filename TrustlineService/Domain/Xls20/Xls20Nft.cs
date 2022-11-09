namespace XRPL.TrustlineService.Domain.Xls20;

public class Xls20Nft
{
    public string NFTokenID { get; set; }
    public string Issuer { get; set; }
    public string Owner { get; set; }
    public long Taxon { get; set; }
    public int TransferFee { get; set; }
    public int Flags { get; set; }
    public long Sequence { get; set; }
    public string URI { get; set; }
    public string URI_valid => URI.FromHexString();
    public int buy_offers { get; set; }
    public int sell_offers { get; set; }
}