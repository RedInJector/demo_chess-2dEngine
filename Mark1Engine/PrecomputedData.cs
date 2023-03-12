using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess.Mark1Engine
{
    public class PrecomputedData
    {
        public static readonly int[] DirectionOffset = {-8, 8, 1, -1, -7, 7, -9, 9 };
        public static readonly int[][] DistanceToTheEdge;
        static PrecomputedData()
        {
            DistanceToTheEdge = new int[64][];
            for (int y = 0; y < 8; y++)
                for(int x = 0; x<8; x++)
                {
                    int numNorth = y;
                    int numSouth = 7 - y;
                    int numWest = 7 - x;
                    int numEast =  x;

                    int squareIndex = x + y*8;

                    DistanceToTheEdge[squareIndex] = new int[] {
                    numNorth,
                    numSouth,
                    numWest,
                    numEast,
                    Math.Min(numNorth, numWest),
                    Math.Min(numSouth, numEast),
                    Math.Min(numNorth, numEast),
                    Math.Min(numSouth, numWest),

                    };
                }
        }

    }
}
