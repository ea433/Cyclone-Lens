using System.Text.Json.Serialization;

namespace Models.Classes
{
    public class NhcStorm
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = "";

        [JsonPropertyName("name")]
        public string Name { get; set; } = "";

        [JsonPropertyName("classification")]
        public string Classification { get; set; } = "";

        [JsonPropertyName("intensity")]
        public string Intensity { get; set; } = "";

        [JsonPropertyName("pressure")]
        public string Pressure { get; set; } = "";

        [JsonPropertyName("latitudeNumeric")]
        public double LatitudeNumeric { get; set; }

        [JsonPropertyName("longitudeNumeric")]
        public double LongitudeNumeric { get; set; }

        [JsonPropertyName("lastUpdate")]
        public DateTime LastUpdate { get; set; }
    }
}