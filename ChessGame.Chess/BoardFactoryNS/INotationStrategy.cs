using ChessGame.Chess.BoardNS;

namespace ChessGame.Chess.BoardFactoryNS;

public interface INotationStrategy
{
    Board BuildBoard(string text);
}
