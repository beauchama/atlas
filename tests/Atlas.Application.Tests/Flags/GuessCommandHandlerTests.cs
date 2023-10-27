// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Application.Fakes;
using Atlas.Application.Flags.Persistence;
using Atlas.Application.Services;
using Fluxor;
using NSubstitute.ReceivedExtensions;

namespace Atlas.Application.Flags;

public class GuessCommandHandlerTests
{
    private readonly FlagActions.GuessRequest _request = new("can", "ita");

    private readonly IFlagRepository _flagRepository = Substitute.For<IFlagRepository>();
    private readonly IFlagGuesser _flagGuesser = Substitute.For<IFlagGuesser>();
    private readonly IDispatcher _dispatcher = Substitute.For<IDispatcher>();

    private readonly GuessCommandHandler _handler;

    public GuessCommandHandlerTests()
    {
        _flagRepository.GetAsync(_request.FlagCode).Returns(FakeFlag.ItalyFlag);
        _flagRepository.GetAsync(_request.GuessedFlagCode).Returns(FakeFlag.CanadaFlag);
        _flagGuesser.Guess(FakeFlag.ItalyFlag, FakeFlag.CanadaFlag).Returns(FakeFlag.GuessedCanadaFlag);

        _handler = new GuessCommandHandler(_flagRepository, _flagGuesser);
    }

    [Fact]
    public async Task HandleShouldGetTheFlagToGuess()
    {
        await _handler.HandleAsync(_request, _dispatcher);

        await _flagRepository.Received(Quantity.Exactly(1)).GetAsync(_request.FlagCode);
    }

    [Fact]
    public async Task HandleShouldGetTheGuessedFlag()
    {
        await _handler.HandleAsync(_request, _dispatcher);

        await _flagRepository.Received(Quantity.Exactly(1)).GetAsync(_request.GuessedFlagCode);
    }

    [Fact]
    public async Task HandleShouldDispatchTheGuessedFlag()
    {
        await _handler.HandleAsync(_request, _dispatcher);

        _dispatcher.Received(Quantity.Exactly(1))
            .Dispatch(Arg.Is<FlagActions.GuessResponse>(f => f.GuessedFlag.Code == FakeFlag.GuessedCanadaFlag.Code));
    }
}
