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

        //CheckIfCheckmate

        //bool currentTurn (false = black / true = white)

        //Check collision
    }
}
