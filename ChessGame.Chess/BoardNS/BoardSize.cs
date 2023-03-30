namespace ChessGame.Chess.BoardNS;

public class BoardSize
{
    public int Rows { get; }
    public int Cols { get; }
    public int Count => Rows * Cols;

    public BoardSize(int rows, int cols)
    {
        Rows = rows;
        Cols = cols;
    }
}