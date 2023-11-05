// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Migration.App.Flags.Dto;
using Atlas.Migration.App.Geography.Converters;
using Atlas.Migration.App.Translations.Converters;
using System.Text.Json.Serialization;

namespace Atlas.Migration.App.Flags;

[JsonSerializable(typeof(IEnumerable<FlagDto>))]
[JsonSourceGenerationOptions(
    GenerationMode = JsonSourceGenerationMode.Metadata,
    Converters = [typeof(TranslationDtoJsonConverter), typeof(GeographicCoordinateDtoJsonConverter)],
    PropertyNameCaseInsensitive = true)]
internal sealed partial class FlagDtoJsonContext : JsonSerializerContext
{
}
