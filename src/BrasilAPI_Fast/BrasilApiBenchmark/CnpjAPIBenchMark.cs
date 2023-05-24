using BrasilAPI;

namespace BrasilApiBenchmark;


[MemoryDiagnoser]
public class CnpjAPIBenchMark
{

	BrasilAPI.BrasilAPI FastApi => BrasilAPI.BrasilAPI.Current;

	SDKBrasilAPI.BrasilAPI oldApi;

	CustomHttpMessageHandler handler;

	[GlobalSetup]
	public async Task Initiliaze()
	{
		using var grab = new SDKBrasilAPI.BrasilAPI();
		var response = await grab.CNPJ("45633726000108");
		
		var json = System.Text.Json.JsonSerializer.Serialize(response);

		handler = new()
		{
			Content = json
		};


		var client = new HttpClient(handler);

		FastApi.ReplaceHttpClient(client);

		oldApi = new(client);
	}


	[Benchmark]
	public Task<CNPJResponse> NewApiAsync()
	{
		return FastApi.Cnpj("1234");
	}

	[Benchmark(Baseline = true)]
	public Task<SDKBrasilAPI.CNPJResponse> OldApiAsync()
	{
		return oldApi.CNPJ("123");
	}
}
