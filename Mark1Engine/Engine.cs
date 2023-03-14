using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.CompilerServices;
using chess.Mark1Engine.BasicPieces;

namespace chess.Mark1Engine
{
    class Canvas : Form
    {
        public Canvas()
        {
            this.DoubleBuffered = true;
        }
    }


    public abstract class Engine
    {
        private Vector2 screenSize = new Vector2(512, 512);
        private string Title;
        private Canvas Window = null;
        private Thread GameLoopThread = null;

        private static List<Tile> AllShapes = new List<Tile>();
        private static List<AbstractPiece> AllAbstractPieces = new List<AbstractPiece>();
        private static List<PossibleMove> PossibleMoves = new List<PossibleMove>();


        public Engine(Vector2 screenSize, string Title)
        {
            this.screenSize = screenSize;
            this.Title = Title;

            Window = new Canvas();
            Window.Size = new Size((int)this.screenSize.x, (int)this.screenSize.y);
            Window.Text = this.Title;
            Window.Paint += Renderer;
            Window.MouseDown += MouseD;
            Window.MouseUp += MouseU;

            OnLoad();

            GameLoopThread = new Thread(GameLoop);
            GameLoopThread.Start();

            Application.Run(Window);
        }
        public abstract void OnLoad();
        public abstract void OnUpdate();
        public abstract void OnDraw();
        public abstract void MouseDown(MouseEventArgs e);
        public abstract void MouseUp(MouseEventArgs e);
        public void MouseD(object sender, MouseEventArgs e)
        {
            //Console.WriteLine(e.X + "     " + e.Y);
            MouseDown(e);
        }
        public void MouseU(object sender, MouseEventArgs e)
        {
            MouseUp(e);
        }

        public static void RegisterShape(Tile shape)
        {
            AllShapes.Add(shape);
        }
        public static void UnRegisterShape(Tile shape)
        {
            AllShapes.Remove(shape);
        }
        public static void UnRegisterAllPossibleMoves()
        {
            PossibleMoves.Clear();
        }

        public static void RegisterPossibleMoves(PossibleMove moves)
        {
            PossibleMoves.Add(moves);
        }
        public static void UnRegisterPossibleMoves(PossibleMove moves)
        {
            PossibleMoves.Remove(moves);
        }

        public static void RegisterPiece(AbstractPiece piece)
        {
            AllAbstractPieces.Add(piece);
        }
        public static void UnRegisterPiece(AbstractPiece piece)
        {
            AllAbstractPieces.Remove(piece);
        }


        void GameLoop()
        {
            while (GameLoopThread.IsAlive)
            {
                if (Window.IsDisposed)
                {
                    this.GameLoopThread.Abort();
                }
                try
                {
                    OnDraw();
                    Window.BeginInvoke((MethodInvoker)delegate { Window.Refresh(); });
                    OnUpdate();
                    Thread.Sleep(50);
                }catch {
                    Console.WriteLine("Loading...");
                }
            }
        }

        private void Renderer(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.Black);

            foreach(Tile shape in AllShapes)
            {
                g.FillRectangle(new SolidBrush(shape.color), shape.Position.x, shape.Position.y, shape.Scale.x, shape.Scale.y);
            }
            if(PossibleMoves.Count != 0)
                foreach(PossibleMove move in PossibleMoves)
                {
                    g.FillRectangle(new SolidBrush(move.color), move.Position.x, move.Position.y, move.Scale.x, move.Scale.y);
                }

            foreach (AbstractPiece piece in AllAbstractPieces)
            {
                g.DrawImage(piece.sprite, piece.Position.x, piece.Position.y);
            }

        }

    }


 
}
