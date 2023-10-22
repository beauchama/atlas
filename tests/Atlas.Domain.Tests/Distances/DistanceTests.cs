// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

namespace Atlas.Domain.Distances;

public class DistanceTests
{
    private readonly GeographicCoordinate _canadaCoordinate = new(60.0, -95.0);
    private readonly GeographicCoordinate _italyCoordinate = new(42.83333333, 12.83333333);

    [Theory]
    [InlineData(6843.3, DistanceUnit.Kilometers)]
    [InlineData(4252.2, DistanceUnit.Miles)]
    public void DistanceShouldCalculateBetweenTwoCoordinates(double expectedDistance, DistanceUnit unit)
    {
        Distance distance = Distance.Calculate(_canadaCoordinate, _italyCoordinate, unit);

        distance.Length.Should().BeApproximately(expectedDistance, 0.5);
        distance.Unit.Should().Be(unit);
    }
}
