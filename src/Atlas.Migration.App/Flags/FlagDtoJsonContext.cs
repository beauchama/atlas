// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using System.Text.Json.Serialization;

namespace Atlas.Migration.App.Flags;

[JsonSerializable(typeof(IEnumerable<FlagDto>))]
[JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Metadata, PropertyNameCaseInsensitive = true)]
internal partial class FlagDtoJsonContext : JsonSerializerContext
{
}
