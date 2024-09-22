using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    abstract class Personne
    {
        #region Initialisation
        protected string nom;
        protected string prenom;
        protected DateTime naissance;
        protected string adresse;
        protected string email;
        protected int telephone;


        //constructeur

        public Personne() { }
        public Personne(string nom, string prenom, DateTime naissance, string adresse,  string email, int telephone)
        {
            this.nom = nom;
            this.prenom = prenom;
            this.naissance = naissance;
            this.adresse = adresse;
            this.email = email;
            this.telephone = telephone;
        }

        //proprietes
        public string Nom
        {
            get { return this.nom; }
            set { this.nom = value; }
        }
        
        public string Prenom { get { return this.prenom; } }

        public DateTime Naissance { get { return this.naissance; } }

        public int Telephone
        {
            get { return this.telephone; }
            set { this.telephone = value; }
        }
        public string Email
        {
            get { return this.email; }
            set { this.email = value; }
        }
        public string Adresse
        {
            get { return this.adresse; }
            set { this.adresse = value; }
        }

        //methodes:

        public override string ToString()
        {
            return "\nNom: " + this.nom + "\nPrenom: " + this.prenom + "\nDate de naissance: " + this.naissance + "\nAdresse: " + this.adresse + "\nEmail: " + this.email + "\nNumero de telephone: " + this.telephone;
        }
        #endregion
    }
}
