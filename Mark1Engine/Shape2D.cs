using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess.Mark1Engine
{
    public class Shape2D
    {

        public Vector2 Position = null;
        public Vector2 Scale = null;
        public string tag = "";
        public Color color;
        public Sprite PieceOnTop { get; set; }

        public Shape2D(Vector2 position, Vector2 scale, Color color, string tag)
        {
            this.Position = position;
            this.Scale = scale;
            this.tag = tag;
            this.color = color;
            

            Engine.RegisterShape(this);
        }

        public void DestroySelf()
        {
            Engine.UnRegisterShape(this);
        }


    }
}
