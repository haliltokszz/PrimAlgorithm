
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimAlgorithm.Abstraction
{
    abstract class Graph
    {
        List<Edge> edges = new List<Edge>();

        public void AddEdge(int fromName, int toName, int weight)
        {
            Edge edge;
            if(GetVertex(fromName)!=null && GetVertex(toName) != null)
            {
                edge = new Edge(GetVertex(fromName), GetVertex(toName), weight);
            }else if (GetVertex(fromName) != null)
            {
                Vertex toVertex = new Vertex(toName);
                edge = new Edge(GetVertex(fromName), toVertex, weight);
            }else if (GetVertex(toName) != null)
            {
                Vertex fromVertex = new Vertex(fromName);
                edge = new Edge(fromVertex, GetVertex(toName), weight);
            }
            else
            {
                Vertex toVertex = new Vertex(toName);
                Vertex fromVertex = new Vertex(fromName);
                edge = new Edge(fromVertex, toVertex, weight);
            }

            edge.GetFromVertex().AddSubset(edge.GetToVertex());
            edge.GetToVertex().AddSubset(edge.GetFromVertex());
            //edge.From.AddSubset(To);
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

        public List<Vertex> GetVertices()
        {
            List<Vertex> vertices = new List<Vertex>();
            foreach (Edge edge in edges)
            {
                vertices.Add(edge.GetFromVertex());
                vertices.Add(edge.GetToVertex());
            }
            return vertices;
        }

        public Edge FindEdge(Vertex vertexFrom, Vertex vertexTo)
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

        /*public List<Vertex> GetSubsets(int vertexName)
        {
            Vertex vertex;
            
            return vertex.GetSubsets();
        }*/

        /*void AddVertex(int name, bool from)
        {
            if (from)
                vertexFrom.Name = name;
            else
                vertexTo.Name = name;
        }*/
    }
}
