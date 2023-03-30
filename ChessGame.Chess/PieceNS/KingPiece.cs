using ChessGame.Chess.BoardNS;

namespace ChessGame.Chess.PieceNS;

public class KingPiece : PieceBase
{
    public override Board Board { get; }
    public override ETeam Team { get; }

    public KingPiece(Board board, ETeam team)
    {
        Board = board;
        Team = team;
    }

    public override char ToChar() => 'K';

    public override IEnumerable<BoardMove> GetPossibleMoves()
    {
        var moves = new List<BoardMove>();

        moves.Add(new(Board, "e1", "e2"));

        return moves;
    }
}