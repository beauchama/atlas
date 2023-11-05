// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Application.Flags.Abstractions;
using Atlas.Domain.Flags;
using Atlas.Domain.Geography;
using Atlas.Domain.Translations;
using Microsoft.Extensions.Caching.Memory;
using NSubstitute.ReceivedExtensions;

namespace Atlas.Infrastructure.Flags.Persistence;

public sealed class CacheFlagRepositoryTests
{
    private readonly IMemoryCache _memoryCache = Substitute.For<IMemoryCache>();
    private readonly IFlagRepository _flagRepository = Substitute.For<IFlagRepository>();
    private readonly Flag _canadaFlag = new()
    {
        Code = "can",
        Translations = [new Translation("fra", "Canada", "Canada"), new Translation("en", "Canada", "Canada")],
        Continent = Continent.America,
        Coordinate = new GeographicCoordinate(60, -95),
        Area = new Area(9984670)
    };

    private readonly CachedFlagRepository _repository;

    public CacheFlagRepositoryTests() => _repository = new CachedFlagRepository(_flagRepository, _memoryCache);

    [Fact]
    public async Task GetAllAsyncShouldUseRepositoryWhenDataIsNotCached()
    {
        _memoryCache.TryGetValue(Arg.Any<string>(), out _).Returns(returnThis: false);

        _ = await _repository.GetAllAsync();

        await _flagRepository.Received(Quantity.Exactly(1)).GetAllAsync();
    }

    [Fact]
    public async Task GetAllASyncShouldCreateAnEntryIntoTheCacheWhenDataIsNotCached()
    {
        _memoryCache.TryGetValue(Arg.Any<string>(), out _).Returns(returnThis: false);

        _ = await _repository.GetAllAsync();

        _memoryCache.Received(Quantity.Exactly(1)).CreateEntry(Arg.Any<string>());
    }

    [Fact]
    public async Task GetAllAsyncShouldReturnAllFlagsFromRepositoryWhenDataIsNotCached()
    {
        IEnumerable<Flag> expectedFlags = [_canadaFlag];

        _flagRepository.GetAllAsync().Returns(expectedFlags);
        _memoryCache.TryGetValue(Arg.Any<string>(), out _).Returns(returnThis: false);

        IEnumerable<Flag> flags = await _repository.GetAllAsync();

        flags.Should().BeEquivalentTo(expectedFlags);
    }

    [Fact]
    public async Task GetAllAsyncShouldUseMemoryCacheWhenDataIsCached()
    {
        object? flags = new Flag[] { _canadaFlag };

        _memoryCache.TryGetValue(Arg.Any<string>(), out Arg.Any<object?>()).Returns(x =>
        {
            x[1] = flags;
            return true;
        });

        _ = await _repository.GetAllAsync();

        await _flagRepository.Received(Quantity.None()).GetAllAsync();
        _memoryCache.Received(Quantity.None()).CreateEntry(Arg.Any<string>());
    }

    [Fact]
    public async Task GetAllAsyncShouldReturnAllFlagsFromMemoryCacheWhenDataIsCached()
    {
        IEnumerable<Flag> cachedFlags = new Flag[] { _canadaFlag };

        _memoryCache.TryGetValue(Arg.Any<string>(), out Arg.Any<object?>()).Returns(x =>
        {
            x[1] = cachedFlags;
            return true;
        });

        IEnumerable<Flag> flags = await _repository.GetAllAsync();

        flags.Should().BeEquivalentTo(cachedFlags);
    }

    [Fact]
    public async Task GetAsyncShouldUseRepositoryWhenDataIsNotCached()
    {
        _memoryCache.TryGetValue(_canadaFlag.Code, out _).Returns(returnThis: false);

        _ = await _repository.GetAsync(_canadaFlag.Code);

        await _flagRepository.Received(Quantity.Exactly(1)).GetAsync(_canadaFlag.Code);
    }

    [Fact]
    public async Task GetASyncShouldCreateAnEntryIntoTheCacheWhenDataIsNotCached()
    {
        _memoryCache.TryGetValue(_canadaFlag.Code, out _).Returns(returnThis: false);

        _ = await _repository.GetAsync(_canadaFlag.Code);

        _memoryCache.Received(Quantity.Exactly(1)).CreateEntry(_canadaFlag.Code);
    }

    [Fact]
    public async Task GetAsyncShouldReturnTheFlagFromRepositoryWhenDataIsNotCached()
    {
        _flagRepository.GetAsync(_canadaFlag.Code).Returns(_canadaFlag);
        _memoryCache.TryGetValue(_canadaFlag.Code, out _).Returns(returnThis: false);

        Flag flag = await _repository.GetAsync(_canadaFlag.Code);

        flag.Should().Be(_canadaFlag);
    }

    [Fact]
    public async Task GetAsyncShouldUseMemoryCacheWhenDataIsCached()
    {
        _memoryCache.TryGetValue(_canadaFlag.Code, out Arg.Any<object?>()).Returns(x =>
        {
            x[1] = _canadaFlag;
            return true;
        });

        _ = await _repository.GetAsync(_canadaFlag.Code);

        await _flagRepository.Received(Quantity.None()).GetAsync(_canadaFlag.Code);
        _memoryCache.Received(Quantity.None()).CreateEntry(_canadaFlag.Code);
    }

    [Fact]
    public async Task GetAsyncShouldReturnTheFlagFromMemoryCacheWhenDataIsCached()
    {
        _memoryCache.TryGetValue(_canadaFlag.Code, out Arg.Any<object?>()).Returns(x =>
        {
            x[1] = _canadaFlag;
            return true;
        });

        Flag flag = await _repository.GetAsync(_canadaFlag.Code);

        flag.Should().BeEquivalentTo(_canadaFlag);
    }
}
