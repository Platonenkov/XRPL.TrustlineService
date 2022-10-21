# XRPL.TrustlineService
XRPL Trustline service
```Install-Package XRPL.TrustlineService -Version 1.0.0.1```

### This client uses the [XRPL service](https://xrpl.services) database 
```C#
XrplTrustlineClient trust = new XrplTrustlineClient(); //create client
TrustlinesServerInfo res = await trust.GetKnownTrustlines();            //download tustlines
IEnumerable<TrustlineInfo> trustlines = res.Map();                            // Map to IEnumerable<TrustlineInfo>
```

[Use test project for example](https://github.com/Platonenkov/XRPL.TrustlineService/tree/dev/Test/ConsoleClient.Test)

![image](https://user-images.githubusercontent.com/44946855/197238647-9ac94615-843c-4c6f-9381-2dc670a2dcff.png)
