// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Application.Geography.Mappers;
using Atlas.Application.Translations.Mappers;
using Atlas.Contracts.Flags;
using GuessedFlagDomain = Atlas.Domain.Flags.GuessedFlag;

namespace Atlas.Application.Flags.Mappers;

internal static class GuessedFlagMapper
{
    internal static GuessedFlag AsContract(this GuessedFlagDomain guessedFlag) => new()
    {
        Code = guessedFlag.Code,
        Translations = guessedFlag.Translations.AsContract(),
        IsSuccess = guessedFlag.IsSuccess,
        Size = guessedFlag.Size.AsContract(),
        Kilometers = guessedFlag.Distance.Kilometers,
        Miles = guessedFlag.Distance.Miles,
        Direction = guessedFlag.Direction,
        SameContinent = guessedFlag.SameContinent
    };
}
