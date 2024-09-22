using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    internal class Camionette : Vehicule ,IIdentifiant<string>
    {
        /// <summary>
        /// pour ce type de vehicule voici les possibilites de causes de transport:
        /// 1. Transport de verre pour vitriers --> le plus chere car necessite materielle precis
        /// 2. Services de restauration et d'événementiel --> moyennement chere car necessite une attention au decors
        /// 3. Transport de meubles --> le moins chere
        /// </summary>

        #region initialisation
        //attribut
        string cause;

        //contructeur
        public Camionette (string cause) : base()
        {
            this.cause = cause;
            this.prix_km = this.GetPrix_KM();
        }

        //proprietes
        public string Cause
        {
            get { return this.cause; }
            set { this.cause = value; }
        }
        #endregion

        #region methodes
        //methodes

        public override string ToString()
        {
            return "\nType du Vehicule: Camionette" + "\n Cause de livraison: " + this.cause + base.ToString();
        }

        /// <summary>
        /// cette fonction revoie le prix par km pour le type de vehicule en queston
        /// </summary>
        /// <returns></returns>
        public override double GetPrix_KM()
        {
            if (cause == "Transport de verre pour vitriers")
            {
                return 12;
            }
            if (cause == "Services de restauration et d'événementiel ")
            {
                return 10;
            }
            else
            {
                return 9;
            }
        }

        /// <summary>
        /// fonction qui retourne l'identifiant 
        /// </summary>
        /// <returns></returns>
        public new string Get_ID()
        {
            return base.Get_ID().ToString() + " : Camoinette";
        }

        #endregion

    }
}
