using System.Numerics;

namespace ChessGame.Chess.BoardNS;

public class BoardCoordinate : IEquatable<BoardCoordinate>
{
    public int Rank { get; }
    public int File { get; }
    public int Index { get; }

    public Board Board { get; }

    public BoardCoordinate(Board board, int index)
    {
        Board = board;
        Index = index;
        (Rank, File) = (ToRank(index), ToFile(index));
    }

    public BoardCoordinate(Board board, int rank, int file)
    {
        Board = board;
        Rank = rank;
        File = file;
        Index = ToIndex(rank, file);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as BoardCoordinate);
    }

    public override string ToString()
    {
        //return string.Format("[{0}] = ({1};{2})", Index, Rank, File);

        char rank = (char)(Rank + '1');
        char file = (char)(File + 'a');

        return string.Format("{0}{1}", file, rank);
    }

    public bool Equals(BoardCoordinate? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;

        return this.Index == other.Index;
    }

    private int ToFile(int index) => index % Board.Size.Cols;

    private int ToRank(int index) => index / Board.Size.Cols;

    private int ToIndex(int rank, int file) => rank * Board.Size.Cols + file;

    public static BoardCoordinate FromInput(Board board, string input)
    {
        input = input.ToLower();

        int rank = input[1] - '1';
        int file = input[0] - 'a';

        return new BoardCoordinate(board, rank, file);
    }

    public static BoardCoordinate operator +(BoardCoordinate self, Vector2 direction)
    {
        var board = self.Board;
        var rank = self.Rank + (int)direction.Y;
        var file = self.File + (int)direction.X;

        return new BoardCoordinate(board, rank, file);
    }

    public static Vector2 operator -(BoardCoordinate self, BoardCoordinate other)
    {
        var x = self.File - other.File;
        var y = self.Rank - other.Rank;

        return new Vector2(x, y);
    }

    public static bool operator ==(BoardCoordinate coord1, BoardCoordinate coord2)
    {
        return coord1.Equals(coord2);
    }

    public static bool operator !=(BoardCoordinate coord1, BoardCoordinate coord2)
    {
        return !(coord1 == coord2);
    }
}
