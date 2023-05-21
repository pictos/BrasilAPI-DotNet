using System.Runtime.Serialization;

namespace BrasilAPI;

public record IBGEMunicipiosResponse : BaseResponse
{
	public IEnumerable<Municipio> Municipios { get; set; }
}
[DataContract]
public record Municipio
{
	[JsonPropertyName( "nome")]
	public string Nome { get; set; }
	[JsonPropertyName( "codigo_ibge")]
	public string CodigoIBGE { get; set; }
}