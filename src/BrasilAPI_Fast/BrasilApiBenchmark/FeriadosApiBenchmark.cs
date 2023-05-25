using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrasilAPI;
using Microsoft.Extensions.Options;

namespace BrasilApiBenchmark;

[MemoryDiagnoser]
public class FeriadosApiBenchmark
{
	BrasilAPI.BrasilAPI FastApi => BrasilAPI.BrasilAPI.Current;

	SDKBrasilAPI.BrasilAPI oldApi;

	[GlobalSetup]
	public async Task Init()
	{
		using var grab = new SDKBrasilAPI.BrasilAPI();
		var response = await FastApi.Feriados(2023);
		
		var json = System.Text.Json.JsonSerializer.Serialize(response.Feriados, InitHelper.options);

		var handler = new CustomHttpMessageHandler()
		{
			Content = json
		};

		var client = new HttpClient(handler);

		FastApi.ReplaceHttpClient(client);

		oldApi = new(client);
	}

	[Benchmark]
	public Task<FeriadosResponse> NewApiAsync()
	{
		return FastApi.Feriados(12);
	}

	[Benchmark(Baseline = true)]
	public Task<SDKBrasilAPI.FeriadosResponse> OldApiAsync()
	{
		return oldApi.Feriados(12);
	}

}
