using System.Runtime.Serialization;

namespace BrasilAPI;

public record TaxasResponse : BaseResponse
{
	public IEnumerable<Taxa> Taxas { get; set; }
}

public record TaxaResponse : BaseResponse
{
	public Taxa Taxa { get; set; }
}

[DataContract]
public record Taxa
{
	[DataMember(Name = "nome")]
	public string Nome { get; set; }
	[DataMember(Name = "valor")]
	public float Valor { get; set; }
}