using System.Runtime.Serialization;

namespace BrasilAPI;

public record IBGEsResponse : BaseResponse
{
	public IEnumerable<Ibge> IBGEs { get; set; }
}

public record IBGEResponse : BaseResponse
{
	public Ibge IBGE { get; set; }
}

[DataContract]
public record Ibge
{
	[JsonPropertyName( "id")]
	public int ID { get; set; }

	public Uf UF { get => (Uf)ID; }

	[JsonPropertyName( "sigla")]
	public string Sigla { get; set; }

	[JsonPropertyName( "nome")]
	public string Nome { get; set; }

	[JsonPropertyName( "regiao")]
	public Regiao Regiao { get; set; }
}

[DataContract]
public record Regiao
{
	[JsonPropertyName( "id")]
	public int ID { get; set; }

	[JsonPropertyName( "sigla")]
	public string Sigla { get; set; }

	[JsonPropertyName( "nome")]
	public string Nome { get; set; }
}