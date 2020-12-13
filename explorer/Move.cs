using System;
using System.Collections.Generic;
using System.Text;

namespace explorer
{
    public class Move
    {
        int xFrom;
        int yFrom;
        int xTo;
        int yTo;
        char letter;
        bool WhiteMove;

        public Move(int x1, int y1, int x2, int y2, char letter, bool whiteMove)
        {
            this.xFrom = x1;
            this.yFrom = y1;
            this.xTo = x2;
            this.yTo = y2;
            this.letter = letter;
            WhiteMove = whiteMove;
        }

        public void printMove()
        {
            Console.WriteLine($"{char.ToUpper(letter)} {Convert.ToChar(yFrom + 65)}{8 - xFrom} to {Convert.ToChar(yTo + 65)}{8 - xTo}");
        }
    }
}
