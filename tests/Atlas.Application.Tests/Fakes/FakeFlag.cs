// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Domain.Flags;
using Atlas.Domain.Geography;
using Atlas.Domain.Translations;

namespace Atlas.Application.Fakes;

internal static class FakeFlag
{
    internal static Flag CanadaFlag { get; } = new()
    {
        Code = "can",
        Translations = [new Translation("fra", "Canada", "Canada"), new Translation("en", "Canada", "Canada")],
        Continent = Continent.America,
        Coordinate = new GeographicCoordinate(60, -95),
        Area = new Area(9984670)
    };

    internal static Flag UsaFlag { get; } = new()
    {
        Code = "usa",
        Translations = [new Translation("fra", "Usa", "Usa"), new Translation("en", "Usa", "Usa")],
        Continent = Continent.America,
        Coordinate = new GeographicCoordinate(38, -97),
        Area = new Area(9372610)
    };

    internal static Flag ItalyFlag { get; } = new()
    {
        Code = "ita",
        Translations = [new Translation("fra", "Italie", "Italie"), new Translation("en", "Italy", "Italy")],
        Continent = Continent.Europe,
        Coordinate = new GeographicCoordinate(42.8333, 12.8333),
        Area = new Area(301336)
    };

    internal static GuessedFlag GuessedCanadaFlag { get; } = new()
    {
        Code = "can",
        IsSuccess = true,
        SameContinent = true,
        Size = AreaSize.Larger,
        Distance = Distance.Calculate(new GeographicCoordinate(0, 0), new GeographicCoordinate(0, 0)),
        Direction = Direction.Calculate(new GeographicCoordinate(0, 0), new GeographicCoordinate(0, 0)),
        Translations = [new Translation("fra", "Canada", "Canada"), new Translation("en", "Canada", "Canada")]
    };
}
