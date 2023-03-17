using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess.Mark1Engine.BasicPieces
{
    internal class Knight : AbstractPiece
    {
        public Knight(Vector2 Position, bool side)
        {
            this.Position = Position;
            Rectangle R;
            this.side = side;
            if (side)
            {
                this.tag = 'N';
                R = this.WN;
            }
            else
            {
                this.tag = 'n';
                R = this.BN;
            }

            using (Graphics graphics = Graphics.FromImage(this.sprite))
            {
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.DrawImage(bitmap, new Rectangle(0, 0, this.Scale.x, this.Scale.x), R, GraphicsUnit.Pixel);
            }

            RegisterPiece();
        }

        public override void CalculateAttackSquares()
        {
            AttackedSquares.Clear();
            int[] possibleMoves = { -17, -15, -10, -6, 6, 10, 15, 17 };

            foreach (int move in possibleMoves)
            {
                int destination = GetMapPosition() + move;

                if (destination >= 0 && destination < 64 &&
                    Math.Abs(GetMapPosition() % 8 - destination % 8) <= 2 &&
                    Math.Abs(GetMapPosition() / 8 - destination / 8) <= 2 &&
                    (DemoGame.Map[destination].PieceOnTop == null ||
                    ((DemoGame.Map[destination].PieceOnTop.side
                    != DemoGame.Map[GetMapPosition()].PieceOnTop.side)) ||
                    DemoGame.Map[destination].PieceOnTop.side
                    == DemoGame.Map[GetMapPosition()].PieceOnTop.side))
                {
                    AttackedSquares.Add(destination);
                }

            }
        }

        public override void Move()
        {
            throw new NotImplementedException();
        }
    }
}
