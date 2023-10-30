// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Migration.App.Extensions;
using System.Text.Json.Serialization;

namespace Atlas.Migration.App.Flags;

public class TranslationsDtoTests
{
    [Fact]
    public void PropertyFrenchShouldHaveJsonPropertyNameAttribute()
    {
        JsonPropertyNameAttribute? attribute = typeof(TranslationsDto).GetAttribute<JsonPropertyNameAttribute>(nameof(TranslationsDto.French));

        attribute.Should().NotBeNull();
        attribute!.Name.Should().Be("fra");
    }
}
