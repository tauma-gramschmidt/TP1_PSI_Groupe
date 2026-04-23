

namespace TourneeFutee
{
    public class Tour
    {
        private List<string> vertices = new List<string>();
        private List<(string source, string destination)> trajets = new List<(string source, string destination)>();
        private float cost;
        private int nbSegments;

        public List<(string source, string destination)> Trajets
        {
            get { return trajets; }
            set { trajets = value; }
        }
        public Tour()
        {
            Vertices = new List<string>();
            Trajets = new List<(string source, string destination)>();
            Cost = 0;
            NbSegments = 0;
        }
        public Tour(List<string> vertices, float cost)
        {
            Vertices = vertices;
            Cost = cost;
            Trajets = new List<(string source, string destination)>();
            NbSegments = vertices.Count > 0 ? vertices.Count - 1 : 0;
        }

        public List<string> Vertices
        {
            get { return vertices; }
            set { vertices = value; }
        }

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