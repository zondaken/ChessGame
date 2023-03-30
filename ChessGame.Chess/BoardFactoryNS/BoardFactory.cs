using ChessGame.Chess.BoardNS;

namespace ChessGame.Chess.BoardFactoryNS;

public class BoardFactory
{
    private INotationStrategy _strategy;

    public BoardFactory(INotationStrategy strategy)
    {
        _strategy = strategy;
    }

    public Board Build(string text)
    {
        return _strategy.BuildBoard(text);
    }
}
