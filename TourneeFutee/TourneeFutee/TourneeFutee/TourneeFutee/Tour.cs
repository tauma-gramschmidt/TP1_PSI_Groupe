namespace TourneeFutee
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
            return false;   // TODO : implémenter 
        }


        // Affiche les informations sur la tournée : coût total et trajets
        public void Print()
        {
            // TODO : implémenter 
        }

        // TODO : ajouter toutes les méthodes que vous jugerez pertinentes 

    }
}
