// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Application.Fakes;
using Atlas.Application.Flags.Abstractions;
using Atlas.Contracts.Flags;
using NSubstitute.ReceivedExtensions;

namespace Atlas.Application.Flags.Handlers;

public sealed class GetAllQueryHandlerTests
{
    private readonly IFlagRepository _flagRepository = Substitute.For<IFlagRepository>();

    private readonly GetAllQueryHandler _handler;

    public GetAllQueryHandlerTests() => _handler = new GetAllQueryHandler(_flagRepository);

    [Fact]
    public async Task HandleShouldRetrieveAllFlags()
    {
        await _handler.Handle(new FlagRequests.GetAll(), CancellationToken.None);

        await _flagRepository.Received(Quantity.Exactly(1)).GetAllAsync(CancellationToken.None);
    }

    [Fact]
    public async Task HandleShouldReturnAllFlags()
    {
        _flagRepository.GetAllAsync(CancellationToken.None).Returns([FakeFlag.ItalyFlag]);

        IEnumerable<Flag> flags = await _handler.Handle(new FlagRequests.GetAll(), CancellationToken.None);

        flags.Should().ContainSingle();

        Flag flag = flags.First();
        flag.Code.Should().Be(FakeFlag.ItalyFlag.Code);
    }
}
