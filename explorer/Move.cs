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

        public Move(int x1, int y1, int x2, int y2, char letter)
        {
            this.xFrom = x1;
            this.yFrom = y1;
            this.xTo = x2;
            this.yTo = y2;
            this.letter = letter;
        }

        public override string ToString()
        {
            return $"{char.ToUpper(letter)}{Convert.ToChar('a' + xFrom)}{8 - yFrom}-{Convert.ToChar('a' + xTo)}{8 - yTo}";
        }
    }
}
