using System.Reflection;
using System.Text;
using static BrasilAPI.Constants;
using static BrasilAPI.Utils.ExceptionExtensions;
using static BrasilAPI.Utils.StreamDesserializer;

namespace BrasilAPI;

public class BrasilAPI
{
	static readonly Lazy<BrasilAPI> lazyBRApi = new(() => new());

	public static BrasilAPI Current => lazyBRApi.Value;

	HttpClient client;

	private static readonly Uri BaseUri = new(BaseUrl);

	BrasilAPI()
	{
		client = new HttpClient
		{
			BaseAddress = BaseUri
		};

		var version = Assembly.GetExecutingAssembly().GetName().Version;
		var userAgent = $"BrasilAPI.DotNet/{version}";

		client.DefaultRequestHeaders.Add("user-agent", userAgent);
	}

	/// <summary>
	/// Utilize esse método para usar uma instância de <see cref="HttpClient"/> customizada.
	/// </summary>
	/// <param name="httpClient"><see cref="HttpClient"/> que será utilizado para fazer as chamadas</param>
	public void ReplaceHttpClient(HttpClient httpClient)
	{
		ThrowIfNull(client);
		client.Dispose();

		client = httpClient;

		client.BaseAddress = BaseUri;
	}

	/// <summary>
	/// Busca por Cnpj na API Minha Receita.
	/// </summary>
	/// <param name="cnpj">
	/// O Cadastro Nacional da Pessoa Jurídica é um número único que identifica uma pessoa 
	/// jurídica e outros tipos de arranjo jurídico sem personalidade jurídica junto à Receita Federal.
	/// </param>
	/// <returns></returns>
	public Task<CNPJResponse> Cnpj(ReadOnlySpan<char> cnpj)
	{
		//Adicionado para remover mascaras caso venha... 
		cnpj = OnlyNumbers(cnpj);

		var url = string.Format(CnpjUrl, cnpj.ToString());

		return ProcessResponseRequest<CNPJResponse>(url);
	}

	/// <summary>
	/// Busca por Cep com múltiplos providers de fallback.
	/// </summary>
	/// <param name="cep">
	/// O Cep (Código de Endereçamento Postal) é um sistema de códigos que visa 
	/// racionalizar o processo de encaminhamento e entrega de correspondências através da divisão do país em regiões postais. ... 
	/// Atualmente, o Cep é composto por oito dígitos, cinco de um lado e três de outro. Cada algarismo do Cep possui um significado.
	/// </param>
	/// <returns></returns>
	public Task<CEPResponse> Cep(string cep)
	{
		//Na doc. ta colocando cep como int64, porem todos os sistemas (que eu tenha visto) trabalham com cep como string...
		//Como estou adicionando esse processo pra remover alguma mascara creio "resolver" o problema
		cep = OnlyNumbers(cep);


		var url = string.Format(CepUrl, cep.ToString());

		return ProcessResponseRequest<CEPResponse>(url);
	}

	/// <summary>
	/// Retorna estado e lista de cidades por DDD
	/// </summary>
	/// <param name="ddd">
	/// DDD significa Discagem Direta à Distância. É um sistema de ligação telefônica automática entre diferentes 
	/// áreas urbanas nacionais. O DDD é um código constituído por 2 dígitos que identificam as principais cidades do país e devem ser 
	/// adicionados ao nº de telefone, juntamente com o código da operadora.
	/// </param>
	/// <returns></returns>
	public Task<DDDResponse> Ddd(in int ddd)
	{
		var url = string.Format(DddUrl, ddd);

		return ProcessResponseRequest<DDDResponse>(url);
	}

	/// <summary>
	/// Lista os feriados nacionais de determinado ano.
	/// Calcula os feriados móveis baseados na Páscoa e adiciona os feriados fixos
	/// </summary>
	/// <param name="ano">Ano para calcular os feriados.</param>
	/// <returns></returns>
	public async Task<FeriadosResponse> Feriados(int ano)
	{
		var url = string.Format(FeriadosUrl, ano);

		var (feriados, stream) = await ProcessRequest<IEnumerable<Feriado>>(url).ConfigureAwait(false);

		var feriadoResponse = new FeriadosResponse
		{
			Feriados = feriados ?? Enumerable.Empty<Feriado>(),
			CalledURL = url,
			JsonResponse = stream
		};

		return feriadoResponse;
	}

