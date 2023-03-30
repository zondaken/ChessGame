using ChessGame.Chess.BoardNS;

namespace ChessGame.Chess.PieceNS;

public class BishopPiece : PieceBase
{
    public override Board Board { get; }
    public override ETeam Team { get; }

    public BishopPiece(Board board, ETeam team)
    {
        Board = board;
        Team = team;
    }

    public override char ToChar() => 'B';
}