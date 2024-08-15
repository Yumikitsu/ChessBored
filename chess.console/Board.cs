using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess.console
{
    public class Board
    {
        struct BoardSquares
        {
            int x = 0;
            int y = 0;
            bool occupied = false;

            BoardSquares(int x, int y, bool occupied)
            {
                this.x = x;
                this.y = y;
                this.occupied = occupied;
            }
        };
        //List of board positions
        private List<BoardSquares> board = new List<BoardSquares>();

        //List of pieces
        private List<Piece> pieces = new List<Piece>();

        //Reset function
        public void ResetBoard()
        {
            
        }

        //print board state
        public void PrintBoard()
        {
            // x and y are coordinates where x is which column and y is which row. The top left corner of the chess board 
            // will be known as (0, 0). 
            for(int y = 0; y < 19; y++)
            {
                string line = "";
                for (int x = 0; x < 19; x++)
                {
                    if (y == 0 || y == 18) // first row and last row, we print
                    {
                        if(x < 3)
                        {
                            line += " ";
                        }
                        else if()
                    }
                }
            }

        }

        //CheckIfCheckmate

        //bool currentTurn (false = black / true = white)

        //Check collision
    }
}
