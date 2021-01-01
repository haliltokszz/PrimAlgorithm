using PrimAlgorithm.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimAlgorithm.Concrete
{
    class Prim : Graph
    {
        public Edge FindMinEdgeWithSubsets(Vertex vertex)
        {
            Edge edge=null;
            foreach (Vertex subsetsFrom in vertex.GetSubsets())
            {
                Edge tempEdge = GetEdge(vertex, subsetsFrom);
                if (CheckCycle(tempEdge))
                {
                    if (edge == null) edge = tempEdge;
                    else edge = MinEdge(tempEdge, edge);
                }
            }
            return edge;
        }

        public Edge MinEdge(Edge edge1, Edge edge2)
        {
            if (edge1 == null) return edge2;
            else if (edge2 == null) return edge1;
            else return edge1.GetWeight() < edge2.GetWeight() ? edge1 : edge2;
        }

        public override void MST(int V)
        {
            Prim graphMST = new Prim();
            int edgeCount = 0;
            while (edgeCount < V - 1)
            {
                if (edgeCount == 0) {
                    graphMST.AddEdge(this.GetEdges()[0]);
                    SetCycle(this.GetEdges()[0]);
                    Cost += this.GetEdges()[0].GetWeight();
                } 
                else
                {
                    Edge minEdge = null;
                    foreach (Edge edge in graphMST.GetEdges())
                    {
                        Vertex vertexFrom = edge.GetFromVertex();
                        Vertex vertexTo = edge.GetToVertex();
                        Edge minEdgeFrom = FindMinEdgeWithSubsets(vertexFrom);
                        Edge minEdgeTo = FindMinEdgeWithSubsets(vertexTo);
                        minEdge = MinEdge(minEdgeFrom, minEdgeTo);
                    }
                    graphMST.AddEdge(minEdge);
                    SetCycle(minEdge);
                    Cost += minEdge.GetWeight();
                }
                edgeCount++;
            }
            graphMST.Write();
            Console.WriteLine("Cost: " + Cost.ToString());
            if (graphMST.GetEdges().Count == V - 1) Console.WriteLine("I guess everything is fine... My edge count equals (Vertex-1)");
        }
    }
}
