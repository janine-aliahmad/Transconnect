using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TransConnect
{
    internal class Arbre_naire
    {
        /// <summary>
        /// cette classe a pour objectif de pouvoir representer l'organigraamme de l'entreprise
        /// elle n'a pas ete forcement utile mais on conserve la classe ou cas ou il y a eu besoin pour future utilisation
        /// </summary>
        
        #region Initialisation 
        //attributs
        Noeud_naire racine;

        //constructeur
        public Arbre_naire()
        {
            this.racine = new Noeud_naire();
        }


        public Arbre_naire(Noeud_naire n)
        {
            this.racine = n;
        }

        //proprietes
        public Noeud_naire Racine
        {
            set { this.racine = value; }
            get { return this.racine; }
        }

        #endregion

        #region Methodes
        public bool EstArbreVide()
        {
            return this == null;
        }
        public bool AssocierRacine(Salarie valeur)
        {
            bool ok = false;
            //si arbre vide et si paramètre non null
            if (this.EstArbreVide())
            {
                Noeud_naire n = new Noeud_naire(valeur);
                this.racine = n;
                ok = true;
            }

            return ok;
        }

        #endregion






    }
}
