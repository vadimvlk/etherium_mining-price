using Newtonsoft.Json;

namespace etherium_mining_price
{
    public class Stex
    {
        [JsonProperty("buy")]
        public double Buy { get; set; }

        [JsonProperty("sell")]
        public Buy Sell { get; set; }

        [JsonProperty("market_name")]
        public string MarketName { get; set; }

        [JsonProperty("updated_time")]
        public long UpdatedTime { get; set; }

        [JsonProperty("server_time")]
        public long ServerTime { get; set; }
    }

    public struct Buy
    {
        public double Integer;
        public string String;
        public static implicit operator Buy(double Integer) => new Buy { Integer = Integer };
        public static implicit operator Buy(string String) => new Buy { String = String };
    }
}

