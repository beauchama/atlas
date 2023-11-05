// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Domain.Geography;
using Atlas.Domain.Translations;

namespace Atlas.Domain.Flags;

public sealed record GuessedFlag
{
    public required string Code { get; init; }

    public required IEnumerable<Translation> Translations { get; init; }

    public required bool IsSuccess { get; init; }

    public required AreaSize Size { get; init; }

    public required bool SameContinent { get; init; }

    public required Distance Distance { get; init; }

    public required Direction Direction { get; init; }
}
