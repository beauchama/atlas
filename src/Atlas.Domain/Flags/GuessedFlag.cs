﻿// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Domain.Geography;

namespace Atlas.Domain.Flags;

public sealed record GuessedFlag
{
    public required string Code { get; init; }

    public required Translations Translations { get; init; }

    public AreaSize Size { get; init; }

    public bool SameContinent { get; init; }

    public required Distance Distance { get; init; }

    public required Direction Direction { get; init; }
}
