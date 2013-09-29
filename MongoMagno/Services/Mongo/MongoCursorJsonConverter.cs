using System;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using Newtonsoft.Json;
using JsonReader = Newtonsoft.Json.JsonReader;
using JsonWriter = Newtonsoft.Json.JsonWriter;

namespace MongoMagno.Services.Mongo
{
    public class MongoCursorJsonConverter : JsonConverter
    {
        public override void WriteJson(
            JsonWriter writer, object value, JsonSerializer serializer)
        {
            var cursor = (IMongoDbCursor) value;
            var settings = new JsonWriterSettings
                {
                    OutputMode = JsonOutputMode.Strict
                };

            writer.WriteStartArray();

            foreach (BsonDocument document in cursor)
            {
                writer.WriteRawValue(document.ToJson(settings));                
                serializer.Serialize(writer, document.ToDictionary());                                        
            }

            writer.WriteEndArray();
        }

        public override object ReadJson(
            JsonReader reader, Type objectType, 
            object existingValue, JsonSerializer serializer)
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