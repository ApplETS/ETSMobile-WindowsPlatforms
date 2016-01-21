using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Ets.Mobile.Business.Entities.Results.Signets.Converters
{
    public class GenericConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader,
                                        Type objectType,
                                         object existingValue,
                                         JsonSerializer serializer)
        {
            // Load JObject from stream
            var jObject = JObject.Load(reader);

            // Create target object based on JObject
            JToken token;
            if (jObject != null && jObject.TryGetValue("d", out token))
            {
                jObject = JObject.Parse(token.ToString());
            }

            var target = Activator.CreateInstance(objectType);

            // Populate the object properties
            if (jObject != null)
            {
                serializer.Populate(jObject.CreateReader(), target);
            }

            return target;
        }

        public override void WriteJson(JsonWriter writer,
                                       object value,
                                       JsonSerializer serializer)
        {
            throw new Exception("Do not store web results. Use <Entity>Vm");
        }
    }
}