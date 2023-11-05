// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Migration.App.Translations.Dto;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Atlas.Migration.App.Translations.Converters;

internal sealed class TranslationDtoJsonConverter : JsonConverter<IEnumerable<TranslationDto>>
{
    private readonly IEnumerable<string> _acceptedLanguages = ["fra"];

    public override IEnumerable<TranslationDto>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        List<TranslationDto> translations = [];

        while (reader.TokenType != JsonTokenType.PropertyName)
        {
            _ = reader.Read();

            if (reader.TokenType == JsonTokenType.EndObject)
                break;

            string language = reader.GetString()!;
            _ = reader.Read();

            string official = SkipPropertyNameAndGetValue(ref reader);
            string common = SkipPropertyNameAndGetValue(ref reader);

            translations.Add(new TranslationDto(language, common, official));

            _ = reader.Read();
        }

        return translations.Where(t => _acceptedLanguages.Any(l => t.Code.Equals(l, StringComparison.Ordinal)));

        static string SkipPropertyNameAndGetValue(ref Utf8JsonReader reader)
        {
            _ = reader.Read();
            _ = reader.Read();

            return reader.GetString()!;
        }
    }

    public override void Write(Utf8JsonWriter writer, IEnumerable<TranslationDto> value, JsonSerializerOptions options)
        => throw new NotSupportedException();
}
