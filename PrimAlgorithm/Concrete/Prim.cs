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
            if (from.GetVisited() != 0 && to.GetVisited() != 0) //Farklı gruptalarsa..
            {
                if (from.GetVisited() == to.GetVisited()) return false;
            }
            return true;
        }

        public void SetCycle(Edge edge)
        {
            Vertex from = edge.GetFromVertex();
            Vertex to = edge.GetToVertex();
            //Console.WriteLine("-Before-From Name: " + from.GetName() + " Group: " + from.GetVisited() + " |To Name: " + to.GetName() + " Group: " + to.GetVisited());
            if (from.GetVisited() == 0 && to.GetVisited() == 0)
            {
                groupNo++;
                from.SetVisited(groupNo);
                to.SetVisited(groupNo);

            } //Eğer herhangi bir grupta değillerse
            else if (from.GetVisited() == 0 || to.GetVisited() == 0) //Biri bir gruptaysa
            {
                if (from.GetVisited() != 0) to.SetVisited(from.GetVisited()); //Grupları eşitle
                else from.SetVisited(to.GetVisited());

            }
            else if (from.GetVisited() != 0 && to.GetVisited() != 0) //Farklı gruptalarsa..
            {
                if (from.GetVisited() != to.GetVisited())
                {
                    int tempGroup = from.GetVisited();
                    foreach (Edge e in this.GetEdges())
                    {
                        if (e.GetFromVertex().GetVisited() == tempGroup) e.GetFromVertex().SetVisited(to.GetVisited()); //Fromun grubundaysa artık To grubunda
                        else if (e.GetToVertex().GetVisited() == tempGroup) e.GetToVertex().SetVisited(to.GetVisited());
                    }//Tüm edgelerde değişimi yaptık
                }


            }
            //Console.WriteLine("-After-From Name: "+from.GetName()+" Group: "+from.GetVisited()+" |To Name: "+ to.GetName()+" Group: "+to.GetVisited());
        }


        public void MinimumCost(Edge edge)
        {
            cost += edge.GetWeight();
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
                    MinimumCost(this.GetEdges()[0]);
                } 
                else
                {
                    Edge tempEdge = null;
                    foreach (Edge edge in graphMST.GetEdges())
                    {
                        Vertex vertexFrom = edge.GetFromVertex();
                        Vertex vertexTo = edge.GetToVertex();
                        foreach(Vertex subsetsFrom in vertexFrom.GetSubsets())
                        {
                            Edge edgeTmpFrom = FindEdge(vertexFrom, subsetsFrom);
                            if (CheckCycle(edgeTmpFrom))
                            {
                                if (tempEdge == null) tempEdge = edgeTmpFrom;
                                else
                                {
                                    if (edgeTmpFrom.GetWeight() < tempEdge.GetWeight()) tempEdge = edgeTmpFrom;
                                }
                            }
                        }
                        foreach (Vertex subsetsTo in vertexTo.GetSubsets())
                        {
                            Edge edgeTmpTo = FindEdge(vertexTo, subsetsTo);
                            if (CheckCycle(edgeTmpTo))
                            {
                                if (tempEdge == null) tempEdge = edgeTmpTo;
                                else
                                {
                                    if (edgeTmpTo.GetWeight() < tempEdge.GetWeight()) tempEdge = edgeTmpTo;
                                }
                            }
                        }
                    }
                    graphMST.AddEdge(tempEdge);
                    SetCycle(tempEdge);
                    MinimumCost(tempEdge);
                }
                edgeCount++;
            }
            graphMST.Write();
            Console.WriteLine("Cost: " + cost.ToString());
            if (graphMST.GetEdges().Count == V - 1) Console.WriteLine("I guess everything is fine... My edge count equals (Vertex-1)");
        }
    }
}
