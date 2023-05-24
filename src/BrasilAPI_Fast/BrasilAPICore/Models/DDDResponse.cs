using System.Runtime.Serialization;

namespace BrasilAPI;


public record DDDResponse : BaseResponse
{
    [JsonPropertyName("state")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Uf UF { get; set; }

    [JsonPropertyName("cities")]
    public IEnumerable<string> Cities { get; set; }
}