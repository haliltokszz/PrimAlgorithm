using PrimAlgorithm.Concrete;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            Prim graph = new Prim();
            int numEdges, numVertices = 0;
            try
            {
                StreamReader sr = new StreamReader("inputs.txt");
                string veri = sr.ReadToEnd();
                veri = veri.Replace("\r\n", " ");
                string[] veriler = veri.Split(' ');
                numVertices = Convert.ToInt32(veriler[0]);
                numEdges = Convert.ToInt32(veriler[1]);
                sr.Close();
                for (int i = 2; i < veriler.Length; i += 3)
                {
                    graph.AddEdge(Convert.ToInt32(veriler[i]), Convert.ToInt32(veriler[i + 1]), Convert.ToInt32(veriler[i + 2]));
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Error - the file could not be read");
                Console.WriteLine(e.Message);
            }
            graph.SortGraph();
            graph.Write();
            Console.WriteLine();
            Console.WriteLine("----------------------------- Prim Minimum Spanning Tree -----------------------------");
            Console.WriteLine();
            graph.MST(numVertices);

            Console.ReadKey();
        }
    }
}
