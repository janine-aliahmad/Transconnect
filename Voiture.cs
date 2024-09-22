using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    internal class Voiture : Vehicule , IIdentifiant<String>
    {
        /// <summary>
        /// on a donc deux type de voiture en fonction du nb de passager : petite voiture (1 a 4 passager) / grande voiture(5 a 7 passager)
        /// le prix par km est de 3 euros/km pour une petite voiture et 5eurso/km pour une grande voiture 
        /// on a un max de 7 passager pour les voitures
        /// </summary>
        /// 

        #region initialisation

        int nb_passager;
        string type;

        //constructeur
        public Voiture (int nb_passager) : base()
        {
            if(nb_passager <= 7) { this.nb_passager = nb_passager; }
            else { this.nb_passager = 7; }

            this.prix_km = this.GetPrix_KM();         
        }

        //propriete
        public int Nb_Passager { get { return this.nb_passager; } }
        public string Type { get { return this.type; } }

        #endregion

        #region Methodes
        //methodes

        public override string ToString()
        {
            if (this.type == "petite")
            {
                return "\nType du Vehicule: Petite voiture" + "\nNombre de passager: " + this.nb_passager + base.ToString() ;

            }
            else
            {
                return base.ToString() + "\nType du Vehicule: Grande voiture" + "\nNombre de passager: " + this.nb_passager;
            }
        }

        /// <summary>
        /// cette fonction renvoie le prix par km du type de vehicule en question
        /// </summary>
        /// <returns></returns>
        public override double GetPrix_KM()
        {
            if (this.nb_passager <= 4)
            {
                this.type = "petite";
                return 3;
            }
            else
            {
                this.type = "grande";
                return 5;
            }
        }

        /// <summary>
        /// fonction qui retourne l'identifiant 
        /// </summary>
        /// <returns></returns>
        public new string Get_ID()
        {
            return base.Get_ID().ToString() + " : Voiture";
        }

        #endregion
    }
}
