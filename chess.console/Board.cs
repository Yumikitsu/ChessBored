using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace chess.console
{
    public class Board
    {
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

        private int IsThisMyPiece(bool currentTurn, int[] comparePos)
        {
            int index = 0;
            //Check if there is a piece on comparePos that belongs to the current player
            foreach (Piece piece in this.pieces)
            {
                if (piece.isBlack == currentTurn) //Correct player
                {
                    if (piece.pos.x == comparePos[0] && piece.pos.y == comparePos[1]) //Same position
                    {
                        return index;
                    }
                }
                index++;
            }
            return -1;
        }

        private bool IsThereAPieceHere(int x, int y)
        {
            //Check if a piece is on this position
            foreach (Piece piece in this.pieces)
            {
                if (piece.pos.x == x && piece.pos.y == y) //Same position
                {
                    return true;
                }
            }
            return false;
        }

        public bool CanMovePiece(bool currentTurn, int[] startPos, int[] endPos)
        {
            //Check if the first part of the inputs equate to your own piece
            int firstIndex = this.IsThisMyPiece(currentTurn, startPos);
            int enemyKingCheckIndex = this.IsThisMyPiece(!currentTurn, endPos);
            if (enemyKingCheckIndex != -1)
            {
                if (pieces.ElementAt(enemyKingCheckIndex).type == (int)Piece.Types.KING)
                {
                    return false;
                }
            }


            if(firstIndex != -1)
            {
                if(pieces.ElementAt(firstIndex).MoveLogic(endPos[0], endPos[1])) //If the piece can move to the end position
                {
                    //Check if it is a knight
                    if(pieces.ElementAt(firstIndex).type == (int)Piece.Types.KNIGHT)
                    {
                        //Is the end position not occupied by your own piece
                        if(this.IsThisMyPiece(currentTurn, endPos) == -1)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        //Check if the path is blocked
                        if (Math.Abs(startPos[0] - endPos[0]) != 0 || Math.Abs(startPos[1] - endPos[1]) == 0) //horizontal movement
                        {
                            if (startPos[0] < endPos[0]) //Moving right
                            {
                                for (int i = startPos[0]; i < endPos[0]; i++)
                                {
                                    if (IsThereAPieceHere(i, startPos[1]))
                                    {
                                        return false;
                                    }
                                }
                            }
                            else //Moving left
                            {
                                for(int i = startPos[0]; i > endPos[0]; i--)
                                {
                                    if (IsThereAPieceHere(i, startPos[1]))
                                    {
                                        return false;
                                    }
                                }
                            }
                        }
                        else if (Math.Abs(startPos[0] - endPos[0]) == 0 || Math.Abs(startPos[1] - endPos[1]) != 0) //vertical movement
                        {
                            if (startPos[1] < endPos[1]) //Moving up
                            {
                                for (int i = startPos[1]; i < endPos[1]; i++)
                                {
                                    if (IsThereAPieceHere(startPos[0], i))
                                    {
                                        return false;
                                    }
                                }
                            }
                            else //Moving down
                            {
                                for (int i = startPos[1]; i > endPos[1]; i--)
                                {
                                    if (IsThereAPieceHere(startPos[0], i))
                                    {
                                        return false;
                                    }
                                }
                            }
                        }
                        else //diagonal movement
                        {
                            if (startPos[0] < endPos[0] && startPos[1] < endPos[1]) //Moving up right
                            {
                                for(int i = 1; i < Math.Abs(startPos[0] - endPos[0]); i++)
                                {
                                    if (IsThereAPieceHere(startPos[0] + i, startPos[1] + i))
                                    {
                                        return false;
                                    }
                                }
                            }
                            else if(startPos[0] > endPos[0] && startPos[1] > endPos[1]) //Moving down left
                            {
                                for (int i = 1; i < Math.Abs(startPos[0] - endPos[0]); i++)
                                {
                                    if (IsThereAPieceHere(startPos[0] - i, startPos[1] - i))
                                    {
                                        return false;
                                    }
                                }
                            }
                            else if (startPos[0] < endPos[0] && startPos[1] > endPos[1]) //Moving down right
                            {
                                for (int i = 1; i < Math.Abs(startPos[0] - endPos[0]); i++)
                                {
                                    if (IsThereAPieceHere(startPos[0] + i, startPos[1] - i))
                                    {
                                        return false;
                                    }
                                }
                            }
                            else //Moving up left
                            {
                                for (int i = 1; i < Math.Abs(startPos[0] - endPos[0]); i++)
                                {
                                    if (IsThereAPieceHere(startPos[0] - i, startPos[1] + i))
                                    {
                                        return false;
                                    }
                                }
                            }
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public void MovePiece(bool currentTurn, int[] startPos, int[] endPos)
        {
            //If there is an enemy on the endPos. Kill it
            int index = this.IsThisMyPiece(!currentTurn, endPos);
            if(index != -1)
            {
                pieces.RemoveAt(index); //Rest in piece
            }

            //Move the piece to the new position
            index = this.IsThisMyPiece(currentTurn, startPos);
            pieces.ElementAt(index).Move(endPos[0], endPos[1]);
        }

        //CheckIfCheckmate

        //bool currentTurn (false = black / true = white)

        //Check collision
    }
}
