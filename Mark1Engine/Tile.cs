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

       /* public void setPiece(Piece piece)
        {
            this.PieceOnTop = piece;
            this.PieceOnTop.Position = this.Position;
        }*/
        public void Eat(Tile WhoAte)
        {
            if (this.PieceOnTop != null)
            {
                this.PieceOnTop.DestroySelf();
                this.PieceOnTop = null;
            }
            this.PieceOnTop = WhoAte.PieceOnTop;
            WhoAte.PieceOnTop = null;
            this.PieceOnTop.Position = this.Position;

        }

        public void Move(Tile where)
        {

        }

        public bool PieceSide()
        {
            return this.PieceOnTop.side;
        }

        public bool hasPiece()
        {
            if (this.PieceOnTop != null)
                return true;
            return false;
        }

        public char PieceTag()
        {
            if (PieceOnTop != null)
                return PieceOnTop.tag;
            return '0';
        }

        public void restoreColor()
        {
            this.color = this.originalColor;
        }

    }
}
