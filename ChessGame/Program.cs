using ChessGame.Chess.BoardFactoryNS;
using ChessGame.MVC;
using ChessGame.MVC.ViewNS;

var fenDefault = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
var fenItalian = "r1bqkbnr/pppp1ppp/2n5/4p3/2B1P3/5N2/PPPP1PPP/RNBQK2R b KQkq - 3 3";
var fenTest = "8/8/8/8/3p4/3p3/4P3/8 w - - 0 1";

var fenChosen = fenTest;

var boardBuilder = new BoardFactory(new FenNotationStrategy());
var board = boardBuilder.Build(fenTest);

var model = new Model
{
    FEN = fenChosen,
    Board = board
};

var view = new View(model);
var controller = new Controller(model, view);

controller.Run();