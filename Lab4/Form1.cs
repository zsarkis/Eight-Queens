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

namespace Lab4
{
    public partial class Form1 : Form
    {
        SqObj[,] arrayAvail = new SqObj[8, 8];
        int QCount = 0;

        public Form1()
        {
            int x = 100;
            int y = 100;
            Point po = new Point(x, y);
            //x = 1;
            //y = x;
            for (x = 0; x < 8; x++)
            {
                for (y = 0; y < 8; y++)
                {
                    if((x+y) % 2 == 0)
                    {
                        SqObj p = new SqObj(po, false, false, true); //color of square alternate
                        po.Y = po.Y + 50;
                        arrayAvail[x, y] = p;
                    }
                    else
                    {
                        SqObj p = new SqObj(po, false, true, true);
                        po.Y = po.Y + 50;
                        arrayAvail[x, y] = p;
                    }
                }
                po.X = po.X + 50;
                po.Y = 100;
            }
            InitializeComponent();
        }

        private void Clear_Click(object sender, EventArgs e)
        { //clears the board
            int x, y;
            for (x = 0; x < 8; x++)
            {
                for (y = 0; y < 8; y++)
                {
                    QCount = 0;
                    label1.Text = String.Format("You have " + QCount + " queens on the board.");
                    SqObj temp = (SqObj)arrayAvail[x, y];
                    temp.isAvailS = true;
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
        {}

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
                    SqObj temp = (SqObj)arrayAvail[x, y];
                    if (temp.sqColS == false)
                    {
                        g.FillRectangle(Brushes.White, horct + x * 50, vertct + y * 50, 49, 49);
                    }
                    if(temp.sqColS == true)
                    {
                        g.FillRectangle(Brushes.Black, horct + x * 50, vertct + y * 50, 49, 49);
                    }
                    if(Hint.Checked && (temp.isAvailS == false))
                    {
                        g.FillRectangle(Brushes.Red, horct + x * 50, vertct + y * 50, 49, 49);
                    }
                    if (temp.isQS == true)
                    {
                        string s;
                        s = "Q";
                        int xx = (((int)temp.x) / 50) * 50; //round location
                        int yy = (((int)temp.y) / 50) * 50;
                        if(Hint.Checked == true)
                        {
                            g.DrawString(s, myFont, Brushes.Black, xx + 2, yy + 2);
                        }
                        if(Hint.Checked == false)
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
                        SqObj temp = (SqObj)arrayAvail[x, y];
                        if ((xx == temp.x) && (yy == temp.y) && (temp.isAvailS == false))
                        {
                            System.Media.SystemSounds.Beep.Play();
                        }
                        if ((temp.isQS == false) && (xx == temp.x) && (yy == temp.y) && (temp.isAvailS == true))
                        {
                            temp.isQS = true;       //disable current spot and add queen
                            temp.isAvailS = false;
                            QCount = QCount + 1;
                            label1.Text = String.Format("You have " + QCount + " queens on the board.");
                            this.Invalidate();

                            int n;                      //disable horizontal and vertical moves
                            for (n = 0; n < 8; n++)
                            {
                                SqObj temp2 = (SqObj)arrayAvail[n, y];
                                temp2.isAvailS = false;
                            }
                            for (n = 0; n < 8; n++)
                            {
                                SqObj temp3 = (SqObj)arrayAvail[x, n];
                                temp3.isAvailS = false;
                            }
                            int m = x;
                            n = y;//disable diagonal moves
                            while (n >= 0 && m < 8)             //upper right
                            {
                                SqObj temp4 = (SqObj)arrayAvail[m, n];
                                temp4.isAvailS = false;
                                n = n - 1;  //moving up one row
                                m = m + 1;  //moving one column to the right
                                //break;
                            }
                            m = x;
                            n = y;
                            while (n >= 0 && m >= 0)             //upper left
                            {
                                SqObj temp4 = (SqObj)arrayAvail[m, n];
                                temp4.isAvailS = false;
                                n = n - 1;  //moving up one row
                                m = m - 1;  //moving one column to the right
                                //break;
                            }
                            m = x;
                            n = y;
                            while (n < 8 && m >= 0)             //lower left
                            {
                                SqObj temp4 = (SqObj)arrayAvail[m, n];
                                temp4.isAvailS = false;
                                n = n + 1;  //moving up one row
                                m = m - 1;  //moving one column to the right
                                //break;
                            }
                            m = x;
                            n = y;
                            while (n < 8 && m < 8)             //lower right
                            {
                                SqObj temp4 = (SqObj)arrayAvail[m, n];
                                temp4.isAvailS = false;
                                n = n + 1;  //moving up one row
                                m = m + 1;  //moving one column to the right
                                //break;
                            }
                        }
                    }
                }
                if(QCount == 8)
                {
                    MessageBox.Show("You did it!","", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
                        SqObj temp = (SqObj)arrayAvail[x, y];
                        if ((temp.isQS == true) && (xx == temp.x) && (yy == temp.y))
                        {
                            temp.isQS = false;       //disable current spot and add queen
                            temp.isAvailS = true;
                            QCount = QCount - 1;
                            label1.Text = String.Format("You have " + QCount + " queens on the board.");
                            this.Invalidate();

                            int n;                      //disable horizontal and vertical moves
                            for (n = 0; n < 8; n++)
                            {
                                SqObj temp2 = (SqObj)arrayAvail[n, y];
                                temp2.isAvailS = true;
                            }
                            for (n = 0; n < 8; n++)
                            {
                                SqObj temp3 = (SqObj)arrayAvail[x, n];
                                temp3.isAvailS = true;
                            }
                            int m = x;
                            n = y;//disable diagonal moves
                            while (n >= 0 && m < 8)             //upper right
                            {
                                SqObj temp4 = (SqObj)arrayAvail[m, n];
                                temp4.isAvailS = true;
                                n = n - 1;  //moving up one row
                                m = m + 1;  //moving one column to the right
                                //break;
                            }
                            m = x;
                            n = y;
                            while (n >= 0 && m >= 0)             //upper left
                            {
                                SqObj temp4 = (SqObj)arrayAvail[m, n];
                                temp4.isAvailS = true;
                                n = n - 1;  //moving up one row
                                m = m - 1;  //moving one column to the right
                                //break;
                            }
                            m = x;
                            n = y;
                            while (n < 8 && m >= 0)             //lower left
                            {
                                SqObj temp4 = (SqObj)arrayAvail[m, n];
                                temp4.isAvailS = true;
                                n = n + 1;  //moving up one row
                                m = m - 1;  //moving one column to the right
                                //break;
                            }
                            m = x;
                            n = y;
                            while (n < 8 && m < 8)             //lower right
                            {
                                SqObj temp4 = (SqObj)arrayAvail[m, n];
                                temp4.isAvailS = true;
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
                        SqObj temp = (SqObj)arrayAvail[x, y];
                        if ((temp.isQS == true) )       //replace deleted spaces
                        {
                            temp.isQS = true;       //disable current spot and add queen
                            temp.isAvailS = false;
                            label1.Text = String.Format("You have " + QCount + " queens on the board.");
                            this.Invalidate();

                            int n;                      //disable horizontal and vertical moves
                            for (n = 0; n < 8; n++)
                            {
                                SqObj temp2 = (SqObj)arrayAvail[n, y];
                                temp2.isAvailS = false;
                            }
                            for (n = 0; n < 8; n++)
                            {
                                SqObj temp3 = (SqObj)arrayAvail[x, n];
                                temp3.isAvailS = false;
                            }
                            int m = x;
                            n = y;//disable diagonal moves
                            while (n >= 0 && m < 8)             //upper right
                            {
                                SqObj temp4 = (SqObj)arrayAvail[m, n];
                                temp4.isAvailS = false;
                                n = n - 1;  //moving up one row
                                m = m + 1;  //moving one column to the right
                                //break;
                            }
                            m = x;
                            n = y;
                            while (n >= 0 && m >= 0)             //upper left
                            {
                                SqObj temp4 = (SqObj)arrayAvail[m, n];
                                temp4.isAvailS = false;
                                n = n - 1;  //moving up one row
                                m = m - 1;  //moving one column to the right
                                //break;
                            }
                            m = x;
                            n = y;
                            while (n < 8 && m >= 0)             //lower left
                            {
                                SqObj temp4 = (SqObj)arrayAvail[m, n];
                                temp4.isAvailS = false;
                                n = n + 1;  //moving up one row
                                m = m - 1;  //moving one column to the right
                                //break;
                            }
                            m = x;
                            n = y;
                            while (n < 8 && m < 8)             //lower right
                            {
                                SqObj temp4 = (SqObj)arrayAvail[m, n];
                                temp4.isAvailS = false;
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


    public class SqObj
    {
        public bool isQS, sqColS, isAvailS;
        public int x, y;
        public SqObj(Point po, bool isQ, bool sqCol, bool isAvail)    //container
        {
            Rectangle myRect = new Rectangle(po.X, po.Y, 50, 50);
            isQS = isQ;
            sqColS = sqCol;
            x = po.X;
            y = po.Y;
            isAvailS = isAvail;
        }
    }
}
