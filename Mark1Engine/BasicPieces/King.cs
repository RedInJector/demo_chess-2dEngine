using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess.Mark1Engine.BasicPieces
{
    internal class King : AbstractPiece
    {
        public King(Vector2 Position, bool side)
        {
            this.Position = Position;
            Rectangle R;
            this.side = side;
            if (side)
            {
                this.tag = 'K';
                R = this.WK;
            }
            else
            {
                this.tag = 'k';
                R = this.BK;
            }

            using (Graphics graphics = Graphics.FromImage(this.sprite))
            {
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.DrawImage(bitmap, new Rectangle(0, 0, this.Scale.x, this.Scale.x), R, GraphicsUnit.Pixel);
            }

            RegisterPiece();
        }

        public override void CalculateAttackSquares(bool[] a)
        {
            int startingPosition = ((Position.x / 64) + (Position.y / 64) * 8);

            for (int i = 0; i < 8; i++)
            {
                int targetPosition = startingPosition + PrecomputedData.DirectionOffset[i];
                if (targetPosition < 0 || targetPosition > 63)
                    continue;

                a[targetPosition] = true;
            }
        }

        public override void CalculatePossibleMoves()
        {
            int startingPosition = ((Position.x / 64) + (Position.y / 64) * 8);

            int startDirIndex = 0;
            int EndDirIndex = 8;


            for (int directionIndex = startDirIndex; directionIndex < EndDirIndex; directionIndex++)
            {
                int targetSquare = startingPosition + PrecomputedData.DirectionOffset[directionIndex];

                if (PrecomputedData.DistanceToTheEdge[startingPosition][directionIndex] == 0)
                    continue;

                if (DemoGame.Map[targetSquare].hasPiece() && DemoGame.Map[targetSquare].PieceOnTop.side == side)
                    continue;

                Vector2 pos = DemoGame.Map[targetSquare].Position;

                if (DemoGame.currentMove == true && DemoGame.AttackedByBlack[targetSquare] == false)
                    DemoGame.Move[targetSquare] = new PossibleMove(pos, BLUE);

                else if (DemoGame.currentMove == false && DemoGame.AttackedByWhite[targetSquare] == false)
                    DemoGame.Move[targetSquare] = new PossibleMove(pos, BLUE);

            }






           /* for (int i = 0; i < 8; i++)
            {
                int targetPosition = startingPosition + PrecomputedData.DirectionOffset[i];
                if (targetPosition < 0 || targetPosition > 63)
                    continue;

                if (DemoGame.Map[targetPosition].hasPiece() && DemoGame.Map[targetPosition].PieceOnTop.side == side)
                    continue;

                Vector2 pos = DemoGame.Map[targetPosition].Position;

                if (DemoGame.currentMove == true && DemoGame.AttackedByBlack[targetPosition] == false)
                    DemoGame.Move[targetPosition] = new PossibleMove(pos, BLUE);

                else if(DemoGame.currentMove == false && DemoGame.AttackedByWhite[targetPosition] == false)
                    DemoGame.Move[targetPosition] = new PossibleMove(pos, BLUE);
            }*/
        }

        public override void Move()
        {
            throw new NotImplementedException();
        }
    }
}
