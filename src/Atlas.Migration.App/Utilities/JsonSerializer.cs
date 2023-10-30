// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace Atlas.Migration.App.Utilities;

[ExcludeFromCodeCoverage]
internal sealed class JsonSerializer<T> : IJsonSerializer<T>
{
    public Task SerializeAsync(Stream stream, T value, JsonTypeInfo<T> metadata, CancellationToken cancellationToken = default)
        => JsonSerializer.SerializeAsync(stream, value, metadata, cancellationToken);
}
