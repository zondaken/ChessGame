using ChessGame.Chess.BoardNS;

namespace ChessGame.Chess.PieceNS;

public abstract class PieceBase
{
    public abstract ETeam Team { get; }
    public abstract Board Board { get; }

    // TODO: make abstract later
    public virtual IEnumerable<BoardMove> GetPossibleMoves() { throw new NotImplementedException(); }
    public abstract char ToChar();

    public override string ToString() => Team.ToString() + "|" + ToChar().ToString();
}
