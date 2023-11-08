// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Microsoft.AspNetCore.Components;

namespace Atlas.Web.App.Components.Images;

public sealed partial class Thumbnail
{
    [Parameter]
    public ThumbnailSize Size { get; init; } = ThumbnailSize.Medium;

    private string SizeCss => Size switch
    {
        ThumbnailSize.Small => "thumbnail-sm",
        ThumbnailSize.Medium => "thumbnail-md",
        ThumbnailSize.Large => "thumbnail-lg",
        _ => string.Empty
    };
}
