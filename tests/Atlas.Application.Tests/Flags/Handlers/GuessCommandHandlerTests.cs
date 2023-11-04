// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Application.Fakes;
using Atlas.Application.Flags.Abstractions;
using Atlas.Contracts.Flags;
using NSubstitute.ReceivedExtensions;

namespace Atlas.Application.Flags.Handlers;

public sealed class GuessCommandHandlerTests
{
    private readonly FlagRequests.Guess _request = new("can", "ita");

    private readonly IFlagRepository _flagRepository = Substitute.For<IFlagRepository>();
    private readonly IFlagGuesser _flagGuesser = Substitute.For<IFlagGuesser>();

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
        await _handler.Handle(_request, CancellationToken.None);

        await _flagRepository.Received(Quantity.Exactly(1)).GetAsync(_request.FlagCode, CancellationToken.None);
    }

    [Fact]
    public async Task HandleShouldGetTheGuessedFlag()
    {
        await _handler.Handle(_request, CancellationToken.None);

        await _flagRepository.Received(Quantity.Exactly(1)).GetAsync(_request.GuessedFlagCode, CancellationToken.None);
    }

    [Fact]
    public async Task HandleShouldReturnTheGuessedFlag()
    {
        GuessedFlag guessedFlag = await _handler.Handle(_request, CancellationToken.None);

        guessedFlag.Code.Should().Be(FakeFlag.GuessedCanadaFlag.Code);
    }
}
