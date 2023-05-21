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

[DataContract]
public record Banco
{
	[DataMember(Name = "ispb")]
	public string ISPB { get; set; }

	[DataMember(Name = "name")]
	public string Nome { get; set; }

	[DataMember(Name = "code")]
	public int? Codigo { get; set; }

	[DataMember(Name = "fullName")]
	public string NomeCompleto { get; set; }
}