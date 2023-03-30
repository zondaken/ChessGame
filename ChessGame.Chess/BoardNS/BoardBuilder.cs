using ChessGame.Chess.PieceNS;

namespace ChessGame.Chess.BoardNS;

public class BoardBuilder
{
    private BoardSize _size = null!;
    private ETeam _nextTurn = ETeam.White;
    private string _castlingRights = "KQkq";
    private string _enPassantSquare = string.Empty;
    private int _plys = 0;
    private int _nextMoveNo = 1;

    public Board Build()
    {
        return new Board(size: _size)
        {
            NextTurn = _nextTurn,
            CastlingRights = _castlingRights,
            EnPassantSquare = _enPassantSquare,
            HalfMovesSincePawnMoveOrCapture = _plys,
            NextMoveNo = _nextMoveNo
        };
    }

    public void WithSize(BoardSize size) => _size = size;
    public void WithNextTurn(ETeam nextTurn) => _nextTurn = nextTurn;
    public void WithCastlingRights(string castlingRights) => _castlingRights = castlingRights;
    public void WithEnPassantSquare(string? enPassantSquare) => _enPassantSquare = enPassantSquare;
    public void WithPlysSincePawnMoveOrCapture(int plys) => _plys = plys;
    public void WithNextMoveNo(int nextMoveNo) => _nextMoveNo = nextMoveNo;
}
