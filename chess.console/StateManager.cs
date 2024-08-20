using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess.console
{
    public class StateManager
    {
        private Dictionary<char, int> charTranslator = new Dictionary<char, int>
        {
            {'a', 1}, {'b', 2}, {'c', 3}, {'d', 4}, {'e', 5}, {'f', 6}, {'g', 7}, {'h', 8}
        };

        //Get the coordinates from two letters
        private int[] GetCoordinates(char firstLetter, char secondLetter)
        {
            //Get the coordinates from the first 2 characters in input
            int x = -1;
            int y = -1;

            if (charTranslator.ContainsKey(firstLetter)) //Can the first letter be translated to an int
            {
                x = charTranslator[firstLetter];
            }

            if (Char.IsDigit(secondLetter)) //Is the second letter a number
            {
                y = (int)Char.GetNumericValue(secondLetter);

                if (y < 1 || y > 8) //Is the second letter out of the acceptable range
                {
                    y = -1;
                }
            }

            return new int[] { x, y };
        }
        bool gameOn { get; set; } //Variable to check if the game is ongoing or should stop
        bool currentTurn { get; set; } //Who's turn is it to play? True = White, False = Black

        public StateManager()
        {
            gameOn = true;
            currentTurn = true;
        }

        //Get a value if the game is ongoing
        public bool GameOngoing()
        {
            return gameOn;
        }

        //Update the game
        public void Update(Board board)
        {
            bool reset = false; //Variable to check if input is invalid
            //Try to get inputs until they are acceptable
            while (true)
            {
                if (reset)
                {
                    Console.WriteLine("Invalid Input, Please Try Again!");
                }
                //Receive inputs
                string inputs = Console.ReadLine();
                inputs.ToLower();
                reset = false;

                //Check if input is 4 characters long
                if (inputs.Count() == 4)
                {
                    //Get both coordinates
                    int[] startPos = GetCoordinates(inputs[0], inputs[1]);
                    int[] endPos = GetCoordinates(inputs[2], inputs[3]);

                    for (int i = 0; i < 2; i++) //Check if any of the two coordinates are invalid
                    {
                        if (startPos[i] == -1 || endPos[i] == -1)
                        {
                            reset = true;
                            break;
                        }
                    }

                    if(reset) //Look for a new input again
                    {
                        continue;
                    }

                    //Check if the first part of the inputs equate to your own piece
                    if (board.IsThisMyPiece(currentTurn, startPos))
                    {

                    }

                }
                else
                {
                    reset = true;
                }
            }
        }

        //KillPiece

        //Input handling
        //First parameter: Pick up piece if it exists there
        //Second parameter: Check if legal space
        //Can piece move there (piece function)?
        //Check for blocks along the way (board)
        //Eliminate Piece or not (Cannot eliminate king)?
        //Check for checkmate

    }
}
