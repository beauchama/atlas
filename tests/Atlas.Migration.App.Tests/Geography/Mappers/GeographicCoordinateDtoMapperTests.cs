// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Domain.Geography;
using Atlas.Migration.App.Geography.Dto;

namespace Atlas.Migration.App.Geography.Mappers;

public sealed class GeographicCoordinateDtoMapperTests
{
    [Fact]
    public void AsDomainConvertDtoToDomain()
    {
        GeographicCoordinateDto dto = new(42.0, 42.0);

        GeographicCoordinate coordinate = dto.AsDomain();

        coordinate.Latitude.Should().Be(dto.Latitude);
        coordinate.Longitude.Should().Be(dto.Longitude);
    }
}
