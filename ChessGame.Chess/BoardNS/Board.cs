using ChessGame.Chess.PieceNS;
using ChessGame.ZFW;
using System.Collections;

namespace ChessGame.Chess.BoardNS;

/*
    a    b    c    d    e    f    g    h
  -----------------------------------------
8 | 56 | 57 | 58 | 59 | 60 | 61 | 62 | 63 | 8
  -----------------------------------------
7 | 48 | 49 | 50 | 51 | 52 | 53 | 54 | 55 | 7
  -----------------------------------------
6 | 40 | 41 | 42 | 43 | 44 | 45 | 46 | 47 | 6
  -----------------------------------------
5 | 32 | 33 | 34 | 35 | 36 | 37 | 38 | 39 | 5
  -----------------------------------------
4 | 24 | 25 | 26 | 27 | 28 | 29 | 30 | 31 | 4
  -----------------------------------------
3 | 16 | 17 | 18 | 19 | 20 | 21 | 22 | 23 | 3
  -----------------------------------------
2 |  8 |  9 | 10 | 11 | 12 | 13 | 14 | 15 | 2
  -----------------------------------------
1 |  0 |  1 |  2 |  3 |  4 |  5 |  6 | 7  | 1
  -----------------------------------------
     a    b    c    d    e    f    g   h
*/

public class Board : IEnumerable<Piece?>
{
    #region Fields
    private Piece?[] _board;
    #endregion

    #region Properties
    public Size Size { get; }

    public ETeam NextTurn { get; set; } = ETeam.White;
    public string CastlingRights { get; set; } = "KQkq";
    public string EnPassantSquare { get; set; } = "-";
    public Piece? EnPassantablePiece { get; set; } = null;
    public int HalfMovesSincePawnMoveOrCapture { get; set; } = 0;
    public int NextMoveNo { get; set; } = 1;

    #region Indexer
    public Piece? this[Coordinate coord]
    {
        get => _board[coord.Index];
        set => _board[coord.Index] = value;
    }

    public Piece? this[int index]
    {
        get => this[new Coordinate(this, index)];
        set => this[new Coordinate(this, index)] = value;
    }

    public Piece? this[int rank, int file]
    {
        get => this[new Coordinate(this, rank, file)];
        set => this[new Coordinate(this, rank, file)] = value;
    }

    public Piece? this[string input]
    {
        get => this[Coordinate.FromString(this, input)];
        set => this[Coordinate.FromString(this, input)] = value;
    }
    #endregion
    #endregion

    #region Constructors
    public Board(Size size)
    {
        Size = size;
        _board = new Piece[size.Count];
    }
    #endregion

    #region Interface implementations
    #region IEnumerable<PieceBase?>
    public IEnumerator<Piece?> GetEnumerator()
    {
        for (int i = 0; i < Size.Count; i++)
        {
            yield return this[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    #endregion

    #endregion

    #region Public methods (interface)
    public void ExecMove(Move move, IEnumerable<Move> possibleMoves)
    {
        var from = move.From;
        var to = move.To;
        var piece = this[move.From];

        if (piece == null) throw new InvalidOperationException("Empty source square.");
        if (piece.Team != NextTurn) throw new InvalidOperationException("Not your turn.");

        if (!possibleMoves.Contains(move)) throw new InvalidOperationException("Invalid move.");

        // NO HUMAN ERROR!

        // TODO: only increase when pawn move or capture
        if (piece is Pawn || this[to] != null)
        {
            HalfMovesSincePawnMoveOrCapture++;
        }

        // general execution
        MovePiece(move);

        // TODO: only works for default chess as of now
        NextTurn = NextTurn == ETeam.White ? ETeam.Black : ETeam.White;

        // TODO: only works for default chess as of now
        NextMoveNo += NextTurn == ETeam.White ? 1 : 0;

        // custom execution
        piece.ExecMove(this, move);
    }
    #endregion

    #region Private methods
    private void MovePiece(Move move)
    {
        var from = move.From;
        var to = move.To;

        if (move.CapturedPiece != null)
        {
            this[move.CapturedPiece.GetPosition(this)] = null;
        }

        this[to] = this[from];
        this[from] = null;
    }
    #endregion
}