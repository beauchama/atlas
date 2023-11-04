// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Migration.App.Geography.Converters;
using Atlas.Migration.App.Geography.Dto;
using System.Text.Json.Serialization;

namespace Atlas.Migration.App.Flags.Dto;

internal sealed record FlagDto
{
    [JsonPropertyName("cca3")]
    public required string Code { get; init; }

    public required TranslationDto Name { get; init; }

    public required string Region { get; init; }

    public required TranslationsDto Translations { get; init; }

    [JsonPropertyName("latlng")]
    [JsonConverter(typeof(GeographicCoordinateDtoJsonConverter))]
    public required GeographicCoordinateDto Coordinate { get; init; }

    public required double Area { get; init; }
}
