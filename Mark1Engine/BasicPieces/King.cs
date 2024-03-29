﻿using System;
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

        public override void CalculateAttackSquares()
        {
            AttackedSquares.Clear();
            int startingPosition = ((Position.x / 64) + (Position.y / 64) * 8);
            int startDirIndex = 0;
            int EndDirIndex = 8;

            for (int i = 0; i < 8; i++)
            {
                int targetPosition = startingPosition + PrecomputedData.DirectionOffset[i];
                if (targetPosition < 0 || targetPosition > 63)
                    continue;

                if (PrecomputedData.DistanceToTheEdge[startingPosition][i] > 0)
                    AttackedSquares.Add(targetPosition);
            }
        }

        public override void ShowPossibleMoves()
        {
            foreach (int square in AttackedSquares)
            {
                if (DemoGame.Map[square].hasPiece() && DemoGame.Map[square].PieceSide() == this.side)
                    continue;

                if (this.side == true && DemoGame.AttackedByBlack[square])
                    continue;

                else if (this.side == false && DemoGame.AttackedByWhite[square])
                    continue;

                Vector2 pos = DemoGame.Map[square].Position;
                DemoGame.Move[square] = new PossibleMove(pos, BLUE);

            }
        }

        public override void Move()
        {
            throw new NotImplementedException();
        }
    }
}
