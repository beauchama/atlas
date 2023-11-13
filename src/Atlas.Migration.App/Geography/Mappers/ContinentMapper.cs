// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Domain.Geography;

namespace Atlas.Migration.App.Geography.Mappers;

internal static class ContinentMapper
{
    internal static Continent AsDomain(this string region) => region switch
    {
        "Americas" => Continent.America,
        "Europe" => Continent.Europe,
        "Asia" => Continent.Asia,
        "Africa" => Continent.Africa,
        "Oceania" => Continent.Oceania,
        "Antarctic" => Continent.Antarctic,
        _ => Continent.America
    };
}
