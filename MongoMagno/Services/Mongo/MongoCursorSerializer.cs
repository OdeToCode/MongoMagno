using System;
using System.Linq;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace MongoMagno.Services.Mongo
{
    public class MongoCursorSerializer : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var cursor = (IMongoDbCursor) value;

            writer.WriteStartArray();

            foreach (BsonDocument record in cursor)
            {
                writer.WriteRawValue(record.ToJson());
            }

            writer.WriteEndArray();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            var types = new[] {typeof (MongoDbCursor)};

            return types.Any(t => t == objectType);
        }
    }
}