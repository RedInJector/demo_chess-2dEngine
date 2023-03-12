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

        Tile[] Map = new Tile[64];
        PossibleMove[]Move = new PossibleMove[64];

        Vector2 MousePressedTile = null;
        Vector2 MouseReleasedTile = null;

        int previousSelectedPos = -1;

        Color RED = Color.Red;
        Color GREEN = Color.Green;

        



        public override void OnDraw()
        {
            
        }

        public override void OnLoad()
        {
            Color color;
            bool b = true;
            int pos = 0;
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
                    Map[pos] = new Tile(new Vector2(j * 64, i * 64), new Vector2(64, 64), color, "Tile");
                    pos++;

                }
                b = !b;
            }


            string position = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR";
            string numbers = "12345678";
            position.Reverse();

            int x = 0;
            int y = 0;
            pos = 0;
            foreach (char c in position)
            {
                //Console.WriteLine(c);
                //Console.WriteLine(pos);
                if (c == '/')
                {
                    y++;
                    x = 0;
                    
                    continue;
                }
                //Console.WriteLine(x + "   " + y);
                if (numbers.Contains(c)) {
                    x = x + int.Parse(c.ToString());
                    pos = pos + int.Parse(c.ToString());
                    continue;
                }

                if (!numbers.Contains(c))
                {
                    Map[pos].PieceOnTop = new Piece(new Vector2(x * 64, y * 64), new Vector2(64, 64), c);
                    x = x + 1;
                    pos++;
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
        private bool currentMove = true;
        public override void MouseUp(MouseEventArgs e)
        {
            //bool wasPressed = false;
            int mouseReleaseX = e.Location.X;
            int mouseReleaseY = e.Location.Y;

            MouseReleasedTile = new Vector2((mouseReleaseX - mouseReleaseX % 64) / 64,
        (mouseReleaseY - mouseReleaseY % 64) / 64);

            int posP = (MousePressedTile.x + MousePressedTile.y * 8) ;
            int posR = (MouseReleasedTile.x + MouseReleasedTile.y * 8);
            //Console.WriteLine(Map[pos].PieceOnTop.tag);


            Console.WriteLine(posP);
            if (posP == posR)
            {
                if (Map[posP].hasPiece() && !isWaitingForSecondClick)
                {
                    if(Map[posP].PieceSide() == currentMove)
                    {
                        isWaitingForSecondClick = true;

                        Map[posP].color = RED;
                        previousSelectedPos = posP;

                        ClearPossibleMoves();
                        LegalMove.Legal(Map[posP].PieceOnTop, Map, Move);
                        
                    }
                }
                else if(Map[posP].hasPiece() && isWaitingForSecondClick)
                {
                    if(Map[posP].PieceOnTop.side == Map[previousSelectedPos].PieceOnTop.side)
                    {
                        Map[previousSelectedPos].restoreColor();
                        Map[posP].color = RED;
                        previousSelectedPos = posP;

                        ClearPossibleMoves();
                        LegalMove.Legal(Map[posP].PieceOnTop, Map, Move);
                    }
                    else
                    {
                        Map[previousSelectedPos].restoreColor();
                        Map[previousSelectedPos].Eat(Map[posP]);
                        //Map[posP].Eat(Map[previousSelectedPos]);

                        isWaitingForSecondClick = false;

                        currentMove = !currentMove;

                        ClearPossibleMoves();
                    }  
                }
                else if (isWaitingForSecondClick)
                {
                    Map[previousSelectedPos].restoreColor();
                    //Map[posP].Eat(Map[previousSelectedPos]);
                    Map[previousSelectedPos].Eat(Map[posP]);
                    isWaitingForSecondClick = false;
                    currentMove = !currentMove;
                    ClearPossibleMoves();
                }
            }


            drawBoard();
        }

        
        public void ClearPossibleMoves()
        {
            PossibleMove.DestroyALL();
            for (int i = 0; i < 64; i++)
                if (Move[i] != null)
                    Move[i] = null;
                
        }

        

        /*
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
        */
        public void drawBoard()
        {
            for (int i = 1; i <= 64; i++)
            {
                if (Map[i-1].PieceOnTop != null)
                    Console.Write(Map[i-1].PieceOnTop.tag);
                else Console.Write('`');
                if((i) % 8 == 0)
                    Console.Write("\n");
            }
        }

        
    }
}
