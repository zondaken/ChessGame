using ChessGame.Chess.BoardNS;
using ChessGame.Chess.PieceFactoryNS;
using ChessGame.Chess.PieceNS;

namespace ChessGame.Chess.BoardFactoryNS;

public class FenNotationStrategy : INotationStrategy
{
    #region Fields
    private Board _board = null!;
    private PieceFactory _factory = null!;
    #endregion

    #region Constructors
    public FenNotationStrategy()
    {
        
    }
    #endregion

    #region Interface implementations
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

        var size = new Size(rows: 8, cols: 8);

        var builder = new BoardBuilder();
        builder.WithSize(size);

        // implement 2.)
        // TODO: only works for default chess as of now
        builder.WithNextTurn(nextTurn == "w" ? ETeam.White : ETeam.Black);

        // implement 3.) 4.) 5.) 6.)
        builder.WithCastlingRights(castlingRights);
        builder.WithEnPassantSquare(enPassantSquare);
        builder.WithPlysSincePawnMoveOrCapture(int.Parse(plysSinceCapture));
        builder.WithNextMoveNo(int.Parse(nextMoveNo));

        // implement 1.)
        string[] ranks = pieces.Split('/');

        _board = builder.Build();
        _factory = new PieceFactory(new FenPieceBuildingStrategy(_board));

        HandleRanks(ranks);

        // add to 4.)
        if (_board.EnPassantSquare != "-")
        {
            // TODO: make "four-sided" chess possible by adding .IsValidSquare(Coordinate) to Board
            // TODO: only works for default chess, as of now
            var coord = Coordinate.FromString(_board, _board.EnPassantSquare);

            if (coord.Rank == 2) // white en-passantable
                coord.Rank += 1;
            else if (coord.Rank == 5) // black en-passantable
                coord.Rank -= 1;

            var piece = _board[coord];

            _board.EnPassantablePiece = piece;
        }

        // return board
        return _board;
    }
    #endregion

    #region Private methods
    private void HandleRanks(string[] ranks)
    {
        for (int rank = 0; rank < ranks.Length; rank++)
        {
            string rankDesc = ranks[_board.Size.Rows - rank - 1];
            HandleRank(rank, rankDesc);
        }
    }

    private void HandleRank(int rank, string rankDesc)
    {
        int fileNo = 0;

        foreach (char pieceDesc in rankDesc)
        {
            if (int.TryParse(pieceDesc.ToString(), out int emptySquares))
            {
                fileNo += emptySquares;
            }
            else
            {
                var coord = new Coordinate(_board, rank, fileNo);
                var piece = _factory.Build(pieceDesc.ToString());

                _board[coord] = piece;
                fileNo++;
            }
        }
    }
    #endregion
}
