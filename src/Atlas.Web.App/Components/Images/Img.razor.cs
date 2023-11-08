// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Microsoft.AspNetCore.Components;

namespace Atlas.Web.App.Components.Images;

public sealed partial class Img
{
    [Parameter]
    public ImgSize Size { get; init; } = ImgSize.Medium;

    private string SizeCss => Size switch
    {
        ImgSize.Small => "img-sm",
        ImgSize.Medium => "img-md",
        ImgSize.Large => "img-lg",
        _ => string.Empty
    };
}
