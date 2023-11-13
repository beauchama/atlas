// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Migration.App.Geography.Converters;
using Atlas.Migration.App.Translations.Converters;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Atlas.Migration.App.Flags;

public sealed class FlagDtoJsonContextTests
{
    [Fact]
    public void FlagDtoJsonContextShouldHaveTrueForPropertyNameCaseInsensitive()
    {
        JsonSerializerOptions options = FlagDtoJsonContext.Default.Options;

        options.PropertyNameCaseInsensitive.Should().BeTrue();
    }

    [Fact]
    public void FlagDtoJsonContextShouldHaveConverters()
    {
        JsonSerializerOptions options = FlagDtoJsonContext.Default.Options;

        options.Converters.Should().HaveCount(2);
        options.Converters[0].Should().BeOfType<TranslationDtoJsonConverter>();
        options.Converters[1].Should().BeOfType<GeographicCoordinateDtoJsonConverter>();
    }

    [Fact]
    public void FlagDtoJsonSerializerShouldHaveMetadataForGenerationMode()
    {
        JsonSourceGenerationOptionsAttribute? attribute = typeof(FlagDtoJsonContext).GetCustomAttribute<JsonSourceGenerationOptionsAttribute>();

        attribute!.GenerationMode.Should().Be(JsonSourceGenerationMode.Metadata);
    }
}
