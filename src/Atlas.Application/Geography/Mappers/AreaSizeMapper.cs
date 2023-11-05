// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Contracts.Geography;
using AreaSizeDomain = Atlas.Domain.Geography.AreaSize;

namespace Atlas.Application.Geography.Mappers;

internal static class AreaSizeMapper
{
    internal static AreaSize AsContract(this AreaSizeDomain areaSize) => areaSize switch
    {
        AreaSizeDomain.Larger => AreaSize.Larger,
        AreaSizeDomain.Smaller => AreaSize.Smaller,
        AreaSizeDomain.Same => AreaSize.Same,
        _ => AreaSize.Same
    };
}
