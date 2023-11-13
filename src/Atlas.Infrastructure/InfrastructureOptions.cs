// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

namespace Atlas.Infrastructure;

public sealed record InfrastructureOptions
{
    public string BaseAddress { get; set; } = string.Empty;

    public bool IsProduction { get; set; }
}
