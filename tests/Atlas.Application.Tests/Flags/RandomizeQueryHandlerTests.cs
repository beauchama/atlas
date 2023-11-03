// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Application.Fakes;
using Atlas.Application.Flags.Persistence;
using Atlas.Application.Utilities;
using Atlas.Domain.Flags;
using Fluxor;
using NSubstitute.ReceivedExtensions;

namespace Atlas.Application.Flags;

public sealed class RandomizeQueryHandlerTests
{
    private readonly IFlagRepository _flagRepository = Substitute.For<IFlagRepository>();
    private readonly IRandomizer _randomizer = Substitute.For<IRandomizer>();
    private readonly IDispatcher _dispatcher = Substitute.For<IDispatcher>();

    private readonly RandomizeQueryHandler _handler;

    public RandomizeQueryHandlerTests()
    {
        _randomizer.Randomize(Arg.Any<IEnumerable<Flag>>()).Returns(FakeFlag.CanadaFlag);

        _handler = new RandomizeQueryHandler(_flagRepository, _randomizer);
    }

    [Fact]
    public async Task HandleShouldRetrieveAllFlags()
    {
        await _handler.HandleAsync(_dispatcher);

        await _flagRepository.Received(Quantity.Exactly(1)).GetAllAsync();
    }

    [Fact]
    public async Task HandleShouldRandomizeTheSource()
    {
        Flag[] flags = [];

        _flagRepository.GetAllAsync().Returns(flags);

        await _handler.HandleAsync(_dispatcher);

        _randomizer.Received(Quantity.Exactly(1)).Randomize(flags);
    }

    [Fact]
    public async Task HandleShouldDispatchTheRandomizedFlag()
    {
        Flag flag = FakeFlag.ItalyFlag;
        Flag[] flags = [flag];

        _flagRepository.GetAllAsync().Returns(flags);
        _randomizer.Randomize(flags).Returns(flag);

        await _handler.HandleAsync(_dispatcher);

        _dispatcher.Received(Quantity.Exactly(1)).Dispatch(Arg.Is<FlagActions.RandomizeResponse>(f => f.Code == flag.Code));
    }
}
