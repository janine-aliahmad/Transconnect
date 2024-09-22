using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    internal class Frigorifique : Camion , IIdentifiant<String>
    {
        /// <summary>
        /// pour les camion frigorifique, aucun parametre de plus que le camion en generale, seul le prix change.
        /// </summary>
        
        //constructeur
        public Frigorifique(int poids, int volume) : base ( poids, volume)
        {
            this.prix_km = this.GetPrix_KM();
        }

        #region methode

        public override string ToString()
        {
            return "\nType du Vehicule: Camion Frigorifique" + base.ToString();
        }

        /// <summary>
        /// cette fonction retoure le prix par km du type de vehicule en question
        /// </summary>
        /// <returns></returns>
        public override double GetPrix_KM()
        {
            return (this.poids + this.volume) * 0.75 * 4;
        }

        /// <summary>
        /// fonction qui retourne l'identifiant 
        /// </summary>
        /// <returns></returns>
        public new string Get_ID()
        {
            return base.Get_ID().ToString() + " : Camion Frigorifique";
        }

        #endregion
    }
}
