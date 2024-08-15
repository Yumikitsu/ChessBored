using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static chess.console.Piece;

namespace chess.console
{
    public class Bishop : Piece
    {
        public Bishop(bool isBlack, Position startPos)
            : base((int)Types.BISHOP, isBlack, startPos)
        {

        }
        //Move
        public override bool MoveLogic(int x, int y)
        {
            //Return true if Bishop can move to that position, otherwise return false

            //Check if the new positions are legal
            if (x >= 1 && y >= 1 && x <= 8 && y <= 8)
            {
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
