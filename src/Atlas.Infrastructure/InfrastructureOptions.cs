// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using System.Diagnostics.CodeAnalysis;

namespace Atlas.Infrastructure;

[ExcludeFromCodeCoverage]
public sealed record InfrastructureOptions
{
    public string BaseAddress { get; set; } = string.Empty;

    public bool IsProduction { get; set; }
}
