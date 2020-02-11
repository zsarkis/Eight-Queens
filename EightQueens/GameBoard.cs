using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace EightQueens
{
    public partial class GameBoard : Form
    {
        Square[,] arrayAvail = new Square[8, 8];
        int queenCount = 0;

        public GameBoard()
        {
            InitializeBoard(new Point(100, 100));

            InitializeComponent();
        }

        #region Initialization

        private void InitializeBoard(Point topOfSpacePoint)
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    SortSquares(x, y, ref topOfSpacePoint);
                }

                topOfSpacePoint.X = topOfSpacePoint.X + 50;

                topOfSpacePoint.Y = 100;
            }
        }

        private void SortSquares(int x, int y, ref Point topOfSpacePoint)
        {
            if ((x + y) % 2 == 0)
            {
                InitializeSquares(ref topOfSpacePoint, x, y, SquareColor.White);
            }
            else
            {
                InitializeSquares(ref topOfSpacePoint, x, y, SquareColor.Black);
            }
        }

        private void InitializeSquares(ref Point topOfSpacePoint, int x, int y, SquareColor color)
        {
            Square p = new Square(topOfSpacePoint, false, color, true);

            topOfSpacePoint.Y = topOfSpacePoint.Y + 50;

            arrayAvail[x, y] = p;
        }

        #endregion Initialization

        private void Clear_Click(object sender, EventArgs e)
        { 
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    queenCount = 0;
                    label1.Text = String.Format("You have " + queenCount + " queens on the board.");
                    Square square = arrayAvail[x, y];
                    square.SquareAvailable = true;
                    square.QueenOnSquare = false;
                }
            }

            this.Invalidate();
        }

        private void Hint_CheckedChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void Hint_MouseClick(object sender, MouseEventArgs e)
        { }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Font myFont = new Font("Arial", 30, FontStyle.Bold);

            int verticalCount = 101;

            int horizontalCount = 101;

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    Square square = arrayAvail[x, y];

                    ColorSquare(horizontalCount, x, verticalCount, y, square, g);

                    if (square.QueenOnSquare == true)
                    {
                        string s = "Q";

                        int xRounded = (square.x / 50) * 50;
                        int yRounded = (square.y / 50) * 50;

                        if (Hint.Checked == true)
                        {
                            g.DrawString(s, myFont, Brushes.Black, xRounded + 2, yRounded + 2);
                        }
                        if (Hint.Checked == false)
                        {
                            if ((x + y) % 2 == 0)
                            {
                                g.DrawString(s, myFont, Brushes.Black, xRounded + 2, yRounded + 2);
                            }
                            else
                            {
                                g.DrawString(s, myFont, Brushes.White, xRounded + 2, yRounded + 2);
                            }
                        }
                    }
                }
            }

            DrawBoardLines(g);
        }

        #region OnPaint Methods 

        private void ColorSquare(int horct, int x, int vertct, int y, Square square, Graphics g)
        {
            Rectangle rectangle = new Rectangle(horct + x * 50, vertct + y * 50, 49, 49);

            if (square.SquareColor == SquareColor.White)
            {
                g.FillRectangle(Brushes.White, rectangle);
            }
            if (square.SquareColor == SquareColor.Black)
            {
                g.FillRectangle(Brushes.Black, rectangle);
            }
            if (Hint.Checked && (square.SquareAvailable == false))
            {
                g.FillRectangle(Brushes.Red, rectangle);
            }
        }

        private static void DrawBoardLines(Graphics g)
        {
            g.DrawLine(Pens.Black, 100, 100, 100, 500);
            g.DrawLine(Pens.Black, 150, 100, 150, 500);
            g.DrawLine(Pens.Black, 200, 100, 200, 500);
            g.DrawLine(Pens.Black, 250, 100, 250, 500);
            g.DrawLine(Pens.Black, 300, 100, 300, 500);
            g.DrawLine(Pens.Black, 350, 100, 350, 500);
            g.DrawLine(Pens.Black, 400, 100, 400, 500);
            g.DrawLine(Pens.Black, 450, 100, 450, 500);
            g.DrawLine(Pens.Black, 500, 100, 500, 500);

            g.DrawLine(Pens.Black, 100, 100, 500, 100);
            g.DrawLine(Pens.Black, 100, 150, 500, 150);
            g.DrawLine(Pens.Black, 100, 200, 500, 200);
            g.DrawLine(Pens.Black, 100, 250, 500, 250);
            g.DrawLine(Pens.Black, 100, 300, 500, 300);
            g.DrawLine(Pens.Black, 100, 350, 500, 350);
            g.DrawLine(Pens.Black, 100, 400, 500, 400);
            g.DrawLine(Pens.Black, 100, 450, 500, 450);
            g.DrawLine(Pens.Black, 100, 500, 500, 500);
        }

        #endregion OnPaint Methods 

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int x, y;
                int xx = (((int)e.X) / 50) * 50; //round these
                int yy = (((int)e.Y) / 50) * 50;

                for (x = 0; x < 8; x++)
                {
                    for (y = 0; y < 8; y++)
                    {
                        Square temp = (Square)arrayAvail[x, y];
                        if ((xx == temp.x) && (yy == temp.y) && (temp.SquareAvailable == false))
                        {
                            System.Media.SystemSounds.Beep.Play();
                        }
                        if ((temp.QueenOnSquare == false) && (xx == temp.x) && (yy == temp.y) && (temp.SquareAvailable == true))
                        {
                            temp.QueenOnSquare = true;       //disable current spot and add queen
                            temp.SquareAvailable = false;
                            queenCount = queenCount + 1;
                            label1.Text = String.Format("You have " + queenCount + " queens on the board.");
                            this.Invalidate();

                            int n;                      //disable horizontal and vertical moves
                            for (n = 0; n < 8; n++)
                            {
                                Square temp2 = (Square)arrayAvail[n, y];
                                temp2.SquareAvailable = false;
                            }
                            for (n = 0; n < 8; n++)
                            {
                                Square temp3 = (Square)arrayAvail[x, n];
                                temp3.SquareAvailable = false;
                            }
                            int m = x;
                            n = y;//disable diagonal moves
                            while (n >= 0 && m < 8)             //upper right
                            {
                                Square temp4 = (Square)arrayAvail[m, n];
                                temp4.SquareAvailable = false;
                                n = n - 1;  //moving up one row
                                m = m + 1;  //moving one column to the right
                                //break;
                            }
                            m = x;
                            n = y;
                            while (n >= 0 && m >= 0)             //upper left
                            {
                                Square temp4 = (Square)arrayAvail[m, n];
                                temp4.SquareAvailable = false;
                                n = n - 1;  //moving up one row
                                m = m - 1;  //moving one column to the right
                                //break;
                            }
                            m = x;
                            n = y;
                            while (n < 8 && m >= 0)             //lower left
                            {
                                Square temp4 = (Square)arrayAvail[m, n];
                                temp4.SquareAvailable = false;
                                n = n + 1;  //moving up one row
                                m = m - 1;  //moving one column to the right
                                //break;
                            }
                            m = x;
                            n = y;
                            while (n < 8 && m < 8)             //lower right
                            {
                                Square temp4 = (Square)arrayAvail[m, n];
                                temp4.SquareAvailable = false;
                                n = n + 1;  //moving up one row
                                m = m + 1;  //moving one column to the right
                                //break;
                            }
                        }
                    }
                }
                if (queenCount == 8)
                {
                    MessageBox.Show("You did it!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            if (e.Button == MouseButtons.Right)
            {
                int x, y;
                int xx = (((int)e.X) / 50) * 50; //round these
                int yy = (((int)e.Y) / 50) * 50;

                for (x = 0; x < 8; x++)
                {
                    for (y = 0; y < 8; y++)
                    {
                        Square temp = (Square)arrayAvail[x, y];
                        if ((temp.QueenOnSquare == true) && (xx == temp.x) && (yy == temp.y))
                        {
                            temp.QueenOnSquare = false;       //disable current spot and add queen
                            temp.SquareAvailable = true;
                            queenCount = queenCount - 1;
                            label1.Text = String.Format("You have " + queenCount + " queens on the board.");
                            this.Invalidate();

                            int n;                      //disable horizontal and vertical moves
                            for (n = 0; n < 8; n++)
                            {
                                Square temp2 = (Square)arrayAvail[n, y];
                                temp2.SquareAvailable = true;
                            }
                            for (n = 0; n < 8; n++)
                            {
                                Square temp3 = (Square)arrayAvail[x, n];
                                temp3.SquareAvailable = true;
                            }
                            int m = x;
                            n = y;//disable diagonal moves
                            while (n >= 0 && m < 8)             //upper right
                            {
                                Square temp4 = (Square)arrayAvail[m, n];
                                temp4.SquareAvailable = true;
                                n = n - 1;  //moving up one row
                                m = m + 1;  //moving one column to the right
                                //break;
                            }
                            m = x;
                            n = y;
                            while (n >= 0 && m >= 0)             //upper left
                            {
                                Square temp4 = (Square)arrayAvail[m, n];
                                temp4.SquareAvailable = true;
                                n = n - 1;  //moving up one row
                                m = m - 1;  //moving one column to the right
                                //break;
                            }
                            m = x;
                            n = y;
                            while (n < 8 && m >= 0)             //lower left
                            {
                                Square temp4 = (Square)arrayAvail[m, n];
                                temp4.SquareAvailable = true;
                                n = n + 1;  //moving up one row
                                m = m - 1;  //moving one column to the right
                                //break;
                            }
                            m = x;
                            n = y;
                            while (n < 8 && m < 8)             //lower right
                            {
                                Square temp4 = (Square)arrayAvail[m, n];
                                temp4.SquareAvailable = true;
                                n = n + 1;  //moving up one row
                                m = m + 1;  //moving one column to the right
                                //break;
                            }
                        }
                    }
                }

                for (x = 0; x < 8; x++)
                {
                    for (y = 0; y < 8; y++)
                    {
                        Square temp = (Square)arrayAvail[x, y];
                        if ((temp.QueenOnSquare == true))       //replace deleted spaces
                        {
                            temp.QueenOnSquare = true;       //disable current spot and add queen
                            temp.SquareAvailable = false;
                            label1.Text = String.Format("You have " + queenCount + " queens on the board.");
                            this.Invalidate();

                            int n;                      //disable horizontal and vertical moves
                            for (n = 0; n < 8; n++)
                            {
                                Square temp2 = (Square)arrayAvail[n, y];
                                temp2.SquareAvailable = false;
                            }
                            for (n = 0; n < 8; n++)
                            {
                                Square temp3 = (Square)arrayAvail[x, n];
                                temp3.SquareAvailable = false;
                            }
                            int m = x;
                            n = y;//disable diagonal moves
                            while (n >= 0 && m < 8)             //upper right
                            {
                                Square temp4 = (Square)arrayAvail[m, n];
                                temp4.SquareAvailable = false;
                                n = n - 1;  //moving up one row
                                m = m + 1;  //moving one column to the right
                                //break;
                            }
                            m = x;
                            n = y;
                            while (n >= 0 && m >= 0)             //upper left
                            {
                                Square temp4 = (Square)arrayAvail[m, n];
                                temp4.SquareAvailable = false;
                                n = n - 1;  //moving up one row
                                m = m - 1;  //moving one column to the right
                                //break;
                            }
                            m = x;
                            n = y;
                            while (n < 8 && m >= 0)             //lower left
                            {
                                Square temp4 = (Square)arrayAvail[m, n];
                                temp4.SquareAvailable = false;
                                n = n + 1;  //moving up one row
                                m = m - 1;  //moving one column to the right
                                //break;
                            }
                            m = x;
                            n = y;
                            while (n < 8 && m < 8)             //lower right
                            {
                                Square temp4 = (Square)arrayAvail[m, n];
                                temp4.SquareAvailable = false;
                                n = n + 1;  //moving up one row
                                m = m + 1;  //moving one column to the right
                                //break;
                            }
                        }
                    }
                }
            }
        }
    }
}
