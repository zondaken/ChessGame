using ChessGame.Chess.PieceNS;
using System.Diagnostics;

namespace ChessGame.Chess.BoardNS;

public class BoardMove : IEquatable<BoardMove>
{
    private Board _board;

    public BoardCoordinate From { get; }
    public BoardCoordinate To { get; }

    public BoardMove(BoardCoordinate from, BoardCoordinate to)
    {
        Debug.Assert(from.Board == to.Board);

        From = from;
        To = to;

        _board = From.Board;
    }

    public BoardMove(Board board, string from, string to)
    {
        _board = board;
        From = BoardCoordinate.FromInput(board, from);
        To = BoardCoordinate.FromInput(board, to);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as BoardMove);
    }

    public override string ToString()
    {
        PieceBase? piece = _board[From];

        return string.Format("{0}{1}-{2}", piece is PawnPiece ? "" : (piece?.ToChar().ToString() ?? ""), From, To);
    }

    public bool Equals(BoardMove? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;

        return this.From == other.From && this.To == other.To;
    }

    public static BoardMove FromInput(Board board, string text)
    {
        // format: e2-e4

        string[] split = text.Split('-');
        string from = split[0];
        string to = split[1];

        var c1 = BoardCoordinate.FromInput(board, from);
        var c2 = BoardCoordinate.FromInput(board, to);

        return new BoardMove(c1, c2);
    }

    public static bool operator ==(BoardMove coord1, BoardMove coord2)
    {
        return coord1.Equals(coord2);
    }

    public static bool operator !=(BoardMove coord1, BoardMove coord2)
    {
        return !(coord1 == coord2);
    }
}
