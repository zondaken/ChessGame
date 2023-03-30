using ChessGame.Chess.BoardNS;

namespace ChessGame.Chess.PieceNS;

public class QueenPiece : PieceBase
{
    public override Board Board { get; }
    public override ETeam Team { get; }

    public QueenPiece(Board board, ETeam team)
    {
        Board = board;
        Team = team;
    }

    public override char ToChar() => 'Q';
}