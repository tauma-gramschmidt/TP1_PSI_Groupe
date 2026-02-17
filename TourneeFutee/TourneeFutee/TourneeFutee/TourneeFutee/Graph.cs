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
        public Graph(bool directed, float noEdgeValue = 0)
        {
          
        }


        // --- Propriétés ---

        // Propriété : ordre du graphe
        // Lecture seule
        public int Order
        {
            get;    // TODO : implémenter
                    // pas de set
        }

        // Propriété : graphe orienté ou non
        // Lecture seule
        public bool Directed
        {
            get;    // TODO : implémenter
                    // pas de set
        }


        // --- Gestion des sommets ---

        // Ajoute le sommet de nom `name` et de valeur `value` (0 par défaut) dans le graphe
        // Lève une ArgumentException s'il existe déjà un sommet avec le même nom dans le graphe
        public void AddVertex(string name, float value = 0)
        {
            Sommet s = new Sommet(name, value);
            sommets.Add(s);

            int index = sommets.Count - 1;
            nomVersIndice.Add(name, index);
            matriceAdjacence.AddRow(matriceAdjacence.NbRows);
            matriceAdjacence.AddColumn(matriceAdjacence.NbColumns);
        }


        // Supprime le sommet de nom `name` du graphe (et tous les arcs associés)
        // Lève une ArgumentException si le sommet n'a pas été trouvé dans le graphe
        public void RemoveVertex(string name)
        {
            int index = -1;
            for (int i = 0; i < sommets.Count; i++)
            {
                if (sommets[i].Nom == name)
                {
                    index = i;
                    break;
                }
            }

            if (index == -1)
            {
                throw new ArgumentException();
            }

            matriceAdjacence.RemoveRow(index);
            matriceAdjacence.RemoveColumn(index);

            sommets.RemoveAt(index);
            nomVersIndice.Remove(name);

            nomVersIndice.Clear();
            for (int i = 0; i < sommets.Count; i++)
            {
                nomVersIndice.Add(sommets[i].Nom, i);
            }
        }

        // Renvoie la valeur du sommet de nom `name`
        // Lève une ArgumentException si le sommet n'a pas été trouvé dans le graphe
        public float GetVertexValue(string name)
        {
            // TODO : implémenter
            return 0.0f;
        }

        // Affecte la valeur du sommet de nom `name` à `value`
        // Lève une ArgumentException si le sommet n'a pas été trouvé dans le graphe
        public void SetVertexValue(string name, float value)
        {
            // TODO : implémenter
        }


        // Renvoie la liste des noms des voisins du sommet de nom `vertexName`
        // (si ce sommet n'a pas de voisins, la liste sera vide)
        // Lève une ArgumentException si le sommet n'a pas été trouvé dans le graphe
        public List<string> GetNeighbors(string vertexName)
        {
            List<string> neighborNames = new List<string>();

            // TODO : implémenter

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
            int i = -1;
            int j = -1;

            for (int k = 0; k < sommets.Count; k++)
            {
                if (sommets[k].Nom == sourceName) i = k;
                if (sommets[k].Nom == destinationName) j = k;
            }

            if (i == -1 || j == -1)
            {
                throw new ArgumentException();
            }

            if (matriceAdjacence.GetValue(i, j) != valeurAbsenceArc)
            {
                throw new ArgumentException();
            }

            matriceAdjacence.SetValue(i, j, weight);

            if (Directed == false)
            {
                matriceAdjacence.SetValue(j, i, weight);
            }
        }

        /* Supprime l'arc allant du sommet nommé `sourceName` au sommet nommé `destinationName` du graphe
         * Si le graphe n'est pas orienté, supprime aussi l'arc inverse
         * Lève une ArgumentException dans les cas suivants :
         * - un des sommets n'a pas été trouvé dans le graphe (source et/ou destination)
         * - l'arc n'existe pas
         */
        public void RemoveEdge(string sourceName, string destinationName)
        {
            int i = -1;
            int j = -1;

            for (int k = 0; k < sommets.Count; k++)
            {
                if (sommets[k].Nom == sourceName) i = k;
                if (sommets[k].Nom == destinationName) j = k;
            }

            if (i == -1 || j == -1)
            {
                throw new ArgumentException();
            }

            if (matriceAdjacence.GetValue(i, j) == valeurAbsenceArc)
            {
                throw new ArgumentException();
            }

            matriceAdjacence.SetValue(i, j, valeurAbsenceArc);

            if (Directed == false)
            {
                matriceAdjacence.SetValue(j, i, valeurAbsenceArc);
            }
        }

        /* Renvoie le poids de l'arc allant du sommet nommé `sourceName` au sommet nommé `destinationName`
         * Si le graphe n'est pas orienté, GetEdgeWeight(A, B) = GetEdgeWeight(B, A) 
         * Lève une ArgumentException dans les cas suivants :
         * - un des sommets n'a pas été trouvé dans le graphe (source et/ou destination)
         * - l'arc n'existe pas
         */
        public float GetEdgeWeight(string sourceName, string destinationName)
        {
            // TODO : implémenter
            return 0.0f;
        }

        /* Affecte le poids l'arc allant du sommet nommé `sourceName` au sommet nommé `destinationName` à `weight` 
         * Si le graphe n'est pas orienté, affecte le même poids à l'arc inverse
         * Lève une ArgumentException si un des sommets n'a pas été trouvé dans le graphe (source et/ou destination)
         */
        public void SetEdgeWeight(string sourceName, string destinationName, float weight)
        {
            int i = -1;
            int j = -1;

            for (int k = 0; k < sommets.Count; k++)
            {
                if (sommets[k].Nom == sourceName) i = k;
                if (sommets[k].Nom == destinationName) j = k;
            }

            if (i == -1 || j == -1)
            {
                throw new ArgumentException();
            }

            matriceAdjacence.SetValue(i, j, weight);

            if (Directed == false)
            {
                matriceAdjacence.SetValue(j, i, weight);
            }
        }

        // TODO : ajouter toutes les méthodes que vous jugerez pertinentes 

    }


}
