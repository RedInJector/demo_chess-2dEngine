using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess.Mark1Engine
{
    public class Vector2 : IEquatable<Vector2>
    {

        public float x;
        public float y;

        public Vector2()
        {

        }

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }


        public bool Equals(Vector2 other)
        {
            if(other == null)
                return false;

            if (other.GetType() != this.GetType())
                return false;

            return x == other.x && y == other.y;
        }

        public override string ToString()
        {
            return "x: "+this.x+" y: "+this.y;
        }


    }
}
