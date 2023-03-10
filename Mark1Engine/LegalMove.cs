using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Security.Policy;

namespace chess.Mark1Engine
{
    public class LegalMove
    {
        Color color = Color.FromArgb(200, 0, 162, 255);
        public LegalMove(Piece piece, Tile[,] Map, PossibleMove[,] moves)
        {
            char c = piece.tag;
            Vector2 pos = new Vector2();
            pos.x = piece.Position.x/64;
            pos.y = piece.Position.y/64;
            switch (c.ToString().ToLower()){
                case "r":
                    HorizontalVertical(piece, Map, moves);
                    break;
                case "p":
                    Pawn(piece, Map, moves);
                    break;
                case "b":
                    Diagonal(piece, Map, moves);
                    break;
                case "q":
                    HorizontalVertical(piece, Map, moves);
                    Diagonal(piece, Map, moves);
                    break;
                    default:

                    break;
            }
        }

        public void HorizontalVertical(Piece piece, Tile[,] Map, PossibleMove[,] moves)
        {
            Vector2 pos = new Vector2();
            pos.x = piece.Position.x / 64;
            pos.y = piece.Position.y / 64;

            int top, left;

            top = (int)pos.y;
            left = (int)pos.x;


            for (int i = top-1; i >= 0; i--){
                if (Map[(int)pos.x, i].hasPiece())
                    if(Map[(int)pos.x, i].PieceOnTop.side == piece.side)
                    break;

                Vector2 newtile = new Vector2(pos.x * 64, i * 64);
                moves[(int)pos.x, i] = new PossibleMove(newtile, color);
                if (Map[(int)pos.x, i].hasPiece())
                    break;
            }

            for (int i = top + 1; i <= 7; i++)
            {
                if (Map[(int)pos.x, i].hasPiece())
                    if (Map[(int)pos.x, i].PieceOnTop.side == piece.side)
                        break;


                Vector2 newtile = new Vector2(pos.x * 64, i * 64);
                moves[(int)pos.x, i] = new PossibleMove(newtile, color);
                if (Map[(int)pos.x, i].hasPiece())
                    break;
            }

            for (int i = left - 1; i >= 0; i--)
            {
                if (Map[i, (int)pos.y].hasPiece())
                    if (Map[i, (int)pos.y].PieceOnTop.side == piece.side)
                        break;


                Vector2 newtile = new Vector2(i * 64, pos.y * 64 );
                moves[(int)pos.x, i] = new PossibleMove(newtile, color);
                if (Map[i, (int)pos.y].hasPiece())
                    break;
            }

            for (int i = left + 1; i <= 7; i++)
            {
                if (Map[i, (int)pos.y].hasPiece())
                    if (Map[i, (int)pos.y].PieceOnTop.side == piece.side)
                        break;

                Vector2 newtile = new Vector2(i * 64, pos.y * 64);
                moves[(int)pos.x, i] = new PossibleMove(newtile, color);
                if (Map[i, (int)pos.y].hasPiece())
                    break;
            }

        }

        public void Diagonal(Piece piece, Tile[,] Map, PossibleMove[,] moves)
        {
            int x = (int)(piece.Position.x / 64);
            int y = (int)(piece.Position.y / 64);

            Vector2 newtile;
            int k = y + 1;
            int i = x + 1;
            while (i <= 7 && k <= 7)
            {
                if (Map[i, k].hasPiece() && Map[i,k].PieceOnTop.side == piece.side)
                    break;
                newtile = new Vector2(i * 64, k * 64);
                moves[i, k] = new PossibleMove(newtile, color);
                if (Map[i, k].hasPiece())
                    break;
                i++;
                k++;
                

            }
            k = y - 1;
            i = x - 1;
            while (i >= 0 && k >= 0)
            {
                if (Map[i, k].hasPiece() && Map[i, k].PieceOnTop.side == piece.side)
                    break;
                newtile = new Vector2(i * 64, k * 64);
                moves[i, k] = new PossibleMove(newtile, color);
                if (Map[i, k].hasPiece())
                    break;
                i--;
                k--;

            }
            k = y - 1;
            i = x + 1;
            while (i <= 7 && k >= 0)
            {
                if (Map[i, k].hasPiece() && Map[i, k].PieceOnTop.side == piece.side)
                    break;
                newtile = new Vector2(i * 64, k * 64);
                moves[i, k] = new PossibleMove(newtile, color);
                if (Map[i, k].hasPiece())
                    break;
                i++;
                k--;


            }
            k = y + 1;
            i = x - 1;
            while (i >= 0 && k <= 7)
            {
                if (Map[i, k].hasPiece() && Map[i, k].PieceOnTop.side == piece.side)
                    break;
                newtile = new Vector2(i * 64, k * 64);
                moves[i, k] = new PossibleMove(newtile, color);
                if (Map[i, k].hasPiece())
                    break;
                i--;
                k++;


            }
        }

        public void Pawn(Piece pawn, Tile[,] Map, PossibleMove[,] moves)
        {
            int x = (int)pawn.Position.x/64;
            int y = (int)pawn.Position.y/64;
            int direction = 0;

            direction = pawn.side == 1 ? direction - 1 : direction + 1;


            if(x >= 0 && x < 7)
            {
                if (Map[x + 1, (y + direction)].hasPiece() && Map[x + 1, (y + direction)].PieceOnTop.side != pawn.side)
                {
                    Vector2 enempos = new Vector2((x + 1) * 64, (y + direction) * 64);
                    moves[x + 1, y + direction] = new PossibleMove(enempos, color);
                }
            }
            if (x <= 7 && x > 0)
            {
                if (Map[x - 1, (y + direction)].hasPiece() && Map[x - 1, (y + direction)].PieceOnTop.side != pawn.side)
                {
                    Vector2 enempos = new Vector2((x - 1) * 64, (y+ direction) * 64);
                    moves[x - 1, y + direction] = new PossibleMove(enempos, color);
                }
            }
            if (!Map[x, (y + direction)].hasPiece())
            {
                Vector2 newtile = new Vector2(x * 64, (y + direction) * 64);
                moves[x, y + direction] = new PossibleMove(newtile, color);
            }
            if (pawn.firstmove)
            {
                if (!Map[x, (y + 2 * direction)].hasPiece())
                {
                    Vector2 newtile = new Vector2(x * 64, (y + 2 * direction) * 64);
                    moves[x, y + direction] = new PossibleMove(newtile, color);
                }
            }

            

        }

        


    }
}
