// See https://aka.ms/new-console-template for more information
using chess.console;

Console.WriteLine("Hello, World!");

//Console.WriteLine("" +
//    "   a   b   c   d   e   f   g   h\r\n" +
//    " +---+---+---+---+---+---+---+---+\r\n" +
//    "8| r | n | b | q | k | b | n | r |8\r\n" +
//    " +---+---+---+---+---+---+---+---+\r\n" +
//    "7| p | p | p | p | p | p | p | p |7\r\n" +
//    " +---+---+---+---+---+---+---+---+\r\n" +
//    "6|   |   |   |   |   |   |   |   |6\r\n" +
//    " +---+---+---+---+---+---+---+---+\r\n" +
//    "5|   |   |   |   |   |   |   |   |5\r\n" +
//    " +---+---+---+---+---+---+---+---+\r\n" +
//    "4|   |   |   |   |   |   |   |   |4\r\n" +
//    " +---+---+---+---+---+---+---+---+\r\n" +
//    "3|   |   |   |   |   |   |   |   |3\r\n" +
//    " +---+---+---+---+---+---+---+---+\r\n" +
//    "2| P | P | P | P | P | P | P | P |2\r\n" +
//    " +---+---+---+---+---+---+---+---+\r\n" +
//    "1| R | N | B | Q | K | B | N | R |1\r\n" +
//    " +---+---+---+---+---+---+---+---+\r\n" +
//    "   a   b   c   d   e   f   g   h"
//);


Board board = new Board();
board.PrintBoard();
//letters on index 3, 7, 11, 15
// mod: y+1 % 4 == 0, then letter