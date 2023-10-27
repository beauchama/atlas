// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Application.Fakes;
using Atlas.Domain.Flags;
using Microsoft.Extensions.Caching.Memory;
using NSubstitute.ReceivedExtensions;

namespace Atlas.Application.Flags.Persistence;

public class CacheFlagRepositoryTests
{
    private readonly IMemoryCache _memoryCache = Substitute.For<IMemoryCache>();
    private readonly IFlagRepository _flagRepository = Substitute.For<IFlagRepository>();

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
        IEnumerable<Flag> expectedFlags = [FakeFlag.CanadaFlag];

        _flagRepository.GetAllAsync().Returns(expectedFlags);
        _memoryCache.TryGetValue(Arg.Any<string>(), out _).Returns(returnThis: false);

        IEnumerable<Flag> flags = await _repository.GetAllAsync();

        flags.Should().BeEquivalentTo(expectedFlags);
    }

    [Fact]
    public async Task GetAllAsyncShouldUseMemoryCacheWhenDataIsCached()
    {
        object? flags = new Flag[] { FakeFlag.CanadaFlag };

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
        IEnumerable<Flag> cachedFlags = new Flag[] { FakeFlag.CanadaFlag };

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
        _memoryCache.TryGetValue(FakeFlag.CanadaFlag.Code, out _).Returns(returnThis: false);

        _ = await _repository.GetAsync(FakeFlag.CanadaFlag.Code);

        await _flagRepository.Received(Quantity.Exactly(1)).GetAsync(FakeFlag.CanadaFlag.Code);
    }

    [Fact]
    public async Task GetASyncShouldCreateAnEntryIntoTheCacheWhenDataIsNotCached()
    {
        _memoryCache.TryGetValue(FakeFlag.CanadaFlag.Code, out _).Returns(returnThis: false);

        _ = await _repository.GetAsync(FakeFlag.CanadaFlag.Code);

        _memoryCache.Received(Quantity.Exactly(1)).CreateEntry(FakeFlag.CanadaFlag.Code);
    }

    [Fact]
    public async Task GetAsyncShouldReturnTheFlagFromRepositoryWhenDataIsNotCached()
    {
        _flagRepository.GetAsync(FakeFlag.CanadaFlag.Code).Returns(FakeFlag.CanadaFlag);
        _memoryCache.TryGetValue(FakeFlag.CanadaFlag.Code, out _).Returns(returnThis: false);

        Flag flag = await _repository.GetAsync(FakeFlag.CanadaFlag.Code);

        flag.Should().Be(FakeFlag.CanadaFlag);
    }

    [Fact]
    public async Task GetAsyncShouldUseMemoryCacheWhenDataIsCached()
    {
        _memoryCache.TryGetValue(FakeFlag.CanadaFlag.Code, out Arg.Any<object?>()).Returns(x =>
        {
            x[1] = FakeFlag.CanadaFlag;
            return true;
        });

        _ = await _repository.GetAsync(FakeFlag.CanadaFlag.Code);

        await _flagRepository.Received(Quantity.None()).GetAsync(FakeFlag.CanadaFlag.Code);
        _memoryCache.Received(Quantity.None()).CreateEntry(FakeFlag.CanadaFlag.Code);
    }

    [Fact]
    public async Task GetAsyncShouldReturnTheFlagFromMemoryCacheWhenDataIsCached()
    {
        _memoryCache.TryGetValue(FakeFlag.CanadaFlag.Code, out Arg.Any<object?>()).Returns(x =>
        {
            x[1] = FakeFlag.CanadaFlag;
            return true;
        });

        Flag flag = await _repository.GetAsync(FakeFlag.CanadaFlag.Code);

        flag.Should().BeEquivalentTo(FakeFlag.CanadaFlag);
    }
}
