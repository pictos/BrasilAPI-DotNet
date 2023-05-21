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

	/// <summary>
	/// Retorna informações de todos os bancos do Brasil
	/// </summary>
	/// <returns></returns>
	public async Task<BankResponse> Banks()
	{
		string baseUrl = $"{BASE_URL}/banks/v1";

		var response = await Client.GetAsync(baseUrl);

		await EnsureSuccess(response, baseUrl);

		var json = await response.Content.ReadAsStringAsync();

		BankResponse bankResponse = new BankResponse()
		{
			Banks = JsonConvert.DeserializeObject<IEnumerable<Bank>>(json),
			CalledURL = baseUrl,
			JsonResponse = json
		};

		return bankResponse;
	}
}
