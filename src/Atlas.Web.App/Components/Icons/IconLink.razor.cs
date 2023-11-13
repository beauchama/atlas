// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Microsoft.AspNetCore.Components;

namespace Atlas.Web.App.Components.Icons;

public sealed partial class IconLink
{
    [Parameter, EditorRequired]
    public required Uri Uri { get; init; }

    [Parameter]
    public RenderFragment? ChildContent { get; init; }
}
