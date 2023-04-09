using ChessGame.Chess.BoardNS;
using ChessGame.Chess.PieceNS;

namespace ChessGame.Chess.PieceFactoryNS;

public class FenPieceBuildingStrategy : IPieceBuildingStrategy
{
    private Board _board;

    public FenPieceBuildingStrategy(Board board)
    {
        _board = board;
    }

    public Piece Build(string fen)
    {
        switch (fen[0])
        {
            case 'P': return new Pawn(_board, ETeam.White);
            case 'R': return new Rook(_board, ETeam.White);
            case 'N': return new Knight(_board, ETeam.White);
            case 'B': return new Bishop(_board, ETeam.White);
            case 'Q': return new Queen(_board, ETeam.White);
            case 'K': return new King(_board, ETeam.White);

            case 'p': return new Pawn(_board, ETeam.Black);
            case 'r': return new Rook(_board, ETeam.Black);
            case 'n': return new Knight(_board, ETeam.Black);
            case 'b': return new Bishop(_board, ETeam.Black);
            case 'q': return new Queen(_board, ETeam.Black);
            case 'k': return new King(_board, ETeam.Black);

            default: throw new ArgumentException("piece not found");
        }
    }
}
