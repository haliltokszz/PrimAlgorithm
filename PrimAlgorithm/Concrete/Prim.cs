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
        private int cost = 0;
        private int groupNo = 0;

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
                groupNo++;
                from.SetVisited(groupNo);
                to.SetVisited(groupNo);
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

        public void MST(int V)
        {
            Prim graphMST = new Prim();
            int edgeCount = 0;
            while (edgeCount < V - 1)
            {
                if (edgeCount == 0) {
                    graphMST.AddEdge(this.GetEdges()[0]);
                    SetCycle(this.GetEdges()[0]);
                    cost += this.GetEdges()[0].GetWeight();
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
                    cost += minEdge.GetWeight();
                }
                edgeCount++;
            }
            graphMST.Write();
            Console.WriteLine("Cost: " + cost.ToString());
            if (graphMST.GetEdges().Count == V - 1) Console.WriteLine("I guess everything is fine... My edge count equals (Vertex-1)");
        }
    }
}
