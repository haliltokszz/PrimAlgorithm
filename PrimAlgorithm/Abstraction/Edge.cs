using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimAlgorithm.Abstraction
{
    internal class Edge : IComparable<Edge>
    {
        private int Weight { get; set; }
        private Vertex From;
        private Vertex To;

        public Edge()
        {
            //
        }

        public Edge(Vertex from,Vertex to, int weight)
        {
            this.From = from;
            this.To = to;
            this.Weight = weight;
        }

        /*public void AddEdge(int fromName, int toName, int weight)
        {
            this.From = new Vertex(fromName);
            this.To = new Vertex(toName);
            this.Weight = weight;
            From.AddSubset(To);
        }*/

        public Vertex GetFromVertex()
        {
            return From;
        }

        public Vertex GetToVertex()
        {
            return To;
        }

        public int GetFromVertexName()
        {
            return From.GetName();
        }

        public int GetToVertexName()
        {
            return To.GetName();
        }

        public int GetWeight()
        {
            return this.Weight;
        }

        public int CompareTo(Edge other)
        {
            return this.Weight - other.Weight;
        }
    }
}