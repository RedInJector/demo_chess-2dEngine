﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess.Mark1Engine
{
    public class Sprite
    {
        public Vector2 Position = null;
        public Vector2 Scale = null;
        public string tag = "";
        public static Image SpriteSheetimage = Image.FromFile("Sprites/Pack1.png");
        public Image image;
        Bitmap bitmap = new Bitmap(SpriteSheetimage);

        public Sprite(Vector2 position, Vector2 scale, string tag, char c)
        {
            this.Position = position;
            this.Scale = scale;
            this.tag = tag;
            
            image = new Bitmap((int)scale.x, (int)scale.y);

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

            Rectangle spriteBounds = WP;

            switch (c)
            {
                case 'P': spriteBounds = WP; break;
                case 'N': spriteBounds = WN; break;
                case 'B': spriteBounds = WB; break;
                case 'Q': spriteBounds = WQ; break;
                case 'K': spriteBounds = WK; break;
                case 'R': spriteBounds = WR; break;
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
                graphics.DrawImage(bitmap, new Rectangle(0, 0, (int)scale.x, (int)scale.x), spriteBounds, GraphicsUnit.Pixel);
            }
            Engine.RegisterSprite(this);
        }

        public void DestroySelf()
        {
            Engine.UnRegisterSprite(this);
        }
    }
}
