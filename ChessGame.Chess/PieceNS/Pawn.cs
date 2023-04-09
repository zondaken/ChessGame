using ChessGame.Chess.BoardNS;
using ChessGame.ZFW;
using System.Numerics;

namespace ChessGame.Chess.PieceNS;

public class Pawn : Piece
{
    #region Fields
    private Board _board;
    #endregion

    #region Properties

    public bool HasMoved
    {
        get
        {
            Coordinate coord = this.GetPosition(_board);

            var whiteHasMoved = Team == ETeam.White && coord.Rank == 1;
            var blackHasMoved = Team == ETeam.Black && coord.Rank == (_board.Size.Rows - 1 - 1);

            var isSecondRank = whiteHasMoved || blackHasMoved;

            var hasMoved = !isSecondRank;

            return hasMoved;
        }
    }

    public Vector2 MoveDirection { get; }

    public Vector2 CaptureDirectionLeft { get; }

    public Vector2 CaptureDirectionRight { get; }
    #endregion

    #region Constructors
    public Pawn(Board board, ETeam team) : base(team)
    {
        _board = board;

        // TODO: as of now, only working for default chess
        MoveDirection = team == ETeam.White ? new Vector2(0, 1) : new Vector2(0, -1);
        CaptureDirectionLeft = team == ETeam.White ? new Vector2(-1, 1) : new Vector2(1, -1);
        CaptureDirectionRight = team == ETeam.White ? new Vector2(1, 1) : new Vector2(-1, -1);
    }
    #endregion

    #region Overridden methods
    public override char ToChar() => 'P';

    public override IEnumerable<Move> GetEligibleMoves()
    {
        Coordinate from;
        Move move;

        var moves = new List<Move>();

        from = this.GetPosition(_board);

        if (CanMoveOneForward(from, out move)) moves.Add(move);
        if (CanMoveTwoForward(from, out move)) moves.Add(move);

        if (CanCaptureRight(from, out move)) moves.Add(move);
        if (CanCaptureLeft(from, out move)) moves.Add(move);

        if (CanEnPassant(from, out move)) moves.Add(move);

        return moves;
    }

    public override void ExecMove(Board board, Move move)
    {
        var from = move.From;
        var to = move.To;

        if ((to - from) == MoveDirection * 2)
        {
            // TODO: observer pattern
            board.EnPassantSquare = (from + MoveDirection).ToString();
            board.EnPassantablePiece = this;
        }
        else
        {
            // TODO: observer pattern
            board.EnPassantSquare = "-";
            board.EnPassantablePiece = null;
        }
    }
    #endregion

    #region Private methods
    private bool CanMoveOneForward(Coordinate from, out Move move)
    {
        Coordinate to = from + MoveDirection;

        return this.CanSimpleMoveTo(_board, from, to, out move);
    }

    private bool CanMoveTwoForward(Coordinate from, out Move move)
    {
        Coordinate to = from + (MoveDirection * 2);

        if (this.CanSimpleMoveTo(_board, from, to, out move))
        {
            return !HasMoved && CanMoveOneForward(from, out _);
        }

        return false;
    }

    private bool CanCaptureRight(Coordinate from, out Move move)
    {
        Coordinate to = from + CaptureDirectionRight;

        return this.CanCaptureOn(_board, from, to, out move);
    }

    private bool CanCaptureLeft(Coordinate from, out Move move)
    {
        Coordinate to = from + CaptureDirectionLeft;

        return this.CanCaptureOn(_board, from, to, out move);
    }

    private bool CanEnPassant(Coordinate from, out Move move)
    {
        if (_board.EnPassantSquare != "-")
        {
            var to = Coordinate.FromString(_board, _board.EnPassantSquare);

            if(this.CanCaptureOn(_board, from, to, captureEmpty: true, out move))
            {
                move.CapturedPiece = _board.EnPassantablePiece;
                //move = move.FindSimilarMove(_board);
                return true;
            }
        }

        move = null!;
        return false;
    }
    #endregion
}