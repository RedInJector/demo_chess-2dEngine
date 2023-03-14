using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Security.Policy;
using chess.Mark1Engine;
using System.Reflection;

namespace chess.Mark1Engine
{
    
    public class LegalMove
    {
        static Color BLUE = Color.FromArgb(200, 0, 162, 255);
        static Color PURPULE = Color.FromArgb(200, 214, 48, 240);
        static int[][] distanceToTheEdge = PrecomputedData.DistanceToTheEdge;
        static int[] directionOffset = PrecomputedData.DirectionOffset;

        public static void Legal(Piece1 piece, Tile[] Map, PossibleMove[] moves)
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
                /*case "p":
                    Pawn(piece, Map, moves);
                    break;*/
                case "k":
                    King(piece, Map, moves);
                    break;
                case "n":
                    Knight(piece, Map, moves);
                    break;
                default:

                    break;
            }
        }

        private static void Sliding(Piece1 piece, Tile[] Map, PossibleMove[] moves)
        {
            int startDirIndex = (piece.IsType('r')) ? 0 : 4;
            int EndDirIndex = (piece.IsType('b')) ? 8 : 4;

            int startingPosition = ((piece.Position.x / 64) + (piece.Position.y / 64) * 8);

            for (int directionIndex = startDirIndex; directionIndex < EndDirIndex; directionIndex++)
                for (int n = 1; n <= distanceToTheEdge[startingPosition][directionIndex]; n++)
                {
                    int targetSquare = startingPosition + directionOffset[directionIndex] * (n);
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
        /*
        private static void Pawn(Piece1 piece, Tile[] Map, PossibleMove[] moves)
        {
            int startingPosition = ((piece.Position.x / 64) + (piece.Position.y / 64) * 8);
            
            int index = 8;
            if (piece.side)
            {
                index *= -1;
            }

            int targetSquare = startingPosition + index;
            if (targetSquare < 0 || targetSquare > 63)
                return;

            if (Map[targetSquare].Position.y / 64 > 0 && Map[targetSquare].Position.y / 64 > 0)
                if (!Map[targetSquare].hasPiece()) {
                    Vector2 pos = Map[targetSquare].Position;
                    moves[targetSquare] = new PossibleMove(pos, BLUE);
                }
            if(piece.firstmove && !Map[targetSquare + index].hasPiece())
            {
                Vector2 pos = Map[targetSquare + index].Position;
                moves[targetSquare + index] = new PossibleMove(pos, BLUE);
            }
            
            int index2 = 1;
            if (Map[targetSquare].Position.x / 64 >= 0)
            {
                if(Map[targetSquare + index2].hasPiece() && Map[targetSquare + index2].PieceOnTop.side != piece.side)
                {
                    Vector2 pos = Map[targetSquare + index2].Position;
                    moves[targetSquare + index2] = new PossibleMove(pos, BLUE);
                }
                if (Map[startingPosition + index2].hasPiece() &&
                   Map[startingPosition + index2].PieceOnTop.IsType('p') &&
                   Map[startingPosition + index2].PieceOnTop.moves == 1 &&
                   Map[startingPosition + index2].PieceOnTop.EnPassantTarget == true)
                { 
                    Vector2 pos = Map[startingPosition + index2 + index].Position;
                    moves[startingPosition + index2 + index] = new PossibleMove(pos, PURPULE);
                }
            }
            index2 = -1;
            if (Map[targetSquare].Position.x / 64 <= 7)
            {
                if (Map[targetSquare + index2].hasPiece() && Map[targetSquare + index2].PieceOnTop.side != piece.side)
                {
                    Vector2 pos = Map[targetSquare + index2].Position;
                    moves[targetSquare + index2] = new PossibleMove(pos, BLUE);
                }
                if (Map[startingPosition + index2].hasPiece() &&
                   Map[startingPosition + index2].PieceOnTop.IsType('p') &&
                   Map[startingPosition + index2].PieceOnTop.moves == 1 &&
                   Map[startingPosition + index2].PieceOnTop.EnPassantTarget == true)
                {
                    Vector2 pos = Map[startingPosition + index2 + index].Position;
                    moves[startingPosition + index2 + index] = new PossibleMove(pos, PURPULE);
                }
            }

        }
        */
        private static void King(Piece1 piece, Tile[] Map, PossibleMove[] moves)
        {
            int startingPosition = ((piece.Position.x / 64) + (piece.Position.y / 64) * 8);

            for(int i = 0; i < 8; i++)
            {
                int targetPosition = startingPosition + directionOffset[i];
                if (targetPosition < 0 || targetPosition > 63)
                    continue;

                if (Map[targetPosition].hasPiece() && Map[targetPosition].PieceOnTop.side == piece.side)
                    continue;

                Vector2 pos = Map[targetPosition].Position;
                moves[targetPosition] = new PossibleMove(pos, BLUE);

               /* if (Map[targetPosition].hasPiece() && Map[targetPosition].PieceOnTop.side != piece.side)
                    continue;
               */

            }
        }

        private static void Knight(Piece1 piece, Tile[] Map, PossibleMove[] moves)
        {
            int[] possibleMoves = { -17, -15, -10, -6, 6, 10, 15, 17 };

            foreach (int move in possibleMoves)
            {
                int destination = piece.GetMapPosition() + move;

                if (destination >= 0 && destination < 64 && 
                    Math.Abs(piece.GetMapPosition() % 8 - destination % 8) <= 2 && 
                    Math.Abs(piece.GetMapPosition() / 8 - destination / 8) <= 2 && 
                    (Map[destination].PieceOnTop == null || 
                    (Map[destination].PieceOnTop.side 
                    != Map[piece.GetMapPosition()].PieceOnTop.side)))
                {
                    Vector2 pos = Map[destination].Position;
                    moves[destination] = new PossibleMove(pos, BLUE);
                }

            }


        }
    
}

  
}
