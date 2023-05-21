using Newtonsoft.Json;

namespace BrasilAPI.Utils;

public static class StreamDesserializer
{
	static readonly JsonSerializer js = new();

	internal static T? DeserializeJsonFromStream<T>(Stream? stream)
	{
		if (stream is null || !stream.CanRead)
			return default;

		using var sr = new StreamReader(stream);
		using var jtr = new JsonTextReader(sr);
		var searchResult = js.Deserialize<T>(jtr);
		return searchResult;
	}

	public static async Task<string> StreamToStringAsync(Stream? stream)
	{
		var content = string.Empty;

		if (stream is null)
			return content;

		using var sr = new StreamReader(stream);
		content = await sr.ReadToEndAsync().ConfigureAwait(false);

		return content;
	}
}
