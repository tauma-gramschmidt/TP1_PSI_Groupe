namespace TourneeFutee
{
    // Résout le problème de voyageur de commerce défini par le graphe `graph`
    // en utilisant l'algorithme de Little
    public class Little
    {
        // TODO : ajouter tous les attributs que vous jugerez pertinents

        private Graph graph;
        private List<string> cities;
        private int nbCities;
        private const float INF = float.PositiveInfinity;


        // Instancie le planificateur en spécifiant le graphe modélisant un problème de voyageur de commerce
        public Little(Graph graph)
        {
            this.graph = graph;
            this.cities = new List<string>();
            this.nbCities = 0;
        }

        // Trouve la tournée optimale dans le graphe `this.graph`
        // (c'est à dire le cycle hamiltonien de plus faible coût)
        public Tour ComputeOptimalTour()
        {
            // TODO : implémenter
            return new Tour();
        }

        // --- Méthodes utilitaires réalisant des étapes de l'algorithme de Little


        // Réduit la matrice `m` et revoie la valeur totale de la réduction
        // Après appel à cette méthode, la matrice `m` est *modifiée*.
        public static float ReduceMatrix(Matrix m)
        {
            float totalReduction = 0.0f;

            // Réduction des lignes
            for (int i = 0; i < m.NbRows; i++)
            {
                float minRow = float.PositiveInfinity;

                // Recherche du minimum fini de la ligne
                for (int j = 0; j < m.NbColumns; j++)
                {
                    float value = m.GetValue(i, j);
                    if (float.IsPositiveInfinity(value) == false && value < minRow)
                    {
                        minRow = value;
                    }
                }

                // Si la ligne contient au moins une valeur finie, on réduit
                if (float.IsPositiveInfinity(minRow) == false && minRow > 0.0f)
                {
                    for (int j = 0; j < m.NbColumns; j++)
                    {
                        float value = m.GetValue(i, j);
                        if (!float.IsPositiveInfinity(value))
                        {
                            m.SetValue(i, j, value - minRow);
                        }
                    }

                    totalReduction += minRow;
                }
            }

            // Réduction des colonnes
            for (int j = 0; j < m.NbColumns; j++)
            {
                float minColumn = float.PositiveInfinity;

                // Recherche du minimum fini de la colonne
                for (int i = 0; i < m.NbRows; i++)
                {
                    float value = m.GetValue(i, j);
                    if (float.IsPositiveInfinity(value) == false && value < minColumn)
                    {
                        minColumn = value;
                    }
                }

                // Si la colonne contient au moins une valeur finie, on réduit
                if (float.IsPositiveInfinity(minColumn) == false && minColumn > 0.0f)
                {
                    for (int i = 0; i < m.NbRows; i++)
                    {
                        float value = m.GetValue(i, j);
                        if (!float.IsPositiveInfinity(value))
                        {
                            m.SetValue(i, j, value - minColumn);
                        }
                    }

                    totalReduction += minColumn;
                }
            }

            return totalReduction;
        }

        // Renvoie le regret de valeur maximale dans la matrice de coûts `m` sous la forme d'un tuple `(int i, int j, float value)`
        // où `i`, `j`, et `value` contiennent respectivement la ligne, la colonne et la valeur du regret maximale
        public static (int i, int j, float value) GetMaxRegret(Matrix m)
        {
            int bestI = -1;
            int bestJ = -1;
            float maxRegret = -1.0f;

            for (int i = 0; i < m.NbRows; i++)
            {
                for (int j = 0; j < m.NbColumns; j++)
                {
                    
                    if (m.GetValue(i, j) == 0.0f)
                    {
                       
                        float minRow = float.PositiveInfinity;
                        for (int col = 0; col < m.NbColumns; col++)
                        {
                            if (col != j && m.GetValue(i, col) < minRow)
                                minRow = m.GetValue(i, col);
                        }

                        float minCol = float.PositiveInfinity;
                        for (int row = 0; row < m.NbRows; row++)
                        {
                            if (row != i && m.GetValue(row, j) < minCol)
                                minCol = m.GetValue(row, j);
                        }

                        float currentRegret = minRow + minCol;

                        if (currentRegret > maxRegret)
                        {
                            maxRegret = currentRegret;
                            bestI = i;
                            bestJ = j;
                        }
                    }
                }
            }
            return (bestI, bestJ, maxRegret);

        }

        /* Renvoie vrai si le segment `segment` est un trajet parasite, c'est-à-dire s'il ferme prématurément la tournée incluant les trajets contenus dans `includedSegments`
         * Une tournée est incomplète si elle visite un nombre de villes inférieur à `nbCities`
         */
        public static bool IsForbiddenSegment((string source, string destination) segment, List<(string source, string destination)> includedSegments, int nbCities)
        {

            
            if (includedSegments.Count < nbCities - 1)
            {
                
                string current = segment.destination;
                bool chainContinues = true;

                while (chainContinues)
                {
                    chainContinues = false;
                    
                    foreach (var s in includedSegments)
                    {
                        if (s.source == current)
                        {
                            current = s.destination;
                            chainContinues = true;
                            break;
                        }
                    }

               
                    if (current == segment.source)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        // TODO : ajouter toutes les méthodes que vous jugerez pertinentes 

    }
}
