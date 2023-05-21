using System.Runtime.Serialization;

namespace BrasilAPI;

public record IBGEMunicipiosResponse : BaseResponse
{
	public IEnumerable<Municipio> Municipios { get; set; }
}
[DataContract]
public record Municipio
{
	[DataMember(Name = "nome")]
	public string Nome { get; set; }
	[DataMember(Name = "codigo_ibge")]
	public string CodigoIBGE { get; set; }
}