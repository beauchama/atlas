// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Web.Shared.Flags;
using FlagEntity = Atlas.Domain.Flags.Flag;
using GuessedFlagEntity = Atlas.Domain.Flags.GuessedFlag;
using TranslationsEntity = Atlas.Domain.Flags.Translations;

namespace Atlas.Application.Flags;

internal static class FlagsMapper
{
    internal static IEnumerable<Flag> MapToShared(this IEnumerable<FlagEntity> entities)
        => entities.Select(MapToShared).ToArray();

    internal static GuessedFlag MapToShared(this GuessedFlagEntity entity) => new()
    {
        Code = entity.Code,
        Translations = entity.Translations.MapToShared(),
        IsSuccess = entity.IsSuccess,
        Size = entity.Size switch
        {
            Domain.Flags.AreaSize.Larger => AreaSize.Larger,
            Domain.Flags.AreaSize.Smaller => AreaSize.Smaller,
            Domain.Flags.AreaSize.Same => AreaSize.Same,
            _ => AreaSize.Same
        },
        Kilometers = entity.Distance.Kilometers,
        Miles = entity.Distance.Miles,
        Direction = entity.Direction,
        SameContinent = entity.SameContinent
    };

    private static Flag MapToShared(this FlagEntity entity) => new()
    {
        Code = entity.Code,
        Translations = entity.Translations.MapToShared()
    };

    private static Translations MapToShared(this TranslationsEntity entity) => new()
    {
        French = new(entity.French.Code, entity.French.Name, entity.French.OfficialName),
        English = new(entity.English.Code, entity.English.Name, entity.English.OfficialName),
    };
}
