using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static chess.console.Piece;

namespace chess.console
{
    public class Queen : Piece
    {
        public Queen(bool isBlack, Position startPos)
            : base((int)Types.QUEEN, isBlack, startPos)
        {

        }
        //Move
        public override bool MoveLogic(int x, int y)
        {
            //Return true if Queen can move to that position, otherwise return false

            //Check if the new positions are legal
            if (x >= 1 && y >= 1 && x <= 8 && y <= 8)
            {
                //Check if the move is entirely vertical
                if (Math.Abs(pos.x - x) == 0 && Math.Abs(pos.y - y) > 0)
                {
                    return true;
                }

                //Check if the move is entirely horizontal
                if (Math.Abs(pos.x - x) > 0 && Math.Abs(pos.y - y) == 0)
                {
                    return true;
                }

                //Check if the move is diagonal
                if (Math.Abs(pos.x - x) > 0 && Math.Abs(pos.y - y) > 0 && Math.Abs(pos.x - x) == Math.Abs(pos.y - y))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
