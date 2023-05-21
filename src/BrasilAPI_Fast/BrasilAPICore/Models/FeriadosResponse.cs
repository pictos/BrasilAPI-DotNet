using System.Runtime.Serialization;

namespace SDKBrasilAPI;

public record FeriadosResponse : BaseResponse
{
	public IEnumerable<Feriado> Feriados { get; set; }
}

[DataContract]
public record Feriado
{
	[DataMember(Name = "date")]
	public DateTime? Date { get; set; }

	[DataMember(Name = "name")]
	public string Name { get; set; }

	[DataMember(Name = "type")]
	public string Type { get; set; }
}