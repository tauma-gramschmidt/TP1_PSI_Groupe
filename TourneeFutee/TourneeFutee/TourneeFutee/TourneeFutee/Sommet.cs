using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourneeFutee
{
    internal class Sommet
    {

        private string nom;
        private float valeur;
        public Sommet(string nom, float valeur)
        {
            this.nom = nom;
            this.valeur = valeur;
        }

        public string Nom
        {
            get
            {
                return nom;
            }
        }

        public float Valeur
        {
            get 
            { 
                return valeur; 
            }
            set 
            {
                this.valeur = value; 
            }

        }
    }
}
