// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Application.Translations.Mappers;
using Atlas.Contracts.Flags;
using FlagDomain = Atlas.Domain.Flags.Flag;

namespace Atlas.Application.Flags.Mappers;

internal static class FlagMapper
{
    internal static IEnumerable<Flag> AsContract(this IEnumerable<FlagDomain> flags)
    {
        return flags.Select(AsContract).ToArray();

        static Flag AsContract(FlagDomain flag) => new()
        {
            Code = flag.Code,
            Translations = flag.Translations.AsContract()
        };
    }
}
