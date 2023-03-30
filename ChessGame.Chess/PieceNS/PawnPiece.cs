using ChessGame.Chess.BoardNS;
using ChessGame.ZFW;
using System.Numerics;

namespace ChessGame.Chess.PieceNS;

public class PawnPiece : PieceBase
{
    public override Board Board { get; }
    public override ETeam Team { get; }

    public bool HasMoved
    {
        get
        {
            BoardCoordinate coord = this.GetPosition();

            var whiteHasMoved = Team == ETeam.White && coord.Rank == 1;
            var blackHasMoved = Team == ETeam.Black && coord.Rank == (Board.Size.Rows - 1 - 1);
            
            var isSecondRank = whiteHasMoved || blackHasMoved;

            var hasMoved = !isSecondRank;

            return hasMoved;
        }
    }

    public Vector2 MoveDirection { get; }
    public Vector2 CaptureDirection { get; }

    public PawnPiece(Board board, ETeam team)
    {
        Board = board;
        Team = team;

        // TODO: as of now, only working for default chess
        MoveDirection = team == ETeam.White ? new Vector2(0, 1) : new Vector2(0, -1);
        CaptureDirection = team == ETeam.White ? new Vector2(1, 1) : new Vector2(1, -1);
    }

    public override char ToChar() => 'P';

    public override IEnumerable<BoardMove> GetPossibleMoves()
    {
        List<BoardMove> moves = new();
        BoardMove move;

        BoardCoordinate from, to;
        from = this.GetPosition();

        #region Check 1 forward
        to = from + MoveDirection;

        if (this.CanSimpleMoveTo(from, to, out move))
        {
            moves.Add(move);
        }
        #endregion

        #region Check 2 forward
        to = from + (MoveDirection * 2);

        if (this.CanSimpleMoveTo(from, to, out move))
        {
            if (!HasMoved && moves.Count > 0)
            {
                moves.Add(move);
            }
        }
        #endregion

        #region Check capture right
        to = from + CaptureDirection;

        if (this.CanCaptureOn(from, to, out move))
        {
            moves.Add(move);
        }
        #endregion

        #region Check capture left
        to = from + CaptureDirection.Rotate(90);

        if (this.CanCaptureOn(from, to, out move))
        {
            moves.Add(move);
        }
        #endregion

        #region Check en-passant
        if (Board.EnPassantSquare != "-")
        {
            to = BoardCoordinate.FromInput(Board, Board.EnPassantSquare);

            move = new BoardMove(from, to);
            moves.Add(move);
        }
        #endregion

        return moves;
    }
}