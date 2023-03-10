using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess.Mark1Engine
{
    public class PossibleMove
    {
        public Vector2 Position = null;
        public Vector2 Scale = null;
        public Color color;

       /* public PossibleMove()
        {
            color = Color.Transparent;
        }*/

        public PossibleMove(Vector2 position, Color color)
        {
            this.Scale = new Vector2(64, 64);
            this.Position = position;
            this.color = color;

            Engine.RegisterPossibleMoves(this);
        }
        public void DestroySelf()
        {
            Engine.UnRegisterPossibleMoves(this);
        }
        public static void DestroyALL()
        {
            Engine.UnRegisterAllPossibleMoves();
        }
    }
}
