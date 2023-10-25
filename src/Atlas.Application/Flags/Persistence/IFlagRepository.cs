// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Domain.Flags;

namespace Atlas.Application.Flags.Persistence;

public interface IFlagRepository
{
    Task<Flag> GetAsync(string code);

    Task<IEnumerable<Flag>> GetAllAsync();
}
