
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimAlgorithm.Abstraction
{
    abstract class Graph
    {
        protected int Cost { get; set; } = 0;
        protected int VertexGroupNo { get; set; } = 0;
        private List<Edge> edges { get; set; } = new List<Edge>();

        public void AddEdge(int fromName, int toName, int weight)
        {
            Edge edge;
            Vertex toVertex = GetVertex(toName);
            Vertex fromVertex = GetVertex(fromName);
            if (fromVertex == null && toVertex == null)
            {
                toVertex = new Vertex(toName);
                fromVertex = new Vertex(fromName);
            }
            else if (fromVertex == null) fromVertex = new Vertex(fromName);
            else if (toVertex == null) toVertex = new Vertex(toName);
            edge = new Edge(fromVertex, toVertex, weight);
            edges.Add(edge);
        }

        public void AddEdge(Edge edge)
        {
            edges.Add(edge);
        }

        public Vertex GetVertex(int vertexName)
        {
            foreach(Edge edge in edges)
            {
                if (edge.GetToVertexName() == vertexName)
                    return edge.GetToVertex();
                else if (edge.GetFromVertexName() == vertexName)
                    return edge.GetFromVertex();
            }
            return null;
        }

        public Edge GetEdge(Vertex vertexFrom, Vertex vertexTo)
        {
            foreach(Edge e in edges)
            {
                if (e.GetFromVertexName() == vertexFrom.GetName() && e.GetToVertexName() == vertexTo.GetName()) return e;
                else if(e.GetFromVertexName() == vertexTo.GetName() && e.GetToVertexName() == vertexFrom.GetName()) return e;
            }
            return null;
        }

        public void Write()
        {
            foreach (Edge edge in edges)
            {
                Console.Write(edge.GetFromVertexName());
                Console.Write(" -> " + edge.GetToVertexName());
                Console.Write(" : " + edge.GetWeight().ToString());
                Console.Write(" --- From Vertex: " + edge.GetFromVertexName() + " Neighbours: ");
                List<Vertex> fromSubsets = edge.GetFromVertex().GetSubsets();
                List<Vertex> toSubsets = edge.GetToVertex().GetSubsets();
                foreach (Vertex subset in fromSubsets)
                {
                    Console.Write(subset.GetName().ToString() + " ");
                }
                Console.Write(" --- To Vertex: " + edge.GetToVertexName() + " Neighbours: ");
                foreach (Vertex subset in toSubsets)
                {
                    Console.Write(subset.GetName().ToString() + " ");
                }
                Console.WriteLine();
            }
        }

        public void SortGraph()
        {
            edges.Sort();
        }

        public List<Edge> GetEdges()
        {
            return this.edges;
        }

        public bool CheckCycle(Edge edge)
        {
            Vertex from = edge.GetFromVertex();
            Vertex to = edge.GetToVertex();
            if (from.GetVisited() != 0 && to.GetVisited() != 0) //İkisi de bir grupta ise
            {
                if (from.GetVisited() == to.GetVisited()) return false; //Ve grupları aynıysa
            }
            return true;
        }

        public void SetCycle(Edge edge)
        {
            Vertex from = edge.GetFromVertex();
            Vertex to = edge.GetToVertex();
            if (from.GetVisited() == 0 && to.GetVisited() == 0)//Eğer herhangi bir grupta değillerse
            {
                VertexGroupNo++;
                from.SetVisited(VertexGroupNo);
                to.SetVisited(VertexGroupNo);
            }
            else if (from.GetVisited() == 0 || to.GetVisited() == 0) //Biri bir gruptaysa grupları eşitle
            {
                if (from.GetVisited() != 0) to.SetVisited(from.GetVisited());
                else from.SetVisited(to.GetVisited());
            }
            else if (from.GetVisited() != 0 && to.GetVisited() != 0) //İkisi de bir gruptaysa
            {
                if (from.GetVisited() != to.GetVisited()) //Ve grupları farklıysa gruplarını eşitle
                {
                    int tempGroup = from.GetVisited();
                    foreach (Edge e in this.GetEdges())
                    {
                        if (e.GetFromVertex().GetVisited() == tempGroup) e.GetFromVertex().SetVisited(to.GetVisited()); //Fromun grubundaysa artık To grubunda
                        else if (e.GetToVertex().GetVisited() == tempGroup) e.GetToVertex().SetVisited(to.GetVisited()); //To grubundaysa artık From grubunda
                    }//Tüm edgelerde değişimi yaptık
                }
            }
        }
        public abstract void MST(int V);
    }
}
