using ChessGame.Chess.BoardFactoryNS;
using ChessGame.MVC;
using ChessGame.MVC.ViewNS;

var fen = new Dictionary<string, string>
{
    ["default"] = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1",
    ["italian"] = "r1bqkbnr/pppp1ppp/2n5/4p3/2B1P3/5N2/PPPP1PPP/RNBQK2R b KQkq - 3 3",
    ["enPassant"] = "rnbqkbnr/ppp2ppp/8/3Pp3/8/8/PPPP1PPP/RNBQKBNR w KQkq e6 0 1",
    ["custom"] = "rnbqkbnr/pppppppp/8/8/1P1R3P/8/3P4/4K3 w kq - 0 1"
};

var model = new Model
{
    FEN = fen["custom"],
};

var view = new View(model);
var controller = new Controller(model, view);

controller.Run();