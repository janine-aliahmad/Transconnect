using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    internal class Citerne : Camion , IIdentifiant<string>
    {
        /// <summary>
        /// pour les citerne l'entreprise dispose des cuves pour les produits suivant (facteur prix):
        /// 1. Matières dangereuses --> 3.5
        /// 2. Produits pétroliers de grande valeur --> 3
        /// 3. Gaz --> 2
        /// 4. Eau --> 1
        /// Le prix/km est calcule en fonction du poids, volume, et la matiere transporte : (poids kg + volume l)*facteur
        /// </summary>


        //attribut
        string matiere;

        //constructeur
        public Citerne(int poids, int volume, string matiere): base(poids, volume)
        {
            this.matiere = matiere;
            this.prix_km = this.GetPrix_KM();
         
        }

        //propriete
        public string Matiere { get { return this.matiere; } }

        //methode
        public override string ToString()
        {
            return "\nType du Vehicule: Camion Citerne" + "\nMatriere a transporte: " + this.matiere + base.ToString();
        }

        /// <summary>
        /// retiurne le prix par km du type de vehicule en question
        /// </summary>
        /// <returns></returns>
        public override double GetPrix_KM()
        {
            double prix = (this.poids + this.volume) * 0.75;

            if (matiere == "Matières dangereuses")
            {
                return prix * 3.5;

            }
            if (matiere == "Produits pétroliers de grande valeur")
            {
                return prix * 3;

            }
            if (matiere == "Gaz")
            {
                return prix * 2;

            }
            else
            {
                return prix;
            }
            
        }

        /// <summary>
        /// fonction qui retourne l'identifiant 
        /// </summary>
        /// <returns></returns>
        public new string Get_ID()
        {
            return base.Get_ID().ToString() + " : Camion Citerne";
        }
    }
}
