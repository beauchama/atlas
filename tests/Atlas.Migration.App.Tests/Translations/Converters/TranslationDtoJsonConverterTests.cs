// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Migration.App.Translations.Dto;
using System.Text;
using System.Text.Json;

namespace Atlas.Migration.App.Translations.Converters;

public sealed class TranslationDtoJsonConverterTests
{
    private readonly TranslationDtoJsonConverter _converter = new();

    [Fact]
    public void ReadShouldGetAListOfTranslations()
    {
        const string json = /*lang=json,strict*/"""{ "translations": { "fra": { "official": "Officiel", "common": "Nom" } } }""";
        Utf8JsonReader reader = CreateJsonReader(json);

        IEnumerable<TranslationDto> translations = _converter.Read(ref reader, typeof(IEnumerable<TranslationDto>), new JsonSerializerOptions())!;

        translations.Should().BeEquivalentTo([new TranslationDto("fra", "Nom", "Officiel")]);
    }

    [Fact]
    public void ReadShouldExcludeAnyLanguagesThatAreNotFrench()
    {
        const string json = /*lang=json,strict*/"""{ "translations": { "fra": { "official": "Officiel", "common": "Nom" }, "ita": { "official": "Ufficiale", "common": "Nome" } } }""";
        Utf8JsonReader reader = CreateJsonReader(json);

        IEnumerable<TranslationDto> translations = _converter.Read(ref reader, typeof(IEnumerable<TranslationDto>), new JsonSerializerOptions())!;

        translations.Should().BeEquivalentTo([new TranslationDto("fra", "Nom", "Officiel")]);
    }

    [Fact]
    public void WriteShouldThrowNotSupportedException()
    {
        using MemoryStream stream = new();
        using Utf8JsonWriter writer = new(stream);

        Action act = () => _converter.Write(writer, [], new JsonSerializerOptions());

        act.Should().ThrowExactly<NotSupportedException>();
    }

    private static Utf8JsonReader CreateJsonReader(string json)
    {
        Utf8JsonReader reader = new(Encoding.UTF8.GetBytes(json));

        reader.Read();
        reader.Read();

        while (reader.TokenType != JsonTokenType.StartObject)
            reader.Read();

        return reader;
    }
}
