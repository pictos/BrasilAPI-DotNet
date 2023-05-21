using System.Runtime.Serialization;

namespace SDKBrasilAPI;

[DataContract]
public record CEPResponse : BaseResponse
{
	[DataMember(Name = "cep")]
	public string CEP { get; set; }
	
	[DataMember(Name = "state")]
	public Uf UF { get; set; }
	
	[DataMember(Name = "city")]
	public string City { get; set; }
	
	[DataMember(Name = "neighborhood")]
	public string Neighborhood { get; set; }
	
	[DataMember(Name = "street")]
	public string Street { get; set; }
	
	[DataMember(Name = "location")]
	public Location Location { get; set; }
}


[DataContract]
public record Location
{
	[DataMember(Name = "type")]
	public string Type { get; set; }
	
	[DataMember(Name = "coordinates")]
	public Coordinates Coordinates { get; set; }
}


[DataContract]
public record Coordinates
{
	[DataMember(Name = "longitude")]
	public string Longitude { get; set; }
	
	[DataMember(Name = "latitude")]
	public string Latitude { get; set; }
}