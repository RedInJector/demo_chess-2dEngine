using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace chess.Mark1Engine.BasicPieces
{
    internal class Pawn : AbstractPiece
    {
        public bool EnPassantTarget = false;
        public Pawn(Vector2 Position, bool side)
        {
            this.Position = Position;
            Rectangle R;
            this.side = side;
            if (side)
            {
                this.tag = 'P';
                R = this.WP;
            }
            else
            {
                this.tag = 'p';
                R = this.BP;
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
            int index = 8;
            if (side)
            {
                index *= -1;
            }

            int targetSquare = startingPosition + index;
            if (targetSquare < 0 || targetSquare > 63)
                return;

            int index2 = 1;
            if (DemoGame.Map[targetSquare].Position.x / 64 <= 6)
                    a[targetSquare + index2] = true;
 

            index2 = -1;
            if (DemoGame.Map[targetSquare].Position.x / 64 >= 1)
                    a[targetSquare + index2] = true;

        }

        public override void CalculatePossibleMoves()
        {
            int startingPosition = ((Position.x / 64) + (Position.y / 64) * 8);

            int index = 8;
            if (side)
            {
                index *= -1;
            }

            int targetSquare = startingPosition + index;
            if (targetSquare < 0 || targetSquare > 63)
                return;

            if (DemoGame.Map[targetSquare].Position.y / 64 > 0 && DemoGame.Map[targetSquare].Position.y / 64 > 0)
                if (!DemoGame.Map[targetSquare].hasPiece())
                {
                    Vector2 pos = DemoGame.Map[targetSquare].Position;
                    DemoGame.Move[targetSquare] = new PossibleMove(pos, BLUE);
                }
            if (moves == 0 && !DemoGame.Map[targetSquare + index].hasPiece())
            {
                Vector2 pos = DemoGame.Map[targetSquare + index].Position;
                DemoGame.Move[targetSquare + index] = new PossibleMove(pos, BLUE);
            }

            
            int index2 = 1;
            if (DemoGame.Map[targetSquare].Position.x / 64 >= 0 && DemoGame.Map[targetSquare + index2].Position.x / 64 >= 0)
            {
                if (DemoGame.Map[targetSquare + index2].hasPiece() && DemoGame.Map[targetSquare + index2].PieceOnTop.side != side)
                {
                    Vector2 pos = DemoGame.Map[targetSquare + index2].Position;
                    DemoGame.Move[targetSquare + index2] = new PossibleMove(pos, BLUE);
                }
                //enPassant calculation
                if (startingPosition + index2 < 64 && startingPosition + index2 >= 0 &&
                   DemoGame.Map[startingPosition + index2].hasPiece() &&
                   DemoGame.Map[startingPosition + index2].PieceOnTop.IsType('p') &&
                   DemoGame.Map[startingPosition + index2].PieceOnTop.moves == 1)
                {
                    Pawn pawn = DemoGame.Map[startingPosition + index2].PieceOnTop as Pawn ;
                    if (pawn != null && pawn.EnPassantTarget)
                    {
                        Vector2 pos = DemoGame.Map[startingPosition + index2 + index].Position;
                        DemoGame.Move[startingPosition + index2 + index] = new PossibleMove(pos, PURPULE);
                    }
                }
            }
            index2 = -1;
            if (DemoGame.Map[targetSquare].Position.x / 64 <= 7 && DemoGame.Map[targetSquare + index2].Position.x / 64 <= 7)
            {
                if (DemoGame.Map[targetSquare + index2].hasPiece() && DemoGame.Map[targetSquare + index2].PieceOnTop.side != side)
                {
                    Vector2 pos = DemoGame.Map[targetSquare + index2].Position;
                    DemoGame.Move[targetSquare + index2] = new PossibleMove(pos, BLUE);
                }
                //enPassant calculation
                if (startingPosition + index2 < 64 && startingPosition + index2 >= 0 &&
                   DemoGame.Map[startingPosition + index2].hasPiece() &&
                   DemoGame.Map[startingPosition + index2].PieceOnTop.IsType('p') &&
                   DemoGame.Map[startingPosition + index2].PieceOnTop.moves == 1)
                {
                    Pawn pawn = DemoGame.Map[startingPosition + index2].PieceOnTop as Pawn;
                    if (pawn != null && pawn.EnPassantTarget)
                    {
                        Vector2 pos = DemoGame.Map[startingPosition + index2 + index].Position;
                        DemoGame.Move[startingPosition + index2 + index] = new PossibleMove(pos, PURPULE);
                    }
                }
            }
        }

        public override void Move()
        {
            throw new NotImplementedException();
        }
    }
}
