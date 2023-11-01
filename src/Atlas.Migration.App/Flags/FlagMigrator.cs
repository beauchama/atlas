// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Domain.Flags;
using Atlas.Infrastructure;
using Atlas.Infrastructure.Flags;
using Atlas.Migration.App.Flags.Dto;
using Atlas.Migration.App.Flags.Mappers;
using Atlas.Migration.App.Utilities;

namespace Atlas.Migration.App.Flags;

internal sealed class FlagMigrator(IFlagRetriever flagRetriever, IJsonFileWriter<IEnumerable<Flag>> jsonFileWriter) : IMigrator
{
    public string Filename => JsonPaths.Flags;

    public async Task MigrateAsync(string path, CancellationToken cancellationToken = default)
    {
        IEnumerable<FlagDto> flags = await flagRetriever.GetAllAsync(cancellationToken).ConfigureAwait(false);

        await jsonFileWriter.WriteToAsync(path, flags.MapToDomain(), FlagJsonContext.Default.IEnumerableFlag, cancellationToken).ConfigureAwait(false);
    }
}
