using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimAlgorithm.Abstraction
{
    internal class Vertex 
    {
        private int Name { get; set; }
        private int VisitedNo { get; set; }
        private List<Vertex> subsets = new List<Vertex>();

        public Vertex(int name)
        {
            this.Name = name;
            this.VisitedNo = 0;
        }

        public int GetName()
        {
            return this.Name;
        }

        public void AddSubset(Vertex vertex)
        {
            this.subsets.Add(vertex);
        }

        public List<Vertex> GetSubsets()
        {
            return this.subsets;
        }

        public void SetVisited(int no)
        {
            this.VisitedNo = no;
        }

        public int GetVisited()
        {
            return this.VisitedNo;
        }
    }
}