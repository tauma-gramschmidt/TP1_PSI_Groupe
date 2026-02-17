namespace TourneeFutee
{
    public class Graph
    {

        // TODO : ajouter tous les attributs que vous jugerez pertinents 


        /// --- Attributs ---
        private bool oriente;
        private float valeurAbsenceArc;

        private Matrix matriceAdjacence;

        private List<Sommet> sommets;
        private Dictionary<string, int> nomVersIndice;

        public Graph(bool oriente, float valeurAbsenceArc = 0)
        {
            this.oriente = oriente;
            this.valeurAbsenceArc = valeurAbsenceArc;

            sommets = new List<Sommet>();
            nomVersIndice = new Dictionary<string, int>();

            matriceAdjacence = new Matrix(0, 0, valeurAbsenceArc);
        }


        // --- Propriétés ---

        // Propriété : ordre du graphe
        // Lecture seule
        public int Order
        {
            get {return sommets.Count;}
        }

        // Propriété : graphe orienté ou non
        // Lecture seule
        public bool Directed
        {
            get {return oriente;}
        }


        // --- Gestion des sommets ---

        // Ajoute le sommet de nom `name` et de valeur `value` (0 par défaut) dans le graphe
        // Lève une ArgumentException s'il existe déjà un sommet avec le même nom dans le graphe
        public void AddVertex(string name, float value = 0)
        {
            // TODO : implémenter
        }


        // Supprime le sommet de nom `name` du graphe (et tous les arcs associés)
        // Lève une ArgumentException si le sommet n'a pas été trouvé dans le graphe
        public void RemoveVertex(string name)
        {
            // TODO : implémenter
        }

        // Renvoie la valeur du sommet de nom `name`
        // Lève une ArgumentException si le sommet n'a pas été trouvé dans le graphe
        public float GetVertexValue(string name)
        {
            if (nomVersIndice.ContainsKey(name) == false)
            {
                throw new ArgumentException("Sommet introuvable : " + name);
            }

            int indice = nomVersIndice[name];
            return sommets[indice].Valeur;
        }

        // Affecte la valeur du sommet de nom `name` à `value`
        // Lève une ArgumentException si le sommet n'a pas été trouvé dans le graphe
        public void SetVertexValue(string name, float value)
        {
            if (nomVersIndice.ContainsKey(name) == false)
            {
                throw new ArgumentException("Sommet introuvable : " + name);
            }

            int indice = nomVersIndice[name];
            sommets[indice].Valeur = value;
        }


        // Renvoie la liste des noms des voisins du sommet de nom `vertexName`
        // (si ce sommet n'a pas de voisins, la liste sera vide)
        // Lève une ArgumentException si le sommet n'a pas été trouvé dans le graphe
        public List<string> GetNeighbors(string vertexName)
        {
            List<string> neighborNames = new List<string>();

            if (nomVersIndice.ContainsKey(vertexName) == false)
            {
                throw new ArgumentException("Sommet introuvable : " + vertexName);
            }

            int i = nomVersIndice[vertexName];

            for (int j = 0; j < Order; j++)
            {
                float poids = matriceAdjacence.GetValue(i, j);

                if (poids != valeurAbsenceArc)
                {
                    neighborNames.Add(sommets[j].Nom);                
                }
            }

            return neighborNames;
        }


        // --- Gestion des arcs ---

        /* Ajoute un arc allant du sommet nommé `sourceName` au sommet nommé `destinationName`, avec le poids `weight` (1 par défaut)
         * Si le graphe n'est pas orienté, ajoute aussi l'arc inverse, avec le même poids
         * Lève une ArgumentException dans les cas suivants :
         * - un des sommets n'a pas été trouvé dans le graphe (source et/ou destination)
         * - il existe déjà un arc avec ces extrémités
         */
        public void AddEdge(string sourceName, string destinationName, float weight = 1)
        {
            // TODO : implémenter
        }

        /* Supprime l'arc allant du sommet nommé `sourceName` au sommet nommé `destinationName` du graphe
         * Si le graphe n'est pas orienté, supprime aussi l'arc inverse
         * Lève une ArgumentException dans les cas suivants :
         * - un des sommets n'a pas été trouvé dans le graphe (source et/ou destination)
         * - l'arc n'existe pas
         */
        public void RemoveEdge(string sourceName, string destinationName)
        {
            // TODO : implémenter
        }

        /* Renvoie le poids de l'arc allant du sommet nommé `sourceName` au sommet nommé `destinationName`
         * Si le graphe n'est pas orienté, GetEdgeWeight(A, B) = GetEdgeWeight(B, A) 
         * Lève une ArgumentException dans les cas suivants :
         * - un des sommets n'a pas été trouvé dans le graphe (source et/ou destination)
         * - l'arc n'existe pas
         */
        public float GetEdgeWeight(string sourceName, string destinationName)
        {
            if (nomVersIndice.ContainsKey(sourceName) == false)
            {
                throw new ArgumentException("Sommet introuvable : " + sourceName);
            }

            if (nomVersIndice.ContainsKey(destinationName) == false)
            {
                throw new ArgumentException("Sommet introuvable : " + destinationName);
            }

            int i = nomVersIndice[sourceName];
            int j = nomVersIndice[destinationName];

            float poids = matriceAdjacence.GetValue(i, j);

            if (poids == valeurAbsenceArc)
            {
                throw new ArgumentException("Arc inexistant : (" + sourceName + ", " + destinationName + ")");
            }

            return poids;
        }


        /* Affecte le poids l'arc allant du sommet nommé `sourceName` au sommet nommé `destinationName` à `weight` 
         * Si le graphe n'est pas orienté, affecte le même poids à l'arc inverse
         * Lève une ArgumentException si un des sommets n'a pas été trouvé dans le graphe (source et/ou destination)
         */
        public void SetEdgeWeight(string sourceName, string destinationName, float weight)
        {
            // TODO : implémenter
        }

        // TODO : ajouter toutes les méthodes que vous jugerez pertinentes 

    }


}
