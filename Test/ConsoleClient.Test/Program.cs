using XRPL.TrustlineService;
using XRPL.TrustlineService.Domain.NFTsOffers;

var api_key = "";
XrplFundedClient funded = new XrplFundedClient(true, api_key); //create client
funded.OnWaitAction += Console.WriteLine;
var funded_offers = await funded.CheckFundedNFTsOffer(new FundedOffersRequest(new List<string>()
{
    "FD9516647477692B95F39DD30E516A5C62E2C953C12A56B5501EC46EFC129296",
    "C4F4D3500D99F4D2DED94A4982200A7521B02559733088D91760C221FB76A1A8"
}));
var funded_offer = await funded.CheckFundedNFTsOffer("FD9516647477692B95F39DD30E516A5C62E2C953C12A56B5501EC46EFC129296");


XrplTrustlineClient trust = new XrplTrustlineClient(true, api_key); //create client
trust.OnWaitAction += Console.WriteLine;

var KnownTrustlines = await trust.GetKnownTrustlines();  //download trustlines
var lines = KnownTrustlines.issuers.SelectMany(c => c.Value.tokens!).ToArray();
while (true)
{
    var AllNFTIssuer = await trust.GetAllNFTIssuer();  //all nfts issuers
    var IssuerNFT = await trust.GetIssuerNFT("rpqqyZXhowA6kb3eQKqRGA3GxbjzZFfMAD");  //download issuer nfts
    var AccountNFT = await trust.GetAccountNFTs("rLiooJRSKeiNfRJcDBUhu4rcjQjGLWqa4p");  //download account nfts
    var IssuerTaxon = await trust.GetIssuerTaxon("rpqqyZXhowA6kb3eQKqRGA3GxbjzZFfMAD");  //download issuer taxon
    var NftInfoById = await trust.GetNftInfoById("000827106CD2CBB743BE141A0FE7EA1F3177161ED3CCDCB21EE07C59000025BD");  //NFT info by ID

    var issuer_nfts_offers = await trust.GetIssuerNFTsOffers("rBZ3FrRzTgy46H3C9Uede8CAuH7rTGA5iE");
    var issuer_nfts_offers_with_taxon = await trust.GetIssuerNFTsOffers("rBZ3FrRzTgy46H3C9Uede8CAuH7rTGA5iE", 0);

    var account_nfts_offers1 = await trust.GetAccountNFTsOffers("rLiooJRSKeiNfRJcDBUhu4rcjQjGLWqa4p");
    var account_nfts_offers2 = await trust.GetNFTsOffersForAccount("rLiooJRSKeiNfRJcDBUhu4rcjQjGLWqa4p");
    var destination_nfts_offers2 = await trust.GetDestinationNFTsOffers("rLiooJRSKeiNfRJcDBUhu4rcjQjGLWqa4p");

    var nft_offers = await trust.GetNFTokenIDOffers("000827106CD2CBB743BE141A0FE7EA1F3177161ED3CCDCB21EE07C59000025BD");
    var nft_offer_info = await trust.GetNFTOfferInfo("2AA3FD4A61C4F968E40B1B49DB61281BD8CDE3674E8B8FECF255ECF6C9F64719");
    var AllAccountNFTsOffers = await trust.GetAllAccountNFTsOffers("rLiooJRSKeiNfRJcDBUhu4rcjQjGLWqa4p");


    var statistics1 = await trust.GetIssuerNFTsStats("rwvQWhjpUncjEbhsD2V9tv4YpKXjfH5RDj");
    var statistics2 = await trust.GetCollectionStats("rwvQWhjpUncjEbhsD2V9tv4YpKXjfH5RDj", 1);

    var data = await statistics2.Response.Content.ReadAsStringAsync();
    var headers = statistics2.Response.Headers.Concat(statistics2.Response.Content.Headers);
    Console.WriteLine("Wait");
    await Task.Delay(5000);
}


Console.ReadLine();
