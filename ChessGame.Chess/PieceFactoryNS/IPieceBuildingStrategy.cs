using ChessGame.Chess.PieceNS;

namespace ChessGame.Chess.PieceFactoryNS;

public interface IPieceBuildingStrategy
{
    Piece Build(string text);
}

public interface I1 { string BuildString(); }
public interface I2 { int BuildInt(); }
public interface I3 { float BuildFloat(); }