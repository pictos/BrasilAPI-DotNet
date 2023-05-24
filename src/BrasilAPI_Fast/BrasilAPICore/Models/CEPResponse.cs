namespace BrasilAPI;

public record CEPResponse : BaseResponse
{
    public string CEP { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Uf State { get; set; }

    public string City { get; set; }

    public string Neighborhood { get; set; }

    public string Street { get; set; }

    public Location Location { get; set; }
}

//string json = 
//	$$"""
//        {
//   "CEP":"30310300",
//   "UF":31,
//   "City":"Belo Horizonte",
//   "Neighborhood":"Sion",
//   "Street":"Rua do Uruguai",
//   "Location":{
//      "Type":"Point",
//      "Coordinates":{
//         "Longitude":"-43.9313934",
//         "Latitude":"-19.9519364"
//      }
//   }
//}
//""";

public record Location
{
    public string Type { get; set; }

    public Coordinates Coordinates { get; set; }
}


public record Coordinates
{
    public string Longitude { get; set; }

    public string Latitude { get; set; }
}