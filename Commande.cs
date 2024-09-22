using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TransConnect
{
    internal class Commande : IIdentifiant<int>
    {
        /// <summary>
        /// pour ajouter une commande: on precise le client, une date un point de depart et une arrivee
        /// un identifiant est attribuer automatiquement par la variable statc nbCommande
        /// le chauffeur est attribuer en fonction des disponibilite
        /// le prix est calculer en fonction de depart/arrive+type de vehicule
        /// </summary>

        #region attribut
        int id;
        Client client;
        string depart;
        string arrivee;
        DateTime date;
        Vehicule vehic;
        double prix;
        Chauffeur chauf;
        private static int nbCommande=0; // static pour dire que c un champ de class.
        #endregion

        #region Constructeur
        public Commande (Client client, string depart, string arrivee, DateTime date, Vehicule vehic, Chauffeur chauf, double prix=0)
        {
            nbCommande++;
            this.id= nbCommande;
            this.client = client;
            this.depart = depart;
            this.arrivee = arrivee; 
            this.date = date;
            this.vehic= vehic;
            this.prix = prix; // on met 0 par defaut puis a calculer en fonction de depart/arrive/vehic
            this.chauf = chauf; //a choisir en fonction de disponibilite 

        }
        #endregion

        #region Proprietes
        public int Id { get { return this.id; } }
        public Client Client { get { return this.client; } }
        public string Depart {  get { return this.depart; } set { this.depart = value; } }
        public string Arrivee { get {  return this.arrivee; } set { this.depart = value; } }
        public DateTime Date { get { return this.date; } set { this.date = value; } }
        public Vehicule Vehic {  get { return this.vehic; } }
        public double Prix { get { return this.prix; } set { this.prix = value; } }
        public Chauffeur Chauf { get { return this.chauf; } set { this.chauf = value; } }

        #endregion

        #region Methodes

        /// <summary>
        /// cette methode permet d'afficher la commande
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "\nCommande Numero " + this.id + "\n\nClient:\n" + this.client.ToString() + "\n\nLivraison de " + this.depart + " a " + this.arrivee + "\nDate de livraison: "
                + this.date + "\nVehicule: " + this.vehic.ToString() + "\nPrix: " + this.prix + "\n\nInfo Chauffeur assigne: " + this.chauf.ToString();
        }

        /// <summary>
        /// cette fonction calcule le prix d'une commande
        /// le prix depend du vehicule choisit + le tarif du chauffeur
        /// pour calculer le temps de livraison (sans reprendre le fichier) on considere une vitesse moyenne de 75KM/heure
        /// </summary>
        /// <param name="distance"></param>
        /// <returns></returns>
        public double Calcul_Prix(int distance)
        {
            double prix_km = this.vehic.Prix_km;
            this.chauf.Tarif_Horaire();
            int tarif_chauff = this.chauf.Tarifs;
            //int temps_livraison = Calcul_temps;

            return prix_km * distance + tarif_chauff * distance/75;


        }

        /// <summary>
        /// cette fonction verifie l'etat de la commande. renvoie true si elle est terminee, false sinon.
        /// </summary>
        /// <returns></returns>
        public bool Terminee()
        {
            if (this.date < DateTime.Now) { return true; }
            return false;
        }

        /// <summary>
        /// fonction qui retourne l'identifiant 
        /// </summary>
        /// <returns></returns>
        public int Get_ID()
        {
            return this.Id;
        }

        #endregion
    }
}
