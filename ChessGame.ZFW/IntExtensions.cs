using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.ZFW;

public static class IntExtensions
{
    public static bool Between(this int self, int from, int to)
    {
        return self >= from && self < to;
    }
}
