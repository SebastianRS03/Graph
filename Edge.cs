using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    internal class Edge
    {
        public int source;
        public int direction;
        public int weight;

        public Edge(int source, int direction, int weight)
        {
            this.source = source;
            this.direction = direction;
            this.weight = weight;
        }
    }
}