	/// <summary>
	/// Retorna informações de todos estados do Brasil
	/// </summary>
	/// <returns></returns>
	public async Task<IBGEsResponse> IbgeUfs()
	{
		var (ibges, stream) = await ProcessRequest<IEnumerable<Ibge>>(IbgeEstadosUrl);

		var ibgeResponse = new IBGEsResponse()
		{
			IBGEs = ibges ?? Enumerable.Empty<Ibge>(),
			CalledURL = IbgeEstadosUrl,
			JsonResponse = stream
		};

		return ibgeResponse;
	}

	/// <summary>
	/// Busca as informações de um um estado a partir da sigla(UF) ou código
	/// </summary>
	/// <param name="code"></param>
	/// <returns></returns>
	public Task<IBGEResponse> IbgeUf(Uf code) => IbgeUf((int)code);

	/// <summary>
	/// Busca as informações de um um estado a partir da sigla(UF) ou código
	/// </summary>
	/// <param name="code"></param>
	/// <returns></returns>
	public async Task<IBGEResponse> IbgeUf(int code)
	{
		var url = string.Format(Ibge_UFUrl, code);

		var (result, stream) = await ProcessRequest<Ibge>(url).ConfigureAwait(false);

		var ibgeResponse = new IBGEResponse
		{
			IBGE = result ?? new(),
			CalledURL = url,
			JsonResponse = stream
		};

		return ibgeResponse;
	}

	/// <summary>
	/// Lista os municicípios a partir da UF
	/// </summary>
	/// <param name="uf"></param>
	/// <returns></returns>
	public async Task<IBGEMunicipiosResponse> IbgeMunicipios(Uf uf)
	{
		var url = string.Format(IbgeMunicipiosUrl, uf);

		var (municipios, stream) = await ProcessRequest<IEnumerable<Municipio>>(url).ConfigureAwait(false);

		var ibgeResponse = new IBGEMunicipiosResponse()
		{
			Municipios = municipios ?? Enumerable.Empty<Municipio>(),
			CalledURL = url,
			JsonResponse = stream
		};

		return ibgeResponse;
	}

	/// <summary>
	/// Lista as marca de veículos referente ao tipo de veículo
	/// </summary>
	/// <param name="tipoVeiculo">Os tipos suportados são caminhoes, carros e motos. Quando o tipo não é específicado são buscada as marcas de todos os tipos de veículos</param>
	/// <param name="tabelaReferencia">Código da tabela fipe de referência. Por padrão é utilizado o código da tabela fipe atual.</param>
	/// <returns></returns>
	public async Task<FIPEMarcasResponse> FipeMarcas(TipoVeiculo? tipoVeiculo = null, long? tabelaReferencia = null)
	{

		var url = string.Format(FipeMarcasUrl, tipoVeiculo);

		if (tabelaReferencia.HasValue)
		{
			url += $"?tabela_referencia={tabelaReferencia.Value}";
		}

		var (marcas, stream) = await ProcessRequest<IEnumerable<MarcaVeiculo>>(url.ToLowerInvariant()).ConfigureAwait(false);


		var fipeResponse = new FIPEMarcasResponse()
		{
			Marcas = marcas ?? Enumerable.Empty<MarcaVeiculo>(),
			CalledURL = url,
			JsonResponse = stream
		};

		return fipeResponse;
	}

	/// <summary>
	/// Lista as tabelas de referência existentes.
	/// </summary>
	/// <returns></returns>
	public async Task<FIPETabelasResponse> FipeTabelas()
	{
		var (tabelas, stream) = await ProcessRequest<IEnumerable<TabelaFIPE>>(FipeTabelasUrl).ConfigureAwait(false);

		var fipeResponse = new FIPETabelasResponse()
		{
			Tabelas = tabelas ?? Enumerable.Empty<TabelaFIPE>(),
			CalledURL = FipeTabelasUrl,
			JsonResponse = stream
		};

		return fipeResponse;
	}

	/// <summary>
	/// Consulta o preço do veículo segundo a tabela fipe.
	/// </summary>
	/// <param name="codigoFipe">Código fipe do veículo.</param>
	/// <param name="tabelaReferencia">Código da tabela fipe de referência. Por padrão é utilizado o código da tabela fipe atual.</param>
	/// <returns></returns>
	public async Task<FIPEPrecosResponse> FipePrecos(string codigoFipe, long? tabelaReferencia = null)
	{
		var url = string.Format(FipePrecosUrl, OnlyNumbers(codigoFipe.AsSpan()).ToString());

		if (tabelaReferencia.HasValue)
		{
			url += "?tabela_referencia=" + tabelaReferencia.Value;
		}

		var (precos, stream) = await ProcessRequest<IEnumerable<PrecoFIPE>>(url).ConfigureAwait(false);

		var fipeResponse = new FIPEPrecosResponse
		{
			Precos = precos ?? Enumerable.Empty<PrecoFIPE>(),
			CalledURL = url,
			JsonResponse = stream
		};

		return fipeResponse;
	}

