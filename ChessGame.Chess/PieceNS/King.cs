using ChessGame.Chess.BoardNS;
using ChessGame.ZFW;

namespace ChessGame.Chess.PieceNS;

public class King : Piece
{
    public Board _board;

    public King(Board board, ETeam team) : base(team)
    {
        _board = board;
    }

    public override char ToChar() => 'K';

    public override void ExecMove(Board board, Move move)
    {
        var from = move.From;
        var to = move.To;

        if (from.Rank == 0) // white king
        {
            board.CastlingRights = board.CastlingRights.RemoveChar('K').RemoveChar('Q');
        }
        else if (from.Rank == board.Size.Rows - 1) // black king
        {
            board.CastlingRights = board.CastlingRights.RemoveChar('k').RemoveChar('q');
        }
    }
}