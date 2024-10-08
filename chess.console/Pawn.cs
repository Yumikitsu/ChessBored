﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static chess.console.Piece;

namespace chess.console
{
    public class Pawn : Piece
    {
        //Legal Move

        //Upgrade

        public Pawn(bool isBlack, Position startPos, bool firstMove = true)
            : base((int)Types.PAWN, isBlack, startPos, firstMove)
        {

        }
        //Move
        public override bool MoveLogic(int x, int y)
        {
            //Return true if Pawn can move to that position, otherwise return false

            //Check if the new positions are legal
            if (x >= 1 && y >= 1 && x <= 8 && y <= 8)
            {
                //Check if the move is entirely vertical
                if (Math.Abs(pos.x - x) == 0 && Math.Abs(pos.y - y) > 0)
                {
                    //Check if it is moving the right direction (If positive and white = move up, If negative and black = move down)
                    if ((pos.y - y < 0 && !isBlack) || (pos.y - y > 0 && isBlack))
                    {
                        //Check if it is the firstMove or not
                        if (firstMove)
                        {
                            //Check if the vertical move is 1 or 2
                            if(Math.Abs(pos.y - y) <= 2)
                            {
                                return true;
                            }
                        }
                        else
                        {
                            //Check if the vertical move is 1
                            if(Math.Abs(pos.y - y) == 1)
                            {
                                return true;
                            }
                        }
                    }
                }
                else if(Math.Abs(pos.x - x) == 1 && Math.Abs(pos.y - y) == 1)//check if pawns is trying a kill move
                {
                    //Check if it is moving the right direction (If positive and white = move up, If negative and black = move down)
                    if ((pos.y - y < 0 && !isBlack) || (pos.y - y > 0 && isBlack))
                    {
                        return true;
                    }
                }
            }
            
            return false;
        }
    }
}
