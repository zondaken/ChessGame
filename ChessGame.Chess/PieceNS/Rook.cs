using ChessGame.Chess.BoardNS;
using ChessGame.ZFW;
using System.Numerics;

namespace ChessGame.Chess.PieceNS;

public class Rook : Piece
{
    #region Fields
    private Board _board;
    #endregion

    #region Constructors
    public Rook(Board board, ETeam team) : base(team)
    {
        _board = board;
    }
    #endregion

    #region Overridden methods
    public override char ToChar() => 'R';

    public override IEnumerable<Move> GetEligibleMoves()
    {
        var moves = new List<Move>();

        var position = this.GetPosition(_board);

        moves.AddRange(GetMovesAbove(position));
        moves.AddRange(GetMovesBelow(position));
        moves.AddRange(GetMovesLeft(position));
        moves.AddRange(GetMovesRight(position));

        return moves;
    }

    public override void ExecMove(Board board, Move move)
    {
        // TODO: make heavy use of observer for whole method

        Coordinate from = move.From;
        var to = move.To;

        if (from.Rank == 0) // white rook
        {
            if (from.File == 0) // white [a] rook (queenside)
            {
                board.CastlingRights = board.CastlingRights.RemoveChar('Q');
            }
            else if (from.File == board.Size.Cols - 1) // white [h] rook (kingside)
            {
                board.CastlingRights = board.CastlingRights.RemoveChar('K');
            }
        }
        else if (from.Rank == board.Size.Rows - 1) // black rook
        {
            if (from.File == 0) // black [a] rook (queenside)
            {
                board.CastlingRights = board.CastlingRights.RemoveChar('q');
            }
            else if (from.File == board.Size.Cols - 1) // black [h] rook (kingside)
            {
                board.CastlingRights = board.CastlingRights.RemoveChar('k');
            }
        }
    }
    #endregion

    #region Private methods
    private IEnumerable<Move> GetMoves(Coordinate position, Vector2 direction)
    {
        var moves = new List<Move>();

        Coordinate i = position.Clone();
        Move move;

        while(true)
        {
            i = i + direction; // increase at the start so we don't move to where we already are
            if (!i.IsValid()) break;

            if (this.CanSimpleMoveTo(_board, position, i, out move))
            {
                moves.Add(move);
            }
            else // if we can't move to [i] => some piece is blocking
            {
                // if blocking piece is ally, we can't move further
                // if blocking piece is enemy, we can capture it but not move further than that
                // that's why we need to [break] in both cases

                if (this.CanCaptureOn(_board, position, i, out move))
                {
                    moves.Add(move);
                }

                break;
            }
        }

        return moves;
    }

    private IEnumerable<Move> GetMovesAbove(Coordinate position) => GetMoves(position, Vector2.UnitY);
    private IEnumerable<Move> GetMovesBelow(Coordinate position) => GetMoves(position, -Vector2.UnitY);
    private IEnumerable<Move> GetMovesLeft(Coordinate position) => GetMoves(position, -Vector2.UnitX);
    private IEnumerable<Move> GetMovesRight(Coordinate position) => GetMoves(position, Vector2.UnitX);
    #endregion
}