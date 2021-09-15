using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triple
{
    class Triple
    {
        public string triple { get; set; }
        public int countTriple { get; set; }

        public Triple(string triple, int countTriple)
        {
            this.triple = triple;
            this.countTriple = countTriple;
        }

        //A method that outputs the 10 most common triplets
        public static void TopTriples(List<Triple> triples)
        {
            triples.Sort(new TripleComparer());
            for(int i = 0; i < 10; i++)
            {
                if (i == 9)
                {
                    Console.WriteLine(triples[i].triple);
                }
                else
                {
                    Console.Write(triples[i].triple + ", ");
                }
            }
        }
        public static bool ContainsTriple(List<Triple> triples, string triple)
        {
            foreach(var i in triples)
            {
                if (i.triple == triple) return true;
            }

            return false;
        }

        public static int FindTriple(List<Triple> triples, string triple)
        {
            for(int i = 0; i < triples.Count; i++)
            {
                if (triples[i].triple == triple) return i;
            }
            return -1;
        }

    }

    class TripleComparer : IComparer<Triple>
    {
        public int Compare(Triple p1, Triple p2)
        {
            if (p1.countTriple > p2.countTriple)
                return -1;
            else if (p1.countTriple < p2.countTriple)
                return 1;
            else
                return 0;
        }
    }

}
