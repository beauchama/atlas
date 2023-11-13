// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Domain.Flags;

namespace Atlas.Application.Flags.Abstractions;

public interface IFlagRepository
{
    Task<IEnumerable<Flag>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<Flag> GetAsync(string code, CancellationToken cancellationToken = default);
}
