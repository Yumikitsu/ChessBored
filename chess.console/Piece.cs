using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess.console
{
    public abstract class Piece
    {
        //Position struct
        public struct Position
        {
            public int x { get; set; }
            public int y { get; set; }

            public void SetNewPos(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        };

        //List of all types
        public enum Types
        {
            PAWN = 0,
            ROOK = 1,
            KNIGHT = 2,
            BISHOP = 3,
            QUEEN = 4,
            KING = 5
        };

        //Identifier
        public int type { get; set; }

        //First move
        public bool firstMove { get; set; }

        //Color (Black/White)
        public bool isBlack { get; set; }

        //Position (2D)
        public Position pos { get; set; }

        //State (Dead/Alive)
        public bool isAlive { get; set; }

        //Constructor
        protected Piece(int type, bool isBlack, Position pos, bool firstMove)
        {
            this.type = type;
            this.isBlack = isBlack;
            this.pos = pos;
            this.isAlive = true;
            this.firstMove = firstMove;
        }

      
    

        //Override MoveLogic function
        public abstract bool MoveLogic(int x, int y);

        //Move piece to new position
        public void Move(int x, int y)
        {
            //this.pos = pos.setNewPos(x, y);
            Position newPos = new Position();
            newPos.x = x; newPos.y = y;
            pos = newPos;

            if(firstMove)
            {
                firstMove = false;
            }
        }

        //Change type (Pawn Exclusive)
        public void Promotion(int newType)
        {
            if(this.type == (int)Types.PAWN)
            {
                this.type = newType;
            }
        }

        //Kill a piece
        public void Kill()
        {
            this.isAlive = false; 
        }
    }
}
