using ChessGame.Chess.BoardNS;

namespace ChessGame.Chess.PieceNS;

public class Bishop : Piece
{
    public Board _board;

    public Bishop(Board board, ETeam team) : base(team)
    {
        _board = board;
    }

    public override char ToChar() => 'B';
}