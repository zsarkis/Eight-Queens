using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EightQueens
{
    public class Square
    {
        public bool QueenOnSquare, SquareAvailable;
        public SquareColor SquareColor;
        public int x, y;
        public Square(Point point, bool queenOnSquare, SquareColor squareColor, bool squareAvailable)
        {
            Rectangle myRect = new Rectangle(point.X, point.Y, 50, 50); //TODO: take another look at this.

            this.QueenOnSquare = queenOnSquare;
            this.SquareColor = squareColor;
            this.x = point.X;
            this.y = point.Y;
            this.SquareAvailable = squareAvailable;
        }
    }

    public enum SquareColor
    {
        Black,
        White
    }
}
