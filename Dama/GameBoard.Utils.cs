using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dama
{
    partial class GameBoard
    {
        Panel[,] GameBoardTable = new Panel[4, 4];
        double[,] GameBoardLayout = new double[3, 5] { { 75, 255, 64, 64, 64 }, { 0.5, 255, 200, 200, 200 }, { 128, 255, 0, 0, 255 } };
        Panel[] GameBoardPlacesArray = new Panel[8*8];
        private void InitRender()
        {
            Screen screen = Screen.FromControl(this);
            this.Width = screen.WorkingArea.Width;
            this.Height = screen.WorkingArea.Height;
            this.Location = new Point(screen.Bounds.X, screen.Bounds.Y);
            this.WindowState = FormWindowState.Normal;
            this.TopMost = false;
            int gap = 3;
            int locationX = 0;
            for (int i = 0; i < 2; i++)
            {
                int div = (int)Math.Round((Double)(GameBoardLayout[i, 0] * this.Width) / 100);
                Panel panel = new Panel();
                panel.Name = $"GameBoardPanel_{i}";
                panel.Size = new Size(div, this.Height);
                panel.BorderStyle = BorderStyle.None;
                panel.Location = new Point(locationX, 0);
                panel.BackColor = Color.FromArgb(
                    Convert.ToInt32(GameBoardLayout[i, 1]),
                    Convert.ToInt32(GameBoardLayout[i, 2]),
                    Convert.ToInt32(GameBoardLayout[i, 3]),
                    Convert.ToInt32(GameBoardLayout[i, 4])
                    );
                this.Controls.Add(panel);
                locationX += div;
                panel.Visible = true;
            }
            int boardSize;
            Panel myPanel = (Panel)Controls["GameBoardPanel_0"];
            if (myPanel.Width > myPanel.Height)
            {
                boardSize = myPanel.Height;
            }
            else
            {
                boardSize = myPanel.Width;
            }
            Panel board = new Panel();
            board.Name = $"BoardPanel";
            board.Size = new Size(boardSize, boardSize);
            board.BorderStyle = BorderStyle.None;
            board.Location = new Point((myPanel.Width - board.Width) / 2, (myPanel.Height - board.Height) / 2);
            board.BackColor = Color.Magenta;
            board.Margin = new Padding(0,0,0,0);
            myPanel.Controls.Add(board);
            board.Visible = true;

            int boardPlace_Width = (board.Width / 8)+1;
            int counter = 0;
            
            Color color = Color.White;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Panel boardPlace = new Panel();
                    boardPlace.Name = $"BoardPlace_{counter}";
                    boardPlace.Margin = new Padding(0, 0, 0, 0);
                    boardPlace.Size = new Size(boardPlace_Width, boardPlace_Width);
                    boardPlace.BorderStyle = BorderStyle.None;
                    boardPlace.Location = new Point(j*boardPlace_Width, i * boardPlace_Width);
                    
                    //boardPlace.BackgroundImage = global::Dama.Properties.Resources.Dark;
                    boardPlace.BackgroundImageLayout = ImageLayout.Stretch;
                    boardPlace.BackColor = color;
                    board.Controls.Add(boardPlace);
                    boardPlace.Visible = true;
                    GameBoardPlacesArray[counter] = boardPlace;

                    if (color == Color.White)
                    {
                        color = Color.Black;
                    }
                    else
                    {
                        color = Color.White;
                    }
                    counter++;
                }
                if (color == Color.White)
                {
                    color = Color.Black;
                }
                else
                {
                    color = Color.White;
                }
            }

            Bitmap[] image = new Bitmap[2] { global::Dama.Properties.Resources.Dark, global::Dama.Properties.Resources.Light };

            bool empty = false;
            int[] offset = new int[4] { 0, 8,48,56 };

            Bitmap img = image[0];
            for (int i = 0; i < 4; i++)
            {
                if (i > 1)
                {
                    img = image[1];
                    
                }
                for (int j = 0; j < 8; j++)
                {
                    if (empty)
                    {
                        GameBoardPlacesArray[j+ offset[i]].BackgroundImage = null;
                        empty = false;
                    }
                    else
                    {
                        GameBoardPlacesArray[j + offset[i]].BackgroundImage = img;
                        empty = true;
                    }
                
                }
                if (empty)
                {
                    empty = false;
                }
                else
                {
                    empty = true;
                }


            }
        }
        private void RenderGameBoard(Panel panel,Panel board)
        {
            
            int boardSize;

            panel.Height = this.Height;
            
            if (panel.Width > panel.Height)
            {
                boardSize = panel.Height;
            }
            else
            {
                boardSize = panel.Width;
            }
            board.Size = new Size(boardSize, boardSize);
            board.Location = new Point((panel.Width - board.Width) / 2, (panel.Height - board.Height) / 2);
        }

        private void GameBoardKeyChk(object sender, KeyEventArgs e)
        {
            return;
            /*
            Panel myPanel = (Panel)Controls["GameBoardPanel_0"];
            Panel board = (Panel)myPanel.Controls["BoardPanel"];
            if (e.Alt && e.KeyCode == Keys.Enter)
            {
                if (this.WindowState == FormWindowState.Maximized)
                {
                    this.WindowState = FormWindowState.Normal;
                    //this.TopMost = false;
                    RenderGameBoard(myPanel, board);
                }
                else
                {
                    this.WindowState = FormWindowState.Maximized;
                    //this.TopMost = true;
                    RenderGameBoard(myPanel, board);
                }
            }
            */
        }
    }
}
