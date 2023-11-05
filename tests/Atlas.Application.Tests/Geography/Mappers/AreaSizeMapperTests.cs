// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Contracts.Geography;
using AreaSizeDomain = Atlas.Domain.Geography.AreaSize;

namespace Atlas.Application.Geography.Mappers;

public sealed class AreaSizeMapperTests
{
    [Theory, ClassData(typeof(AreaSizes))]
    public void AsContractShouldConvertDomainToContract(AreaSizeDomain areaSize, AreaSize expectedSize)
    {
        AreaSize size = areaSize.AsContract();

        size.Should().Be(expectedSize);
    }

    internal sealed class AreaSizes : TheoryData<AreaSizeDomain, AreaSize>
    {
        public AreaSizes()
        {
            Add(AreaSizeDomain.Larger, AreaSize.Larger);
            Add(AreaSizeDomain.Same, AreaSize.Same);
            Add(AreaSizeDomain.Smaller, AreaSize.Smaller);
            Add((AreaSizeDomain)999, AreaSize.Same);
        }
    }
}
