# XRPL.TrustlineService
XRPL Trustline service
```Install-Package XRPL.TrustlineService -Version 1.0.4.0```

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

available points:
* `var lines = avait client.GetKnownTrustlines()` - all trustlines
* `var AllNFTIssuer = await trust.GetAllNFTIssuer()` - all NFTs Issuers
* `var issuer_nfts = await client.GetIssuerTaxon(issuer)` - all taxon for current issuer
* `var issuer_nft = await client.GetIssuerNFT(issuer, taxon)` - NFTs for current issuer, possible filter by taxon (collections)
* `var account_nft = await client.GetAccountNFT(issuer, taxon)` - NFTs for current account
* `var nft_info = await client.GetNftInfoById(NFTokenID)` - current nft info

[Use test project for example](https://github.com/Platonenkov/XRPL.TrustlineService/tree/dev/Test/ConsoleClient.Test)

![image](https://user-images.githubusercontent.com/44946855/200853929-5b77000d-f6f3-4ea0-9d0f-4cd88ce717c6.png)
