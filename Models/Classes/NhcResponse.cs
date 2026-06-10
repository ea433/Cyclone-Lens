using System.Text.Json.Serialization;

namespace Models.Classes
{
    public class NhcResponse
    {
        [JsonPropertyName("activeStorms")]
        public List<NhcStorm> ActiveStorms { get; set; } = new();
    }
}
