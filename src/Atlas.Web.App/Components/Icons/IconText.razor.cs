// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Microsoft.AspNetCore.Components;

namespace Atlas.Web.App.Components.Icons;

public sealed partial class IconText
{
    [Parameter, EditorRequired]
    public required RenderFragment ChildContent { get; init; }
}
