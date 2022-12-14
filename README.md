# XRPL.TrustlineService
XRPL Trustline service
```Install-Package XRPL.TrustlineService -Version 1.1.0.0```

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
* `var account_nfts_offers = await trust.GetAccountNFTsOffers("rLiooJRSKeiNfRJcDBUhu4rcjQjGLWqa4p");` get all offers for NFTs owned by a specific account
* `var nft_offers = await trust.GetNFTokenIDOffers("000827106CD2CBB743BE141A0FE7EA1F3177161ED3CCDCB21EE07C59000025BD");` get all offers for a single NFT
* `var nft_offer_info = await trust.GetNFTOfferInfo("2AA3FD4A61C4F968E40B1B49DB61281BD8CDE3674E8B8FECF255ECF6C9F64719");` get a specific offer by its ID


[Use test project for example](https://github.com/Platonenkov/XRPL.TrustlineService/tree/dev/Test/ConsoleClient.Test)

![image](https://user-images.githubusercontent.com/44946855/200853929-5b77000d-f6f3-4ea0-9d0f-4cd88ce717c6.png)
