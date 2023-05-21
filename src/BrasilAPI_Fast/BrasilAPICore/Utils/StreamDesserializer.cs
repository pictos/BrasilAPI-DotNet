using Newtonsoft.Json;

namespace BrasilAPI.Utils;

static class StreamDesserializer
{
	public static T? DeserializeJsonFromStream<T>(Stream? stream)
	{
		if (stream is null || stream.CanRead == false)
			return default;

		using var sr = new StreamReader(stream);
		using var jtr = new JsonTextReader(sr);
		var js = new JsonSerializer();
		var searchResult = js.Deserialize<T>(jtr);
		return searchResult;
	}

	public static async Task<string> StreamToStringAsync(Stream? stream)
	{
		var content = string.Empty;

		if (stream is not null)
		{
			using var sr = new StreamReader(stream);
			content = await sr.ReadToEndAsync().ConfigureAwait(false);
		}

		return content;
	}
}
