using ChessGame.Chess.BoardNS;

namespace ChessGame.Chess.PieceNS;

public class RookPiece : PieceBase
{
    public override Board Board { get; }
    public override ETeam Team { get; }

    public RookPiece(Board board, ETeam team)
    {
        Board = board;
        Team = team;
    }

    public override char ToChar() => 'R';

    public override IEnumerable<BoardMove> GetPossibleMoves()
    {
        return base.GetPossibleMoves();
    }
}