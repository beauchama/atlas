// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Contracts.Translations;
using TranslationDomain = Atlas.Domain.Translations.Translation;

namespace Atlas.Application.Translations.Mappers;

internal static class TranslationMapper
{
    internal static IEnumerable<Translation> AsContract(this IEnumerable<TranslationDomain> translations)
    {
        return translations.Select(AsContract).ToArray();

        static Translation AsContract(TranslationDomain translation)
            => new(translation.Code, translation.Name, translation.OfficialName);
    }
}
