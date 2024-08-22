using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static chess.console.Piece;
using chess.console.ConsoleOrSpeech;
using chess.console.Speech;
using System.Runtime.ExceptionServices;

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

        public Communicate communicate = new Communicate();
        public ConsoleCommunicator consoleCommunicator = new ConsoleCommunicator();
        public SpeechCommunicator speechCommunicator = new SpeechCommunicator();

        public Dictionary<int, string> letters = new Dictionary<int, string> // helps printing letters
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

        public Board()
        {
            

            //Create and add and pieces to list
            for (int i = 1; i < 9; i++)
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
                Pawn pawn2 = new Pawn(true, pos);
                pieces.Add(pawn2);
            }

            for(int i = 1; i < 9; i++)
            {
                Piece.Position pos = new Piece.Position();
                pos.x = i;
                pos.y = 8; // white first 
                if (i == 1 || i == 8) //rooks
                {
                    Rook piece = new Rook(true, pos);
                    pieces.Add(piece);
                    pos.y = 1;
                    Rook piece2 = new Rook(false, pos);
                    pieces.Add(piece2);
                }
                else if(i == 2 || i == 7) //knights
                {
                    Knight piece = new Knight(true, pos);
                    pieces.Add(piece);
                    pos.y = 1;
                    Knight piece2 = new Knight(false, pos);
                    pieces.Add(piece2);
                }
                else if(i == 3 || i == 6) //bishops
                {
                    Bishop piece = new Bishop(true, pos);
                    pieces.Add(piece);
                    pos.y = 1;
                    Bishop piece2 = new Bishop(false, pos);
                    pieces.Add(piece2);
                }
                else if(i == 4) // KING
                {
                    King piece = new King(true, pos);
                    pieces.Add(piece);
                    pos.y = 1;
                    King piece2 = new King(false, pos);
                    pieces.Add(piece2);
                }
                else // QUEEN
                {
                    Queen piece = new Queen(true, pos);
                    pieces.Add(piece);
                    pos.y = 1;
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
                    if (piece.isBlack)
                    {
                        textPiece = textPiece.ToLower(); //todo. this doesnt trigger
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

            communicate.SendMessage("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n", consoleCommunicator);
            // x and y are coordinates where x is which column and y is which row. The top left corner of the chess board 
            // will be known as (0, 0). 
            for (int y = 0; y < 19; y++) // for each row
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
                        else if((x+1) % 4 == 0 && y > 1 && y < 17)   // Position for pieces
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
                communicate.SendMessage(line, consoleCommunicator);
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
        private bool IsPieceThreat(int x, int y, bool kingIsBlack, Types[] threats)
        {
            //Check if a piece is on this position
            foreach (Piece piece in this.pieces)
            {
                if (piece.pos.x == x && piece.pos.y == y) //Same position
                {
                    if(piece.isBlack != kingIsBlack)// different color from king
                    {
                        foreach (var threat in threats)
                        {
                            if((int)threat == piece.type)
                            {
                                // this is dangerous, threat!
                                return true;
                            }
                        }
                    }
                    break;
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
                        if(pieces.ElementAt(firstIndex).type == (int)Piece.Types.PAWN) //if piece is a pawn
                        {
                            //if it moves diagonally
                            if(Math.Abs(pieces.ElementAt(firstIndex).pos.x - endPos[0]) == 1 && Math.Abs(pieces.ElementAt(firstIndex).pos.y - endPos[1]) == 1)
                            {
                                int index = this.IsThisMyPiece(!currentTurn, endPos);
                                if (index == -1)
                                {
                                    // cannot kill
                                    return false;
                                }
                            }
                        }
                        foreach (var isKing in pieces)
                        {
                            if (isKing.type == (int)Piece.Types.KING && isKing.isBlack == pieces.ElementAt(firstIndex).isBlack)
                            {
                                int[] position = new int[2];
                                position[0] = isKing.pos.x;
                                position[1] = isKing.pos.y;

                                // to check so a player does not put himself in check
                                if (CheckCheck(position, pieces.ElementAt(firstIndex).isBlack))
                                {
                                    //puts himself in check, not allowed
                                    return false;
                                }
                            }
                        }
                        
                        //Check if the path is blocked
                        if (Math.Abs(startPos[0] - endPos[0]) != 0 && Math.Abs(startPos[1] - endPos[1]) == 0) //horizontal movement
                        {
                            if (startPos[0] < endPos[0]) //Moving right
                            {
                                for (int i = startPos[0] + 1; i < endPos[0]; i++)
                                {
                                    if (IsThereAPieceHere(i, startPos[1]))
                                    {
                                        return false;
                                    }
                                }
                            }
                            else //Moving left
                            {
                                for(int i = startPos[0] - 1; i > endPos[0]; i--)
                                {
                                    if (IsThereAPieceHere(i, startPos[1]))
                                    {
                                        return false;
                                    }
                                }
                            }
                        }
                        else if (Math.Abs(startPos[0] - endPos[0]) == 0 && Math.Abs(startPos[1] - endPos[1]) != 0) //vertical movement
                        {
                            if (startPos[1] < endPos[1]) //Moving up
                            {
                                for (int i = startPos[1] + 1; i < endPos[1]; i++)
                                {
                                    if (IsThereAPieceHere(startPos[0], i))
                                    {
                                        return false;
                                    }
                                }
                            }
                            else //Moving down
                            {
                                for (int i = startPos[1] - 1; i > endPos[1]; i--)
                                {
                                    if (IsThereAPieceHere(startPos[0], i))
                                    {
                                        return false;
                                    }
                                }
                            }
                            //For pawns
                            if (pieces.ElementAt(firstIndex).type == (int)Piece.Types.PAWN)
                            {
                                if (IsThereAPieceHere(endPos[0], endPos[1]))
                                {
                                    //cannot move forward into another piece
                                    //filthy pawn peseant 
                                    return false;
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
                        }
                        return true;
                    }
                }
            }
            return false;
        }

       
        public bool StateCheck(bool isBlackKing)
        {
            int[] ints = new int[2];
            //find opposite color king, so we can see if a move put the opponent in check
            foreach (var piece in pieces)
            {
                if (piece.isBlack != isBlackKing && piece.type == (int)Types.KING)//enemy king
                {
                    ints[0] = piece.pos.x;
                    ints[1] = piece.pos.y;
                    if(CheckCheck(ints, !isBlackKing))
                    {
                        return true;
                    }
                    return false;
                }
            }
            return false;
        }
        //returns false if everything is fine, returns true for check 
        private bool CheckCheck(int[] kingPosition, bool isBlackKing)
        {
            foreach (var piece in pieces)
            {
                if(piece.isBlack != isBlackKing)//enemy piece
                {
                    int[] ints = new int[2];
                    ints[0] = piece.pos.x;
                    ints[1] = piece.pos.y;

                    if (CanMovePiece(piece.isBlack, ints, kingPosition))
                    {
                        //piece can move here, aka check
                        return true;
                    }
                }
            }

            return false;
        }



        public string BoolColorToStringColor(bool black)
        {
            if (black)
            {
                return "black";
            }
            return "white";
        }

        public string SpeechPreparer(int[] startPos, int[] endPos, string piece, bool color, string kill)
        {
            if(kill != "") // a piece was eliminated
            {
                return $"Moving {BoolColorToStringColor(color)} {piece} from {letters[PlaySpaceToBoardSpaceWidth(startPos[0])]} {startPos[1]} to {letters[PlaySpaceToBoardSpaceWidth(endPos[0])]} {endPos[1]}" +
                    $" eliminating {BoolColorToStringColor(!color)} {kill}";
            }
            return $"Moving {BoolColorToStringColor(color)} {piece} from {letters[PlaySpaceToBoardSpaceWidth(startPos[0])]} {startPos[1]} to {letters[PlaySpaceToBoardSpaceWidth(endPos[0])]} {endPos[1]}";
        }

        public string GetPieceName(int piece)
        {
            switch(piece)
            {
                case 0:
                    return "pawn";
                case 1:
                    return "rook";
                case 2:
                    return "knight";
                case 3:
                    return "bishop";
                case 4:
                    return "queen";
                case 5:
                    return "king";
            }
            return ""; //intellinonsense
        }

        public void MovePiece(bool currentTurn, int[] startPos, int[] endPos, int castling = 0)
        {
            //If there is an enemy on the endPos. Kill it
            int index = this.IsThisMyPiece(!currentTurn, endPos);
            string kill = "";
            if(index != -1)
            {
                kill = GetPieceName(pieces[index].type);
                pieces.RemoveAt(index); //Rest in piece
            }

            //Move the piece to the new position
            index = this.IsThisMyPiece(currentTurn, startPos);
            pieces.ElementAt(index).Move(endPos[0], endPos[1]);
            if(castling == 0)
            {
                communicate.SendMessage(SpeechPreparer(startPos, endPos, GetPieceName(pieces[index].type), currentTurn, kill), speechCommunicator);
            }
            else if(castling == 1) //Castled Queenside
            {
                if(currentTurn)
                {
                    communicate.SendMessage("Black player castled queenside", speechCommunicator);
                }
                else
                {
                    communicate.SendMessage("White player castled queenside", speechCommunicator);
                }
            }
            else if(castling == 2) //Castled Kingside
            {
                if (currentTurn)
                {
                    communicate.SendMessage("Black player castled kingside", speechCommunicator);
                }
                else
                {
                    communicate.SendMessage("White player castled kingside", speechCommunicator);
                }
            }
        }

        public bool Castling(bool currentTurn, int[] startPos, int[] endPos)
        {
            bool found = false;
            foreach (var king in pieces.Where(x => x.type == (int)Types.KING)) //All kings
            {
                if (king.isBlack == currentTurn && king.pos.x == startPos[0] && king.pos.y == startPos[1] && king.firstMove && !CheckCheck(startPos, king.isBlack)) //Condition for king to be able to castle
                {
                    foreach (var rook in pieces.Where(x => x.type == (int)Types.ROOK)) //All rooks
                    {
                        if (rook.isBlack == currentTurn && rook.pos.x == endPos[0] && rook.pos.y == endPos[1] && rook.firstMove) //Condition for rook to be able to castle
                        {
                            found = true;
                            break;
                        }
                    }
                    break;
                }
            }

            if(found) //Found castleable at start and endPos
            {
                bool movingLeft = false;
                if (startPos[0] > endPos[0]) //King moving left
                {
                    movingLeft = true;
                    for(int i = startPos[0] - 1; i > endPos[0]; i--) //Check for all middle positions if they are blocked
                    {
                        if(IsThereAPieceHere(i, startPos[1])) //Block check
                        {
                            return false;
                        }
                    }
                }
                else //King moving right
                {
                    for (int i = startPos[0] + 1; i < endPos[0]; i++) //Check for all middle positions if they are blocked
                    {
                        if (IsThereAPieceHere(i, startPos[1])) //Block check
                        {
                            return false;
                        }
                    }
                }


                if(movingLeft) //King moving left
                {
                    //New king and rook positions
                    int[] kingEndPos = new int[2] { startPos[0] - 2, startPos[1] };
                    int[] rookEndPos = new int[2] { startPos[0] - 1, startPos[1] };

                    //Check if these positions result in king being in check
                    if(!CheckCheck(kingEndPos, currentTurn) && !CheckCheck(rookEndPos, currentTurn))
                    {
                        //Move king and rook to new position
                        MovePiece(currentTurn, startPos, kingEndPos, 2);
                        MovePiece(currentTurn, endPos, rookEndPos, 3);
                        return true;
                    }
                }
                else //King moving right
                {
                    //New king and rook positions
                    int[] kingEndPos = new int[2] { startPos[0] + 2, startPos[1] };
                    int[] rookEndPos = new int[2] { startPos[0] + 1, startPos[1] };

                    //Check if these positions result in king being in check
                    if (!CheckCheck(kingEndPos, currentTurn) && !CheckCheck(rookEndPos, currentTurn))
                    {
                        //Move king and rook to new position
                        MovePiece(currentTurn, startPos, kingEndPos, 1);
                        MovePiece(currentTurn, endPos, rookEndPos, 3);
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
