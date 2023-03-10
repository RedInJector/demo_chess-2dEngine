using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public  Color color;
        private Color originalColor;
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

        public void setPiece(Piece piece)
        {
            this.PieceOnTop = piece;
            this.PieceOnTop.Position = this.Position;
        }

        public void removePiece()
        {
            this.PieceOnTop = null;
        }
        public void eatPiece()
        {
            this.PieceOnTop.DestroySelf();
            this.PieceOnTop = null;
        }

        public int PieceSide()
        {
            if(!hasPiece())
                return -1;
            return this.PieceOnTop.side;
        }

        public bool hasPiece()
        {
            if (this.PieceOnTop != null)
                return true;
            return false;
        }

        public void restoreColor()
        {
            this.color = this.originalColor;
        }

    }
}
