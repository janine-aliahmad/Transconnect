using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    internal class Program
    {
        /// <summary>
        /// cette fonction initialise la flottes de vehicules de l'entreprise
        /// </summary>
        /// <returns></returns>
        public static List<Vehicule> Initialisation_Flottes()
        {
            #region Flottes de vehicules
            Voiture voiture1 = new Voiture(1);
            Voiture voiture2 = new Voiture(2);
            Voiture voiture3 = new Voiture(3);
            Voiture voiture4 = new Voiture(4);
            Voiture voiture5 = new Voiture(5);
            Voiture voiture6 = new Voiture(6);
            Voiture voiture7 = new Voiture(7);
            Camionette camionette1 = new Camionette("Transport de verre pour vitriers");
            Camionette camionette2 = new Camionette("Services de restauration et d'événementiel");
            Camionette camionette3 = new Camionette("Transport de meubles");
            Citerne citerne1 = new Citerne(1, 25, "Matières dangereuses");
            Citerne citerne2 = new Citerne(1, 25, "Produits pétroliers de grande valeur");
            Citerne citerne3 = new Citerne(1, 25, "Gaz");
            Citerne citerne4 = new Citerne(1, 25, "Eau");
            Citerne citerne5 = new Citerne(2, 40, "Matières dangereuses");
            Citerne citerne6 = new Citerne(2, 40, "Produits pétroliers de grande valeur");
            Citerne citerne7 = new Citerne(2, 40, "Gaz");
            Citerne citerne8 = new Citerne(2, 40, "Eau");
            // Benne with 1 matiere
            Benne benne1 = new Benne(1, 25, 1, "Sable", false);
            Benne benne2 = new Benne(1, 25, 1, "Terre", false);
            Benne benne3 = new Benne(1, 25, 1, "Gravier", false);
            Benne benne4 = new Benne(2, 40, 1, "Sable", false);

            // Benne with 2 matieres
            Benne benne5 = new Benne(1, 25, 2, "Sable", "Terre", false);
            Benne benne6 = new Benne(1, 25, 2, "Terre", "Gravier", false);
            Benne benne7 = new Benne(2, 40, 2, "Sable", "Gravier", false);
            Benne benne8 = new Benne(1, 25, 2, "Sable", "Terre", false);

            // Benne with 3 matieres
            Benne benne9 = new Benne(1, 25, 3, "Sable", "Terre", "Gravier", false);
            Benne benne10 = new Benne(2, 40, 3, "Sable", "Terre", "Gravier", false);

            Frigorifique frigo1 = new Frigorifique(1, 25);
            Frigorifique frigo2 = new Frigorifique(1, 40);
            Frigorifique frigo3 = new Frigorifique(2, 25);
            Frigorifique frigo4 = new Frigorifique(2, 40);

            List<Vehicule> vehicules = new List<Vehicule>
            {
                voiture1,
                voiture2,
                voiture3,
                voiture4,
                voiture5,
                voiture6,
                voiture7,
                camionette1,
                camionette2,
                camionette3,
                citerne1,
                citerne2,
                citerne3,
                citerne4,
                citerne5,
                citerne6,
                citerne7,
                citerne8,
                benne1,
                benne2,
                benne3,
                benne4,
                benne5,
                benne6,
                benne7,
                benne8,
                benne9,
                benne10,
                frigo1,
                frigo2,
                frigo3,
                frigo4
            };

            return vehicules;
            #endregion
        }

        /// <summary>
        /// cette fonction initialise la liste des salarie de l'entreprise (en cas d'echec d'importation du fichier)
        /// </summary>
        /// <returns></returns>
        public static List<Salarie> Initialisation_Salaries()
        {
            #region Instancee
            // Directeur général
            Salarie directeurGeneral = new Salarie("Dupont", "Jean", new DateTime(1975, 5, 15), "123 rue de la République", "jean.dupont@example.com", 12789, 17890, new DateTime(2000, 1, 1), "Directeur Général", 10000.00);

            // Directrice commerciale
            Salarie directriceCommerciale = new Salarie("Durand", "Marie", new DateTime(1980, 8, 20), "456 avenue des Champs-Élysées", "marie.durand@example.com", 954321, 9870, new DateTime(2005, 3, 15), "Directrice Commerciale", 9000.00);

            // Directeur des opérations
            Salarie directeurOperations = new Salarie("Lefevre", "Pierre", new DateTime(1978, 12, 10), "789 rue du Faubourg", "pierre.lefevre@example.com", 23450, 278901, new DateTime(2003, 7, 20), "Directeur des Opérations", 9500.00);

            // Directeur financier
            Salarie directeurFinancier = new Salarie("Martin", "Sophie", new DateTime(1976, 3, 25), "1010 boulevard Haussmann", "sophie.martin@example.com", 345901, 3489012, new DateTime(2001, 9, 10), "Directrice Financière", 9200.00);

            // Directrice RH
            Salarie directriceRH = new Salarie("Dubois", "Isabelle", new DateTime(1977, 9, 5), "202 chemin des Alouettes", "isabelle.dubois@example.com", 456012, 450123, new DateTime(2004, 5, 25), "Directrice RH", 8800.00);

            // Commerciaux
            Salarie commercial1 = new Salarie("Moreau", "Thomas", new DateTime(1990, 4, 8), "11 rue des Lilas", "thomas.moreau@example.com", 567823, 5678234, new DateTime(2010, 2, 12), "Commercial", 7500.00);
            Salarie commercial2 = new Salarie("Garcia", "Julie", new DateTime(1992, 6, 18), "22 avenue du Soleil", "julie.garcia@example.com", 678234, 012345, new DateTime(2012, 8, 21), "Commercial", 7400.00);

            // Chefs d'équipe
            Salarie chefEquipe1 = new Salarie("Roux", "Antoine", new DateTime(1985, 10, 30), "33 route de la Plage", "antoine.roux@example.com", 78345, 123456, new DateTime(2008, 10, 5), "Chef d'Équipe", 8200.00);
            Salarie chefEquipe2 = new Salarie("Bonnefoy", "Emma", new DateTime(1987, 7, 15), "44 chemin du Bois", "emma.bonnefoy@example.com", 89056, 8904567, new DateTime(2009, 12, 18), "Chef d'Équipe", 8100.00);

            // Formation
            Salarie formation = new Salarie("Girard", "Luc", new DateTime(1979, 1, 3), "55 rue de la Fontaine", "luc.girard@example.com", 34567, 9012378, new DateTime(2002, 11, 30), "Formation", 7800.00);

            // Contrats et direction comptable
            Salarie contrats = new Salarie("Fernandez", "Luis", new DateTime(1983, 11, 20), "66 avenue des Roses", "luis.fernandez@example.com", 123765, 1987654, new DateTime(2006, 6, 9), "Contrats", 8300.00);
            Salarie directionComptable = new Salarie("Petit", "Sophie", new DateTime(1981, 5, 12), "77 impasse des Mûriers", "sophie.petit@example.com", 230984, 2876543, new DateTime(2007, 4, 15), "Direction Comptable", 8700.00);

            // Contrôleur de gestion
            Salarie controleurGestion = new Salarie("Andre", "Paul", new DateTime(1984, 8, 28), "88 avenue de la Libération", "paul.andre@example.com", 347654, 3476542, new DateTime(2005, 8, 2), "Contrôleur de Gestion", 8400.00);

            // Comptables
            Salarie comptable1 = new Salarie("Lecomte", "Marie", new DateTime(1982, 2, 17), "99 rue du Pont", "marie.lecomte@example.com", 450654, 456541, new DateTime(2006, 12, 3), "Comptable", 7600.00);
            Salarie comptable2 = new Salarie("Rodriguez", "David", new DateTime(1986, 6, 7), "1001 avenue des Ormes", "david.rodriguez@example.com", 560984, 576540, new DateTime(2008, 3, 28), "Comptable", 7500.00);

            // Chauffeurs
            Chauffeur chauffeur1 = new Chauffeur("Dumont", "Pierre", new DateTime(1988, 3, 10), "12 rue de la Gare", "pierre.dumont@example.com", 123489, 1237890, new DateTime(2015, 5, 20), 3000.00);
            Chauffeur chauffeur2 = new Chauffeur("Leroy", "Marie", new DateTime(1990, 7, 15), "24 avenue du Lac", "marie.leroy@example.com", 267890, 2345901, new DateTime(2016, 8, 12), 3100.00);
            Chauffeur chauffeur3 = new Chauffeur("Dubois", "Paul", new DateTime(1985, 11, 25), "36 route de la Montagne", "paul.dubois@example.com", 678901, 6789012, new DateTime(2017, 10, 5), 3200.00);
            Chauffeur chauffeur4 = new Chauffeur("Martin", "Sophie", new DateTime(1982, 5, 5), "48 chemin du Moulin", "sophie.martin@example.com", 459012, 4890123, new DateTime(2018, 3, 15), 3300.00);
            Chauffeur chauffeur5 = new Chauffeur("Moreau", "Thomas", new DateTime(1989, 9, 20), "60 rue des Roses", "thomas.moreau@example.com", 567123, 5671234, new DateTime(2019, 6, 18), 3400.00);

            #endregion

            List<Salarie> salaries = new List<Salarie>
            {
                directeurGeneral,
                directriceCommerciale,
                directeurOperations,
                directeurFinancier,
                directriceRH,
                commercial1,
                commercial2,
                chefEquipe1,
                chefEquipe2,
                formation,
                contrats,
                directionComptable,
                controleurGestion,
                comptable1,
                comptable2,
                chauffeur1,
                chauffeur2,
                chauffeur3,
                chauffeur4,
                chauffeur5,
            };

            return salaries;

        }

        /// <summary>
        /// cette fonction initialise la liste des clients de l'entreprise (en cas d'echec d'importation du fichier)
        /// </summary>
        /// <returns></returns>
        public static List<Client> Initialisation_Client()
        {
            Client client1 = new Client("Tremblay", "Sophie", new DateTime(1983, 7, 18), "Paris", "sophie.tremblay@example.com", 123456789);
            Client client2 = new Client("Gagnon", "Maxime", new DateTime(1978, 4, 22), "Paris", "maxime.gagnon@example.com", 987654321);
            Client client3 = new Client("Leblanc", "Julie", new DateTime(1985, 11, 30), "Marseille", "julie.leblanc@example.com", 234567890);
            Client client4 = new Client("Lavoie", "David", new DateTime(1976, 9, 14), "Montpelier", "david.lavoie@example.com", 345678901);
            Client client5 = new Client("Fortin", "Catherine", new DateTime(1982, 5, 8), "Lyon", "catherine.fortin@example.com", 456789012);

            List<Client> list = new List<Client> { client1,client2,client3,client4,client5};

            return list;

        }
        static void Main(string[] args)
        {
            List<Salarie> salarie = new List<Salarie>();
            List<Commande> commande = new List<Commande>();
            List<Client> clients = new List<Client>();  
            List<Vehicule> vehic =Initialisation_Flottes();

            TransConnect TRANSCONNECT = new TransConnect(salarie, vehic, clients, commande, 100);

            TRANSCONNECT.ImporterClientFichier();
            TRANSCONNECT.ImporterSalarieFichier();
            TRANSCONNECT.Update_Chauffeur();

            /*Commande c1 = new Commande(clients[0], "Paris", "Marseille", new DateTime(2024, 7, 18), vehic[0], TRANSCONNECT.Ensemble_Chauffeurs[0],200);
            Commande c2 = new Commande(clients[0], "Paris", "Marseille", new DateTime(2024, 12, 1), vehic[0], TRANSCONNECT.Ensemble_Chauffeurs[1],200);

            TRANSCONNECT.Ensemble_Commande.Add(c1);
            TRANSCONNECT.Ensemble_Commande.Add(c2);*/

            int x = -1;
            do
            {
                Console.Clear();

                #region ACCEUIL
                Console.WriteLine("               ==================================================================");
                Console.WriteLine("\n                 T    R    A    N    S    C    O    N    N    E    C    T    ");
                Console.WriteLine("\n               ==================================================================");

                Console.WriteLine("\n\n========== Choisir un Module ==========");
                Console.WriteLine("\t1/Clients");
                Console.WriteLine("\t2/Salaries");
                Console.WriteLine("\t3/Commandes");
                Console.WriteLine("\t4/Statistique");
                Console.WriteLine("\t5/Comptables (Achats, Bilan, Depenses)");
                Console.WriteLine("\t0/Quitter");

                Console.WriteLine();
                #endregion

                do
                {
                    string y = Console.ReadLine();
                    if (y == "") x = 0;
                    else x = int.Parse(y);
                }
                while (x < 0 && x > 5);

                switch (x)
                {
                    case 0:
                        break;

                    case 1: // Clients
                        #region Clients
                        Console.WriteLine("\n\n =========================   Choississez une Option   =========================");
                        Console.WriteLine("\t1/Afficher les clients par ordre Alphabétique");
                        Console.WriteLine("\t2/Afficher les clients par ville");
                        Console.WriteLine("\t3/Afficher les clients par achat cumulées");
                        Console.WriteLine("\t4/Ajouter un nouveau Cient");
                        Console.WriteLine("\t5/Supprimer un Client");
                        Console.WriteLine("\t6/Modifier les paramètre d'un Client");
                        Console.WriteLine("\t0/Quitter");

                        int m1 = -1;
                        do
                        {
                            string y = Console.ReadLine();
                            if (y == "") m1 = 0;
                            else m1 =int.Parse(y);
                        } while (m1 < 0 && m1 > 6);

                        switch (m1)
                        {
                            case 0:
                                break;
                            case 1:
                                Console.Clear();
                                TRANSCONNECT.Affiche_Clients_Alpha();
                                break;
                            case 2:
                                Console.Clear();
                                TRANSCONNECT.Affiche_Clients_Ville();
                                break;
                            case 3:
                                Console.Clear();
                                TRANSCONNECT.Affiche_Clients_Commandes();
                                break;
                            case 4:
                                TRANSCONNECT.AjouterClient();
                                break;
                            case 5:
                                Console.Clear();
                                TRANSCONNECT.Affiche_Clients_Alpha();
                                Console.WriteLine();

                                TRANSCONNECT.SupprimerClient();
                                break;
                            case 6:
                                Console.Clear();
                                TRANSCONNECT.Affiche_Clients_Alpha();
                                Console.WriteLine();
                                TRANSCONNECT.ModifierClient();
                                break;
                        }

                        Console.ReadKey();
                        #endregion
                        break;
                    case 2: //salarie
                        #region Salaries
                        Console.WriteLine("\n\n =========================   Choississez une Option   =========================");
                        Console.WriteLine("\t1/Afficher les Salaries");
                        Console.WriteLine("\t2/Afficher les Chauffeurs");
                        Console.WriteLine("\t3/Afficher l'Organigramme Generale de l'entreprise");
                        Console.WriteLine("\t4/Embaucher un salarie");
                        Console.WriteLine("\t5/Licencier un salarie");
                        Console.WriteLine("\t6/Modifier un salarie");
                        Console.WriteLine("\t0/Quitter");

                        int m2 = -1;
                        do
                        {
                            string y = Console.ReadLine();
                            if (y == "") m2 = 0;
                            else m2 = int.Parse(y);
                        } while (m2 < 0 && m2 > 6);

                        TRANSCONNECT.Update_Chauffeur();

                        switch (m2)
                        {
                            case 0:
                                Console.Clear();
                                break;
                            case 1:
                                Console.Clear();
                                TRANSCONNECT.Affiche_Salaries_Alpha();
                                break;
                            case 2:
                                Console.Clear();
                                TRANSCONNECT.Affiche_Chauffeurs_Alpha();
                                break;
                            case 3:
                                Console.Clear();
                                TRANSCONNECT.Construit_Organigramme();
                                break;
                            case 4:
                                Console.Clear();
                                TRANSCONNECT.EmbaucherSalarie();
                                break; 
                            case 5:
                                Console.Clear();
                                TRANSCONNECT.LicensierSalarie();
                                break;
                            case 6:
                                Console.Clear();
                                TRANSCONNECT.ModifierSalarie();
                                break;
                        }

                        Console.ReadKey();

                        #endregion
                        break;
                    case 3: //Commandes
                        #region Commande
                        Console.WriteLine("\n\n =========================   Choississez une Option   =========================");
                        Console.WriteLine("\t1/Afficher les Commandes");
                        Console.WriteLine("\t2/Ajouter une commande");
                        Console.WriteLine("\t3/Modifier une commande");
                        Console.WriteLine("\t4/Supprimer une commande");
                        Console.WriteLine("\t0/Quitter");

                        int m3 = -1;
                        do
                        {
                            string y = Console.ReadLine();
                            if (y == "") m3 = 0;
                            else m3 = int.Parse(y);
                        } while (m3 < 0 && m3 > 4);

                        switch (m3)
                        {
                            case 0:
                                break;
                            case 1:
                                Console.Clear();
                                TRANSCONNECT.Affiche_Commandes();
                                break;
                            case 2:
                                Console.Clear();
                                TRANSCONNECT.AjouterCommande();
                                break;
                            case 3:
                                Console.Clear();
                                TRANSCONNECT.ModifierCommande();
                                break;
                            case 4:
                                Console.Clear();
                                TRANSCONNECT.SupprimerCommande();
                                break;
                        }
                        Console.ReadKey();
                        #endregion
                        break;
                    case 4: //statistiques
                        #region Statistique
                        Console.WriteLine("\n\n =========================   Choississez une Option   =========================");
                        Console.WriteLine("\t1/Nombre de livraison par chauffeur");
                        Console.WriteLine("\t2/Commande par periode de temps");
                        Console.WriteLine("\t3/Moyenne des prix des commandes");
                        Console.WriteLine("\t4/Moyenne des comptes Clients");
                        Console.WriteLine("\t5/Commandes d'un Client donnee");
                        Console.WriteLine("\t0/Quitter");

                        int m4 = -1;
                        do
                        {
                            string y = Console.ReadLine();
                            if (y == "") m3 = 0;
                            else m4 = int.Parse(y);
                        } while (m4 < 0 && m4 > 5);

                        switch (m4)
                        {
                            case 0:
                                break;
                            case 1:
                                #region Livraison/Chauffeur
                                Console.Clear();
                                TRANSCONNECT.Nb_Livraison_Chauffeur();
                                #endregion
                                break;
                            case 2:
                                #region Commande/periode
                                Console.Clear();
                                TRANSCONNECT.Commande_Periode();
                                #endregion
                                break;
                            case 3:
                                #region Moyenne prix commande
                                Console.Clear();
                                TRANSCONNECT.MoyennePrixCommande();
                                #endregion
                                break;
                            case 4:
                                #region Moyenne compte client
                                Console.Clear();
                                TRANSCONNECT.MoyCompteClients();
                                #endregion
                                break;
                            case 5:
                                #region commande par clients
                                Console.Clear();
                                TRANSCONNECT.Commande_parClient();
                                #endregion
                                break;

                        }

                        Console.ReadKey();

                        #endregion
                        break;
                    case 5: //comptable
                        #region Comptable
                        Console.WriteLine("\n\n =========================   Choississez une Option   =========================");
                        Console.WriteLine("\t1/Enregistrement achat de vehicule");
                        Console.WriteLine("\t2/Affichage Flottes de Vehicules");
                        Console.WriteLine("\t3/Affichage Bilan Comptable");
                        Console.WriteLine("\t0/Quitter");

                        int m5 = -1;
                        do
                        {
                            string y = Console.ReadLine();
                            if (y == "") m3 = 0;
                            else m5 = int.Parse(y);
                        } while (m5 < 0 && m5 > 5);

                        switch (m5)
                        {
                            case 0:
                                break;
                            case 1:
                                #region Achat vehicule
                                Console.Clear();
                                TRANSCONNECT.Achat_Vehicule();
                                #endregion
                                break;
                            case 2:
                                #region Affichage vehicules
                                Console.Clear();
                                TRANSCONNECT.Afficher_Flotte_Vehicule();
                                #endregion
                                break;
                            case 3:
                                #region Affichage Bilan
                                Console.Clear();
                                TRANSCONNECT.Bilan_compte();
                                #endregion
                                break;
                        }

                        Console.ReadKey();
                        #endregion
                        break;

                }

            } while (x != 0);
            

        }
    }
}
