using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess.console
{
    public class King : Piece
    {
        public King(bool isBlack, Position startPos, bool firstMove = true)
            : base((int)Types.KING, isBlack, startPos, firstMove)
        {

        }
        //Move
        public override bool MoveLogic(int x, int y)
        {
            //Return true if King can move to that position, otherwise return false

            //Check if the new positions are legal
            if (x >= 1 && y >= 1 && x <= 8 && y <= 8)
            {
                //Check if the distance moved in x is 1 and distance moved in y is 1
                if(Math.Abs(pos.x - x) == 1 && Math.Abs(pos.y - y) == 1)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
