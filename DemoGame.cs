using chess.Mark1Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace chess
{
    class DemoGame : Engine
    {
        public DemoGame() : base(new Vector2(528, 551), "Engine Demo") { }

        Tile[,] Map = new Tile[8, 8];

        Vector2 SelectedTile = new Vector2();
        Vector2 previousSelectedTile = null;

        int mousePressX;
        int mousePressY;



        public override void OnDraw()
        {
            
        }

        public override void OnLoad()
        {
            Color color;
            bool b = true;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (b)
                    {
                        color = Color.FromArgb(255, 255, 204, 110);
                    }
                    else
                    {
                        color = Color.FromArgb(255, 176, 121, 21);
                    }
                    b = !b;
                    Map[i, j] = new Tile(new Vector2(i * 64, j * 64), new Vector2(64, 64), color, "Tile");

                }
                b = !b;
            }

            string position = "8/5k2/3p4/1p1Pp2p/pP2Pp1P/P4P1K/8/8";
            string numbers = "12345678";
            position.Reverse();

            int x = 0;
            int y = 0;
            foreach (char c in position)
            {
                //Console.WriteLine(c);

                if (c == '/')
                {
                    y++;
                    x = 0;
                    continue;
                }
                //Console.WriteLine(x + "   " + y);
                if (numbers.Contains(c)) {
                    x = x + int.Parse(c.ToString());

                }

                if (!numbers.Contains(c))
                {
                    Map[x,y].PieceOnTop = new Piece(new Vector2(x * 64, y * 64), new Vector2(64, 64), c.ToString(), c);
                    x = x + 1;
                }

            }


        }

        public override void OnUpdate()
        {

        }

        public override void Mouse(MouseEventArgs e)
        {
            mousePressX = e.X;
            mousePressY = e.Y;

            SelectedTile = new Vector2 ((mousePressX - mousePressX % 64)/64,
        (mousePressY - mousePressY % 64)/64);

                


            if (previousSelectedTile != null && SelectedTile != null) {
                if (Map[(int)SelectedTile.x, (int)SelectedTile.y].PieceOnTop == null && 
                            Map[(int)previousSelectedTile.x, (int)previousSelectedTile.y].PieceOnTop != null)
                {
                    Map[(int)SelectedTile.x, (int)SelectedTile.y].PieceOnTop =
                        Map[(int)previousSelectedTile.x, (int)previousSelectedTile.y].PieceOnTop;

                    Map[(int)previousSelectedTile.x, (int)previousSelectedTile.y].PieceOnTop = null;

                    Map[(int)SelectedTile.x, (int)SelectedTile.y].PieceOnTop.Position =
                        Map[(int)SelectedTile.x, (int)SelectedTile.y].Position;

                    Map[(int)previousSelectedTile.x, (int)previousSelectedTile.y].color =
                        Map[(int)previousSelectedTile.x, (int)previousSelectedTile.y].originalColor;
                }

                else if (Map[(int)previousSelectedTile.x, (int)previousSelectedTile.y].PieceOnTop.side ==
                    Map[(int)SelectedTile.x, (int)SelectedTile.y].PieceOnTop.side)
                {
                    Map[(int)previousSelectedTile.x, (int)previousSelectedTile.y].color =
                Map[(int)previousSelectedTile.x, (int)previousSelectedTile.y].originalColor;

                    Map[(int)SelectedTile.x, (int)SelectedTile.y].color = Color.Red;
                } 
                else
                {
                    Map[(int)SelectedTile.x, (int)SelectedTile.y].PieceOnTop.DestroySelf();

                    Map[(int)SelectedTile.x, (int)SelectedTile.y].PieceOnTop =
                        Map[(int)previousSelectedTile.x, (int)previousSelectedTile.y].PieceOnTop;

                    Map[(int)SelectedTile.x, (int)SelectedTile.y].PieceOnTop.Position =
                        Map[(int)SelectedTile.x, (int)SelectedTile.y].Position;

                    Map[(int)previousSelectedTile.x, (int)previousSelectedTile.y].color =
                        Map[(int)previousSelectedTile.x, (int)previousSelectedTile.y].originalColor;

                    //tiles[(int)previousSelectedTile.x, (int)previousSelectedTile.y].PieceOnTop.DestroySelf();
                    Map[(int)previousSelectedTile.x, (int)previousSelectedTile.y].PieceOnTop = null;

                }

                SelectedTile = null;
            }
            

            //tiles[(int)SelectedTile.x, (int)SelectedTile.y].color = Color.Red;

            previousSelectedTile = SelectedTile;


            //tiles[TileX, TileY].PieceOnTop.DestroySelf();
            
        }
    }
}
