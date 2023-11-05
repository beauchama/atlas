// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Contracts.Translations;

namespace Atlas.Contracts.Flags;

public sealed record Flag
{
    public required string Code { get; init; }

    public required IEnumerable<Translation> Translations { get; init; }
}
