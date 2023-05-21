using static BrasilAPI.Constants;

namespace BrasilAPI;

public class BrasilAPI
{
	static readonly Lazy<BrasilAPI> lazyBRApi = new(() => new());

	public static BrasilAPI Current => lazyBRApi.Value;

	readonly HttpClient client;

	BrasilAPI()
	{
		client = new HttpClient
		{
			BaseAddress = new(BaseUrl)
		};
	}


}
