// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Contracts.Translations;
using TranslationEntity = Atlas.Domain.Translations.Translation;

namespace Atlas.Application.Translations.Mappers;

public sealed class TranslationMapperTests
{
    [Fact]
    public void AsDomainShouldConvertDtoToDomain()
    {
        TranslationEntity entity = new("fra", "Canada", "Canada");

        IEnumerable<Translation> translations = new[] { entity }.AsContract();

        Translation translation = translations.Single();

        translation.Code.Should().Be(entity.Code);
        translation.Name.Should().Be(entity.Name);
        translation.OfficialName.Should().Be(entity.OfficialName);
    }
}
