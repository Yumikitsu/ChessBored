using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static chess.console.Piece;

namespace chess.console
{
    public class Knight : Piece
    {
        public Knight(bool isBlack, Position startPos, bool firstMove = true)
            : base((int)Types.KNIGHT, isBlack, startPos, firstMove)
        {

        }
        //Move
        public override bool MoveLogic(int x, int y)
        {
            //Return true if Knight can move to that position, otherwise return false

            //Check if the new positions are legal
            if (x >= 1 && y >= 1 && x <= 8 && y <= 8)
            {
                //Check if horizontal move is 2 and vertical move is 1
                if(Math.Abs(pos.x - x) == 2 && Math.Abs(pos.y - y) == 1)
                {
                    return true;
                }

                //Check if horizontal move is 1 and vertical move is 2
                if(Math.Abs(pos.x - x) == 1 && Math.Abs(pos.y - y) == 2)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
