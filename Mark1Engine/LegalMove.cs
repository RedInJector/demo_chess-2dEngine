using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Security.Policy;
using chess.Mark1Engine;

namespace chess.Mark1Engine
{
    
    public class LegalMove
    {
        static Color BLUE = Color.FromArgb(200, 0, 162, 255);
        static int[][] distanceToTheEdge = PrecomputedData.DistanceToTheEdge;
        static int[] directionOffset = PrecomputedData.DirectionOffset;


        public static void Legal(Piece piece, Tile[] Map, PossibleMove[] moves)
        {
            

            char c = piece.tag;
            int pos = (piece.Position.x / 64 * piece.Position.y / 64 * 8);

            //HorizontalVertical(piece, Map, moves);

            switch (c.ToString().ToLower())
            {
                case "r":
                    Sliding(piece, Map, moves);
                    break;
                case "b":
                    Sliding(piece, Map, moves);
                    break;
                case "q":
                    Sliding(piece, Map, moves);
                    break;
                default:

                    break;
            }
        }

        private static void Sliding(Piece piece, Tile[] Map, PossibleMove[] moves)
        {
            int startDirIndex = (piece.IsType('r')) ? 0 : 4;
            int EndDirIndex = (piece.IsType('b')) ? 8 : 4;

            Console.WriteLine(startDirIndex + "  " + EndDirIndex);

            int startingPosition = ((piece.Position.x / 64) + (piece.Position.y / 64) * 8);

            Console.WriteLine("startDirIndex: " + startDirIndex + "  EndDirIndex: " + EndDirIndex + "  startingPosition: " + startingPosition);

            for (int directionIndex = startDirIndex; directionIndex < EndDirIndex; directionIndex++)
                for (int n = 1; n <= distanceToTheEdge[startingPosition][directionIndex]; n++)
                {
                    int targetSquare = startingPosition + directionOffset[directionIndex] * (n);

                    Console.WriteLine("Distance : " + distanceToTheEdge[startingPosition][directionIndex]);
                    Console.WriteLine("Target: " + targetSquare);
                    /*if (targetSquare > 63 || targetSquare < 0)
                        continue;*/
                    if (Map[targetSquare].hasPiece() && Map[targetSquare].PieceSide() == piece.side)
                        break;

                    Vector2 pos = Map[targetSquare].Position;
                    moves[targetSquare] = new PossibleMove(pos, BLUE);

                    if (Map[targetSquare].hasPiece() && Map[targetSquare].PieceSide() != piece.side)
                        break;
                }
        }
    }


  
}
