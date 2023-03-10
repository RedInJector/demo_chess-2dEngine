using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess.Mark1Engine
{
    public class Tile
    {

        public Vector2 Position = null;
        public Vector2 Scale = null;
        public string tag = "";
        public Color color;
        public Color originalColor;
        public Piece PieceOnTop { get; set; }

        public Tile(Vector2 position, Vector2 scale, Color color, string tag)
        {
            this.Position = position;
            this.Scale = scale;
            this.tag = tag;
            this.color = color;
            this.originalColor = color;



            Engine.RegisterShape(this);
        }

        public void DestroySelf()
        {
            Engine.UnRegisterShape(this);
        }


    }
}
