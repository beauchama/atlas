// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using FlagEntity = Atlas.Domain.Flags.Flag;
using FlagShared = Atlas.Web.Shared.Flags.Flag;

namespace Atlas.Application.Flags;

internal static class FlagsMapper
{
    internal static IEnumerable<FlagShared> MapToShared(this IEnumerable<FlagEntity> entities)
        => entities.Select(MapToShared).ToArray();

    private static FlagShared MapToShared(this FlagEntity entity) => new()
    {
        Code = entity.Code,
        Translations = new()
        {
            French = new(entity.Translations.French.Code, entity.Translations.French.Name, entity.Translations.French.OfficialName),
            English = new(entity.Translations.English.Code, entity.Translations.English.Name, entity.Translations.English.OfficialName)
        }
    };
}
