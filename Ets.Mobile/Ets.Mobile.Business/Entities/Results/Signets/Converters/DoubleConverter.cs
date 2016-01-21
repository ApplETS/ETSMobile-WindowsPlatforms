using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Ets.Mobile.Business.Entities.Results.Signets.Converters
{
    public class DoubleConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(double) || objectType == typeof(double?));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var token = JToken.Load(reader);
            if (token.Type == JTokenType.Float || token.Type == JTokenType.Integer)
            {
                return token.ToObject<double>();
            }
            if (token.Type == JTokenType.String)
            {
                return string.IsNullOrEmpty(token.ToString()) ? default(double) : 
                    double.Parse(token.ToString().Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture);
            }
            if (token.Type == JTokenType.Null && objectType == typeof(double?))
            {
                return null;
            }
            throw new JsonSerializationException("Unexpected token type: " + token.Type);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}