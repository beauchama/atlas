// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Domain.Flags;
using Atlas.Domain.Geography;

namespace Atlas.Application.Fakes;

internal static class FakeFlag
{
    public static Flag CanadaFlag { get; } = new()
    {
        Code = "can",
        Translations = new Translations()
        {
            English = new Translation("eng", "Italy", "Italy"),
            French = new Translation("fra", "Italy", "Italy")
        },
        Continent = Continent.America,
        Coordinate = new GeographicCoordinate(42.0, 42.0),
        Area = 10.0,
    };

    public static Flag ItalyFlag { get; } = new()
    {
        Code = "ita",
        Translations = new Translations()
        {
            English = new Translation("eng", "Italy", "Italy"),
            French = new Translation("fra", "Italy", "Italy")
        },
        Continent = Continent.Europe,
        Coordinate = new GeographicCoordinate(42.0, 42.0),
        Area = 10.0,
    };
}
