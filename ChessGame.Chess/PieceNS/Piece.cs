using ChessGame.Chess.BoardNS;

namespace ChessGame.Chess.PieceNS;

public abstract class Piece
{
    // TODO: implement coordinate property (needs to be manager by board)
    
    public virtual ETeam Team { get; }

    public Piece(ETeam team)
    {
        Team = team;
    }

    public virtual void ExecMove(Board board, Move move) { }

    // TODO: make abstract later
    public virtual IEnumerable<Move> GetEligibleMoves() { return new List<Move>(); }
    public virtual 
    
    public virtual char ToChar() => '?';

    public override string ToString() => Team.ToString() + "|" + ToChar().ToString();
}

public static class PieceExtensions
{
    public static Coordinate GetPosition(this Piece self, Board board)
    {
        int i = -1;

        foreach (var piece in board)
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

        return new Coordinate(board, i);
    }

    public static bool CanSimpleMoveTo(this Piece self, Board board, Coordinate from, Coordinate to, out Move move)
    {
        if (from == to)
        {
            move = new Move(board, from, to, capturedPiece: null);
            return true;
        }

        Piece? blockingPiece = board[to];

        if (blockingPiece == null)
        {
            move = new Move(board, from, to);
            return true;
        }

        move = null!;
        return false;
    }

    public static bool CanCaptureOn(this Piece self, Board board, Coordinate from, Coordinate to, bool captureEmpty, out Move move)
    {
        if (from == to)
        {
            move = null!;
            return false;
        }

        Piece? blockingPiece = board[to];

        if ((!captureEmpty && blockingPiece == null) || (blockingPiece?.Team == self.Team))
        {
            move = null!;
            return false;
        }

        move = new Move(board, from, to, blockingPiece);
        return true;
    }

    public static bool CanCaptureOn(this Piece self, Board board, Coordinate from, Coordinate to, out Move move)
    {
        return self.CanCaptureOn(board, from, to, false, out move);
    }
}
