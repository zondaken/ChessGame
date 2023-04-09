using ChessGame.Chess.BoardFactoryNS;
using ChessGame.Chess.BoardNS;
using ChessGame.MVC.ViewNS;

namespace ChessGame.MVC;

public class Controller
{
    private Model _model;
    private View _view;

    public Controller(Model model, View view)
    {
        _model = model;
        _view = view;

        _view.ResetCalled += ResetCalled;
        _view.InputSubmitted += InputSubmitted;
    }

    public void Run()
    {
        ResetCalled(this, EventArgs.Empty);

        while (_view.Render()) ;
    }

    private void InputSubmitted(object? sender, string input)
    {
        if (!(sender is View view)) return;

        Move move;

        try
        {
            move = Move.FromString(_model.Board, input, out var possibleMoves);
            _model.Board.ExecMove(move, possibleMoves);
            _model.LastError = string.Empty;
        }
        catch(Exception ex)
        {
            _model.LastError = ex.Message;
        }
    }

    private void ResetCalled(object? sender, EventArgs e)
    {
        var boardBuilder = new BoardFactory(new FenNotationStrategy());
        var board = boardBuilder.Build(_model.FEN);

        _model.Board = board;
    }
}
 