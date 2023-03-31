using ChessGame.Chess.BoardNS;

namespace ChessGame.MVC.ViewNS;

public class View
{
    private Model _model;

    public event EventHandler<string>? InputSubmitted;
    public event EventHandler? ResetCalled;

    public View(Model model)
    {
        _model = model;
    }

    public bool Render()
    {
        Console.Clear();

        Board board = _model.Board;
        
        var drawer = new BoardDrawer(board);
        drawer.Draw();
        Console.WriteLine();

        Console.WriteLine("Turn: {0}", board.NextTurn);
        Console.WriteLine("Castling rights: {0}", board.CastlingRights);
        Console.WriteLine("En-passant: {0}", board.EnPassantSquare);
        Console.WriteLine("Plys: {0}", board.HalfMovesSincePawnMoveOrCapture);
        Console.WriteLine("Next move no.: {0}", board.NextMoveNo);
        Console.WriteLine();

        if (_model.LastError.Length > 0)
        {
            Console.WriteLine("Error: {0}", _model.LastError);
            Console.WriteLine();
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine();
        }

        Console.Write("Your input: ");
        string input = Console.ReadLine() ?? throw new Exception("Input error");

        if (input == "q") return false;
        if (input == "r") ResetCalled?.Invoke(this, EventArgs.Empty);
        else InputSubmitted?.Invoke(this, input);

        return true;
    }
}
