using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Graphs
{
    internal class Graph
    {
        private List<LinkedList<Vertex>> adjacency;
        private List<Vertex> vertices;
        private List<Edge> edges;
        private int[,] matrix;

        public Graph(int size)
        {
            this.adjacency = new List<LinkedList<Vertex>>();
            this.vertices = new List<Vertex>();
            this.edges = new List<Edge>();
            this.matrix = new int[size, size];
        }

        public void addEdge(Edge edge)
        {
            //Add the edge to the list of edges. This will be used on the "Shortest Path" method.
            this.edges.Add(edge);

            //Adjacency Matrix.
            /*Create an "edge" using the source as a row and the direction as a column, and adding 1
              to that position, denoting an existent edge between those vertices.*/
            this.matrix[edge.source, edge.direction] = 1;

            //Adjacency List.
            /*Create a temporary linked list from the list of linked lists in the position of the source
              of the edge.*/
            LinkedList<Vertex> currentList = this.adjacency[edge.source];
            //Extract and cast the vertex from the list of linked lists an add it to a temporary vertex.
            Vertex destinationVertex = (Vertex) this.adjacency[edge.direction].ElementAt(0);
            //Add the temporary vertex to the temporary linked list.
            currentList.AddLast(destinationVertex);
        }

        public void addVertex(Vertex vertex)
        {
            //Adjacency Matrix.
            //Add the vertex to the list of vertices.
            vertices.Add(vertex);

            //Adjacency List.
            //Create a temporary linked list.
            LinkedList<Vertex> currentList = new LinkedList<Vertex>();
            //Add the vertex to the temporary linked list.
            currentList.AddLast(vertex);
            //Add that linked list to the list of linked lists of our program.
            this.adjacency.Add(currentList);
        }

        public void deleteEdge(Edge edge)
        {
            //Remove the edge of the list of edges.
            this.edges.Remove(edge);

            //Adjacency Matrix.
            /*Delete an "edge" using the source as a row and the direction as a column, and adding 0
              to that position, denoting the lack of an edge between those vertices.*/
            this.matrix[edge.source, edge.direction] = 0;

            //Adjacency List.
            //The same as the "addEdge" method but removing the edge.
            LinkedList<Vertex> currentList = this.adjacency[edge.source];
            Vertex destinationVertex = (Vertex)this.adjacency[edge.direction].ElementAt(0);
            currentList.Remove(destinationVertex);
        }

        public void deleteVertex(Vertex vertex)
        {
            //Adjacency Matrix.
            //Remove the vertex from the list of vertices.
            vertices.Remove(vertex);

            //Adjacency List.
            //The same as the "addVertex" method but removin the vertex.
            LinkedList<Vertex> currentList = new LinkedList<Vertex>();
            currentList.Remove(vertex);
            this.adjacency.Remove(currentList);
        }

        public void adjacencyMatrix()
        {
            Console.Write("  ");
            foreach (Vertex vertex in vertices)
            {

                Console.Write(vertex.data + " ");
            }
            Console.WriteLine("");

            for (int i = 0; i < this.matrix.GetLength(0); i++)
            {
                Console.Write(vertices[i].data + " ");
                for (int j = 0; j < this.matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + " ");

                }
                Console.WriteLine("");
            }
            Console.WriteLine("");
            weightedMatrix();
        }

        private void weightedMatrix()
        {
            int[,] weights = weigthMatrix(this.matrix, this.edges);

            Console.Write("  ");
            foreach (Vertex vertex in vertices)
            {

                Console.Write(vertex.data + " ");
            }
            Console.WriteLine("");

            for (int i = 0; i < this.matrix.GetLength(0); i++)
            {
                Console.Write(this.vertices[i].data + " ");
                for (int j = 0; j < this.matrix.GetLength(1); j++)
                {
                    Console.Write(weights[i, j] + " ");

                }
                Console.WriteLine("");
            }
            Console.WriteLine("");
        }

        public void adjacencyList()
        {
            foreach (LinkedList<Vertex> currentList in this.adjacency)
            {
                foreach (Vertex vertex in currentList)
                {
                    Console.Write(vertex.data + " -> ");

                }
                Console.WriteLine("");
            }
            Console.WriteLine("");
        }

        public void depthFirstSearch(int source)
        {

            Boolean[] visited = new Boolean[matrix.GetLength(0)];
            dFSHelper(source, visited);
            Console.WriteLine("");

        }

        private void dFSHelper(int source, Boolean[] visited)
        {

            if (visited[source])
            {
                return;

            }
            else
            {
                visited[source] = true;
                Console.WriteLine(this.vertices[source].data + " = visited");
            }

            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                if (matrix[source, i] == 1)
                {

                    dFSHelper(i, visited);
                }
            }
            return;
        }

        public void breadthFirstSearch(int source)
        {
            Queue<int> queue = new Queue<int>();
            Boolean[] visited = new Boolean[matrix.GetLength(0)];

            queue.Enqueue(source);
            visited[source] = true;

            while (queue.Count != 0)
            {
                source = queue.Dequeue();
                Console.WriteLine(vertices[source].data + " = visited");

                for (int i = 0; i < matrix.GetLength(1); i++)
                {
                    if (matrix[source, i] == 1 && !visited[i])
                    {
                        queue.Enqueue(i);
                        visited[i] = true;
                    }
                }
            }
            Console.WriteLine("");
        }
        
        private int[,] weigthMatrix(int[,] matrix, List<Edge> edges)
        {
            int[,] weights = new int[matrix.GetLength(0), this.matrix.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == 1)
                    {
                        for (int k = 0; k < edges.Count; k++)
                        {
                            if (i == edges[k].source && j == edges[k].direction)
                            {
                                weights[i, j] = edges[k].weight;
                            }
                        }
                    }
                }
            }
            return weights;
        }

        public void shortestPath()
        {
            sPHelper(weigthMatrix(this.matrix, this.edges), 0);
        }

        private void sPHelper(int[,] weights, int source)
        {
            int[] distance = new int[this.matrix.GetLength(0)];
            Boolean[] visited = new Boolean[this.matrix.GetLength(1)];

            for (int i = 0; i < distance.Length; i++)
            {
                distance[i] = Int32.MaxValue;
                visited[i] = false;
            }

            distance[0] = 0;
            Console.WriteLine("Path: ");
            for (int j = 0; j < distance.Length; j++)
            {
                int m = minDistance(distance, visited);
                visited[m] = true;
                for (int k = 0; k < distance.Length; k++)
                {
                    if (!visited[k] && weights[m, k] != 0 && distance[m] != Int32.MaxValue 
                        && distance[m] + weights[m, k] <= distance[k])
                    {
                        distance[k] = distance[m] + weights[m, k];
                        for (int e = 0; e < this.edges.Count; e++)
                        {
                            if (m == this.edges[e].source && k == this.edges[e].direction)
                            {
                                Console.Write(this.vertices[m].data + " -- > " + this.vertices[k].data + " || Price to pass: " + weights[m, k] + " || Price at the moment: " + distance[k]);
                                Console.WriteLine();
                            }
                        }
                    }
                }
            }
            showTravel(distance, this.vertices);
        }

        private int minDistance(int[] distance, Boolean[] visited)
        {
            int index = -1;
            int min = Int32.MaxValue;
            for (int i = 0; i < this.matrix.GetLength(0); i++)
            {
                if (!visited[i] && distance[i] <= min)
                {
                    min = distance[i];
                    index = i;
                }
            }
            return index;
        }

        private void showTravel(int[] distance, List<Vertex> vertices)
        {
            Console.WriteLine("Cheapest price: " + distance[distance.Length - 1]);
        }
    }
}
