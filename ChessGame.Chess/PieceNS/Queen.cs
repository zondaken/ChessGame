using ChessGame.Chess.BoardNS;

namespace ChessGame.Chess.PieceNS;

public class Queen : Piece
{
    public Board _board;

    public Queen(Board board, ETeam team) : base(team)
    {
        _board = board;
    }

    public override char ToChar() => 'Q';
}