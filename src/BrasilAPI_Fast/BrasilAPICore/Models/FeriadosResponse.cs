using System.Runtime.Serialization;

namespace BrasilAPI;

public record FeriadosResponse : BaseResponse
{
    public IEnumerable<Feriado> Feriados { get; set; }
}


public record Feriado
{
    [JsonPropertyName("date")]
    public DateTime? Date { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }
}