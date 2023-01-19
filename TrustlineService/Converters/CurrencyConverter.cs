using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XRPL.TrustlineService.Domain;

namespace XRPL.TrustlineService.Converters
{
    /// <summary> currency json converter </summary>
    internal class CurrencyConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (value is ServiceCurrency currency)
            {
                if (currency.CurrencyCode == "XRP")
                {
                    writer.WriteValue(currency.Value);
                }
                else
                {
                    JToken t = JToken.FromObject(currency);
                    t.WriteTo(writer);
                }
            }
            else
            {
                throw new NotSupportedException("Cannot write this object type");
            }
        }
        public override object ReadJson(JsonReader reader, Type objectType, object? existingValue,
            JsonSerializer serializer)
        {
            return reader.TokenType switch
            {
                JsonToken.Null => null,
                JsonToken.String => new ServiceCurrency
                {
                    CurrencyCode = "XRP",
                    Value = reader.Value?.ToString()
                },

                JsonToken.StartObject => serializer.Deserialize<ServiceCurrency>(reader),
                _ => throw new NotSupportedException("Cannot convert value " + objectType)
            };
        }
        /// <summary> Can convert object to currency </summary>
        /// <param name="objectType">object type</param>
        /// <returns>bool result</returns>
        public override bool CanConvert(Type objectType) => objectType == typeof(ServiceCurrency);
    }
}
