// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Domain.Translations;
using Atlas.Migration.App.Translations.Dto;

namespace Atlas.Migration.App.Translations.Mappers;

public sealed class TranslationDtoMapperTests
{
    [Fact]
    public void AsDomainShouldConvertDtoToDomain()
    {
        TranslationDto dto = new("fra", "Canada", "Canada");
        NameDto name = new("Canada", "Canada");

        IEnumerable<Translation> translations = new[] { dto }.AsDomain(name);

        translations.Should().Contain(t => t.Code == "fra");
        translations.Should().Contain(t => t.Code == "eng");
    }
}
