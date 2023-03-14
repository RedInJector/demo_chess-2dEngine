using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;

namespace chess.Mark1Engine.BasicPieces
{
    public abstract class AbstractPiece
    {
        public Vector2 Position { get; set; }
        public Vector2 Scale { get; set; }
        public Image sprite { get; set; }
        public bool side { get; set; }
        public int moves { get; set; }
        public Bitmap bitmap { get; private set; }
        public char tag { get; set; }
        private static Image SpriteSheetimage { get; set; }
        public readonly Rectangle WK = new Rectangle(0, 0, 128, 128);
        public readonly Rectangle WQ = new Rectangle(128, 0, 128, 128);
        public readonly Rectangle WB = new Rectangle(256, 0, 128, 128);
        public readonly Rectangle WN = new Rectangle(384, 0, 128, 128);
        public readonly Rectangle WR = new Rectangle(512, 0, 128, 128);
        public readonly Rectangle WP = new Rectangle(640, 0, 128, 128);

        public readonly Rectangle BK = new Rectangle(0, 128, 128, 128);
        public readonly Rectangle BQ = new Rectangle(128, 128, 128, 128);
        public readonly Rectangle BB = new Rectangle(256, 128, 128, 128);
        public readonly Rectangle BN = new Rectangle(384, 128, 128, 128);
        public readonly Rectangle BR = new Rectangle(512, 128, 128, 128);
        public readonly Rectangle BP = new Rectangle(640, 128, 128, 128);

        public readonly Color BLUE = Color.FromArgb(200, 0, 162, 255);
        public readonly Color PURPULE = Color.FromArgb(200, 214, 48, 240);

        public AbstractPiece()
        {
            Scale = new Vector2(64, 64);
            moves = 0;
            SpriteSheetimage = Image.FromFile("Sprites/Pack1.png");
            bitmap = new Bitmap(SpriteSheetimage);
            sprite = new Bitmap(this.Scale.x, this.Scale.y);
            //side = false;
        }
        public bool getSide()
        {
            return this.side;
        }


        public abstract void Move();
        public abstract void CalculatePossibleMoves();
        public abstract void CalculateAttackSquares(bool[] a);

        public int GetMapPosition()
        {
            return ((this.Position.x / 64) + (this.Position.y / 64) * 8); ;
        }
        public bool IsType(Char c)
        {
            if (this.tag.ToString().ToLower() == "q" && c.ToString().ToLower() == "b")
                return true;
            if (this.tag.ToString().ToLower() == "q" && c.ToString().ToLower() == "r")
                return true;

            if (this.tag.ToString().ToLower() == c.ToString().ToLower())
                return true;

            return false;
        }

        public void RegisterPiece()
        {
            Engine.RegisterPiece(this);
        }
        public void DestroySelf()
        {
            Engine.UnRegisterPiece(this);
        }
    }
}
