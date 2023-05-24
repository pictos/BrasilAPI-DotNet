using BrasilAPI;

namespace BrasilApiBenchmark;

[MemoryDiagnoser]
public class DDDApiBenchmark
{
	
	BrasilAPI.BrasilAPI FastApi => BrasilAPI.BrasilAPI.Current;

	SDKBrasilAPI.BrasilAPI oldApi;

	CustomHttpMessageHandler handler;

	[GlobalSetup]
	public async Task Init()
	{
		var response= await FastApi.Ddd(31);

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
	public Task<DDDResponse> NewApiAsync()
	{
		return FastApi.Ddd(31);
	}

	[Benchmark(Baseline = true)]
	public Task<SDKBrasilAPI.DDDResponse> OldApiAsync()
	{
		return oldApi.DDD(31);
	}
}
