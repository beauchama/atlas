// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using System.Reflection;

namespace Atlas.Migration.App.Extensions;

internal static class AttributeExtensions
{
    internal static TAttribute? GetAttribute<TAttribute>(this Type type, string propertyName) where TAttribute : Attribute
        => type.GetProperty(propertyName)?.GetCustomAttribute<TAttribute>();
}
