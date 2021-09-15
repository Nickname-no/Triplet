using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Triple
{
    class Program
    {

        //Searching for triplets using multithreading
        static List<Triple> triples = new List<Triple>();
        static object locker = new object();
        static void Main(string[] args)
        {
            
            Stopwatch stopWatchMultiThread = new Stopwatch();
            stopWatchMultiThread.Start();

            string textMultiThread = "";
            string pathMultiThread = Directory.GetCurrentDirectory() + "\\text.txt";
            try
            {
                using (StreamReader sr = new StreamReader(pathMultiThread))
                {
                    textMultiThread += sr.ReadToEnd();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


            textMultiThread = textMultiThread.Replace(",", "")
                                             .Replace(";", "")
                                             .Replace(".", "")
                                             .Replace("(", "")
                                             .Replace(")", "")
                                             .Replace("?", "")
                                             .Replace("!", "")
                                             .Replace(":", "")
                                             .Replace("\"", "")
                                             .Replace("-", "")
                                             .Replace("\n", "")
                                             .Replace("«", "")
                                             .Replace("»", "")
                                             .ToLower();

            var wordsMultiThread = textMultiThread.Split(" ");

            Parallel.ForEach(wordsMultiThread, CountTripleInWordMultiThread);
            Triple.TopTriples(triples);
            stopWatchMultiThread.Stop();
            Console.WriteLine(stopWatchMultiThread.ElapsedMilliseconds);

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            //Searching for triplets without using multithreading
            triples = new List<Triple>();
            string text = "";
            string path = Directory.GetCurrentDirectory() + "\\text.txt";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    text += sr.ReadToEnd();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


            text = text.Replace(",", "")
                        .Replace(";", "")
                        .Replace(".", "")
                        .Replace("(", "")
                        .Replace(")", "")
                        .Replace("?", "")
                        .Replace("!", "")
                        .Replace(":", "")
                        .Replace("\"", "")
                        .Replace("-", "")
                        .Replace("\n", "")
                        .Replace("«", "")
                        .Replace("»", "");

            text = text.ToLower();
            var words = text.Split(" ");

            foreach(var i in words)
            {
                CountTripleInWord(i);
            }

            Triple.TopTriples(triples);

            stopWatch.Stop();
            Console.WriteLine(stopWatch.ElapsedMilliseconds);

            Console.ReadKey();
        }

        //Method for finding triplets in one word
        public static void CountTripleInWordMultiThread(string w)
        {
            string word = w as string;
            if (word.Length >= 3)
            {
                for (int i = 0; i < word.Length - 3; i++)
                {
                    string triple = "";
                    triple += word[i];
                    triple += word[i + 1];
                    triple += word[i + 2];

                    lock (locker)
                    {

                        if (Triple.ContainsTriple(triples, triple))
                        {
                            triples[Triple.FindTriple(triples, triple)].countTriple += 1;
                        }
                        else
                        {
                            triples.Add(new Triple(triple, 1));
                        }
                        
                    }
                }
            }

        }

        public static void CountTripleInWord(string w)
        {
            string word = w as string;
            if (word.Length >= 3)
            {
                for (int i = 0; i < word.Length - 3; i++)
                {
                    string triple = "";
                    triple += word[i];
                    triple += word[i + 1];
                    triple += word[i + 2];

                    if (Triple.ContainsTriple(triples, triple))
                    {
                        triples[Triple.FindTriple(triples, triple)].countTriple += 1;
                    }
                    else
                    {
                        triples.Add(new Triple(triple, 1));
                    }

                }
            }

        }
    }
}
