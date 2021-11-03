using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using POC.Bff.Web.Domain.Responses;
using System;

namespace POC.Bff.Web.Infrastructure
{
    public class AttributeConverter : JsonConverter
    {
        public override bool CanRead => false;
        public override bool CanWrite => true;

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(AttributeResponse);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.ContractResolver = new CamelCasePropertyNamesContractResolver();
            serializer.Serialize(writer, value);
        }
    }
}