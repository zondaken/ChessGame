using ChessGame.Chess.BoardNS;
using ChessGame.Chess.PieceNS;
using ChessGame.ZFW;

namespace ChessGame.MVC.ViewNS;

public class BoardDrawer
{
    private Board _board;

    public BoardDrawer(Board board)
    {
        _board = board;
    }

    private static IDictionary<ETeam, ConsoleColor> _teamColorMap = new Dictionary<ETeam, ConsoleColor>
    {
        [ETeam.White] = ConsoleColor.Green,
        [ETeam.Black] = ConsoleColor.Red
    };

    public void Draw()
    {
        var size = _board.Size;
        
        DrawFileCaptions();

        Console.Write("   ");
        Console.WriteLine(new string('-', 1 + size.Cols * 4));

        for (int row = size.Rows - 1; row >= 0; row--)
        {
            Console.Write(" {0} ", row + 1);

            for (int col = 0; col < size.Cols; col++)
            {
                Piece? piece = _board[row, col];

                Console.Write("| ");
                DrawPiece(piece);
                Console.Write(" ");
            }

            Console.Write("|");
            Console.Write(" {0} ", row + 1);
            Console.WriteLine();
            Console.Write("   ");
            Console.WriteLine(new string('-', 1 + size.Cols * 4));
        }

        DrawFileCaptions();
    }

    private void DrawPiece(Piece? piece)
    {
        if(piece == null)
        {
            Console.Write(piece?.ToChar() ?? ' ');
            return;
        }

        var oldColor = Console.ForegroundColor;
        var newColor = _teamColorMap.GetValue(piece.Team, oldColor);
        Console.ForegroundColor = newColor;

        Console.Write(piece?.ToChar() ?? ' ');

        Console.ForegroundColor = oldColor;
    }

    private void DrawFileCaptions()
    {
        Console.Write("  ");
        
        for(int i = 0; i < _board.Size.Cols; i++)
        {
            Console.Write("   {0}", (char)(i + 'a'));
        }

        Console.WriteLine();
    }
}
