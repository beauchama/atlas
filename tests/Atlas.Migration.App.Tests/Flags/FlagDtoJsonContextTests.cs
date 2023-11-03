// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

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
    public void FlagDtoJsonSerializerShouldHaveMetadataForGenerationMode()
    {
        JsonSourceGenerationOptionsAttribute? attribute = typeof(FlagDtoJsonContext).GetCustomAttribute<JsonSourceGenerationOptionsAttribute>();

        attribute!.GenerationMode.Should().Be(JsonSourceGenerationMode.Metadata);
    }
}
