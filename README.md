# XRPL.TrustlineService
XRPL Trustline service
```Install-Package XRPL.TrustlineService -Version 1.3.0.0```

### This client uses the [XRPL service](https://xrpl.services) database 
### API documentation https://api.xrpldata.com/docs
```C#
XrplTrustlineClient trust = new XrplTrustlineClient();                             //create client
TrustlinesServerResponse res = await trust.GetKnownTrustlines();                       //download tustlines
IEnumerable<TrustlineInfo> trustlines = res.Map();                                 // Map to IEnumerable<TrustlineInfo>
var nfts = await trust.GetXls20NFT();   //download nfts database
var issuer_nfts = await trust.GetIssuerNFT("rwvQWhjpUncjEbhsD2V9tv4YpKXjfH5RDj");  //download issuer nfts
var account_nfts = await trust.GetAccountNFT("rwvQWhjpUncjEbhsD2V9tv4YpKXjfH5RDj");  //download account nfts
var nft_info = await trust.GetNftInfoById("000827106CD2CBB743BE141A0FE7EA1F3177161ED3CCDCB21EE07C59000025BD");  //download nft info
```

### available points:

#### Trustlines
* `var lines = avait client.GetKnownTrustlines();` - all trustlines

#### NFTs
* `var AllNFTIssuer = await trust.GetAllNFTIssuer();` - all NFTs Issuers
* `var issuer_nfts = await client.GetIssuerTaxon(issuer);` - all taxon for current issuer
* `var issuer_nft = await client.GetIssuerNFT(issuer, taxon);` - NFTs for current issuer, possible filter by taxon (collections)
* `var account_nft = await client.GetAccountNFT(issuer, taxon);` - NFTs for current account
* `var nft_info = await client.GetNftInfoById(NFTokenID);` - current nft info

#### NFTs Offers points
* `var issuer_nfts_offers = await trust.GetIssuerNFTsOffers("rBZ3FrRzTgy46H3C9Uede8CAuH7rTGA5iE");` get all offers for NFTs from specific Issuer
* `var issuer_nfts_offers_with_taxon = await trust.GetIssuerNFTsOffers("rBZ3FrRzTgy46H3C9Uede8CAuH7rTGA5iE", 0);` get all offers for NFTs from a specific Issuer & Taxon
* `var account_nfts_offers = await trust.GetAccountNFTsOffers("rLiooJRSKeiNfRJcDBUhu4rcjQjGLWqa4p");` Get all NFT offers owned by specific account
* `var account_nfts_offers2 = await trust.GetNFTsOffersForAccount("rLiooJRSKeiNfRJcDBUhu4rcjQjGLWqa4p");` Get all offers for NFTs owned by specific account
* `var destination_nfts_offers2 = await trust.GetDestinationNFTsOffers("rLiooJRSKeiNfRJcDBUhu4rcjQjGLWqa4p");` The Offer Destination Account which you want to get the Offers for.
* `var nft_offers = await trust.GetNFTokenIDOffers("000827106CD2CBB743BE141A0FE7EA1F3177161ED3CCDCB21EE07C59000025BD");` get all offers for a single NFT
* `var nft_offer_info = await trust.GetNFTOfferInfo("2AA3FD4A61C4F968E40B1B49DB61281BD8CDE3674E8B8FECF255ECF6C9F64719");` get a specific offer by its ID
* `var AllAccountNFTsOffers = await trust.GetAllAccountNFTsOffers("rLiooJRSKeiNfRJcDBUhu4rcjQjGLWqa4p");` get all relevant offers for a specific account

#### Statistics

* `var statistics1 = await trust.GetIssuerNFTsStats("rwvQWhjpUncjEbhsD2V9tv4YpKXjfH5RDj");` Get stats about all NFTs from specific issuer.
* `var statistics2 = await trust.GetCollectionStats("rwvQWhjpUncjEbhsD2V9tv4YpKXjfH5RDj",1);` Get stats about specific NFTs from specific issuer with the given Taxon.

#### Funded offers
### THIS API ENDPOINT IS ONLY ACCESSIBLE VIA API KEY!
`XrplFundedClient funded = new XrplFundedClient(true, api_key);`
* CheckFundedNFTsOffer
```C#
var funded_offers = await funded.CheckFundedNFTsOffer(new FundedOffersRequest(new List<string>()
{
    "FD9516647477692B95F39DD30E516A5C62E2C953C12A56B5501EC46EFC129296",
    "C4F4D3500D99F4D2DED94A4982200A7521B02559733088D91760C221FB76A1A8"
}));
```
* `var funded_offer = await funded.CheckFundedNFTsOffer("FD9516647477692B95F39DD30E516A5C62E2C953C12A56B5501EC46EFC129296");`

[Use test project for example](https://github.com/Platonenkov/XRPL.TrustlineService/tree/dev/Test/ConsoleClient.Test)

![image](https://user-images.githubusercontent.com/44946855/200853929-5b77000d-f6f3-4ea0-9d0f-4cd88ce717c6.png)
