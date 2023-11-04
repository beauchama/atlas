// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Application.Flags.Abstractions;
using Fluxor;
using NSubstitute.ReceivedExtensions;

namespace Atlas.Application.Flags.Handlers;

public sealed class GetAllQueryHandlerTests
{
    private readonly IFlagRepository _flagRepository = Substitute.For<IFlagRepository>();
    private readonly IDispatcher _dispatcher = Substitute.For<IDispatcher>();

    private readonly GetAllQueryHandler _handler;

    public GetAllQueryHandlerTests() => _handler = new GetAllQueryHandler(_flagRepository);

    [Fact]
    public async Task HandleShouldRetrieveAllFlags()
    {
        await _handler.HandleAsync(_dispatcher);

        await _flagRepository.Received(Quantity.Exactly(1)).GetAllAsync(CancellationToken.None);
    }

    [Fact]
    public async Task HandleShouldDispatchTheResponse()
    {
        await _handler.HandleAsync(_dispatcher);

        _dispatcher.Received(Quantity.Exactly(1)).Dispatch(Arg.Any<FlagActions.GetAllResponse>());
    }
}
