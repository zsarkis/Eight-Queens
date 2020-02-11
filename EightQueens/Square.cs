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
        public bool isQS, squareColor, squareAvailable;
        public int x, y;
        public Square(Point point, bool isQ, bool squareColor, bool squareAvailable)
        {
            Rectangle myRect = new Rectangle(point.X, point.Y, 50, 50);
            isQS = isQ;
            this.squareColor = squareColor;
            this.x = point.X;
            this.y = point.Y;
            this.squareAvailable = squareAvailable;
        }
    }
}
