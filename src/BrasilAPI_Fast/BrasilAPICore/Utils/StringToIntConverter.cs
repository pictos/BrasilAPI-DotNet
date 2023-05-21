using System.Text.Json;

namespace BrasilAPI.Utils;

public class StringToIntConverter : JsonConverter<int>
{
	public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType == JsonTokenType.String)
		{
			if (int.TryParse(reader.GetString(), out var value))
			{
				return value;
			}
		}
		throw new JsonException();
	}

	public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
	{
		writer.WriteStringValue(value.ToString());
	}
}
