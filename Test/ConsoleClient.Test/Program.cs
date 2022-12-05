using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using XRPL.TrustlineService;


XrplTrustlineClient trust = new XrplTrustlineClient(); //create client
//var lines = await trust.GetKnownTrustlines();  //download tustlines
//if (lines is null)
//{
//    Console.WriteLine("Database not found");
//    return;
//}
//var trustlines = lines.Map();  // Map to IEnumerable

//var count = trustlines.ToArray().Length;
//Console.WriteLine($"{count} Trustlines in database");
//Console.WriteLine();
//var RLT_Currency = trustlines.FirstOrDefault(f => f.CurrencyCode == "RLT");
//Console.WriteLine(
//    RLT_Currency != null
//        ? JObject.Parse(JsonConvert.SerializeObject(RLT_Currency))
//        : JObject.Parse(JsonConvert.SerializeObject(trustlines?.FirstOrDefault()))
//        );
//Console.WriteLine();

//var nfts = await trust.GetXls20NFT();   //download nfts database
//var nfts_issuers = nfts.nfts.Count;
//Console.WriteLine($"{nfts_issuers} NFT issuers");
//Console.WriteLine();

var AllNFTIssuer = await trust.GetAllNFTIssuer();  //all nfts issuers
var IssuerNFT = await trust.GetIssuerNFT("rpqqyZXhowA6kb3eQKqRGA3GxbjzZFfMAD");  //download issuer nfts
var AccountNFT = await trust.GetAccountNFTs("rLiooJRSKeiNfRJcDBUhu4rcjQjGLWqa4p");  //download account nfts
//var Xls20NFT = await trust.GetXls20NFT();  //download issuer taxon
var IssuerTaxon = await trust.GetIssuerTaxon("rpqqyZXhowA6kb3eQKqRGA3GxbjzZFfMAD");  //download issuer taxon
var NftInfoById = await trust.GetNftInfoById("000827106CD2CBB743BE141A0FE7EA1F3177161ED3CCDCB21EE07C59000025BD");  //NFT info by ID
var KnownTrustlines = await trust.GetKnownTrustlines();  //download trustlines
var lines = KnownTrustlines.issuers.SelectMany(c => c.Value.tokens).ToArray();
Console.WriteLine();
//Console.WriteLine($"{IssuerTaxon.Data.Issuer} has {IssuerTaxon.Data.Taxon.Count} collections");
//var collections = (await trust.GetIssuerNFT("rpqqyZXhowA6kb3eQKqRGA3GxbjzZFfMAD")).Data.NFTs.GroupBy(c=>c.Taxon).ToArray();
//foreach (var t in IssuerTaxon.Data.Taxon)
//{
//    var issuer_nfts = await trust.GetIssuerNFT("rpqqyZXhowA6kb3eQKqRGA3GxbjzZFfMAD", t);  //download issuer nfts
//    Console.WriteLine($"{t} collection: count = {issuer_nfts.Data.NFTs.Count}");
//}

//var issuer_nfts1 = await trust.GetIssuerNFT("rwvQWhjpUncjEbhsD2V9tv4YpKXjfH5RDj");  //download issuer nfts
//Console.WriteLine($"{issuer_nfts1.Data.Issuer} has {issuer_nfts1.Data.NFTs.Count} NFT");
//Console.WriteLine();

//var nft_info = await trust.GetNftInfoById("000827106CD2CBB743BE141A0FE7EA1F3177161ED3CCDCB21EE07C59000025BD");  //download nft info
//Console.WriteLine(
//    nft_info != null
//        ? JObject.Parse(JsonConvert.SerializeObject(nft_info))
//        : null);

//Console.WriteLine("Hello, from XrplDaddy!");
