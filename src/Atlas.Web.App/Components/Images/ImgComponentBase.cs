// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Microsoft.AspNetCore.Components;

namespace Atlas.Web.App.Components.Images;

public class ImgComponentBase : ComponentBase
{
    [Parameter, EditorRequired]
    public required string Source { get; init; }

    [Parameter]
    public string Css { get; set; } = string.Empty;
}
