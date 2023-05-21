using BrasilAPI;
using static BrasilAPI.BrasilAPI;


//var bancos= await Current.ObterBancosAsync();

//var cep = await Current.Cep("30310-300");

//var banco = await Current.Bank(70);

//var feriados = await Current.Feriados(2023);

var tabelas = await Current.FipeTabelas();

var fipeM = await Current.FipeMarcas(null, 285);

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
