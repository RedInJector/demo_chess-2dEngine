using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace chess.Mark1Engine
{
    public class LegalMove
    {
        public LegalMove(char c, Vector2 pos, Tile[,] Map, PossibleMove[,] moves)
        {
            c.ToString().ToLower();
            switch (c){
                case 'r':
                    HorizontalVertical(pos, Map, moves);
                    break;
                    default:

                    break;
            }
        }

        public void HorizontalVertical(Vector2 pos, Tile[,] Map, PossibleMove[,] moves)
        {
            int top, bottom, left,right;

            top = (int)pos.y;
            left = (int)pos.x;


            for (int i = top-1; i >= 0; i--){

                if (Map[(int)pos.x, i].hasPiece())
                    break;
                Vector2 newtile = new Vector2(pos.x * 64, i * 64);
                moves[(int)pos.x, i] = new PossibleMove(newtile, Color.FromArgb(200, 0, 162, 255));
            }

            for (int i = top + 1; i <= 7; i++)
            {
                if (Map[(int)pos.x, i].hasPiece())
                    break;

                Vector2 newtile = new Vector2(pos.x * 64, i * 64);
                moves[(int)pos.x, i] = new PossibleMove(newtile, Color.FromArgb(200, 0, 162, 255));
            }

            for (int i = left - 1; i >= 0; i--)
            {
                if (Map[i, (int)pos.y].hasPiece())
                    break;

                Vector2 newtile = new Vector2(i * 64, pos.y * 64 );
                moves[(int)pos.x, i] = new PossibleMove(newtile, Color.FromArgb(200, 0, 162, 255));
            }

            for (int i = left + 1; i <= 7; i++)
            {
                if (Map[i, (int)pos.y].hasPiece())
                    break;

                Vector2 newtile = new Vector2(i * 64, pos.y * 64);
                moves[(int)pos.x, i] = new PossibleMove(newtile, Color.FromArgb(200, 0, 162, 255));
            }

        }
    }
}
