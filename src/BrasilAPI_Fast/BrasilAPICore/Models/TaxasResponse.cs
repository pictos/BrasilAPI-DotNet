using System.Runtime.Serialization;

namespace SDKBrasilAPI.Responses;

public record TaxasResponse : BaseResponse
{
	public IEnumerable<Taxa> Taxas { get; set; }
}


[DataContract]
public record Taxa
{
	[DataMember(Name = "nome")]
	public string Nome { get; set; }
	[DataMember(Name = "valor")]
	public float Valor { get; set; }
}