using System.Runtime.Serialization;

namespace SDKBrasilAPI;

public record BankResponse : BaseResponse
{
	public IEnumerable<Bank> Banks { get; set; }
}

[DataContract]
public record Bank
{
	[DataMember(Name = "ispb")]
	public string ISPB { get; set; }

	[DataMember(Name = "name")]
	public string Name { get; set; }

	[DataMember(Name = "code")]
	public int? Code { get; set; }

	[DataMember(Name = "fullName")]
	public string FullName { get; set; }
}