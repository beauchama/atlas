// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Domain.Translations;
using Atlas.Migration.App.Translations.Dto;

namespace Atlas.Migration.App.Translations.Mappers;

internal static class TranslationDtoMapper
{
    internal static IEnumerable<Translation> AsDomain(this IEnumerable<TranslationDto> translations, NameDto name)
    {
        return [.. translations.Select(AsDomain), AsTranslation(name)];

        static Translation AsDomain(TranslationDto translation)
            => new(translation.Code, translation.Common, translation.Official);

        static Translation AsTranslation(NameDto name)
            => new("eng", name.Common, name.Official);
    }
}
