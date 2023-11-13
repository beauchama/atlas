// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Application.Fakes;
using Atlas.Application.Flags.Abstractions;
using Atlas.Application.Utilities;
using Atlas.Domain.Flags;
using NSubstitute.ReceivedExtensions;

namespace Atlas.Application.Flags.Handlers;

public sealed class RandomizeQueryHandlerTests
{
    private readonly IFlagRepository _flagRepository = Substitute.For<IFlagRepository>();
    private readonly IRandomizer _randomizer = Substitute.For<IRandomizer>();

    private readonly RandomizeQueryHandler _handler;

    public RandomizeQueryHandlerTests()
    {
        _randomizer.Randomize(Arg.Any<IEnumerable<Flag>>()).Returns(FakeFlag.CanadaFlag);

        _handler = new RandomizeQueryHandler(_flagRepository, _randomizer);
    }

    [Fact]
    public async Task HandleShouldRetrieveAllFlags()
    {
        await _handler.Handle(new FlagRequests.Randomize(), CancellationToken.None);

        await _flagRepository.Received(Quantity.Exactly(1)).GetAllAsync(CancellationToken.None);
    }

    [Fact]
    public async Task HandleShouldRandomizeAllFlags()
    {
        Flag[] flags = [];

        _flagRepository.GetAllAsync(CancellationToken.None).Returns(flags);

        await _handler.Handle(new FlagRequests.Randomize(), CancellationToken.None);

        _randomizer.Received(Quantity.Exactly(1)).Randomize(flags);
    }

    [Fact]
    public async Task HandleShouldDispatchTheRandomizedFlag()
    {
        Flag flag = FakeFlag.ItalyFlag;
        Flag[] flags = [flag];

        _flagRepository.GetAllAsync(CancellationToken.None).Returns(flags);
        _randomizer.Randomize(flags).Returns(flag);

        string code = await _handler.Handle(new FlagRequests.Randomize(), CancellationToken.None);

        code.Should().Be(flag.Code);
    }
}
