using System.Runtime.Serialization;

namespace BrasilAPI;

public record BancosResponse : BaseResponse
{
    public IEnumerable<Banco> Bancos { get; set; }
}

public record BancoResponse : BaseResponse
{
    public Banco Banco { get; set; }
}


public record Banco
{
    [JsonPropertyName("ispb")]
    public string ISPB { get; set; }

    [JsonPropertyName("name")]
    public string Nome { get; set; }

    [JsonPropertyName("code")]
    public int? Codigo { get; set; }

    [JsonPropertyName("fullName")]
    public string NomeCompleto { get; set; }
}