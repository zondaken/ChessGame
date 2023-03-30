using ChessGame.Chess.BoardNS;

namespace ChessGame.MVC;

public class Model
{
    public string FEN { get; set; } = null!;
    public Board Board { get; set; } = null!;
    public string LastError { get; set; } = string.Empty;
}
