// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Domain.Flags;
using Atlas.Infrastructure.Geography.Converters;
using System.Text.Json.Serialization;

namespace Atlas.Infrastructure.Flags;

[JsonSerializable(typeof(Flag))]
[JsonSerializable(typeof(IEnumerable<Flag>))]
[JsonSourceGenerationOptions(
    GenerationMode = JsonSourceGenerationMode.Metadata,
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    Converters = [typeof(AreaJsonConverter)],
    PropertyNameCaseInsensitive = true,
    UseStringEnumConverter = true)]
public sealed partial class FlagJsonContext : JsonSerializerContext
{
}
