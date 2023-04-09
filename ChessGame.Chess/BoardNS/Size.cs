namespace ChessGame.Chess.BoardNS;

public class Size
{
    public int Rows { get; }
    public int Cols { get; }
    public int Count => Rows * Cols;

    public Size(int rows, int cols)
    {
        Rows = rows;
        Cols = cols;
    }
}