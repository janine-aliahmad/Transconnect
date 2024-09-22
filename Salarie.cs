using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    internal class Salarie : Personne, IIdentifiant<int>
    {
        #region attributs
        //attributs
        int ss;
        DateTime entree;
        string poste;
        double salaire;

        #endregion

        #region  constructeurs

        public Salarie(string nom, string prenom, DateTime naissance, string adresse, string email, int telephone, int ss, DateTime entree, string poste, double salaire) 
            : base( nom,  prenom,  naissance,  adresse,  email,  telephone)
        {
            this.ss = ss;
            this.entree = entree;
            this.poste = poste;
            this.salaire = salaire;
        }

        #endregion

        #region proprietes
        public int SS {  get { return this.ss; } }

        public DateTime Entree { get { return this.entree; } }

        public string Poste
        {
            get { return this.poste; }
            set { this.poste = value; }
        }

        public double Salaire
        {
            get { return this.salaire; }
            set { this.salaire = value;}
        }
        #endregion


        public override string ToString()
        {
            return base.ToString() + "\nNumero de Securite Sociale: "+ this.ss + "\nDate d'entree: "+this.entree+"\nPoste: "+this.poste+"\nSalaire: "+this.salaire;
        }

        /// <summary>
        /// fonction qui retourne l'identifiant 
        /// </summary>
        /// <returns></returns>
        public int Get_ID()
        {
            return this.ss;
        }

    }
}
