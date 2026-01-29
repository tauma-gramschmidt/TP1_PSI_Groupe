namespace TourneeFutee
{
    public class Matrix
    {
        // TODO : ajouter tous les attributs que vous jugerez pertinents 
        /* Crée une matrice de dimensions `nbRows` x `nbColumns`.
         * Toutes les cases de cette matrice sont remplies avec `defaultValue`.
         * Lève une ArgumentOutOfRangeException si une des dimensions est négative
         */
        private int nbRows;
        private int nbColumns;
        private List<List<float>> matrix;
        private float defaultValue;


        public Matrix(int nbRows = 0, int nbColumns = 0, float defaultValue = 0)
        {
            if (nbRows < 0 || nbColumns < 0)
            {
                throw new ArgumentOutOfRangeException("Les dimensions de la matrice ne peuvent pas être négatives.");
            }

            this.nbRows = nbRows;
            this.nbColumns = nbColumns;
            this.defaultValue = defaultValue;

            this.matrix = new List<List<float>>(nbRows);
            for (int i = 0; i < nbRows; i++)
            {
                var row = new List<float>(nbColumns);
                for (int j = 0; j < nbColumns; j++)
                {
                    row.Add(defaultValue);
                }
                this.matrix.Add(row);
            }
        }


        // Propriété : valeur par défaut utilisée pour remplir les nouvelles cases
        // Lecture seule
        public float DefaultValue
        {
            get { return defaultValue; }
        }

        // Propriété : nombre de lignes
        // Lecture seule
        public int NbRows
        {
            get { return nbRows; }
        }

        // Propriété : nombre de colonnes
        // Lecture seule
        public int NbColumns
        {
            get { return nbColumns; }
        }

        /* Insère une ligne à l'indice `i`. Décale les lignes suivantes vers le bas.
         * Toutes les cases de la nouvelle ligne contiennent DefaultValue.
         * Si `i` = NbRows, insère une ligne en fin de matrice
         * Lève une ArgumentOutOfRangeException si `i` est en dehors des indices valides
         */
        public void AddRow(int i)
        {
            if(i<0 || i>NbRows)
            {
                throw new ArgumentOutOfRangeException("i doit être compris entre 0 et NbRows");
            }

            var newRow = new List<float>(nbColumns);
            for (int j = 0; j < nbColumns; j++)
            {
                newRow.Add(defaultValue);
            }
            matrix.Insert(i, newRow);
            nbRows++;
        }

        /* Insère une colonne à l'indice `j`. Décale les colonnes suivantes vers la droite.
         * Toutes les cases de la nouvelle ligne contiennent DefaultValue.
         * Si `j` = NbColums, insère une colonne en fin de matrice
         * Lève une ArgumentOutOfRangeException si `j` est en dehors des indices valides
         */
        public void AddColumn(int j)
        {
            if (j < 0 || j > nbColumns)
            {
                throw new ArgumentOutOfRangeException("j doit être compris entre 0 et NbColumns (inclus).");
            }

            for (int i = 0; i < nbRows; i++)
            {
                matrix[i].Insert(j, defaultValue);
            }

            nbColumns++;
        }


        // Supprime la ligne à l'indice `i`. Décale les lignes suivantes vers le haut.
        // Lève une ArgumentOutOfRangeException si `i` est en dehors des indices valides
        public void RemoveRow(int i)
        {
            // TODO : implémenter
        }

        // Supprime la colonne à l'indice `j`. Décale les colonnes suivantes vers la gauche.
        // Lève une ArgumentOutOfRangeException si `j` est en dehors des indices valides
        public void RemoveColumn(int j)
        {
            // TODO : implémenter
        }

        // Renvoie la valeur à la ligne `i` et colonne `j`
        // Lève une ArgumentOutOfRangeException si `i` ou `j` est en dehors des indices valides
        public float GetValue(int i, int j)
        {
            // TODO : implémenter
            return 0.0f;
        }

        // Affecte la valeur à la ligne `i` et colonne `j` à `v`
        // Lève une ArgumentOutOfRangeException si `i` ou `j` est en dehors des indices valides
        public void SetValue(int i, int j, float v)
        {
            // TODO : implémenter
        }

        // Affiche la matrice
        public void Print()
        {
            // TODO : implémenter
        }


        // TODO : ajouter toutes les méthodes que vous jugerez pertinentes 

    }


}
