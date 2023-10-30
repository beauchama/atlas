// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Migration.App.Flags.Dto;

namespace Atlas.Migration.App.Flags;

internal interface IFlagRetriever
{
    Task<IEnumerable<FlagDto>> GetAllAsync(CancellationToken cancellationToken = default);
}
