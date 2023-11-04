// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

namespace Atlas.Domain.Geography;

public sealed class AreaTests
{
    [Theory, ClassData(typeof(AreaSizes))]
    public void CompareToShouldReturnTheGoodAreaSize(double left, double right, AreaSize expectedSize)
    {
        AreaSize size = new Area(left).CompareTo(new Area(right));

        size.Should().Be(expectedSize);
    }

    [Fact]
    public void ImplicitConversionShouldReturnTheArea()
    {
        double area = new Area(42.0);

        area.Should().Be(42.0);
    }
}

file sealed class AreaSizes : TheoryData<double, double, AreaSize>
{
    public AreaSizes()
    {
        Add(0.0, 0.0, AreaSize.Same);
        Add(0.0, 1.0, AreaSize.Smaller);
        Add(1.0, 0.0, AreaSize.Larger);
    }
}
