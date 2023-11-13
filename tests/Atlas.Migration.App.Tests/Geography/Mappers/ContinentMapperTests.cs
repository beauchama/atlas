// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Domain.Geography;

namespace Atlas.Migration.App.Geography.Mappers;

public sealed class ContinentMapperTests
{
    [Theory, ClassData(typeof(Continents))]
    public void AsDomainShouldConvertDtoToDomain(string region, Continent expectedContinent)
    {
        Continent continent = region.AsDomain();

        continent.Should().Be(expectedContinent);
    }

    internal sealed class Continents : TheoryData<string, Continent>
    {
        public Continents()
        {
            Add("Americas", Continent.America);
            Add("Europe", Continent.Europe);
            Add("Asia", Continent.Asia);
            Add("Africa", Continent.Africa);
            Add("Oceania", Continent.Oceania);
            Add("Antarctic", Continent.Antarctic);
            Add("Region", Continent.America);
        }
    }
}
