using System.Runtime.Serialization;

namespace BrasilAPI;

public record IBGEResponse : BaseResponse
{
	public IEnumerable<IBGE> IBGEs { get; set; }
}

[DataContract]
public record IBGE
{
	[DataMember(Name = "id")]
	public int ID { get; set; }

	public Uf UF { get => (Uf)ID; }

	[DataMember(Name = "sigla")]
	public string Sigla { get; set; }

	[DataMember(Name = "nome")]
	public string Nome { get; set; }

	[DataMember(Name = "regiao")]
	public Regiao Regiao { get; set; }
}

[DataContract]
public record Regiao
{
	[DataMember(Name = "id")]
	public int ID { get; set; }

	[DataMember(Name = "sigla")]
	public string Sigla { get; set; }

	[DataMember(Name = "nome")]
	public string Nome { get; set; }
}