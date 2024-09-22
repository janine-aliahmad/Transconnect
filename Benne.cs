using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    internal class Benne : Camion, IIdentifiant<string>
    {
        /// <summary>
        /// pour les camion-benne: on a un choix entre 1 a 3 benne avec chacune une matiere de transport au choix : Sable - Terre - Gravier
        /// on a aussi le choix entre True/False pour une grue auxiliaire
        /// </summary>

        #region attribut
        int nb_benne;
        string matiere1;
        string matiere2;
        string matiere3;
        bool grue;
        #endregion

        #region constructeur

        //on a 3 constructeur pour 1,2 ou 3 bennes
        public Benne ( int poids, int volume, int nb_benne, string matiere1, string matiere2, string matiere3, bool grue) : base( poids, volume)
        {
            this.nb_benne = nb_benne;
            this.matiere1 = matiere1;
            this.matiere2 = matiere2;
            this.matiere3 = matiere3;
            this.grue = grue;
            this.prix_km= this.GetPrix_KM(); 
        }

        public Benne( int poids, int volume, int nb_benne, string matiere1, string matiere2, bool grue) : base( poids, volume)
        {
            this.nb_benne = nb_benne;
            this.matiere1 = matiere1;
            this.matiere2 = matiere2;
            this.grue = grue;
            this.prix_km = this.GetPrix_KM();

        }

        public Benne( int poids, int volume, int nb_benne, string matiere1, bool grue) : base( poids, volume)
        {
            this.nb_benne = nb_benne;
            this.matiere1 = matiere1;
            this.grue = grue;
            this.prix_km = this.GetPrix_KM();

        }
        #endregion

        #region propriete

        public int Nb_benne { get { return this.nb_benne; } }
        public string Matiere1 { get {  return this.matiere1; } set { this.matiere1 = value; } }
        public string Matiere2 { get {  return this.matiere2; } set { this.matiere2 = value; } }
        public string Matiere3 { get {  return this.matiere3; } set { this.matiere3 = value; } }
        public bool Grue { get { return this.grue; } set { this.grue = value; } }

        #endregion

        #region methode

        public override string ToString()
        {
            if(this.nb_benne == 3)
            {
                return "\nType du Vehicule: Camion Citerne" + "\nNombre de benne: " + this.nb_benne + "\nMatiere1: " + this.matiere1 + "\nMatiere2: " + this.matiere2 + "\nMatiere3: " + this.matiere3 + "\nGrue auxiliaire? " + this.grue + base.ToString();
            }
            if (this.nb_benne == 2)
            {
                return "\nType du Vehicule: Camion Citerne" + "\nNombre de benne: " + this.nb_benne + "\nMatiere1: " + this.matiere1 + "\nMatiere2: " + this.matiere2 + "\nGrue auxiliaire? " + this.grue + base.ToString();
            }
            else
            {
                return "\nType du Vehicule: Camion Citerne" + "\nNombre de benne: " + this.nb_benne + "\nMatiere1: " + this.matiere1 + "\nGrue auxiliaire? " + this.grue + base.ToString();
            }
        }

        /// <summary>
        /// retourne le prix par km du type de vehicule en question
        /// supplement en fcontion du nb benne et de la grue
        /// </summary>
        /// <returns></returns>
        public override double GetPrix_KM()
        {
            //prix pour une benne puis augmente avec le nombre de benne + prix grue 

            double prix = (this.poids + this.volume) * 0.75 + 5;

            if (this.nb_benne == 3) { prix = prix * 2.5; }
            if (this.nb_benne == 2) { prix = prix * 1.75; }
            if (this.grue) { prix = prix + 0.5; }

            return prix;
            
        }

        /// <summary>
        /// fonction qui retourne l'identifiant 
        /// </summary>
        /// <returns></returns>
        public new string Get_ID()
        {
            return base.Get_ID().ToString() + " : Camion Benne";
        }

        #endregion
    }
}
