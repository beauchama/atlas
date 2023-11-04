// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Contracts.Geography;

namespace Atlas.Contracts.Flags;

public sealed record GuessedFlag
{
    public required string Code { get; init; }

    public required Translations Translations { get; init; }

    public required bool IsSuccess { get; init; }

    public required AreaSize Size { get; init; }

    public required bool SameContinent { get; init; }

    public required double Kilometers { get; init; }

    public required double Miles { get; init; }

    public required double Direction { get; init; }
}
