// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Domain.Flags;
using Atlas.Domain.Geography;
using Atlas.Migration.App.Flags.Dto;
using Atlas.Migration.App.Geography.Mappers;
using Atlas.Migration.App.Translations.Mappers;

namespace Atlas.Migration.App.Flags.Mappers;

internal static class FlagMapper
{
    internal static IEnumerable<Flag> AsDomain(this IEnumerable<FlagDto> flags)
    {
        return flags.Select(AsDomain).ToArray();

        static Flag AsDomain(FlagDto flag) => new()
        {
            Code = flag.Code,
            Translations = flag.Translations.AsDomain(flag.Name),
            Continent = flag.Region.AsDomain(),
            Coordinate = flag.Coordinate.AsDomain(),
            Area = new Area(flag.Area)
        };
    }
}
