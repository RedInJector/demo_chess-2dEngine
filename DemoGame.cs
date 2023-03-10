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
        PossibleMove[,]Move = new PossibleMove[8, 8];

        Vector2 MousePressedTile = null;
        Vector2 MouseReleasedTile = null;
        Vector2 previousSelectedTile = null;

        Color RED = Color.Red;
        Color GREEN = Color.Green;

        



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

            string position = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR";
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
                    Map[x,y].PieceOnTop = new Piece(new Vector2(x * 64, y * 64), new Vector2(64, 64), c);
                    x = x + 1;
                }

            }


            


        }

        public override void OnUpdate()
        {

        }

        public override void MouseDown(MouseEventArgs e)
        {
            

            int mousePressX = e.X;
            int mousePressY = e.Y;

            MousePressedTile = new Vector2((mousePressX - mousePressX % 64) / 64,
        (mousePressY - mousePressY % 64) / 64);

            if(Map[(int)MousePressedTile.x, (int)MousePressedTile.y].hasPiece())
            {
                new LegalMove(Map[(int)MousePressedTile.x, (int)MousePressedTile.y].PieceOnTop,
                    Map, Move);
            }

            Console.WriteLine();
            Console.WriteLine("Click: " + MousePressedTile.ToString());
        }

        public override void MouseUp(MouseEventArgs e)
        {
            bool wasCklicked = false;
            int mouseReleaseX = e.Location.X;
            int mouseReleaseY = e.Location.Y;

            MouseReleasedTile = new Vector2((mouseReleaseX - mouseReleaseX % 64) / 64,
        (mouseReleaseY - mouseReleaseY % 64) / 64);
            Console.WriteLine("Release: " + MouseReleasedTile.ToString());


            if (previousSelectedTile != null)
            {
                wasCklicked = true;
                Console.WriteLine("Previous: " + previousSelectedTile.ToString());
                //Map[(int)MousePressedTile.x, (int)MousePressedTile.y].color = RED;
                if (!Map[(int)MousePressedTile.x, (int)MousePressedTile.y].hasPiece())
                {
                    Map[(int)MousePressedTile.x, (int)MousePressedTile.y].setPiece(
                        Map[(int)previousSelectedTile.x, (int)previousSelectedTile.y].PieceOnTop);
                    Map[(int)MousePressedTile.x, (int)MousePressedTile.y].PieceOnTop.firstmove = false;

                    Map[(int)previousSelectedTile.x, (int)previousSelectedTile.y].removePiece();
                    Map[(int)previousSelectedTile.x, (int)previousSelectedTile.y].restoreColor();

                    Map[(int)MousePressedTile.x, (int)MousePressedTile.y].restoreColor();
                    Map[(int)MouseReleasedTile.x, (int)MouseReleasedTile.y].restoreColor();

                    Console.WriteLine("ColorRestored");

                    previousSelectedTile = null;
                }
                else if (Map[(int)MousePressedTile.x, (int)MousePressedTile.y].hasPiece())
                {
                    if (Map[(int)MousePressedTile.x, (int)MousePressedTile.y].PieceSide() !=
                        Map[(int)previousSelectedTile.x, (int)previousSelectedTile.y].PieceSide())
                    {
                        Map[(int)MousePressedTile.x, (int)MousePressedTile.y].eatPiece();
                        Map[(int)MousePressedTile.x, (int)MousePressedTile.y].setPiece(
                            Map[(int)previousSelectedTile.x, (int)previousSelectedTile.y].PieceOnTop);

                        Map[(int)MousePressedTile.x, (int)MousePressedTile.y].PieceOnTop.firstmove = false;

                        Map[(int)previousSelectedTile.x, (int)previousSelectedTile.y].removePiece();
                        Map[(int)previousSelectedTile.x, (int)previousSelectedTile.y].restoreColor();

                        previousSelectedTile = null;
                    }
                    else
                    {
                        Map[(int)previousSelectedTile.x, (int)previousSelectedTile.y].restoreColor();
                        previousSelectedTile = MousePressedTile;
                        Map[(int)MousePressedTile.x, (int)MousePressedTile.y].color = RED;
                    }
                }

                //previousSelectedTile = null;
            }
            
            if(!MouseReleasedTile.Equals(MousePressedTile))
            {
                previousSelectedTile = null;
                if (!Map[(int)MouseReleasedTile.x, (int)MouseReleasedTile.y].hasPiece())
                {
                    Map[(int)MouseReleasedTile.x, (int)MouseReleasedTile.y].setPiece(
                            Map[(int)MousePressedTile.x, (int)MousePressedTile.y].PieceOnTop);

                    Map[(int)MouseReleasedTile.x, (int)MouseReleasedTile.y].PieceOnTop.firstmove = false;

                    Map[(int)MousePressedTile.x, (int)MousePressedTile.y].removePiece();
                }
                else if (Map[(int)MouseReleasedTile.x, (int)MouseReleasedTile.y].hasPiece())
                {
                    if (Map[(int)MouseReleasedTile.x, (int)MouseReleasedTile.y].PieceSide() !=
                        Map[(int)MousePressedTile.x, (int)MousePressedTile.y].PieceSide())
                    {

                        Map[(int)MouseReleasedTile.x, (int)MouseReleasedTile.y].eatPiece();
                        Map[(int)MouseReleasedTile.x, (int)MouseReleasedTile.y].setPiece(
                            Map[(int)MousePressedTile.x, (int)MousePressedTile.y].PieceOnTop);

                        Map[(int)MouseReleasedTile.x, (int)MouseReleasedTile.y].PieceOnTop.firstmove = false;

                        Map[(int)MousePressedTile.x, (int)MousePressedTile.y].removePiece();

                    }
                    else
                    {

                    }
                    

                }
                ClearPossibleMoves();
                Map[(int)MousePressedTile.x, (int)MousePressedTile.y].restoreColor();
            }

            if (MouseReleasedTile.Equals(MousePressedTile) && 
                Map[(int)MousePressedTile.x, (int)MousePressedTile.y].hasPiece() && 
                wasCklicked == false && 
                previousSelectedTile == null)
            {
                previousSelectedTile = MousePressedTile;
                Map[(int)MousePressedTile.x, (int)MousePressedTile.y].color = RED;
                Console.WriteLine(previousSelectedTile.ToString());
            }

            if(wasCklicked)
                ClearPossibleMoves();
            drawBoard();
        }

        public void ClearPossibleMoves()
        {
            PossibleMove.DestroyALL();
        }

        public void drawBoard()
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    if (j == 0) Console.Write("\n");
                    if (Map[j, i].PieceOnTop != null)
                        Console.Write(Map[j, i].PieceOnTop.tag);
                    else Console.Write('`');
                    
                }

        }
    }
}
