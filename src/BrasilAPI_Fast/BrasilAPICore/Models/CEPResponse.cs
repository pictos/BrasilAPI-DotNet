using System.Runtime.Serialization;

namespace BrasilAPI;

[DataContract]
public record CEPResponse : BaseResponse
{
	[JsonPropertyName( "cep")]
	public string CEP { get; set; }
	
	[JsonPropertyName( "state")]
	[JsonConverter(typeof(JsonStringEnumConverter))]
	public Uf UF { get; set; }
	
	[JsonPropertyName( "city")]
	public string City { get; set; }
	
	[JsonPropertyName( "neighborhood")]
	public string Neighborhood { get; set; }
	
	[JsonPropertyName( "street")]
	public string Street { get; set; }
	
	[JsonPropertyName( "location")]
	public Location Location { get; set; }
}


[DataContract]
public record Location
{
	[JsonPropertyName( "type")]
	public string Type { get; set; }
	
	[JsonPropertyName( "coordinates")]
	public Coordinates Coordinates { get; set; }
}


[DataContract]
public record Coordinates
{
	[JsonPropertyName( "longitude")]
	public string Longitude { get; set; }
	
	[JsonPropertyName( "latitude")]
	public string Latitude { get; set; }
}