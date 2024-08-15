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

        private int PlaySpaceToBoardSpaceWidth(int playWidth) // enter play space (position A-H where A is 1 and H is 8) and get the board space position of printout
        {
            Dictionary<int, int> width = new Dictionary<int, int>
            {
                {1, 3},
                {2, 7},
                {3, 11},
                {4, 15},
                {5, 19},
                {6, 23},
                {7, 27},
                {8, 31}
            };
            
            return width[playWidth];
        }
        private int PlaySpaceToBoardSpaceHeight(int playHeight)  //enter play space (position 1-8) and get the board space position of printout
        {
            Dictionary<int, int> height = new Dictionary<int, int>
            {
                {1, 15},
                {2, 13},
                {3, 11},
                {4, 9},
                {5, 7},
                {6, 5},
                {7,3},
                {8, 1}
            };
            return height[playHeight];  
        }

        //print board state
        public void PrintBoard()
        {
            Dictionary<int, string> letters = new Dictionary<int, string> // helps printing letters
            {
                {3, "A"},
                {7, "B"},
                {11, "C"},
                {15, "D"},
                {19, "E"},
                {23, "F"},
                {27, "G"},
                {31, "H"}
            };


            // x and y are coordinates where x is which column and y is which row. The top left corner of the chess board 
            // will be known as (0, 0). 
            for(int y = 0; y < 19; y++) // for each row
            {
                string line = "";
                for (int x = 0; x < 35; x++) // "column" in said row
                {
                    if (y == 0 || y == 18) // first row and last row, we print
                    {
                        if((x+1) % 4 == 0) // this finds where middle of squares are
                        {
                            line += letters[x];
                        }
                        else //if we are not printing anything, it's a space
                        {
                            line += " ";
                        }
                    }
                    else if(y % 2 == 0) //every 2 (2,4,6) rows, first and last column
                    {
                        if (x == 0 || x == 34)
                        {
                            line += (9 - y / 2).ToString();
                        }
                        else if((x + 3) % 4 == 0)//every 1 5 9.... column
                        {
                            line += "|";
                        }
                        else //if nothing else, we print empty space
                        {
                            line += " ";
                        }
                    }
                    else if(y % 2 == 1)//every 2  (1,3,5) rows, every column
                    {
                        line = (" +---+---+---+---+---+---+---+---+");
                        break;
                    }
                    else //if nothing else, we print empty space
                    {
                        line += " ";
                    }

                }
                Console.WriteLine(line);
            }

        }

        //CheckIfCheckmate

        //bool currentTurn (false = black / true = white)

        //Check collision
    }
}
