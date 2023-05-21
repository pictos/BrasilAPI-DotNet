using System.Runtime.Serialization;

namespace BrasilAPI;

[DataContract]
public record DDDResponse : BaseResponse
{
	[JsonPropertyName( "state")]
	public Uf UF { get; set; }

	[JsonPropertyName( "cities")]
	public IEnumerable<string> Cities { get; set; }
}