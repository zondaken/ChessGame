using ChessGame.Chess.PieceNS;

namespace ChessGame.Chess.PieceFactoryNS;

public interface IPieceBuildStrategy
{
    PieceBase BuildPiece(string text);
}
