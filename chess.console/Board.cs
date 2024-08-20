using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static chess.console.Piece;

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

        public Board()
        {
            //Create and add and pieces to list
            for(int i = 1; i < 9; i++)
            {
               
                //math for position
                
                Piece.Position pos = new Piece.Position();
                pos.x = i;
                pos.y = 2;

                //white
                Pawn pawn = new Pawn(false, pos);
                pieces.Add(pawn);

                //black
                pos.y = 7;
                Pawn pawn2 = new Pawn(false, pos);
                pieces.Add(pawn2);
            }

            for(int i = 1; i < 9; i++)
            {
                Piece.Position pos = new Piece.Position();
                pos.x = i;
                pos.y = 1;
                if (i == 1 || i == 8) //rooks
                {
                    Rook piece = new Rook(true, pos);
                    pieces.Add(piece);
                    pos.y = 8;
                    Rook piece2 = new Rook(false, pos);
                    pieces.Add(piece2);
                }
                else if(i == 2 || i == 7) //knights
                {
                    Knight piece = new Knight(true, pos);
                    pieces.Add(piece);
                    pos.y = 8;
                    Knight piece2 = new Knight(false, pos);
                    pieces.Add(piece2);
                }
                else if(i == 3 || i == 6) //bishops
                {
                    Bishop piece = new Bishop(true, pos);
                    pieces.Add(piece);
                    pos.y = 8;
                    Bishop piece2 = new Bishop(false, pos);
                    pieces.Add(piece2);
                }
                else if(i == 4) // KING
                {
                    King piece = new King(true, pos);
                    pieces.Add(piece);
                    pos.y = 8;
                    King piece2 = new King(false, pos);
                    pieces.Add(piece2);
                }
                else // QUEEN
                {
                    Queen piece = new Queen(true, pos);
                    pieces.Add(piece);
                    pos.y = 8;
                    Queen piece2 = new Queen(false, pos);
                    pieces.Add(piece2);
                }
            }

        }

        
        //List of board positions
        private List<BoardSquares> board = new List<BoardSquares>();

        //List of pieces
        private List<Piece> pieces = new List<Piece>();

        //Reset function
        public void ResetBoard()
        {
            
        }
        public string RepresentPieceOnBoard(int x, int y)
        {
            string textPiece = "";
            foreach (var piece in pieces)
            {
                if (piece.pos.x == x && piece.pos.y == y)
                {
                    if (piece.type == 0) // PAWN
                    {
                        textPiece = "P";
                    }
                    else if (piece.type == 1) // ROOK
                    {
                        textPiece = "R";
                    }
                    else if (piece.type == 2) // KNIGHT
                    {
                        textPiece = "N";
                    }
                    else if (piece.type == 3) // BISHOP
                    {
                        textPiece = "B";
                    }
                    else if (piece.type == 4) // QUEEN
                    {
                        textPiece = "Q";
                    }
                    else if (piece.type == 5) // KING
                    {
                        textPiece = "K";
                    }
                    if (piece.isBlack == true)
                    {
                        textPiece.ToLower(); //todo. this doesnt trigger
                    }
                    return textPiece;
                }
            }
            return " ";
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

        private int BoardSpaceToPlaySpaceWidth(int playWidth) // enter play space (position A-H where A is 1 and H is 8) and get the board space position of printout
        {
            Dictionary<int, int> width = new Dictionary<int, int>
            {
                {3, 1},
                {7, 2},
                {11, 3},
                {15, 4},
                {19, 5},
                {23, 6},
                {27, 7},
                {31, 8}
            };

            return width[playWidth];
        }
        private int BoardSpaceToPlaySpaceHeight(int playHeight)  //enter play space (position 1-8) and get the board space position of printout
        {
            Dictionary<int, int> height = new Dictionary<int, int>
            {
                {16, 1},
                {14, 2},
                {12, 3},
                {10, 4},
                {8, 5},
                {6, 6},
                {4, 7},
                {2, 8}
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
                        else if((x+1) % 4 == 0 && y > 1 && y < 16)   // Position for pieces
                        {
                            //line += "T";
                            line += RepresentPieceOnBoard(BoardSpaceToPlaySpaceWidth(x), BoardSpaceToPlaySpaceHeight(y));
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

        public bool IsThisMyPiece(bool currentTurn, int[] comparePos)
        {
            //Check if there is a piece on comparePos that belongs to the current player

            return false;
        }

        //CheckIfCheckmate

        //bool currentTurn (false = black / true = white)

        //Check collision
    }
}
