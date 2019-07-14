using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace etherium_mining_price
{


public partial class Stex
    {
        [JsonProperty("buy")]
        public Buy Buy { get; set; }

        [JsonProperty("sell")]
        public Buy Sell { get; set; }

        [JsonProperty("market_name")]
        public string MarketName { get; set; }

        [JsonProperty("updated_time")]
        public long UpdatedTime { get; set; }

        [JsonProperty("server_time")]
        public long ServerTime { get; set; }
    }

    public partial struct Buy
    {
        public long? Integer;
        public string String;

        public static implicit operator Buy(long Integer) => new Buy { Integer = Integer };
        public static implicit operator Buy(string String) => new Buy { String = String };
    }

    public partial class Stex
    {
        public static Stex[] FromJson(string json) => JsonConvert.DeserializeObject<Stex[]>(json);
    }

    public static class Serialize
    {
        public static string ToJson(this Stex[] self) => JsonConvert.SerializeObject(self);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                BuyConverter.Singleton,
                new IsoDateTimeConverter {}
            },
        };
    }

    internal class BuyConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Buy) || t == typeof(Buy?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.Integer:
                    var integerValue = serializer.Deserialize<long>(reader);
                    return new Buy { Integer = integerValue };
                case JsonToken.String:
                case JsonToken.Date:
                    var stringValue = serializer.Deserialize<string>(reader);
                    return new Buy { String = stringValue };
            }
            throw new Exception("Cannot unmarshal type Buy");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (Buy)untypedValue;
            if (value.Integer != null)
            {
                serializer.Serialize(writer, value.Integer.Value);
                return;
            }
            if (value.String != null)
            {
                serializer.Serialize(writer, value.String);
                return;
            }
            throw new Exception("Cannot marshal type Buy");
        }

        public static readonly BuyConverter Singleton = new BuyConverter();
    }

}