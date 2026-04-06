namespace TourneeFutee
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Graph g1 = new Graph(true);
            foreach (string s in new[] { "A", "B", "C", "D" })
            {
                g1.AddVertex(s);
            }

            g1.AddEdge("A", "B", 10);
            g1.AddEdge("A", "C", 15);
            g1.AddEdge("A", "D", 20);
            g1.AddEdge("B", "A", 10);
            g1.AddEdge("B", "C", 35);
            g1.AddEdge("B", "D", 25);
            g1.AddEdge("C", "A", 15);
            g1.AddEdge("C", "B", 35);
            g1.AddEdge("C", "D", 30);
            g1.AddEdge("D", "A", 20);
            g1.AddEdge("D", "B", 25);
            g1.AddEdge("D", "C", 30);

            Console.WriteLine("Test 1 : 4 villes");
            Little algo1 = new Little(g1);
            Tour res1 = algo1.ComputeOptimalTour();
            res1.Print();

            Console.WriteLine();

            Graph g2 = new Graph(true);
            foreach (string s in new[] { "X", "Y", "Z" })
            {
                g2.AddVertex(s);
            }

            g2.AddEdge("X", "Y", 1);
            g2.AddEdge("Y", "Z", 2);
            g2.AddEdge("Z", "X", 3);
            g2.AddEdge("X", "Z", 10);
            g2.AddEdge("Z", "Y", 10);
            g2.AddEdge("Y", "X", 10);

            Console.WriteLine("Test 2 : 3 villes (coût attendu : 6)"); // solution attendue : X -> Y -> Z -> X
            Little algo2 = new Little(g2);
            Tour res2 = algo2.ComputeOptimalTour();
            res2.Print();

            Console.WriteLine();
            Console.WriteLine("Appuyez sur une touche pour fermer...");
            Console.ReadKey();
        }
    }
}
