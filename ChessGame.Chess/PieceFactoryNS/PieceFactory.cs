using ChessGame.Chess.PieceNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Chess.PieceFactoryNS;

public class PieceFactory
{
    private IPieceBuildingStrategy _strategy;

    public PieceFactory(IPieceBuildingStrategy strategy)
    {
        _strategy = strategy;
    }

    public Piece Build(string text)
    {
        return _strategy.Build(text);
    }
}
