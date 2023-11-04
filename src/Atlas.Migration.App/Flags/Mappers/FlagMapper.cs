// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Domain.Flags;
using Atlas.Domain.Geography;
using Atlas.Migration.App.Flags.Dto;
using Atlas.Migration.App.Geography.Mappers;

namespace Atlas.Migration.App.Flags.Mappers;

internal static class FlagMapper
{
    internal static IEnumerable<Flag> MapToDomain(this IEnumerable<FlagDto> flags) => flags.Select(MapToDomain);

    private static Flag MapToDomain(this FlagDto flag) => new()
    {
        Code = flag.Code,
        Translations = new Translations()
        {
            English = new Translation(flag.Name.Common, flag.Name.Official),
            French = new Translation(flag.Translations.French.Common, flag.Translations.French.Official)
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
        Coordinate = flag.Coordinate.AsDomain(),
        Area = new Area(flag.Area)
    };
}
