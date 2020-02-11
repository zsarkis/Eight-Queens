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
            Point topOfSpacePoint = new Point(100, 100);

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    InitializeBoard(x, y, ref topOfSpacePoint);
                }

                topOfSpacePoint.X = topOfSpacePoint.X + 50;

                topOfSpacePoint.Y = 100;
            }

            InitializeComponent();
        }

        private void InitializeBoard(int x, int y, ref Point topOfSpacePoint)
        {
            if ((x + y) % 2 == 0)
            {
                InitializeSquares(ref topOfSpacePoint, x, y, false);
            }
            else
            {
                InitializeSquares(ref topOfSpacePoint, x, y, true);
            }
        }

        private void InitializeSquares(ref Point topOfSpacePoint, int x, int y, bool color)
        {
            Square p = new Square(topOfSpacePoint, false, color, true);

            topOfSpacePoint.Y = topOfSpacePoint.Y + 50;

            arrayAvail[x, y] = p;
        }

        private void Clear_Click(object sender, EventArgs e)
        { //clears the board
            int x, y;
            for (x = 0; x < 8; x++)
            {
                for (y = 0; y < 8; y++)
                {
                    queenCount = 0;
                    label1.Text = String.Format("You have " + queenCount + " queens on the board.");
                    Square temp = (Square)arrayAvail[x, y];
                    temp.squareAvailable = true;
                    temp.isQS = false;

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
            int x, y;
            int vertct = 101;
            int horct = 101;
            for (x = 0; x < 8; x++)
            {
                for (y = 0; y < 8; y++)
                {
                    Square temp = (Square)arrayAvail[x, y];
                    if (temp.squareColor == false)
                    {
                        g.FillRectangle(Brushes.White, horct + x * 50, vertct + y * 50, 49, 49);
                    }
                    if (temp.squareColor == true)
                    {
                        g.FillRectangle(Brushes.Black, horct + x * 50, vertct + y * 50, 49, 49);
                    }
                    if (Hint.Checked && (temp.squareAvailable == false))
                    {
                        g.FillRectangle(Brushes.Red, horct + x * 50, vertct + y * 50, 49, 49);
                    }
                    if (temp.isQS == true)
                    {
                        string s;
                        s = "Q";
                        int xx = (((int)temp.x) / 50) * 50; //round location
                        int yy = (((int)temp.y) / 50) * 50;
                        if (Hint.Checked == true)
                        {
                            g.DrawString(s, myFont, Brushes.Black, xx + 2, yy + 2);
                        }
                        if (Hint.Checked == false)
                        {
                            if (((x + y) % 2) == 0)
                            {
                                g.DrawString(s, myFont, Brushes.Black, xx + 2, yy + 2);
                            }
                            else
                            {
                                g.DrawString(s, myFont, Brushes.White, xx + 2, yy + 2);
                            }
                        }
                    }
                }
            }
            //draw board
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
                        if ((xx == temp.x) && (yy == temp.y) && (temp.squareAvailable == false))
                        {
                            System.Media.SystemSounds.Beep.Play();
                        }
                        if ((temp.isQS == false) && (xx == temp.x) && (yy == temp.y) && (temp.squareAvailable == true))
                        {
                            temp.isQS = true;       //disable current spot and add queen
                            temp.squareAvailable = false;
                            queenCount = queenCount + 1;
                            label1.Text = String.Format("You have " + queenCount + " queens on the board.");
                            this.Invalidate();

                            int n;                      //disable horizontal and vertical moves
                            for (n = 0; n < 8; n++)
                            {
                                Square temp2 = (Square)arrayAvail[n, y];
                                temp2.squareAvailable = false;
                            }
                            for (n = 0; n < 8; n++)
                            {
                                Square temp3 = (Square)arrayAvail[x, n];
                                temp3.squareAvailable = false;
                            }
                            int m = x;
                            n = y;//disable diagonal moves
                            while (n >= 0 && m < 8)             //upper right
                            {
                                Square temp4 = (Square)arrayAvail[m, n];
                                temp4.squareAvailable = false;
                                n = n - 1;  //moving up one row
                                m = m + 1;  //moving one column to the right
                                //break;
                            }
                            m = x;
                            n = y;
                            while (n >= 0 && m >= 0)             //upper left
                            {
                                Square temp4 = (Square)arrayAvail[m, n];
                                temp4.squareAvailable = false;
                                n = n - 1;  //moving up one row
                                m = m - 1;  //moving one column to the right
                                //break;
                            }
                            m = x;
                            n = y;
                            while (n < 8 && m >= 0)             //lower left
                            {
                                Square temp4 = (Square)arrayAvail[m, n];
                                temp4.squareAvailable = false;
                                n = n + 1;  //moving up one row
                                m = m - 1;  //moving one column to the right
                                //break;
                            }
                            m = x;
                            n = y;
                            while (n < 8 && m < 8)             //lower right
                            {
                                Square temp4 = (Square)arrayAvail[m, n];
                                temp4.squareAvailable = false;
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
                        if ((temp.isQS == true) && (xx == temp.x) && (yy == temp.y))
                        {
                            temp.isQS = false;       //disable current spot and add queen
                            temp.squareAvailable = true;
                            queenCount = queenCount - 1;
                            label1.Text = String.Format("You have " + queenCount + " queens on the board.");
                            this.Invalidate();

                            int n;                      //disable horizontal and vertical moves
                            for (n = 0; n < 8; n++)
                            {
                                Square temp2 = (Square)arrayAvail[n, y];
                                temp2.squareAvailable = true;
                            }
                            for (n = 0; n < 8; n++)
                            {
                                Square temp3 = (Square)arrayAvail[x, n];
                                temp3.squareAvailable = true;
                            }
                            int m = x;
                            n = y;//disable diagonal moves
                            while (n >= 0 && m < 8)             //upper right
                            {
                                Square temp4 = (Square)arrayAvail[m, n];
                                temp4.squareAvailable = true;
                                n = n - 1;  //moving up one row
                                m = m + 1;  //moving one column to the right
                                //break;
                            }
                            m = x;
                            n = y;
                            while (n >= 0 && m >= 0)             //upper left
                            {
                                Square temp4 = (Square)arrayAvail[m, n];
                                temp4.squareAvailable = true;
                                n = n - 1;  //moving up one row
                                m = m - 1;  //moving one column to the right
                                //break;
                            }
                            m = x;
                            n = y;
                            while (n < 8 && m >= 0)             //lower left
                            {
                                Square temp4 = (Square)arrayAvail[m, n];
                                temp4.squareAvailable = true;
                                n = n + 1;  //moving up one row
                                m = m - 1;  //moving one column to the right
                                //break;
                            }
                            m = x;
                            n = y;
                            while (n < 8 && m < 8)             //lower right
                            {
                                Square temp4 = (Square)arrayAvail[m, n];
                                temp4.squareAvailable = true;
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
                        if ((temp.isQS == true))       //replace deleted spaces
                        {
                            temp.isQS = true;       //disable current spot and add queen
                            temp.squareAvailable = false;
                            label1.Text = String.Format("You have " + queenCount + " queens on the board.");
                            this.Invalidate();

                            int n;                      //disable horizontal and vertical moves
                            for (n = 0; n < 8; n++)
                            {
                                Square temp2 = (Square)arrayAvail[n, y];
                                temp2.squareAvailable = false;
                            }
                            for (n = 0; n < 8; n++)
                            {
                                Square temp3 = (Square)arrayAvail[x, n];
                                temp3.squareAvailable = false;
                            }
                            int m = x;
                            n = y;//disable diagonal moves
                            while (n >= 0 && m < 8)             //upper right
                            {
                                Square temp4 = (Square)arrayAvail[m, n];
                                temp4.squareAvailable = false;
                                n = n - 1;  //moving up one row
                                m = m + 1;  //moving one column to the right
                                //break;
                            }
                            m = x;
                            n = y;
                            while (n >= 0 && m >= 0)             //upper left
                            {
                                Square temp4 = (Square)arrayAvail[m, n];
                                temp4.squareAvailable = false;
                                n = n - 1;  //moving up one row
                                m = m - 1;  //moving one column to the right
                                //break;
                            }
                            m = x;
                            n = y;
                            while (n < 8 && m >= 0)             //lower left
                            {
                                Square temp4 = (Square)arrayAvail[m, n];
                                temp4.squareAvailable = false;
                                n = n + 1;  //moving up one row
                                m = m - 1;  //moving one column to the right
                                //break;
                            }
                            m = x;
                            n = y;
                            while (n < 8 && m < 8)             //lower right
                            {
                                Square temp4 = (Square)arrayAvail[m, n];
                                temp4.squareAvailable = false;
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
