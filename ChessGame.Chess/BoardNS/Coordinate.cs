using ChessGame.ZFW;
using System.Numerics;
using System.Text.RegularExpressions;

namespace ChessGame.Chess.BoardNS;

public class Coordinate : IEquatable<Coordinate>
{
    #region Fields
    private Board _board;
    private int _rank, _file, _index;
    #endregion

    #region Properties
    public int Rank
    {
        get => _rank;

        set
        {
            _rank = value;
            _index = ToIndex(Rank, File);
        }
    }

    public int File
    {
        get => _file;

        set
        {
            _file = value;
            _index = ToIndex(Rank, File);
        }
    }

    public int Index 
    {
        get => _index;
        
        set
        {
            _index = value;
            _rank = ToRank(value);
            _file = ToFile(value);
        }
    }
    #endregion

    #region Constructors
    public Coordinate(Board board, int index)
    {
        _board = board;
        Index = index;
    }

    public Coordinate(Board board, int rank, int file)
    {
        _board = board;
        _rank = rank;
        _file = file;
        _index = ToIndex(rank, file);
    }
    #endregion

    #region Interface implementations
    public bool Equals(Coordinate? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;

        return this.Index == other.Index;
    }
    #endregion

    #region Overriden methods
    public override bool Equals(object? obj) => Equals(obj as Coordinate);

    public override int GetHashCode() => Index;

    public override string ToString()
    {
        //return string.Format("[{0}] = ({1};{2})", Index, Rank, File);

        char rank = (char)(Rank + '1');
        char file = (char)(File + 'a');

        return string.Format("{0}{1}", file, rank);
    }
    #endregion

    #region Public methods (interface)
    public Coordinate Clone()
    {
        return new Coordinate(_board, this.Index);
    }

    public bool IsValid()
    {
        var size = _board.Size;

        return this.Rank.Between(0, size.Rows) && this.File.Between(0, size.Cols)
            && this.Index.Between(0, size.Rows * size.Cols);
    }
    #endregion

    #region Operators
    public static Coordinate operator +(Coordinate self, Vector2 direction)
    {
        var board = self._board;
        var rank = self.Rank + (int)direction.Y;
        var file = self.File + (int)direction.X;

        return new Coordinate(board, rank, file);
    }

    public static Vector2 operator -(Coordinate self, Coordinate other)
    {
        var x = self.File - other.File;
        var y = self.Rank - other.Rank;

        return new Vector2(x, y);
    }

    public static bool operator ==(Coordinate coord1, Coordinate coord2)
    {
        return coord1.Equals(coord2);
    }

    public static bool operator !=(Coordinate coord1, Coordinate coord2)
    {
        return !(coord1 == coord2);
    }
    #endregion

    #region Public static methods
    public static Coordinate FromString(Board board, string input)
    {
        // TODO: only works for default chess as of now
        Regex longNotation = new Regex("^([a-h])([0-9])$");
        Match match;

        if ((match = longNotation.Match(input)).Success)
        {
            input = input.ToLower();

            int rank = match.Groups[2].Value[0] - '1';
            int file = match.Groups[1].Value[0] - 'a';

            return new Coordinate(board, rank, file);
        }

        throw new ArgumentException("Invalid notation");
    }
    #endregion

    #region Private methods
    private int ToFile(int index) => index % _board.Size.Cols;

    private int ToRank(int index) => index / _board.Size.Cols;

    private int ToIndex(int rank, int file) => rank * _board.Size.Cols + file;
    #endregion
}

public static class CoordinateExtensions
{
    
}