using ChessGame.Chess.BoardNS;

namespace ChessGame.Chess.PieceNS;

public class Knight : Piece
{
    public Board _board;

    public Knight(Board board, ETeam team) : base(team)
    {
        _board = board;
    }

    public override char ToChar() => 'N';
}