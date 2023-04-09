using ChessGame.Chess.PieceNS;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace ChessGame.Chess.BoardNS;

public class Move : IEquatable<Move>
{
    #region Fields
    private Board _board;
    #endregion

    #region Properties
    public Coordinate From { get; set; }
    public Coordinate To { get; set; }
    public Piece? CapturedPiece { get; set; }
    #endregion

    #region Constructors
    public Move(Board board, Coordinate from, Coordinate to, Piece? capturedPiece = null)
    {
        _board = board;

        From = from;
        To = to;
        CapturedPiece = capturedPiece;
    }

    public Move(Board board, string from, string to)
    {
        _board = board;
        From = Coordinate.FromString(board, from);
        To = Coordinate.FromString(board, to);
    }
    #endregion

    #region Interface implementations
    public bool Equals(Move? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;

        return this.From == other.From && this.To == other.To;
    }
    #endregion

    #region Overriden methods
    public override bool Equals(object? obj)
    {
        Move move = (Move)obj!;
        return Equals(move);
    }

    public override int GetHashCode() => From.GetHashCode() + To.GetHashCode();

    public override string ToString()
    {
        Piece? piece = _board[From];

        return string.Format("{0}{1}-{2}", piece is Pawn ? "" : (piece?.ToChar().ToString() ?? ""), From, To);
    }
    #endregion

    #region Operators
    public static bool operator ==(Move coord1, Move coord2)
    {
        return coord1.Equals(coord2);
    }

    public static bool operator !=(Move coord1, Move coord2)
    {
        return !(coord1 == coord2);
    }
    #endregion

    #region Public static methods
    public static Move FromString(Board board, string text, out IEnumerable<Move> possibleMoves)
    {
        // TODO: only works for default chess as of now
        var regex = new Regex("^([a-h][0-9])\\-([a-h][0-9])$");
        var match = regex.Match(text);
        if (!match.Success) throw new ArgumentException("Wrong format");

        // format: e2-e4

        string[] split = text.Split('-');
        string from = match.Groups[1].Value;
        string to = match.Groups[2].Value;

        var c1 = Coordinate.FromString(board, from);
        var c2 = Coordinate.FromString(board, to);

        Move move = new Move(board, c1, c2);
        move = move.FindSimilarMove(board, out possibleMoves);
        return move;
    }
    #endregion
}

public static class MoveExtensions
{
    // in some cases (like en passant) a pure coordinate-to-coordinate move is not enough
    // so we need to find a similar move that also stores the captured move
    public static Move FindSimilarMove(this Move self, Board board, out IEnumerable<Move> possibleMoves)
    {
        Piece? piece = board[self.From];

        if (piece == null)
        {
            possibleMoves = new List<Move>(new[] { self });
            return self;
        }

        possibleMoves = piece.GetEligibleMoves();
        return possibleMoves.FirstOrDefault(m => m.From == self.From && m.To == self.To, self);
    }
}