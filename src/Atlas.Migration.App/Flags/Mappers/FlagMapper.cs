// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Domain.Flags;
using Atlas.Domain.Geography;
using Atlas.Migration.App.Flags.Dto;

namespace Atlas.Migration.App.Flags.Mappers;

internal static class FlagMapper
{
    internal static IEnumerable<Flag> MapToDomain(this IEnumerable<FlagDto> flags) => flags.Select(MapToDomain);

    private static Flag MapToDomain(this FlagDto flag) => new()
    {
        Code = flag.Code,
        Translations = new Translations()
        {
            English = new Translation("eng", flag.Name.Common, flag.Name.Official),
            French = new Translation("fra", flag.Translations.French.Common, flag.Translations.French.Official)
        },
        Continent = flag.Region switch
        {
            "Americas" => Continent.America,
            "Europe" => Continent.Europe,
            "Asia" => Continent.Asia,
            "Africa" => Continent.Africa,
            "Oceania" => Continent.Oceania,
            "Antarctic" => Continent.Antarctic,
            _ => Continent.America
        },
        Coordinate = new GeographicCoordinate(flag.Coordinate.Latitude, flag.Coordinate.Longitude),
        Area = flag.Area
    };
}
