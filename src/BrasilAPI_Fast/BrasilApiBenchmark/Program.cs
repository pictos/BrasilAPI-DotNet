// See https://aka.ms/new-console-template for more information
using System.Net;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using BrasilAPI;
using BrasilApiBenchmark;
using  FastApi = BrasilAPI.BrasilAPI;

Console.WriteLine("Hello, World!");


IConfig config = new DebugBuildConfig();
config.WithOptions(ConfigOptions.DisableOptimizationsValidator);

BenchmarkRunner.Run<CepApiBenchmark>();


//var response = await new SDKBrasilAPI.BrasilAPI().CNPJ("45633726000108");

//var re = await BrasilAPI.BrasilAPI.Current.Cnpj("45633726000108");

//var json = System.Text.Json.JsonSerializer.Serialize(response);


//var handler = new CustomHttpMessageHandler
//{
//	Content = json
//};

//var client = new HttpClient(handler);

//FastApi.Current.ReplaceHttpClient(client);

//var cep = new CepApiBenchmark();

//await cep.Initiliaze();


//for (int i = 0; i < 10; i++)
//{
//	var r = await cep.NewApiAsync();
//}


