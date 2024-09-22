using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    internal class Chauffeur : Salarie
    {
        /// <summary>
        /// cette classe devrait nous permettre d'acceder facilement au disponibilite des chauffeurs pour leur attribuer des commandes
        /// </summary>

        #region Initialisation
        //attribut
        List<DateTime> dates_livraisons;
        int tarifs;
        
        //constructeur
        public Chauffeur(string nom, string prenom, DateTime naissance, string adresse, string email, int telephone, int ss, DateTime entree, double salaire, List<DateTime> dates_livraisons, string poste = "Chauffeur")
            : base(nom, prenom, naissance, adresse, email, telephone, ss, entree, poste, salaire)
        {
            this.dates_livraisons = dates_livraisons;
        }

        public Chauffeur(string nom, string prenom, DateTime naissance, string adresse, string email, int telephone, int ss, DateTime entree, double salaire, string poste = "Chauffeur")
            : base(nom, prenom, naissance, adresse, email, telephone, ss, entree, poste, salaire)
        { this.dates_livraisons = new List<DateTime>(); }
        

        //proprietes

        public List<DateTime> Dates_Livraisons { get { return this.dates_livraisons; } }

        public int Tarifs { get { return this.tarifs; } set { this.tarifs = value; } }

        #endregion

        #region Methodes
        public override string ToString()
        {
            return base.ToString();
        }

        /// <summary>
        /// cette Fonction met a jours la liste des dates occupees par le chauffeur
        /// </summary>
        /// <param name="commandes"></param>
        public void  Livraisons(List<Commande> commandes)
        {
            List<DateTime> dates = new List<DateTime>();
            foreach (Commande commande in commandes)
            {
                if (commande.Chauf == this) { dates.Add(commande.Date); }
            }

            this.dates_livraisons = dates;
        }


        /// <summary>
        /// cette Fonction attribut un tarif horaire au chauffeur en fonction de son anciennete
        /// </summary>
        public void Tarif_Horaire()
        {
            int anciennete = DateTime.Now.Year - this.Entree.Year;

            if (anciennete > 2)
            {
                this.tarifs = 12;
            }
            if (anciennete > 5)
            {
                this.tarifs = 15;
            }
            if (anciennete > 8)
            {
                this.tarifs = 20;
            }
            else { this.tarifs = 10; }
        }

        /// <summary>
        /// cette fonction test si un chauffeur est disponible pour une date donnee
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public bool Disponible(DateTime date)
        {
            foreach(DateTime d in this.Dates_Livraisons)
            {
                if(d==date) return false;
            }

            return true;
        }

        #endregion
    }
}
