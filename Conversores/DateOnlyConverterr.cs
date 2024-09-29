using System;
using System.Text.Json;
using System.Text.Json.Serialization;

public class DateOnlyConverterr : JsonConverter<DateOnly>
{
    private const string DateFormat = "yyyy-MM-dd";

    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String && DateOnly.TryParseExact(reader.GetString(), DateFormat, null, System.Globalization.DateTimeStyles.None, out var date))
        {
            return date;
        }
        throw new JsonException($"Invalid date format. Expected format is {DateFormat}.");
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(DateFormat));
    }
}

