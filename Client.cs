using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace TransConnect
{
    internal class Client : Personne, IIdentifiant<int>
    {

        #region Attribut
        /// <summary>
        /// cette classe nous permet de cree des instance de client. 
        /// un client est une personne auquels on associe un identifiant, le nombre de commande et le taux de remises qui lui est attribuer en fonction du nombre de commande faites
        /// </summary>
        int id;
        int nb_commandes;
        int remises;
        private static int nbClient= 0; // static pour dire que c un champ de class.
        #endregion

        #region Constructeur
        /// <summary>
        /// on cree un instance client en precisant tout les parametre sauf 3:
        /// l'identifiant du client id est automatiquement attribuer de facons unique pour chaque client instancie grace a la variable nbClient
        /// le nombre de commande par clients est calculer au moment de l'ajout d'un client (puis est mise a jour avec chauque nouvelle commande faite)
        /// la remises est ensuite attribue en fonction du nombre de commandes
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="prenom"></param>
        /// <param name="naissance"></param>
        /// <param name="adresse"></param>
        /// <param name="email"></param>
        /// <param name="telephone"></param>

        public Client (string nom, string prenom, DateTime naissance, string adresse, string email, int telephone)
            : base(nom, prenom, naissance, adresse, email, telephone)
        {
            nbClient++;
            this.id = nbClient;
            this.remises = 0; // on initialise les remises a 0 puis en fonction du nombre de leur commandes on attrivera des remises;
            this.nb_commandes = 0;

        }

        public Client() { }

        #endregion

        #region Proprietes
        public int Id { get { return this.id; } }

        public int Nb_Commandes { get { return this.nb_commandes; } set { this.nb_commandes = value; } }

        public int Remises { get { return this.remises; } set { this.remises = value; } }
        #endregion

        #region Methodes utiles

        /// <summary>
        /// Fonction qui permet l'affichage d'un client
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString() + "\nID: "+this.id+"\nNombre de commandes passe: "+this.nb_commandes;
        }

        /// <summary>
        /// Fonction qui calcule le nombre de commande passer par un client a partir de la liste de commande de l'entreprise
        /// </summary>
        /// <param name="l_commande"></param>
        /// <returns></returns>
        public int Count_Commande_Client(List<Commande> l_commande)
        {
            int cpt = 0;

            if (l_commande != null)
            {
                foreach (Commande c in l_commande)
                {
                    if (c.Client == this) { cpt++; }
                }
            }

            return cpt;

        }

        /// <summary>
        /// fonction qui attribut des remises au client en fonction du nombre de leur commandes
        /// </summary>
        public int Attribuer_Remise()
        {
            if (this.nb_commandes >= 20) { return 15; }
            if (this.nb_commandes >= 12) { return 10; }
            if (this.nb_commandes >= 5) { return 5; }
            else { return 0; }
            
        }

        /// <summary>
        /// fonction qui retourne l'identifiant 
        /// </summary>
        /// <returns></returns>
        public  int Get_ID()
        {
            return this.Id;
        }

        #endregion
    }
}
