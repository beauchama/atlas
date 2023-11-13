// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Infrastructure.Geography.Converters;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Atlas.Infrastructure.Flags;

public sealed class FlagJsonContextTests
{
    [Fact]
    public void ContextShouldHaveFlagAsJsonSerializable()
    {
        IEnumerable<JsonSerializableAttribute> attributes = typeof(FlagJsonContext).GetCustomAttributes<JsonSerializableAttribute>()!;

        attributes.Should().HaveCount(2);
    }

    [Fact]
    public void ContextShouldHaveJsonSourceGenerationOptions()
    {
        JsonSourceGenerationOptionsAttribute attribute = typeof(FlagJsonContext).GetCustomAttribute<JsonSourceGenerationOptionsAttribute>()!;

        attribute.GenerationMode.Should().Be(JsonSourceGenerationMode.Metadata);
        attribute.PropertyNamingPolicy.Should().Be(JsonKnownNamingPolicy.CamelCase);
        attribute.PropertyNameCaseInsensitive.Should().BeTrue();
        attribute.UseStringEnumConverter.Should().BeTrue();
        attribute.Converters.Should().Contain(c => c == typeof(AreaJsonConverter));
    }
}