	/// <summary>
	/// Avalia o status de um dominio .br
	/// </summary>
	/// <param name="dominio">O domínio ou nome a ser avaliado</param>
	/// <returns></returns>
	public Task<RegistroBrResponse> RegistroBR(in string dominio)
	{
		var url = string.Format(RegistroUrl, dominio);

		return ProcessResponseRequest<RegistroBrResponse>(url);
	}

	public async Task<TaxasResponse> Taxas()
	{
		var (taxas, stream) = await ProcessRequest<IEnumerable<Taxa>>(TaxasUrl).ConfigureAwait(false);

		var taxasResponse = new TaxasResponse
		{
			Taxas = taxas ?? Enumerable.Empty<Taxa>(),
			CalledURL = TaxasUrl,
			JsonResponse = stream
		};

		return taxasResponse;
	}

	public async Task<TaxaResponse> Taxas(string sigla = "")
	{
		var url = string.Format(TaxasSiglaUrl, sigla);

		var (taxa, stream) = await ProcessRequest<Taxa>(url).ConfigureAwait(false);

		var taxasResponse = new TaxaResponse
		{
			Taxa = taxa ?? new(),
			CalledURL = url,
			JsonResponse = stream
		};

		return taxasResponse;
	}

	/// <summary>
	/// Busca as informações de um banco a partir de um código
	/// </summary>
	/// <param name="code"></param>
	/// <returns></returns>
	public async Task<BancoResponse> Bank(int code)
	{
		var url = string.Format(BancosCodigoUrl, code);

		var (bank, stream) = await ProcessRequest<Banco>(url).ConfigureAwait(false);

		var bankResponse = new BancoResponse
		{
			Banco = bank ?? new(),
			CalledURL = url,
			JsonResponse = stream
		};

		return bankResponse;
	}

	/// <summary>
	/// Retorna informações de todos os bancos do Brasil
	/// </summary>
	/// <returns></returns>
	public async Task<BancosResponse> ObterBancosAsync()
	{
		var (bancos, stream) = await ProcessRequest<IEnumerable<Banco>>(BancosUrl);

		var bancoResponse = new BancosResponse
		{
			Bancos = bancos ?? Enumerable.Empty<Banco>(),
			CalledURL = BancosUrl,
			JsonResponse = stream
		};

		return bancoResponse;
	}


	async Task<T> ProcessResponseRequest<T>(string url)
		where T : BaseResponse, new()
	{
		//await Task.Delay(5_000);
		using var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

		await EnsureSuccess(response, BaseUrl);

#if DEBUG
		var json = await response.Content.ReadAsStringAsync();
#endif
		
		var stream = await response.Content.ReadAsStreamAsync();
		var result = DeserializeJsonFromStream<T>(stream) ?? new();

		result.CalledURL = url;
		result.JsonResponse = stream;

		return result;
	}

	async Task<(T?, Stream? stream)> ProcessRequest<T>(string url)
	{
		using var response = await client.GetAsync(url).ConfigureAwait(false);

		await EnsureSuccess(response, BaseUrl);

		var stream = await response.Content.ReadAsStreamAsync();

		var result = DeserializeJsonFromStream<T>(stream);

		return (result, stream);
	}

	static async Task EnsureSuccess(HttpResponseMessage response, string url)
	{
		if ((int)response.StatusCode >= 400)
		{
			var message = "Error while trying to access the BrasilAPI";
			string content = string.Empty;
			try
			{
				content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
			}
			catch
			{

			}
			finally
			{
				throw new BrasilAPIException(message)
				{
					ContentData = content,
					Code = (int)response.StatusCode,
					Url = url
				};
			}
		}
	}

	static string OnlyNumbers(string input)
	{
		var sb = new StringBuilder();

		foreach (var c in input)
		{
			if (!char.IsDigit(c))
				continue;

			sb.Append(c);
		}

		return sb.ToString();
	}
	static ReadOnlySpan<char> OnlyNumbers(ReadOnlySpan<char> input)
	{
		var size = input.Length;
		Span<char> result = new char[size];
		var resultIndex = 0;
		for (var i = 0; i < size; i++)
		{
			if (!char.IsDigit(input[i]))
				continue;

			result[resultIndex] = input[i];
			resultIndex++;
		}

		var indexEmpty = result.IndexOf('\0');

		if (indexEmpty is -1)
			return result;

		return result.Slice(0, indexEmpty);
	}
}
