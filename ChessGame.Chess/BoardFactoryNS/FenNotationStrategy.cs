using ChessGame.Chess.BoardNS;
using ChessGame.Chess.PieceFactoryNS;
using ChessGame.Chess.PieceNS;

namespace ChessGame.Chess.BoardFactoryNS;

public class FenNotationStrategy : INotationStrategy
{
    private Board _board = null!;
    private BoardBuilder _builder = null!;
    private PieceFactory _factory = null!;

    private int _rankNo;
    private string _rank = null!;

    public FenNotationStrategy()
    {
        var size = new BoardSize(rows: 8, cols: 8);

        _builder = new BoardBuilder();
        _builder.WithSize(size);
    }

    public Board BuildBoard(string fen)
    {
        // format:
        // 1. pieces
        // 2. whose turn (b/w)
        // 3. castling rights
        // 4. possible en passant
        // 5. played half moves since last pawn move or capture
        // 6. next move number

        // pieces:
        // 1. empty squares
        // 2. pieces (rnbqk)

        // castling rights:
        // K: white can castle kingside
        // Q: white can castle queenside
        // k: black  can castle kingside
        // q: black can castle queenside
        // -: no castles anymore

        // en passant:
        // - when pawn moved 2 squares, note down the "skipped" square
        // - example: when f2-f4 -> write f3
        // - no e.p.: write -

        // played half moves since last pawn move or capture:
        // - because of [50 moves draw rule]
        // - zero or even number

        // move number:
        // - starts with 1

        string[] split = fen.Split(' ');

        string pieces = split[0];
        string nextTurn = split[1];
        string castlingRights = split[2];
        string enPassantSquare = split[3];
        string plysSinceCapture = split[4];
        string nextMoveNo = split[5];

        // implement 2.)
        // TODO: only works for default chess as of now
        _builder.WithNextTurn(nextTurn == "w" ? ETeam.White : ETeam.Black);

        // implement 3.) 4.) 5.) 6.)
        _builder.WithCastlingRights(castlingRights);
        _builder.WithEnPassantSquare(enPassantSquare);
        _builder.WithPlysSincePawnMoveOrCapture(int.Parse(plysSinceCapture));
        _builder.WithNextMoveNo(int.Parse(nextMoveNo));

        // implement 1.)
        string[] ranks = pieces.Split('/');
        
        _board = _builder.Build();
        _factory = new PieceFactory(new FenPieceBuildStrategy(_board));
        
        HandleRanks(ranks);

        // return board
        return _board;
    }

    private void HandleRanks(string[] ranks)
    {
        for (_rankNo = 0; _rankNo < ranks.Length; _rankNo++)
        {
            _rank = ranks[_rankNo];
            HandleRank();
        }
    }

    private void HandleRank()
    {
        int fileNo = 0;

        foreach (char c in _rank)
        {
            if (int.TryParse(c.ToString(), out int emptySquares))
            {
                fileNo += emptySquares;
            }
            else
            {
                var reverseRankNo = _board.Size.Rows - _rankNo - 1; // reverse row order
                var file = fileNo;
                var coordinate = new BoardCoordinate(_board, reverseRankNo, file);

                var piece = _factory.Build(c.ToString());

                _board[coordinate] = piece;
                fileNo++;
            }
        }
    }
}
