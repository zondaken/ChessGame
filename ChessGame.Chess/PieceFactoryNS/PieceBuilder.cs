using ChessGame.Chess.PieceNS;

namespace ChessGame.Chess.PieceFactoryNS;

public class PieceFactory
{
    private IPieceBuildStrategy _strategy;

    public PieceFactory(FenPieceBuildStrategy strategy)
    {
        _strategy = strategy;
    }

    public PieceBase Build(string text)
    {
        return _strategy.BuildPiece(text);
    }
}
