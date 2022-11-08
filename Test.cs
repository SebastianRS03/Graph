using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Graphs
{
    internal class Test
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph(5);

            graph.addVertex(new Vertex('A'));
            graph.addVertex(new Vertex('B'));
            graph.addVertex(new Vertex('C'));
            graph.addVertex(new Vertex('D'));
            graph.addVertex(new Vertex('E'));

            graph.addEdge(new Edge(0, 1, 20));
            graph.addEdge(new Edge(1, 2, 60));
            graph.addEdge(new Edge(2, 3, 15));
            graph.addEdge(new Edge(2, 4, 40));
            graph.addEdge(new Edge(3, 4, 20));
            graph.addEdge(new Edge(4, 0, 30));
            graph.addEdge(new Edge(4, 2, 5));

            graph.shortestPath();
            //graph.adjacencyMatrix();
            //graph.shortestPath();
            //graph.adjacencyList();

            //graph.deleteEdge(1, 2);
            //graph.deleteEdge(4, 2);

            //graph.adjacencyMatrix();
            //graph.adjacencyList();

            //graph.depthFirstSearch(1);
            //graph.breadthFirstSearch(3);

        }
    }
}
