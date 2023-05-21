namespace BrasilAPI;

class Constants
{
	public const string BaseUrl= "https://brasilapi.com.br/api/";
	
	public const string TaxasSiglaUrl = "taxas/v1/{0}";

	public const string TaxasUrl = "taxas/v1/";

	public const string FipePrecosUrl = "fipe/preco/v1/{0}";

	public const string FipeTabelasUrl = "fipe/tabelas/v1";

	public const string FipeMarcasUrl = "fipe/marcas/v1/{0}";

	public const string IbgeMunicipiosUrl = "ibge/municipios/v1/{0}";

	public const string Ibge_UFUrl = IbgeEstadosUrl + "{0}";

	public const string IbgeEstadosUrl = "ibge/uf/v1/";

	public const string FeriadosUrl = "feriados/v1/{0}";

	public const string BancosUrl = "banks/v1/";

	public const string BancosCodigoUrl = BancosUrl + "{0}";

	public const string DddUrl = "ddd/v1/{0}";

	public const string CepUrl = "cep/v2/{0}";

	public const string CnpjUrl = "cnpj/v1/{0}";

	public const string RegistroUrl = "registrobr/v1/{0}";
}
