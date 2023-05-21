using System.Runtime.Serialization;

namespace BrasilAPI;

[DataContract]
public record DDDResponse : BaseResponse
{
	[DataMember(Name = "state")]
	public Uf UF { get; set; }

	[DataMember(Name = "cities")]
	public IEnumerable<string> Cities { get; set; }
}