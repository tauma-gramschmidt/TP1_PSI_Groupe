 namespace TourneeFutee
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Connexion a la base de donnees
            string server = "127.0.0.1";
            string dbname = "tourneefutee";
            string user = "root";
            string pwd = "root";
            Console.WriteLine("Tournee Futee");
            Console.WriteLine();
            // Creation du graphe de test (6 villes)
            Graph graphe = new Graph(true);

            graphe.AddVertex("A", 0);
            graphe.AddVertex("B", 0);
            graphe.AddVertex("C", 0);
            graphe.AddVertex("D", 0);
            graphe.AddVertex("E", 0);
            graphe.AddVertex("F", 0);

            graphe.AddEdge("A", "B", 1); graphe.AddEdge("A", "C", 7);
            graphe.AddEdge("A", "D", 3); graphe.AddEdge("A", "E", 14); graphe.AddEdge("A", "F", 2);
            graphe.AddEdge("B", "A", 3); graphe.AddEdge("B", "C", 6);
            graphe.AddEdge("B", "D", 9); graphe.AddEdge("B", "E", 1); graphe.AddEdge("B", "F", 24);
            graphe.AddEdge("C", "A", 6); graphe.AddEdge("C", "B", 14);
            graphe.AddEdge("C", "D", 3); graphe.AddEdge("C", "E", 7); graphe.AddEdge("C", "F", 3);
            graphe.AddEdge("D", "A", 2); graphe.AddEdge("D", "B", 3);
            graphe.AddEdge("D", "C", 5); graphe.AddEdge("D", "E", 9); graphe.AddEdge("D", "F", 11);
            graphe.AddEdge("E", "A", 15); graphe.AddEdge("E", "B", 7);
            graphe.AddEdge("E", "C", 11); graphe.AddEdge("E", "D", 2); graphe.AddEdge("E", "F", 4);
            graphe.AddEdge("F", "A", 20); graphe.AddEdge("F", "B", 5);
            graphe.AddEdge("F", "C", 13); graphe.AddEdge("F", "D", 4); graphe.AddEdge("F", "E", 18);

            Console.WriteLine("Graphe cree avec " + graphe.Order + " sommets.");
            Console.WriteLine("Graphe oriente : " + graphe.Directed);
            Console.WriteLine();

            // Calcul de la tournee optimale avec l'algorithme de Little
            Console.WriteLine("Calcul de la tournee optimale...");
            Little algo = new Little(graphe);
            Tour tournee = algo.ComputeOptimalTour();
            tournee.Print();
            Console.WriteLine();

            // Sauvegarde et chargement depuis la base de donnees
            Console.WriteLine("Connexion a la base de donnees...");
            try
            {
                ServicePersistance service = new ServicePersistance(server, dbname, user, pwd);
                Console.WriteLine("Connexion reussie !");
                Console.WriteLine();

                // Sauvegarde du graphe
                uint grapheId = service.SaveGraph(graphe);
                Console.WriteLine("Graphe sauvegarde, id = " + grapheId);

                // Rechargement du graphe
                Graph grapheCharge = service.LoadGraph(grapheId);
                Console.WriteLine("Graphe recharge : " + grapheCharge.Order + " sommets");

                // Sauvegarde de la tournee
                uint tourneeId = service.SaveTour(grapheId, tournee);
                Console.WriteLine("Tournee sauvegardee, id = " + tourneeId);

                // Rechargement de la tournee
                Tour tourneeChargee = service.LoadTour(tourneeId);
                Console.WriteLine("Tournee rechargee : cout = " + tourneeChargee.Cost);
                Console.WriteLine("Sequence : ");
                foreach (string ville in tourneeChargee.Vertices)
                {
                    Console.Write(ville + " ");
                }
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur : " + e.Message);
            }

            Console.WriteLine();
            Console.WriteLine("Appuyez sur une touche pour fermer...");
            Console.ReadKey();
        }
    }
}
