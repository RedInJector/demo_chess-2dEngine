using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess.Mark1Engine
{
    public class Piece1
    {
        public Vector2 Position = null;
        public Vector2 Scale = null;
        public char tag;
        public static Image SpriteSheetimage = Image.FromFile("Sprites/Pack1.png");
        public Image image;
        public bool side = false;
        public int moves = 0;
        public bool firstmove = true;
        Bitmap bitmap = new Bitmap(SpriteSheetimage);
        public bool EnPassantTarget = false;

        Rectangle WK = new Rectangle(0, 0, 128, 128);
        Rectangle WQ = new Rectangle(128, 0, 128, 128);
        Rectangle WB = new Rectangle(256, 0, 128, 128);
        Rectangle WN = new Rectangle(384, 0, 128, 128);
        Rectangle WR = new Rectangle(512, 0, 128, 128);
        Rectangle WP = new Rectangle(640, 0, 128, 128);

        Rectangle BK = new Rectangle(0, 128, 128, 128);
        Rectangle BQ = new Rectangle(128, 128, 128, 128);
        Rectangle BB = new Rectangle(256, 128, 128, 128);
        Rectangle BN = new Rectangle(384, 128, 128, 128);
        Rectangle BR = new Rectangle(512, 128, 128, 128);
        Rectangle BP = new Rectangle(640, 128, 128, 128);

        public Piece1() { }
        public Piece1(Vector2 position, Vector2 scale, char c)
        {
            this.Position = position;
            this.Scale = scale;
            this.tag = c;
            
            image = new Bitmap(scale.x, scale.y);

            Rectangle spriteBounds = WP;

            tag = c;
            switch (c)
            {

                case 'P': spriteBounds = WP; side = true; break;
                case 'N': spriteBounds = WN; side = true; break;
                case 'B': spriteBounds = WB; side = true; break;
                case 'Q': spriteBounds = WQ; side = true; break;
                case 'K': spriteBounds = WK; side = true; break;
                case 'R': spriteBounds = WR; side = true; break;
                case 'p': spriteBounds = BP; break;
                case 'n': spriteBounds = BN; break;
                case 'b': spriteBounds = BB; break;
                case 'q': spriteBounds = BQ; break;
                case 'k': spriteBounds = BK; break;
                case 'r': spriteBounds = BR; break;
            }


            using (Graphics graphics = Graphics.FromImage(image))
            {
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.DrawImage(bitmap, new Rectangle(0, 0, scale.x, scale.x), spriteBounds, GraphicsUnit.Pixel);
            }
            Engine.RegisterSprite(this);
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

        public int GetMapPosition()
        {

            return ((this.Position.x / 64) + (this.Position.y / 64) * 8); ;
        }

        public void DestroySelf()
        {
            Engine.UnRegisterSprite(this);
        }
    }
}
