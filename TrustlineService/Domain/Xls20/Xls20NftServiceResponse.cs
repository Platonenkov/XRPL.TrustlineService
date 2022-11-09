namespace XRPL.TrustlineService.Domain.Xls20;

public class Xls20NftServiceResponse : BaseServerResponse
{
    public Dictionary<string, List<Xls20Nft>> nfts { get; set; }
}