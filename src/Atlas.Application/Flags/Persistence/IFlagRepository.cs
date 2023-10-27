// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Domain.Flags;

namespace Atlas.Application.Flags.Persistence;

public interface IFlagRepository
{
    Task<IEnumerable<Flag>> GetAllAsync();

    Task<Flag> GetAsync(string code);
}
