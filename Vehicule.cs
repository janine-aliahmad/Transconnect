using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    abstract class Vehicule : IIdentifiant<int>
    {
        /// <summary>
        /// classe mere de tout les vehicule de l'entreprise
        /// on precie pas le prix car celui-ci est calculer en fonction du type choisit
        /// chaque vehicule est identifier par son unique numero d'immatriculation
        /// </summary>

        #region Initialisation
        //attribut
        protected int immatriculation;
        protected double prix_km;
        protected List<DateTime> date_prises;
        private static int nb_vehicule = 0;
        

        //constructeur
        public Vehicule()
        {
            nb_vehicule++;
            this.immatriculation = nb_vehicule;
            date_prises = new List<DateTime>();
        }

        //propriete

        public int Immatriculation { get { return this.immatriculation; } }

        public double Prix_km { get {  return this.prix_km; } }

        public List<DateTime> Date_prises { get {  return this.date_prises; } set { this.date_prises = value; } }

        #endregion

        #region Methodes
        public override string ToString()
        {
            return "\nImmatriculation du Vehicule: " + this.immatriculation.ToString() + "\nPrix_KM: " + this.prix_km;
        }

        abstract public double GetPrix_KM(); // cette fonction est imposer a toute les classes filles pour calculer le prix/km en fonction du type de vehicule

        /// <summary>
        /// cette fonction teste si un vehicule et disponible pour une date donnee
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public bool Disponible(DateTime date)
        {
            foreach (DateTime d in this.date_prises)
            {
                if (d == date) return false;
            }

            return true;
        }

        /// <summary>
        /// fonction qui retourne l'identifiant 
        /// </summary>
        /// <returns></returns>
        public int Get_ID()
        {
            return this.immatriculation;
        }

        #endregion
    }
}
