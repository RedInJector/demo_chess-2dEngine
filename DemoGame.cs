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

            //drawPossibleMoves();

            Console.WriteLine();
            Console.WriteLine("Click: " + MousePressedTile.ToString());
        }


        private bool isWaitingForSecondClick = false;
        public override void MouseUp(MouseEventArgs e)
        {
            bool wasPressed = false;
            int mouseReleaseX = e.Location.X;
            int mouseReleaseY = e.Location.Y;

            //Console.WriteLine("\n\n\n LEGAL MOVE: " + );
            if (previousSelectedTile == null || Map[MousePressedTile.x, MousePressedTile.y].PieceOnTop.side == 1))
                ClearPossibleMoves();

                

            MouseReleasedTile = new Vector2((mouseReleaseX - mouseReleaseX % 64) / 64,
        (mouseReleaseY - mouseReleaseY % 64) / 64);
            Console.WriteLine("Release: " + MouseReleasedTile.ToString());


            if ((Map[MousePressedTile.x, MousePressedTile.y].hasPiece() && previousSelectedTile == null) || Map[MousePressedTile.x, MousePressedTile.y].PieceOnTop.side == 1)
            {
                LegalMove.Legal(Map[MousePressedTile.x, MousePressedTile.y].PieceOnTop,
                    Map, Move);
                
            }
            drawPossibleMoves();

            if (previousSelectedTile != null && Move[MouseReleasedTile.x, MouseReleasedTile.y] != null)
            {
                wasPressed = true;
                Console.WriteLine("Previous: " + previousSelectedTile.ToString());
                //Map[MousePressedTile.x, MousePressedTile.y].color = RED;
                if (!Map[MousePressedTile.x, MousePressedTile.y].hasPiece() )
                {
                    Map[MousePressedTile.x, MousePressedTile.y].setPiece(
                        Map[previousSelectedTile.x, previousSelectedTile.y].PieceOnTop);
                    Map[MousePressedTile.x, MousePressedTile.y].PieceOnTop.firstmove = false;

                    Map[previousSelectedTile.x, previousSelectedTile.y].removePiece();
                    Map[previousSelectedTile.x, previousSelectedTile.y].restoreColor();

                    Map[MousePressedTile.x, MousePressedTile.y].restoreColor();
                    Map[MouseReleasedTile.x, MouseReleasedTile.y].restoreColor();

                    isWaitingForSecondClick = false;
                    previousSelectedTile = null;
                }
                else if (Map[MousePressedTile.x, MousePressedTile.y].hasPiece())
                {
                    if (Map[MousePressedTile.x, MousePressedTile.y].PieceSide() !=
                        Map[previousSelectedTile.x, previousSelectedTile.y].PieceSide())
                    {
                        Map[MousePressedTile.x, MousePressedTile.y].eatPiece();
                        Map[MousePressedTile.x, MousePressedTile.y].setPiece(
                            Map[previousSelectedTile.x, previousSelectedTile.y].PieceOnTop);

                        Map[MousePressedTile.x, MousePressedTile.y].PieceOnTop.firstmove = false;

                        Map[previousSelectedTile.x, previousSelectedTile.y].removePiece();
                        Map[previousSelectedTile.x, previousSelectedTile.y].restoreColor();

                        isWaitingForSecondClick = false;
                        previousSelectedTile = null;
                    }
                    else
                    {
                        Map[previousSelectedTile.x, previousSelectedTile.y].restoreColor();
                        previousSelectedTile = MousePressedTile;
                        wasPressed = false;
                        Map[MousePressedTile.x, MousePressedTile.y].color = RED;
                    }
                }

                //previousSelectedTile = null;
            }
            
            if(!MouseReleasedTile.Equals(MousePressedTile) && !isWaitingForSecondClick)
            {
                previousSelectedTile = null;
                if (!Map[MouseReleasedTile.x, MouseReleasedTile.y].hasPiece())
                {
                    Map[MouseReleasedTile.x, MouseReleasedTile.y].setPiece(
                            Map[MousePressedTile.x, MousePressedTile.y].PieceOnTop);

                    Map[MouseReleasedTile.x, MouseReleasedTile.y].PieceOnTop.firstmove = false;

                    Map[MousePressedTile.x, MousePressedTile.y].removePiece();
                }
                else if (Map[MouseReleasedTile.x, MouseReleasedTile.y].hasPiece())
                {
                    if (Map[MouseReleasedTile.x, MouseReleasedTile.y].PieceSide() !=
                        Map[MousePressedTile.x, MousePressedTile.y].PieceSide())
                    {

                        Map[MouseReleasedTile.x, MouseReleasedTile.y].eatPiece();
                        Map[MouseReleasedTile.x, MouseReleasedTile.y].setPiece(
                            Map[MousePressedTile.x, MousePressedTile.y].PieceOnTop);

                        Map[MouseReleasedTile.x, MouseReleasedTile.y].PieceOnTop.firstmove = false;

                        Map[MousePressedTile.x, MousePressedTile.y].removePiece();

                    }
                    else
                    {

                    }
                    

                }
                Map[MousePressedTile.x, MousePressedTile.y].restoreColor();
            }

            

            if (MouseReleasedTile.Equals(MousePressedTile) && 
                Map[MousePressedTile.x, MousePressedTile.y].hasPiece() &&
                wasPressed == false && 
                previousSelectedTile == null)
            {
                //isWaitingForSecondClick = true;
                previousSelectedTile = MousePressedTile;
                Map[MousePressedTile.x, MousePressedTile.y].color = RED;
            }

            if (wasPressed)
            {
                ClearPossibleMoves();
            }

            
            drawBoard();
        }

        public void ClearPossibleMoves()
        {
            PossibleMove.DestroyALL();
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    if (Move[j, i] != null)
                        Move[j, i] = null;
                }
        }
        public void drawPossibleMoves()
        {
            Console.WriteLine("\n");
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    if (j == 0) Console.Write("\n");
                    if (Move[j, i] != null)
                        Console.Write("#");
                    else Console.Write("`");

                }
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
