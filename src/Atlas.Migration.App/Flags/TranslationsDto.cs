// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using System.Text.Json.Serialization;

namespace Atlas.Migration.App.Flags;

internal sealed record TranslationsDto
{
    [JsonPropertyName("fra")]
    public required TranslationDto French { get; init; }
}
