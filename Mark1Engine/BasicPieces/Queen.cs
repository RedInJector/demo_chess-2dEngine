using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess.Mark1Engine.BasicPieces
{
    public class Queen : AbstractPiece
    {
        public Queen(Vector2 Position, bool side)
        {
            this.Position = Position;
            Rectangle R;
            this.side = side;
            if (side)
            {
                this.tag = 'Q';
                R = this.WQ;
            }
            else
            {
                this.tag = 'q';
                R = this.BQ;
            }

            using (Graphics graphics = Graphics.FromImage(this.sprite))
            {
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.DrawImage(bitmap, new Rectangle(0, 0, this.Scale.x, this.Scale.x), R, GraphicsUnit.Pixel);
            }

            Console.WriteLine("Queen: " + this.tag + "     " + this.IsType('p'));

            RegisterPiece();
        }

        public override void CalculateAttackSquares()
        {
            AttackedSquares.Clear();
            int startDirIndex = 0;
            int EndDirIndex = 8;

            int startingPosition = ((this.Position.x / 64) + (this.Position.y / 64) * 8);

            for (int directionIndex = startDirIndex; directionIndex < EndDirIndex; directionIndex++)
                for (int n = 1; n <= PrecomputedData.DistanceToTheEdge[startingPosition][directionIndex]; n++)
                {
                    int targetSquare = startingPosition + PrecomputedData.DirectionOffset[directionIndex] * (n);

                    AttackedSquares.Add(targetSquare);

                    if (DemoGame.Map[targetSquare].hasPiece())
                        break;
                }
        }


        public override void Move()
        {
            throw new NotImplementedException();
        }
    }
}
