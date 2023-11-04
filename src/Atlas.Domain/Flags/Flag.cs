// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Domain.Geography;

namespace Atlas.Domain.Flags;

public sealed record Flag
{
    public required string Code { get; init; }

    public required Translations Translations { get; init; }

    public required Continent Continent { get; init; }

    public required GeographicCoordinate Coordinate { get; init; }

    public required Area Area { get; init; }
}
