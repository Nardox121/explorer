using System;
using System.Collections.Generic;
using System.Text;

namespace explorer {
    class Point {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y) {
            X = x;
            Y = y;
        }

        public static Point operator +(Point b, Point a)
            => new Point(a.X + b.X, a.Y + b.Y);

        public static bool operator ==(Point b, Point a)
            => a.X == b.X && a.Y == b.Y;

        public static bool operator !=(Point b, Point a)
            => a.X != b.X || a.Y != b.Y;
    }
}
