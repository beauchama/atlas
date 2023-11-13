// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Microsoft.AspNetCore.Components;

namespace Atlas.Web.App.Components.Icons;

public sealed partial class IconButton
{
    [Parameter, EditorRequired]
    public EventCallback OnClick { get; init; }

    [Parameter]
    public RenderFragment? ChildContent { get; init; }
}
