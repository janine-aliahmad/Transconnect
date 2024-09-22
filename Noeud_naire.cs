using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{

    internal class Noeud_naire
    {
        #region attribut
        
        Salarie valeur;
        Noeud_naire frere; //pas utilisation de classe predefinie List<>
        List<Noeud_naire> successeurs; //pas utilisation de classe predefinie List<>
        #endregion

        #region Constructeur
        public Noeud_naire()
        {
            this.valeur = null;
            this.frere = null;
            this.successeurs = null;
        }
        public Noeud_naire(Salarie v)
        {
            this.valeur = v;
            this.frere = null;
            this.successeurs = null;
        }

        public Noeud_naire(Salarie v, Noeud_naire x, List<Noeud_naire> y)
        {
            this.valeur = v;
            this.frere = x;
            this.successeurs = y;
        }

        #endregion

        #region Proprietes
        public Salarie Valeur
        {
            get { return this.valeur; }
            set { this.valeur = value; }
        }

        public Noeud_naire Frere
        {
            get { return this.frere; }
            set { this.frere = value; }
        }

        public List<Noeud_naire> Successeurs
        {
            get { return this.successeurs; }
            set { this.successeurs = value; }
        }
        #endregion

        #region Methodes

        public override string ToString()
        {
            return this.Valeur.ToString();
        }

        /// <summary>
        /// Cette fonction ajoute un enfant à la liste des successeurs d'un nœud parent.
        /// Si le parent n'a aucun enfant, on crée une liste de successeurs et on l'ajoute.
        /// Si le parent possède déjà des successeurs, on ajoute le nouvel enfant à la liste.
        /// On recherche ensuite le dernier élément de la liste et on associe le nouveau enfant comme son frère.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public bool AssocierSuccesseur(Noeud_naire s)
        {
            bool ok = false;

            if (this != null && s != null)
            {
                //si pas déjà de successeur 
                if (this.successeurs == null)
                {
                    List<Noeud_naire> l= new List<Noeud_naire>();
                    l.Add(s);
                    this.successeurs = l;
                    ok = true;
                }
                else //si deja successeurs
                {
                    this.successeurs.Add(s);
                    (this.successeurs[this.successeurs.Count() - 1]).Frere = s;
                    ok = true;
                }

            }

            return ok;
        }

        /// <summary>
        /// cette fonction affiche l'ensemble des successeur d'un noeud
        /// </summary>
        public void EnsSuccesseurs()
        {
            /*Cette fonction affiche l'ensemble des successeur d'un noeud*/

            if (this != null && this.successeurs != null)
            {
                foreach (Noeud_naire n in  this.successeurs)
                {
                    Console.WriteLine(n);
                }
            }

        }

        /// <summary>
        /// cette fonction retourne l'ensemble des feres d'un noeud
        /// </summary>
        public void EnsFreres()
        {

            if (this != null)
            {
                Noeud_naire courant = new Noeud_naire();
                courant = this.frere;
                while (courant.Frere != null)
                {
                    Console.Write(courant.Frere);
                    courant = courant.Frere;
                }
            }
        }

        /// <summary>
        /// cette fonction affiche l'organigramme d'une entreprise a partir de la racine 
        /// </summary>
        /// <param name="indent"></param>
        /// <param name="last"></param>
        public void Afficher_Organigramme(string indent, bool last)
        {
            Console.Write(indent);
            if (last)
            {
                Console.Write("\\-");
                indent += "  ";
            }
            else
            {
                Console.Write("|-");
                indent += "| ";
            }
            Console.WriteLine(this.Valeur.Poste + " : " +this.Valeur.Nom+" "+ this.Valeur.Prenom);

            if(this.Successeurs != null)
            {
                for (int i = 0; i < this.Successeurs.Count; i++)
                { this.Successeurs[i].Afficher_Organigramme(indent, i == this.Successeurs.Count - 1); }
            }
            
        }

        #endregion

    }
}
