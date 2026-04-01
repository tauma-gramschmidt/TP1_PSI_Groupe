namespace TourneeFutee
{
    public class Tour
    {
        public List<(string source, string destination)> Trajets = new List<(string source, string destination)>();
        private float cost;
        private int nbSegments;

        public float Cost
        {
            get { return cost; }
            set { cost = value; }
        }

        public int NbSegments
        {
            get { return nbSegments; }
            set { nbSegments = value; }
        }

        public bool ContainsSegment((string source, string destination) segment)
        {
            return Trajets.Contains(segment);
        }

        public void Print()
        {
            Console.WriteLine("Tournée");
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
    }
}