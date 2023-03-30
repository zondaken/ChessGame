using ChessGame.Chess.BoardNS;

namespace ChessGame.Chess.PieceNS;

public static class PieceExtensions
{
    public static BoardCoordinate GetPosition(this PieceBase self)
    {
        int i = -1;

        foreach (var piece in self.Board)
        {
            i++;

            if (piece == self)
            {
                break;
            }
        }

        if (i < 0)
        {
            throw new Exception("piece not on board");
        }

        return new BoardCoordinate(self.Board, i);
    }

    public static bool CanSimpleMoveTo(this PieceBase self, BoardCoordinate from, BoardCoordinate to, out BoardMove move)
    {
        PieceBase? blockingPiece = self.Board[to];

        if(blockingPiece == null)
        {
            move = new BoardMove(from, to);
            return true;
        }

        move = null!;
        return false;
    }

    public static bool CanCaptureOn(this PieceBase self, BoardCoordinate from, BoardCoordinate to, out BoardMove move)
    {
        PieceBase? blockingPiece = self.Board[to];

        if (blockingPiece == null || blockingPiece.Team == self.Team)
        {
            move = null!;
            return false;
        }

        move = new BoardMove(from, to);
        return true;
    }
}
