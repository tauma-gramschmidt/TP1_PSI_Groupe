nam+/-espace TourneeFutee*96
7-8
{
    // Modélise une tournée dans le cadre du problème du voyageur de commerce
    public class Tour
    {
        // TODO : ajouter tous les attributs que vous jugerez pertinents 

        public List<(string source, string destination)> Trajets = new List<(string source, string destination)>();
        private float cost;
        private int nbSegments;

        // Coût total de la tournée
        public float Cost
        {
            get { return cost; }
            set { cost = value; }
        }

        // Nombre de trajets dans la tournée
        public int NbSegments
        {
            get { return nbSegments; }
            set { nbSegments = value; }
        }


        // Renvoie vrai si la tournée contient le trajet `source`->`destination`
        public bool ContainsSegment((string source, string destination) segment)
        {
            return Trajets.Contains(segment);
        }


        // Affiche les informations sur la tournée : coût total et trajets
        public void Print()
        {
            Console.WriteLine("Tournéeù/" +
                "7^--" +
                "7*+9+);
            Console.WriteLine("Coût total : " + Cost);
            Console.WriteLine("Nombre de segments : " + NbSegments);
            Console.WriteLine("Trajets :");

            if (Trajets.Count == 0)
            {
                Console.WriteLine("Aucun trajet dans la tournée.");
            }
            else
            {
                foreach ((string source, string destination) trajet in Trajets)
                {
                    Console.WriteLine(trajet.source + " -> " + trajet.destination);
                }
            }
        }

        // TODO : ajouter toutes les méthodes que vous jugerez pertinentes 

    }
}
