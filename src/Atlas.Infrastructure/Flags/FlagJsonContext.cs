// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Domain.Flags;
using System.Text.Json.Serialization;

namespace Atlas.Infrastructure.Flags;

[JsonSerializable(typeof(Flag))]
[JsonSerializable(typeof(IEnumerable<Flag>))]
[JsonSourceGenerationOptions(
    PropertyNameCaseInsensitive = true,
    GenerationMode = JsonSourceGenerationMode.Metadata,
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
public sealed partial class FlagJsonContext : JsonSerializerContext
{
}
