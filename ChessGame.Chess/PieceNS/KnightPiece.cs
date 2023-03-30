using ChessGame.Chess.BoardNS;

namespace ChessGame.Chess.PieceNS;

public class KnightPiece : PieceBase
{
    public override Board Board { get; }
    public override ETeam Team { get; }

    public KnightPiece(Board board, ETeam team)
    {
        Board = board;
        Team = team;
    }

    public override char ToChar() => 'N';
}