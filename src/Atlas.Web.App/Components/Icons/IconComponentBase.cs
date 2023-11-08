// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Microsoft.AspNetCore.Components;

namespace Atlas.Web.App.Components.Icons;

public class IconComponentBase : ComponentBase
{
    [Parameter, EditorRequired]
    public required string Name { get; init; }

    [Parameter]
    public IconSize Size { get; init; } = IconSize.Medium;

    [Parameter]
    public string Css { get; set; } = string.Empty;

    protected override void OnParametersSet()
    {
        Css += GetIconSizeCss(Size);

        static string GetIconSizeCss(IconSize size) => size switch
        {
            IconSize.Small => "icon-sm",
            IconSize.Medium => "icon-md",
            IconSize.Large => "icon-lg",
            IconSize.XLarge => "icon-xl",
            IconSize.XXLarge => "icon-xxl",
            _ => string.Empty
        };
    }
}
