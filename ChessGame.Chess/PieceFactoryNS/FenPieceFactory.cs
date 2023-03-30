using ChessGame.Chess.BoardNS;
using ChessGame.Chess.PieceNS;

namespace ChessGame.Chess.PieceFactoryNS;

public class FenPieceBuildStrategy : IPieceBuildStrategy
{
    private Board _board;

    public FenPieceBuildStrategy(Board board)
    {
        _board = board;
    }

    public PieceBase BuildPiece(string fen)
    {
        switch (fen[0])
        {
            case 'P': return new PawnPiece(_board, ETeam.White);
            case 'R': return new RookPiece(_board, ETeam.White);
            case 'N': return new KnightPiece(_board, ETeam.White);
            case 'B': return new BishopPiece(_board, ETeam.White);
            case 'Q': return new QueenPiece(_board, ETeam.White);
            case 'K': return new KingPiece(_board, ETeam.White);

            case 'p': return new PawnPiece(_board, ETeam.Black);
            case 'r': return new RookPiece(_board, ETeam.Black);
            case 'n': return new KnightPiece(_board, ETeam.Black);
            case 'b': return new BishopPiece(_board, ETeam.Black);
            case 'q': return new QueenPiece(_board, ETeam.Black);
            case 'k': return new KingPiece(_board, ETeam.Black);

            default: throw new ArgumentException("piece not found");
        }
    }
}
