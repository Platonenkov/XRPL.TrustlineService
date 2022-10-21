using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using XRPL.TrustlineService;


XrplTrustlineClient trust = new XrplTrustlineClient(); //create client
var res = await trust.GetKnownTrustlines();            //download tustlines
if (res is null)
{
    Console.WriteLine("Database not found");
    return;
}
var trustlines = res.Map();                            // Map to IEnumerable

var count = trustlines.ToArray().Length;
Console.WriteLine($"{count} Trustlines in database");
Console.WriteLine();
Console.WriteLine();
var RLT_Currency = trustlines.FirstOrDefault(f => f.CurrencyCode == "RLT");
Console.WriteLine(
    RLT_Currency != null 
        ? JObject.Parse(JsonConvert.SerializeObject(RLT_Currency)) 
        : JObject.Parse(JsonConvert.SerializeObject(trustlines?.FirstOrDefault()))
        );

Console.WriteLine("Hello, from XrplDaddy!");
