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

public class Board : IEnumerable<PieceBase?>
{
    #region Fields
    private PieceBase?[] _board;
    #endregion

    #region Properties
    public BoardSize Size { get; }

    public ETeam NextTurn { get; set; } = ETeam.White;
    
    public string CastlingRights { get; set; } = "KQkq";

    public string EnPassantSquare { get; set; } = "-";
    
    public int HalfMovesSincePawnMoveOrCapture { get; set; } = 0;
    
    public int NextMoveNo { get; set; } = 1;

    #region Indexer
    public PieceBase? this[BoardCoordinate coord]
    {
        get => _board[coord.Index];
        set => _board[coord.Index] = value;
    }

    public PieceBase? this[int index]
    {
        get => this[new BoardCoordinate(this, index)];
        set => this[new BoardCoordinate(this, index)] = value;
    }

    public PieceBase? this[int rank, int file]
    {
        get => this[new BoardCoordinate(this, rank, file)];
        set => this[new BoardCoordinate(this, rank, file)] = value;
    }

    public PieceBase? this[string input]
    {
        get => this[BoardCoordinate.FromInput(this, input)];
        set => this[BoardCoordinate.FromInput(this, input)] = value;
    }
    #endregion
    #endregion

    #region Constructors
    public Board(BoardSize size)
    {
        Size = size;
        _board = new PieceBase[size.Count];
    }
    #endregion

    #region IEnumerable<PieceBase?>
    public IEnumerator<PieceBase?> GetEnumerator()
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

    #region Methods
    public void ExecMove(BoardMove move)
    {
        var from = move.From;
        var to = move.To;
        var piece = this[from];

        if (piece == null) throw new InvalidOperationException("Empty source square.");
        if (piece.Team != NextTurn) throw new InvalidOperationException("Not your turn.");

        var possibleMoves = piece.GetPossibleMoves();

        if (!possibleMoves.Contains(move)) throw new InvalidOperationException("Invalid move.");

        // no human error!!!

        this[to] = this[from];
        this[from] = null;

        if (piece is PawnPiece pawn && (to - from) == pawn.MoveDirection * 2)
        {
            EnPassantSquare = (from + pawn.MoveDirection).ToString();
        }
        else
        {
            EnPassantSquare = "-";
        }

        // handle ability to castle
        // TODO: Chess960 problems
        if (piece is RookPiece)
        {
            if (from.Rank == 0) // white rook
            {
                if(from.File == 0) // white [a] rook (queenside)
                {
                    CastlingRights = CastlingRights.RemoveChar('Q');
                }
                else if(from.File == Size.Cols - 1) // white [h] rook (kingside)
                {
                    CastlingRights = CastlingRights.RemoveChar('K');
                }
            }
            else if(from.Rank == Size.Rows - 1) // black rook
            {
                if (from.File == 0) // black [a] rook (queenside)
                {
                    CastlingRights = CastlingRights.RemoveChar('q');
                }
                else if (from.File == Size.Cols - 1) // black [h] rook (kingside)
                {
                    CastlingRights = CastlingRights.RemoveChar('k');
                }
            }
        }
        else if(piece is KingPiece)
        {
            if (from.Rank == 0) // white king
            {
                CastlingRights = CastlingRights.RemoveChar('K').RemoveChar('Q');
            }
            else if(from.Rank == Size.Rows - 1) // black king
            {
                CastlingRights = CastlingRights.RemoveChar('k').RemoveChar('q');
            }
        }

        // TODO: only increase when pawn move or capture
        if (piece is PawnPiece || this[to] != null)
        {
            HalfMovesSincePawnMoveOrCapture++;
        }

        // TODO: only works for default chess as of now
        NextTurn = NextTurn == ETeam.White ? ETeam.Black : ETeam.White;

        // TODO: only works for default chess as of now
        NextMoveNo += NextTurn == ETeam.White ? 1 : 0;
    }
    #endregion
}