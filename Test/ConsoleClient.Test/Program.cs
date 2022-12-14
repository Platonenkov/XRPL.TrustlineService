using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using XRPL.TrustlineService;


XrplTrustlineClient trust = new XrplTrustlineClient(); //create client
var AllNFTIssuer = await trust.GetAllNFTIssuer();  //all nfts issuers
var IssuerNFT = await trust.GetIssuerNFT("rpqqyZXhowA6kb3eQKqRGA3GxbjzZFfMAD");  //download issuer nfts
var AccountNFT = await trust.GetAccountNFTs("rLiooJRSKeiNfRJcDBUhu4rcjQjGLWqa4p");  //download account nfts
var IssuerTaxon = await trust.GetIssuerTaxon("rpqqyZXhowA6kb3eQKqRGA3GxbjzZFfMAD");  //download issuer taxon
var NftInfoById = await trust.GetNftInfoById("000827106CD2CBB743BE141A0FE7EA1F3177161ED3CCDCB21EE07C59000025BD");  //NFT info by ID
var KnownTrustlines = await trust.GetKnownTrustlines();  //download trustlines
var lines = KnownTrustlines.issuers.SelectMany(c => c.Value.tokens).ToArray();

var issuer_nfts_offers = await trust.GetIssuerNFTsOffers("rBZ3FrRzTgy46H3C9Uede8CAuH7rTGA5iE");
var issuer_nfts_offers_with_taxon = await trust.GetIssuerNFTsOffers("rBZ3FrRzTgy46H3C9Uede8CAuH7rTGA5iE", 0);
var account_nfts_offers = await trust.GetAccountNFTsOffers("rLiooJRSKeiNfRJcDBUhu4rcjQjGLWqa4p");
var nft_offers = await trust.GetNFTokenIDOffers("000827106CD2CBB743BE141A0FE7EA1F3177161ED3CCDCB21EE07C59000025BD");
var nft_offer_info = await trust.GetNFTOfferInfo("2AA3FD4A61C4F968E40B1B49DB61281BD8CDE3674E8B8FECF255ECF6C9F64719");
Console.WriteLine();
