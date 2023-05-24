using BrasilAPI;

namespace BrasilApiBenchmark;

[MemoryDiagnoser]
public class CepApiBenchmark
{
	BrasilAPI.BrasilAPI FastApi => BrasilAPI.BrasilAPI.Current;

	SDKBrasilAPI.BrasilAPI oldApi;

	CustomHttpMessageHandler handler;

	[GlobalSetup]
	public async Task Initiliaze()
	{
		using var grab =  new SDKBrasilAPI.BrasilAPI();
		var response = await grab.CEP_V2("30310-300");

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
	public async Task<CEPResponse> NewApiAsync()
	{
		//CEPResponse cep = null!;
		//for(var i = 0;i <= 100; i++)
		//	cep = await FastApi.Cep("30310-300");

		//return cep;

		return await FastApi.Cep("30310-300");
	}

	[Benchmark(Baseline = true)]
	public async Task<SDKBrasilAPI.CEPResponse> OldApiAsync()
	{
		//SDKBrasilAPI.CEPResponse cep = null!;
		//for(var i = 0;i <= 100; i++)
		//	cep = await oldApi.CEP_V2("30310-300");

		//return cep;

		
		return  await oldApi.CEP_V2("30310-300");
	}
}
