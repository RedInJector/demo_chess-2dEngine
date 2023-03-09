using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.CompilerServices;

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

        private static List<Shape2D> AllShapes = new List<Shape2D>();
        private static List<Sprite> AllSprites = new List<Sprite>();


        public Engine(Vector2 screenSize, string Title)
        {
            this.screenSize = screenSize;
            this.Title = Title;

            Window = new Canvas();
            Window.Size = new Size((int)this.screenSize.x, (int)this.screenSize.y);
            Window.Text = this.Title;
            Window.Paint += Renderer;
            Window.MouseClick += MouseEvents;

            OnLoad();

            GameLoopThread = new Thread(GameLoop);
            GameLoopThread.Start();

            Application.Run(Window);
        }
        public abstract void OnLoad();
        public abstract void OnUpdate();
        public abstract void OnDraw();
        public abstract void Mouse(MouseEventArgs e);
        public void MouseEvents(object sender, MouseEventArgs e)
        {
            //Console.WriteLine(e.X + "     " + e.Y);
            Mouse(e);
        }

        public static void RegisterShape(Shape2D shape)
        {
            AllShapes.Add(shape);
        }
        public static void UnRegisterShape(Shape2D shape)
        {
            AllShapes.Remove(shape);
        }

        public static void RegisterSprite(Sprite sprite)
        {
            AllSprites.Add(sprite);
        }
        public static void UnRegisterSprite(Sprite sprite)
        {
            AllSprites.Remove(sprite);
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
                    Thread.Sleep(5);
                }catch {
                    Console.WriteLine("Loading...");
                }
            }
        }

        private void Renderer(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.Black);

            foreach(Shape2D shape in AllShapes)
            {
                g.FillRectangle(new SolidBrush(shape.color), shape.Position.x, shape.Position.y, shape.Scale.x, shape.Scale.y);
            }
            foreach(Sprite sprite in AllSprites)
            {
                g.DrawImage(sprite.image, sprite.Position.x, sprite.Position.y);
            }


        }

    }


 
}
