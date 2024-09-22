using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    internal class TransConnect
    {
        #region I-INITIALISATION DE LA CLASSE
        //attribut
        List<Salarie> ensemble_salaries;
        List<Vehicule> flotte_vehicules;
        List<Client> ensemble_clients;
        List<Chauffeur> ensemble_chauffeurs;
        List<Commande> ensemble_commande;
        double depense;

        //Constructeur
        public TransConnect(List<Salarie> ensemble_salaries, List<Vehicule> flotte_vehicules, List<Client> ensemble_clients, List<Commande> ensemble_commande, double depense=0)
        {
            this.ensemble_salaries = ensemble_salaries;
            this.flotte_vehicules = flotte_vehicules;
            this.ensemble_clients = ensemble_clients;
            this.ensemble_commande = ensemble_commande;
            this.depense = depense;

            foreach (Salarie c in ensemble_salaries)
            {
                if (c is Chauffeur chauffeur)
                {
                    if (this.ensemble_chauffeurs != null) { this.ensemble_chauffeurs.Add(chauffeur); }
                    else { this.ensemble_chauffeurs = new List<Chauffeur> { chauffeur }; }
                }
            }

        }

        //proprietes
        public List<Salarie> Ensemble_Salaries {  get { return this.ensemble_salaries; } set { this.ensemble_salaries = value; } }
        public List<Vehicule> Flotte_Vehicule { get { return this.flotte_vehicules; } set { this.flotte_vehicules = value; } }
        public List<Client> Ensemble_Clients { get { return this.ensemble_clients; } set { this.ensemble_clients = value; } }
        public List<Chauffeur> Ensemble_Chauffeurs { get { return this.ensemble_chauffeurs; } set { this.ensemble_chauffeurs = value; } }
        public List<Commande> Ensemble_Commande { get { return this.ensemble_commande; } set { this.ensemble_commande = value; } }
        public double Depenses { get { return this.depense; } set { this.depense = value; } }


        #endregion

        #region II-METHODES

        #region IIa-GESTION CLIENT

        #region i-Fonction utiles
        /// <summary>
        /// cette fonction permet de verifier si un client existe deja dans la liste des client de l'entreprise
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public bool ClientExist(Client c)
        {
            if (this.ensemble_clients != null && c != null)
            {
                foreach (Client client in this.ensemble_clients)
                {
                    if (client == c)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// cette fonction permet de recuperer l'unique client avec son identifiant
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Client RechercheClient_ID(int id)
        {
            foreach (Client c in this.ensemble_clients)
            {
                if (c.Id == id) { return c; }
            }

            return null;
        }

        /// <summary>
        /// cette fonction permet de recupere tout les clients avec le nom et prenom recherchee
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="prenom"></param>
        /// <returns></returns>
        public Client RechercheClient_NP(string nom, string prenom)
        {
            foreach (Client c in this.ensemble_clients)
            {
                if (c.Nom.ToLower() == nom.ToLower() && c.Prenom.ToLower() == prenom.ToLower()) { return c; }
            }
            return null;
        }

        #endregion

        #region ii-Affichage

        /// <summary>
        /// Fonction qui affiche tout les clients par order alphabetique de leurs noms
        /// </summary>
        public void Affiche_Clients_Alpha()
        {
            this.ensemble_clients.Sort((c1, c2) => string.Compare(c1.Nom, c2.Nom, StringComparison.OrdinalIgnoreCase));

            foreach (Client c in this.ensemble_clients)
            {
                Console.WriteLine(c.ToString());
            }
        }

        /// <summary>
        /// Fonction qui affiche tout les clients par ordre alphabetique de leur Adresse (nom de la ville)
        /// </summary>
        public void Affiche_Clients_Ville()
        {
            this.ensemble_clients.Sort((c1, c2) => string.Compare(c1.Adresse, c2.Adresse, StringComparison.OrdinalIgnoreCase));

            foreach (Client c in this.ensemble_clients)
            {
                Console.WriteLine(c.ToString());
            }
        }

        /// <summary>
        /// Fonction qui affiche tout les clients par ordre du nombre de commande decroissant
        /// </summary>
        public void Affiche_Clients_Commandes()
        {
            this.ensemble_clients = this.ensemble_clients.OrderByDescending(c => c.Nb_Commandes).ToList();

            foreach (Client c in this.ensemble_clients)
            {
                Console.WriteLine(c.ToString());
            }
        }

        #endregion

        #region iii-Gestion Liste de Clients

        /// <summary>
        /// cette fonction import l'ensemble des clients depuis un fichier
        /// </summary>
        /// <param name="filePath"></param>
        public void ImporterClientFichier(string filePath = "Clients.csv")
        {
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                // Split the line into fields
                string[] attribut = line.Split(',');

                // Parse the fields and create a Client instance
                Client client = new Client(
                    attribut[1], // Nom
                    attribut[2], // Prenom
                    DateTime.Parse(attribut[3]), // Naissance
                    attribut[4], // Adresse
                    attribut[5], // Email
                    int.Parse(attribut[6]) // Telephone
                );

                // Add the Client instance to the list
                if (this.ensemble_clients != null)
                {
                    this.ensemble_clients.Add(client);
                }
                else
                {
                    this.ensemble_clients = new List<Client> { client };
                }
            }
        }


        /// <summary>
        /// cette fonction permet de rajouter un nouveau client dans le fichier des clients
        /// </summary>
        /// <param name="c"></param>
        /// <param name="nomfichier"></param>
        public void EnregistrerClientFichier(Client c, string nomfichier = "Clients.csv")
        {

            StreamWriter writer = new StreamWriter(nomfichier);
            string enregistrement = c.Id + "," + c.Nom + "," + c.Prenom + "," + c.Naissance.ToString() + "," + c.Adresse + "," + c.Email + "," + c.Telephone + "," + c.Nb_Commandes;
            writer.WriteLine(enregistrement);
            writer.Close();

            Console.WriteLine("Le Clients à été enregistré dans le fichier");
        }

        /// <summary>
        /// cette fonction permet de reinitialiser et remettre a jour le fichier client apres avoir supprimer un client de la liste
        /// on implemente cette fonction juste apres avoir supprimer des clients
        /// elle commence par suprimer le contenu du fichier pour ensuite reecrire l'integraliste des client 
        /// </summary>
        /// <param name="nomfichier"></param>
        public void MiseAJour_FichierClient(string nomfichier = "Clients.csv")
        {
            // on efface le contenue du fichier
            File.WriteAllText(nomfichier, string.Empty);
            string enregistrement;


            // remettre tout les clients
            using (StreamWriter writer = new StreamWriter(nomfichier, true))
            {
                for (int i = 0; i < this.ensemble_clients.Count() - 1; i++)
                {
                    enregistrement = ensemble_clients[i].Id + "," + ensemble_clients[i].Nom + "," + ensemble_clients[i].Prenom + "," + ensemble_clients[i].Naissance.ToString() + "," + ensemble_clients[i].Adresse + "," + ensemble_clients[i].Email + "," + ensemble_clients[i].Telephone + "," + ensemble_clients[i].Nb_Commandes;
                    writer.WriteLine(enregistrement);
                }
                enregistrement = ensemble_clients[ensemble_clients.Count() - 1].Id + "," + ensemble_clients[ensemble_clients.Count() - 1].Nom + "," + ensemble_clients[ensemble_clients.Count() - 1].Prenom + "," + ensemble_clients[ensemble_clients.Count() - 1].Naissance.ToString() + "," + ensemble_clients[ensemble_clients.Count() - 1].Adresse + "," + ensemble_clients[ensemble_clients.Count() - 1].Email + "," + ensemble_clients[ensemble_clients.Count() - 1].Telephone + "," + ensemble_clients[ensemble_clients.Count() - 1].Nb_Commandes;

                writer.Write(enregistrement);
            }

            Console.WriteLine("Liste des Clients a été mise a jour avec succès");
        }

        /// <summary>
        /// cette fonction permet d'ajouter un nouveau clients a la liste des clients. elle ajoute aussi ce client au fichier clients.
        /// on demande a l'utilisateur les donnees du clients
        /// on cree l'instance et on cherche le nombre de commande deja faite par ce client (ou cas ou il avait deja passer des commandes mais a ete supprimer)
        /// on l'ajoute si il n'existe pas deja, sinon un message d'erreur est afficher
        /// </summary>
        public void AjouterClient()
        {
            #region Demande utilisateur des variables
            Console.WriteLine("Nom du client: ");
            string nom = Console.ReadLine();
            Console.WriteLine("Prenom du client: ");
            string prenom = Console.ReadLine();
            Console.WriteLine("Date de naissance: ");
            string date = Console.ReadLine();
            DateTime naissance = new DateTime();
            if (date != "") naissance = Convert.ToDateTime(date);
            Console.WriteLine("Adresse (ville): ");
            string adresse = Console.ReadLine();
            Console.WriteLine("Mail du client: ");
            string email = Console.ReadLine();
            Console.WriteLine("n° de téléphone ?");
            int telephone = int.Parse(Console.ReadLine());
            #endregion

            Client nouveau_client = new Client(nom, prenom, naissance, adresse, email, telephone);

            nouveau_client.Nb_Commandes = nouveau_client.Count_Commande_Client(this.ensemble_commande);

            if (nouveau_client != null)
            {
                if (this.ClientExist(nouveau_client) == false)
                {
                    this.ensemble_clients.Add(nouveau_client);
                    Console.WriteLine("Client ajouter avec succes");
                    EnregistrerClientFichier(nouveau_client);
                    MiseAJour_FichierClient();
                }
                else { Console.WriteLine("Client existe deja"); }
            }
            else Console.WriteLine("Le client n'a pas pu etre ajouter !");


        }

        /// <summary>
        /// Cette methode nous permet de supprimer un client de la liste de client et remet a jour le fichier client. 
        /// on demande a l'utlisateur l'identifiant du client a effacer (id plutot que nom prenom ou cas il y adeux client avec meme nom prenom
        /// </summary>
        public void SupprimerClient()
        {
            //demande utilisateur donnee du client a supprimer
            Console.WriteLine("Identifiant du client à supprimer : ");
            int id;
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Veuillez entrer un identifiant valide.");
                return;
            }

            Client c = this.RechercheClient_ID(id);

            if (this.ensemble_clients != null && c != null && this.ClientExist(c))
            {
                this.ensemble_clients.Remove(c);
                Console.WriteLine("Le client à été supprimer avec succès !");
                this.MiseAJour_FichierClient();

            }
            else { Console.WriteLine("Le client n'a pas pu etre supprimer"); }
        }

        /// <summary>
        /// cette fonction permet de modifier les donnee d'un client et met a jour le fichier clients
        /// on demande a l'utlisateur de choisir quelle donnee modifier et la nouvelle donnee a saisir
        /// </summary>
        public void ModifierClient()
        {
            //demande utilisateur donnee du client a modifier
            Console.WriteLine("Identifiant du client à modifier : ");
            int id;
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Veuillez entrer un identifiant valide.");
                return;
            }

            Client c = this.RechercheClient_ID(id);

            int choix = -1;
            do
            {
                Console.WriteLine();
                Console.WriteLine("Quelle information souhaitez vous modifier pour " + c.Nom + " " + c.Prenom);
                Console.WriteLine("\t1 - Nom");
                Console.WriteLine("\t2 - Adresse (ville)");
                Console.WriteLine("\t3 - Email");
                Console.WriteLine("\t4 - Telephone");
                Console.WriteLine("\t0 - Annuler");
                Console.WriteLine();

                do
                {
                    choix = int.Parse(Console.ReadLine());
                } while (choix < 0 && choix > 3);

                switch (choix)
                {
                    case 0:
                        break;
                    case 1:
                        Console.WriteLine("Veuillez saisir la nouveau Nom: ");
                        c.Nom = Console.ReadLine();
                        break;
                    case 2:
                        Console.WriteLine("Veuillez saisir la nouvelle Adresse: ");
                        c.Adresse = Console.ReadLine();
                        break;
                    case 3:
                        Console.WriteLine("Veuillez saisir le nouvelle Email ?");
                        c.Email = Console.ReadLine(); ;
                        break;
                    case 4:
                        Console.WriteLine("Veuillez saisir le nouveau numero de téléphone ?");
                        c.Telephone = int.Parse(Console.ReadLine());
                        break;
                }

            } while (choix < 0);
            this.MiseAJour_FichierClient();
            Console.WriteLine("Modification enregistrée avec succès");
        }

        #endregion


        #endregion

        #region IIb-GESTION SALARIES

        #region i-Fonction utiles

        /// <summary>
        /// vcette fonction permet de recupere le salaries a partir de son nom et prenom
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="prenom"></param>
        /// <returns></returns>
        public Salarie RechercheSalarie(string nom, string prenom)
        {
            foreach (Salarie s in this.ensemble_salaries)
            {
                if (s.Nom.ToLower() == nom.ToLower() && s.Prenom.ToLower() == prenom.ToLower()) { return s; }
            }
            return null;
        }

        /// <summary>
        /// cette fonction permet de verifier si un salarie travail deja dans l'entreprise
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public bool SalarieExist(Salarie s)
        {
            if (this.ensemble_salaries != null && s != null)
            {
                foreach (Salarie salarie in this.ensemble_salaries)
                {
                    if (salarie == s)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        

        #endregion

        #region ii-Organigramme
        /// <summary>
        /// cette fonction permet de construire l'organigramme de l'entreprise et le stock sous forme d'arbre
        /// </summary>
        public Noeud_naire Construit_Organigramme()
        {
            Noeud_naire organigramme = new Noeud_naire();

            #region Racine
            //La Racine --> le directeur Generale
            Noeud_naire DG = new Noeud_naire();
            foreach (Salarie s in this.ensemble_salaries)
            {
                if (s.Poste == "Directeur General") { DG.Valeur = s; }
            }
            #endregion

            #region Directeurs/rices
            //successeur de la racine --> directeur/directrice des departements
            Noeud_naire DC = new Noeud_naire();
            Noeud_naire DO = new Noeud_naire();
            Noeud_naire DRH = new Noeud_naire();
            Noeud_naire DF = new Noeud_naire();

            foreach (Salarie s in this.ensemble_salaries)
            {
                if (s.Poste == "Directrice Commerciale" || s.Poste == "Directeur Commerciale")
                {
                    DC.Valeur = s;
                    DG.AssocierSuccesseur(DC);
                }

                if (s.Poste == "Directrice des Operations" || s.Poste == "Directeur des Operations")
                {
                    DO.Valeur = s;
                    DG.AssocierSuccesseur(DO);
                }

                if (s.Poste == "Directrice RH" || s.Poste == "Directeur RH")
                {
                    DRH.Valeur = s;
                    DG.AssocierSuccesseur(DRH);
                }

                if (s.Poste == "Directrice Financiere" || s.Poste == "Directeur Financier")
                {
                    DF.Valeur = s;
                    DG.AssocierSuccesseur(DF);
                }
            }
            #endregion

            #region Departements Commerciales
            //Departement Commerciale
            Noeud_naire C1 = new Noeud_naire();
            Noeud_naire C2 = new Noeud_naire();
            Noeud_naire C3 = new Noeud_naire();

            foreach (Salarie s in this.ensemble_salaries)
            {
                if (s.Poste == "Commerciale" || s.Poste == "Commercial")
                {
                    if (C1.Valeur != null && C2.Valeur != null)
                    {
                        C3 = new Noeud_naire(s);
                        DC.AssocierSuccesseur(C3);

                    }
                    else
                    {
                        if (C1.Valeur != null)
                        {
                            C2 = new Noeud_naire(s);
                            DC.AssocierSuccesseur(C2);
                        }
                        else
                        {
                            C1 = new Noeud_naire(s);
                            DC.AssocierSuccesseur(C1);
                        }
                    }
                }
            }
            #endregion

            #region Departements des Operations
            //Departement operation
            Noeud_naire Ce1 = new Noeud_naire();
            Noeud_naire Ce2 = new Noeud_naire();

            foreach (Salarie s in this.ensemble_salaries)
            {
                if (s.Poste == "Chef d'Equipe")
                {
                    if (Ce1.Valeur != null)
                    {
                        Ce2 = new Noeud_naire(s);
                        DO.AssocierSuccesseur(Ce2);

                    }
                    else
                    {
                        Ce1 = new Noeud_naire(s);
                        DO.AssocierSuccesseur(Ce1);
                    }
                }
            }
            #endregion

            #region Departement RH
            //Departement RH
            Noeud_naire F = new Noeud_naire();
            Noeud_naire C = new Noeud_naire();
            foreach (Salarie s in this.ensemble_salaries)
            {
                if (s.Poste == "Formation")
                {
                    F.Valeur = s;
                    DRH.AssocierSuccesseur(F);
                }

                if (s.Poste == "Contrats")
                {
                    C.Valeur = s;
                    DRH.AssocierSuccesseur(C);
                }

            }
            #endregion

            #region Departement Financier
            //Departement finance
            Noeud_naire DCo = new Noeud_naire();
            Noeud_naire CG = new Noeud_naire();
            foreach (Salarie s in this.ensemble_salaries)
            {
                if (s.Poste == "Directrice Comptable" || s.Poste == "Directeur Comptable")
                {
                    DCo.Valeur = s;
                    DF.AssocierSuccesseur(DCo);
                }

                if (s.Poste == "Controleur de Gestion")
                {
                    CG.Valeur = s;
                    DF.AssocierSuccesseur(CG);
                }

            }
            #endregion

            #region Chauffeurs
            Noeud_naire Cf1 = new Noeud_naire();
            Noeud_naire Cf2 = new Noeud_naire();
            Noeud_naire Cf3 = new Noeud_naire();
            Noeud_naire Cf4 = new Noeud_naire();
            Noeud_naire Cf5 = new Noeud_naire();
            Noeud_naire Cf6 = new Noeud_naire();
            Ce1.Successeurs = new List<Noeud_naire>();
            Ce2.Successeurs = new List<Noeud_naire>();

            foreach (Salarie s in this.ensemble_salaries)
            {
                if (s.Poste == "Chauffeur")
                {
                    if (Cf1.Valeur == null)
                    {
                        Cf1 = new Noeud_naire(s);
                        if (Ce1.Successeurs.Count < 3)
                        {
                            Ce1.AssocierSuccesseur(Cf1);
                        }
                        else
                        {
                            Ce2.AssocierSuccesseur(Cf1);
                        }

                    }
                    else if (Cf2.Valeur == null)
                    {
                        Cf2 = new Noeud_naire(s);
                        if (Ce1.Successeurs.Count < 3)
                        {
                            Ce1.AssocierSuccesseur(Cf2);
                        }
                        else
                        {
                            Ce2.AssocierSuccesseur(Cf2);
                        }

                    }
                    else if (Cf3.Valeur == null)
                    {
                        Cf3 = new Noeud_naire(s);
                        if (Ce1.Successeurs.Count < 3)
                        {
                            Ce1.AssocierSuccesseur(Cf3);
                        }
                        else
                        {
                            Ce2.AssocierSuccesseur(Cf3);
                        }

                    }
                    else if (Cf4.Valeur == null)
                    {
                        Cf4 = new Noeud_naire(s);
                        if (Ce1.Successeurs.Count < 3)
                        {
                            Ce1.AssocierSuccesseur(Cf4);
                        }
                        else
                        {
                            Ce2.AssocierSuccesseur(Cf4);
                        }
                    }
                    else if (Cf5.Valeur == null)
                    {
                        Cf5 = new Noeud_naire(s);
                        if (Ce1.Successeurs.Count < 3)
                        {
                            Ce1.AssocierSuccesseur(Cf5);
                        }
                        else
                        {
                            Ce2.AssocierSuccesseur(Cf5);
                        }
                    }
                    else
                    {
                        Cf6 = new Noeud_naire(s);
                        if (Ce1.Successeurs.Count < 3)
                        {
                            Ce1.AssocierSuccesseur(Cf6);
                        }
                        else
                        {
                            Ce2.AssocierSuccesseur(Cf6);
                        }
                    }

                }
            }
            #endregion

            #region Comptables
            Noeud_naire Cp1 = new Noeud_naire();
            Noeud_naire Cp2 = new Noeud_naire();
            Noeud_naire Cp3 = new Noeud_naire();
            Noeud_naire Cp4 = new Noeud_naire();


            foreach (Salarie s in this.ensemble_salaries)
            {
                if (s.Poste == "Comptable")
                {
                    if (Cp1 == null)
                    {
                        Cp1 = new Noeud_naire(s);
                        DCo.AssocierSuccesseur(Cp1);
                    }
                    else if (Cp2 == null)
                    {
                        Cp2 = new Noeud_naire(s);
                        DCo.AssocierSuccesseur(Cp2);
                    }
                    else if (Cp3 == null)
                    {
                        Cp3 = new Noeud_naire(s);
                        DCo.AssocierSuccesseur(Cp3);
                    }
                    else if (Cf4 == null)
                    {
                        Cp4 = new Noeud_naire(s);
                        DCo.AssocierSuccesseur(Cp4);
                    }

                }
            }
            #endregion

            organigramme = DG;

            organigramme.Afficher_Organigramme("",true);

            return organigramme;
        }
        #endregion

        #region iii-Affichage
        /// <summary>
        /// Fonction qui affiche tout les clients par order alphabetique de leurs noms
        /// </summary>
        public void Affiche_Salaries_Alpha()
        {
            this.ensemble_salaries.Sort((c1, c2) => string.Compare(c1.Nom, c2.Nom, StringComparison.OrdinalIgnoreCase));

            foreach (Salarie s in this.ensemble_salaries)
            {
                Console.WriteLine(s.ToString());
            }
        }

        /// <summary>
        /// Fonction qui affiche tout les clients par order alphabetique de leurs noms
        /// </summary>
        public void Affiche_Chauffeurs_Alpha()
        {
            if (this.ensemble_chauffeurs != null)
            {
                this.ensemble_chauffeurs.Sort((c1, c2) => string.Compare(c1.Nom, c2.Nom, StringComparison.OrdinalIgnoreCase));
                foreach (Chauffeur c in this.ensemble_chauffeurs)
                {
                    Console.WriteLine(c.ToString());
                }
            }

            
        }

        #endregion

        #region iv-Gestion Liste des Salaries et Chauffeurs

        /// <summary>
        /// cette fonction importe l'ensemble des  salaries depuis le fichier
        /// </summary>
        /// <param name="filePath"></param>
        public void ImporterSalarieFichier(string filePath = "Salaries.csv")
        {
            foreach (string line in File.ReadLines(filePath))
            {
                // Split the line into fields
                string[] attribut = line.Split(',');

                // Parse the fields and create a Salarie instance
                Salarie salarie = new Salarie(
                    attribut[0], // Nom
                    attribut[1], // Prenom
                    DateTime.Parse(attribut[2]), // Naissance
                    attribut[3], // Adresse
                    attribut[4], // Email
                    int.Parse(attribut[5]), // Telephone
                    int.Parse(attribut[6]), // SS
                    DateTime.Parse(attribut[7]), // Entree
                    attribut[8], // Poste
                    double.Parse(attribut[9]) // Salaire
                );

                // Add the Salarie instance to the list
                if (salarie.Poste == "Chauffeur")
                {
                    salarie = new Chauffeur(salarie.Nom, salarie.Prenom, salarie.Naissance, salarie.Adresse, salarie.Email, salarie.Telephone, salarie.SS, salarie.Entree, salarie.Salaire);
                }
                if (this.ensemble_salaries != null) { this.ensemble_salaries.Add(salarie); }
                else { this.ensemble_salaries=new List<Salarie> { salarie }; }
            }
        }

        /// <summary>
        /// cette fonction permet de rajouter un nouveau salarie dans le fichier des clients
        /// </summary>
        /// <param name="c"></param>
        /// <param name="filePath"></param>
        public void EnregistrerSalarieFichier(Salarie s, string filePath = "Salaries.csv")
        {

            StreamWriter writer = new StreamWriter(filePath);
            string enregistrement = s.Nom + "," + s.Prenom + "," + s.Naissance.ToString() + "," + s.Adresse + "," + s.Email + "," + s.Telephone + "," + s.SS + "," + s.Entree + "," + s.Poste + "," + s.Salaire;
            writer.WriteLine(enregistrement);
            writer.Close();

            Console.WriteLine("Le Salarie à été enregistré dans le fichier");
        }

        /// <summary>
        /// cette fonction permet de reinitialiser et remettre a jour le fichier salarie apres avoir licenser un salarie
        /// on implemente cette fonction juste apres avoir supprimer des clients
        /// elle commence par suprimer le contenu du fichier pour ensuite reecrire l'integralite des salaries 
        /// </summary>
        /// <param name="nomfichier"></param>
        public void MiseAJour_FichierSalarie(string filePath = "Salaries.csv")
        {
            // on efface le contenue du fichier
            File.WriteAllText(filePath, string.Empty);
            string enregistrement;


            // remettre tout les clients
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                for (int i = 0; i < this.ensemble_salaries.Count() - 1; i++)
                {
                    enregistrement = ensemble_salaries[i].Nom + "," + ensemble_salaries[i].Prenom + "," + ensemble_salaries[i].Naissance.ToString() + "," + ensemble_salaries[i].Adresse + "," + ensemble_salaries[i].Email + "," + ensemble_salaries[i].Telephone + "," + ensemble_salaries[i].SS + "," + ensemble_salaries[i].Entree + "," + ensemble_salaries[i].Poste + "," + ensemble_salaries[i].Salaire;
                    writer.WriteLine(enregistrement);
                }
                enregistrement = ensemble_salaries[ensemble_salaries.Count() - 1].Nom + "," + ensemble_salaries[ensemble_salaries.Count() - 1].Prenom + "," + ensemble_salaries[ensemble_salaries.Count() - 1].Naissance.ToString() + "," + ensemble_salaries[ensemble_salaries.Count() - 1].Adresse + "," + ensemble_salaries[ensemble_salaries.Count() - 1].Email + "," + ensemble_salaries[ensemble_salaries.Count() - 1].Telephone + "," + ensemble_salaries[ensemble_salaries.Count() - 1].SS + "," + ensemble_salaries[ensemble_salaries.Count() - 1].Entree + "," + ensemble_salaries[ensemble_salaries.Count() - 1].Poste + "," + ensemble_salaries[ensemble_salaries.Count() - 1].Salaire;

                writer.Write(enregistrement);
            }

            Console.WriteLine("Liste des Salaries a été mise a jour avec succès");
        }

        /// <summary>
        /// cette fonction permet d'embaucher un nouveau salarie
        /// l'utilisateur saisie les donnees du salarie qui est ensuite ajouter dans le fichier et la liste des salaries de l'entreprise
        /// cette fonction aussi ajout automatiquement les chauffeur a la liste si le poste attribuer au nouveau salarie est Chauffeur
        /// </summary>
        public void EmbaucherSalarie()
        {
            #region Donnee du nouveau salarie
            Console.WriteLine();
            Console.WriteLine("=========== Embauche d'un nouveau Salarié ==========");

            Console.WriteLine("Nom du Salarie: ");
            string nom = Console.ReadLine();
            Console.WriteLine("Prénom du Salarie: ");
            string prenom = Console.ReadLine();
            Console.WriteLine("Date de naissance: ");
            string date = Console.ReadLine();
            DateTime naissance = new DateTime();
            naissance = Convert.ToDateTime(naissance);
            Console.WriteLine("Adresse: ");
            string adresse = Console.ReadLine();
            Console.WriteLine("Email: ");
            string email = Console.ReadLine();
            Console.WriteLine("Numero de téléphone: ");
            int telephone = int.Parse(Console.ReadLine());
            Console.WriteLine("Numero de Sécurité Social: ");
            int ss = int.Parse(Console.ReadLine());
            Console.WriteLine("La date d'entrée dans l'entreprise: ");
            string dateentre = Console.ReadLine();
            DateTime entree = new DateTime();
            entree = Convert.ToDateTime(dateentre);

            #region poste
            int p;

            do
            {
                Console.WriteLine("Son poste:\n" +
                                  "\n\t1/Directeur des Operations " +
                                  "\n\t2/Directrice des Operations " +
                                  "\n\t3/Directeur RH " +
                                  "\n\t4/Directerice RH " +
                                  "\n\t5/Directeur Commerciale " +
                                  "\n\t6/Directrice Commerciale " +
                                  "\n\t7/Directeur Financier " +
                                  "\n\t8/Directrice Financiere " +
                                  "\n\t9/Commercial " +
                                  "\n\t10/Chef d'Equipe " +
                                  "\n\t11/Formation" +
                                  "\n\t12/Contrats" +
                                  "\n\t13/Direction Comptable " +
                                  "\n\t14/Controleur de Gestion " +
                                  "\n\t15/Comptable " +
                                  "\n\t16/Chauffeur");
                p=int.Parse(Console.ReadLine());

            } while (p < 1 && p > 3);

            string poste;
            switch (p)
            {
                case 1:
                    poste = "Directeur des Operations";
                    break;
                case 2:
                    poste = "Directrice des Operations";
                    break;
                case 3:
                    poste = "Directeur RH";
                    break;
                case 4:
                    poste = "Directerice RH";
                    break;
                case 5:
                    poste = "Directeur Commerciale";
                    break;
                case 6:
                    poste = "Directrice Commerciale";
                    break;
                case 7:
                    poste = "Directeur Financier";
                    break;
                case 8:
                    poste = "Directrice Financiere";
                    break;
                case 9:
                    poste = "Commercial";
                    break;
                case 10:
                    poste = "Chef d'Equipe";
                    break;
                case 11:
                    poste = "Formation";
                    break;
                case 12:
                    poste = "Contrats";
                    break;
                case 13:
                    poste = "Direction Comptable";
                    break;
                case 14:
                    poste = "Controleur de Gestion";
                    break;
                case 15:
                    poste = "Comptable";
                    break;
                case 16:
                    poste = "Chauffeur";
                    break;
                default:
                    poste = "Poste non spécifié";
                    break;
            }


            #endregion


            Console.WriteLine("Son salaire: ");
            string sal = Console.ReadLine();
            double salaire;
            salaire = Convert.ToDouble(sal);

            #endregion


            Salarie s = new Salarie(nom, prenom, naissance, adresse, email, telephone, ss, entree, poste, salaire);


            if (s != null)
            {
                if (this.SalarieExist(s) == false)
                {
                    if (this.ensemble_salaries != null) { this.ensemble_salaries.Add(s); }
                    else { this.ensemble_salaries = new List<Salarie> { s }; }
                    Console.WriteLine("Salarie ajouter avec succes");
                    EnregistrerSalarieFichier(s);
                    MiseAJour_FichierSalarie();

                    if (s.Poste == "Chauffeur")
                    {
                        Chauffeur c = new Chauffeur(nom, prenom, naissance, adresse, email, telephone, ss, entree, salaire);
                        c.Livraisons(this.ensemble_commande);
                        if (this.ensemble_chauffeurs != null) { this.ensemble_chauffeurs.Add(c); }
                        else { this.ensemble_chauffeurs=new List<Chauffeur> { c }; }
                    }

                }
                else { Console.WriteLine("Salarie existe deja"); }
            }
            else Console.WriteLine("Le Salarie n'a pas pu etre ajouter !");

        }

        /// <summary>
        /// cette fonction permet de licensier un salarie
        /// on met a jour le fichier des salaries
        /// on met a jour la liste des chauffeur si il s'agissait d'une chuaffeur
        /// </summary>
        public void LicensierSalarie()
        {
            Console.WriteLine("========== Licensier un salarie ==========");
            //demande utilisateur donnee du client a supprimer
            Console.WriteLine("Nom du Salarie à licensier : ");
            string nom = Console.ReadLine();
            Console.WriteLine("Prenom du Salarie à licensier : ");
            string prenom = Console.ReadLine();

            Salarie s = RechercheSalarie(nom, prenom);

            if (this.ensemble_salaries != null && s != null && this.SalarieExist(s))
            {
                this.ensemble_salaries.Remove(s);
                Console.WriteLine("Le salarie à été supprimer avec succès !");
                this.MiseAJour_FichierSalarie();
                if (s.Poste == "Chauffeur")
                {
                    foreach (Chauffeur c in this.ensemble_chauffeurs)
                    {
                        if (c.Nom.ToLower() == nom.ToLower() && c.Prenom.ToLower() == prenom.ToLower())
                        {
                            this.ensemble_chauffeurs.Remove(c);
                            break;
                        }
                    }
                }

            }
            else { Console.WriteLine("Le salarie n'a pas pu etre supprimer"); }

        }

        /// <summary>
        /// cette fonction met a jour la liste de chauffeur de l'entreprise.
        /// elle est surtout utile lors de l'initialisation de la base de donnee de l'entreprise
        /// </summary>
        public void Update_Chauffeur()
        {
            foreach (Salarie s in this.ensemble_salaries)
            {
                if (s is Chauffeur c)
                {
                    if (this.ensemble_chauffeurs != null) { this.ensemble_chauffeurs.Add(c); }
                    else { this.ensemble_chauffeurs = new List<Chauffeur> { c }; }
                }
            }
        }

        /// <summary>
        /// cette fonction permet de mettre a jours les donnee d'un salarie
        /// elle met a jours le fichier salarie avec les nouvelle donnees
        /// </summary>
        public void ModifierSalarie()
        {
            Console.WriteLine("============ Modifier Salarie ==========");
            Console.WriteLine("\nNom de Salarie: ");
            string nom = Console.ReadLine();
            Console.WriteLine("\nPrenom de Salarie: ");
            string prenom = Console.ReadLine();

            Salarie s = RechercheSalarie(nom, prenom);

            if (s == null) { Console.WriteLine("\nNon Existant ! "); }
            else
            {
                Console.WriteLine("Que voulez vous modifer? ");
                Console.WriteLine("\t1/Nom");
                Console.WriteLine("\t2/Poste");
                Console.WriteLine("\t0/Annuler");

                int x = -1;
                do
                {
                    x=int.Parse(Console.ReadLine());
                } while (x < 0 || x > 2);

                switch (x)
                {
                    case 0:
                        break;
                    case 1:
                        Console.WriteLine("\nNouveau Nom: ");
                        string nom1=Console.ReadLine();
                        s.Nom= nom1;
                        break;
                    case 2:
                        Console.WriteLine("\nNouveau Poste:  (Directeur/rice des Operations - Directeur/rice RH - Directeur/rice Commerciale - Directeur/rice Financiere - Commercial/e - Chef d'Equipe - Formation  - Contrats - Direction Comptable - Controleur de Gestion - Comptable - Chauffeur) ");
                        string poste = Console.ReadLine();
                        s.Poste = poste;
                        break;

                }

                Console.WriteLine("\nModification Terminee ! ");
                Console.WriteLine("\n"+s.ToString());
                MiseAJour_FichierSalarie();
            }

        }

        #endregion

        #endregion

        #region IIc-GESTION COMMANDES

        #region i- fonctions utiles

        /// <summary>
        /// cette fonction renvoie un chauffeur qui est est disponible pour faire une livraison 
        /// il est disponible si il a pas d'autre livraison le jour la commande en question
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public Chauffeur Choix_Chauffeur(DateTime date)
        {
            foreach (Chauffeur c in this.ensemble_chauffeurs)
            {
                if (c.Disponible(date)) { c.Dates_Livraisons.Add(date); return c; }
            }

            return null;
        }

        /// <summary>
        /// cette fonction renvoie une voiture parmi la flotte des vehicules de l'entreprise qui est disponible pour une date indiquer 
        /// </summary>
        /// <param name="date"></param>
        /// <param name="nb_passager"></param>
        /// <returns></returns>
        public Vehicule Choix_Voiture(DateTime date, int nb_passager)
        {
            string type;
            if (nb_passager <= 4) { type = "petite"; }
            else { type = "grande"; }
            foreach (Vehicule v in this.flotte_vehicules)
            {
                if (v is Voiture && ((Voiture)v).Type == type && v.Disponible(date))
                {
                    v.Date_prises.Add(date);
                    return v;
                }

            }
            return null;
        }

        /// <summary>
        /// cette fonction renvoie une voiture parmi la flotte des vehicules de l'entreprise qui est disponible pour une date indiquer 
        /// </summary>
        /// <param name="date"></param>
        /// <param name="cause"></param>
        /// <returns></returns>
        public Vehicule Choix_Camionette(DateTime date, string cause)
        {
            foreach (Vehicule v in this.flotte_vehicules)
            {
                if (v is Camionette && ((Camionette)v).Cause == cause && v.Disponible(date))
                {
                    v.Date_prises.Add(date);
                    return v;
                }

            }
            return null;
        }

        /// <summary>
        /// cette fonction renvoie une voiture parmi la flotte des vehicules de l'entreprise qui est disponible pour une date indiquer 
        /// </summary>
        /// <param name="date"></param>
        /// <param name="poids"></param>
        /// <param name="volume"></param>
        /// <param name="matiere"></param>
        /// <returns></returns>
        public Vehicule Choix_Citerne(DateTime date, int poids, int volume, string matiere)
        {
            foreach (Vehicule v in this.flotte_vehicules)
            {
                if (v is Citerne && ((Citerne)v).Matiere == matiere && ((Citerne)v).Volume == volume && ((Citerne)v).Poids == poids && v.Disponible(date))
                {
                    v.Date_prises.Add(date);
                    return v;
                }

            }
            return null;
        }

        /// <summary>
        /// cette fonction renvoie une voiture parmi la flotte des vehicules de l'entreprise qui est disponible pour une date indiquer 
        /// </summary>
        /// <param name="date"></param>
        /// <param name="poids"></param>
        /// <param name="volume"></param>
        /// <param name="matiere1"></param>
        /// <param name="grue"></param>
        /// <returns></returns>
        public Vehicule Choix_Benne1(DateTime date, int poids, int volume, string matiere1, bool grue)
        {
            foreach (Vehicule v in this.flotte_vehicules)
            {
                if (v is Benne && ((Benne)v).Nb_benne == 1 && ((Benne)v).Volume == volume && ((Benne)v).Poids == poids && v.Disponible(date))
                {
                    v.Date_prises.Add(date);
                    ((Benne)v).Matiere1 = matiere1;
                    ((Benne)v).Grue = grue;
                    return v;
                }

            }
            return null;
        }

        /// <summary>
        /// cette fonction renvoie une voiture parmi la flotte des vehicules de l'entreprise qui est disponible pour une date indiquer 
        /// </summary>
        /// <param name="date"></param>
        /// <param name="poids"></param>
        /// <param name="volume"></param>
        /// <param name="matiere1"></param>
        /// <param name="matiere2"></param>
        /// <param name="grue"></param>
        /// <returns></returns>
        public Vehicule Choix_Benne2(DateTime date, int poids, int volume, string matiere1, string matiere2, bool grue)
        {
            foreach (Vehicule v in this.flotte_vehicules)
            {
                if (v is Benne && ((Benne)v).Nb_benne == 2 && ((Benne)v).Volume == volume && ((Benne)v).Poids == poids && v.Disponible(date))
                {
                    v.Date_prises.Add(date);
                    ((Benne)v).Matiere1 = matiere1;
                    ((Benne)v).Matiere2 = matiere2;
                    ((Benne)v).Grue = grue;

                    return v;
                }

            }
            return null;
        }

        /// <summary>
        /// cette fonction renvoie une voiture parmi la flotte des vehicules de l'entreprise qui est disponible pour une date indiquer 
        /// </summary>
        /// <param name="date"></param>
        /// <param name="poids"></param>
        /// <param name="volume"></param>
        /// <param name="matiere1"></param>
        /// <param name="matiere2"></param>
        /// <param name="matiere3"></param>
        /// <param name="grue"></param>
        /// <returns></returns>
        public Vehicule Choix_Benne3(DateTime date, int poids, int volume, string matiere1, string matiere2, string matiere3, bool grue)
        {
            foreach (Vehicule v in this.flotte_vehicules)
            {
                if (v is Benne && ((Benne)v).Nb_benne == 3 && ((Benne)v).Volume == volume && ((Benne)v).Poids == poids && v.Disponible(date))
                {
                    v.Date_prises.Add(date);
                    ((Benne)v).Matiere1 = matiere1;
                    ((Benne)v).Matiere2 = matiere2;
                    ((Benne)v).Matiere3 = matiere3;
                    ((Benne)v).Grue = grue;


                    return v;
                }

            }
            return null;
        }

        /// <summary>
        /// cette fonction renvoie une voiture parmi la flotte des vehicules de l'entreprise qui est disponible pour une date indiquer 
        /// </summary>
        /// <param name="date"></param>
        /// <param name="poids"></param>
        /// <param name="volume"></param>
        /// <returns></returns>
        public Vehicule Choix_Frigorifique(DateTime date, int poids, int volume)
        {
            foreach (Vehicule v in this.flotte_vehicules)
            {
                if (v is Frigorifique && ((Frigorifique)v).Volume == volume && ((Frigorifique)v).Poids == poids && v.Disponible(date))
                {
                    v.Date_prises.Add(date);
                    return v;
                }

            }
            return null;
        }

        /// <summary>
        /// cette fonction permet de recuperer une commande a partir de son identifiant
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Commande RechercheCommande_ID(int id)
        {
            foreach (Commande c in this.ensemble_commande)
            {
                if (c.Id == id) { return c; }
            }

            return null;
        }

        #endregion

        #region ii- Affichage

        /// <summary>
        /// cette fonction affiche l'ensemble des commandes
        /// </summary>
        public void Affiche_Commandes()
        {
            Console.WriteLine("========== Liste des Commandes ==========");
            if (this.ensemble_commande != null)
            {
                foreach (Commande c in this.ensemble_commande)
                {
                    Console.WriteLine(c.ToString());
                }
            }
            else
            {
                Console.WriteLine("Aucune commande. ");
            }

        }

        #endregion

        #region iii-Gestion List Commande

        /// <summary>
        /// Méthode qui permet de créer une commande en prenant en compte tout les paramètres et de vérifier les disponibilité des véhicule, du chauffeur etc....
        /// </summary>
        /// <param name="map"></param>
        public void AjouterCommande()
        {
            Console.WriteLine("========== Nouvelle Commande ==========");
            #region Client
            Console.WriteLine("Nom du client: ");
            string nom = Console.ReadLine();
            Console.WriteLine("Prénom du client: ");
            string prenom = Console.ReadLine();
            Client client = this.RechercheClient_NP(nom, prenom);

            if (client == null)
            {
                Console.WriteLine("Ce client n'existe pas. Nous allons le rajouter");
                this.AjouterClient();
                client = new Client();
                client = this.RechercheClient_NP(nom, prenom);

            }

            client.Nb_Commandes++;
            client.Remises = client.Attribuer_Remise();

            #endregion

            Console.WriteLine("\n Poursuite Commande: \n");

            #region Distance (Depart/Arrivee)

            Console.WriteLine("Ville de départ: ");
            string depart = Console.ReadLine();
            Console.WriteLine("Ville d'arrivée: ");
            string arrivee = Console.ReadLine();

            Graph map = new Graph();
            map = map.Construire_graph();
            int distance = map.Distance_Chemain_court(map.Matrice_adj(), map.Labels(), depart, arrivee); //affiche chemin + retourne distance

            #endregion

            DateTime date = new DateTime();
            Vehicule vehicule = null;
            Chauffeur chauffeur = null;

            do
            {
                #region Date
                do
                {
                    Console.WriteLine("Date de livraison: ");
                    date = Convert.ToDateTime(Console.ReadLine());

                } while (date.Year < DateTime.Now.Year || (date.Month <= DateTime.Now.Month && date.Day <= DateTime.Now.Day && date.Year == DateTime.Now.Year) || (date.Month < DateTime.Now.Month && date.Year == DateTime.Now.Year));
                #endregion

                #region Choix Vehicule
                Console.WriteLine("Quel véhicule souhaitez vous ?");
                Console.WriteLine("\t1/Voiture : Pour transport de passagers exclusivement");
                Console.WriteLine("\t2/Camionette : Pour usage spécifique ");
                Console.WriteLine("\t3/Camion : Pour transport de gros volume , plusieurs types de camion proposé");

                int v = 0;
                do
                {
                    v = int.Parse(Console.ReadLine());
                } while (v <= 0 || v > 3);

                switch (v)
                {
                    case 1: //Voiture
                        #region Voiture 

                        Console.WriteLine("Veuillez indiquer le nombre de passager à transporter ?");
                        int nb_passagers = int.Parse(Console.ReadLine());
                        vehicule = Choix_Voiture(date, nb_passagers);

                        #endregion
                        break;


                    case 2: //Camionette
                        #region Camionette

                        int choix;
                        do
                        {
                            Console.WriteLine("Précisez l'usage de la camionette:\n1/Transport de verre pour vitriers\n2/Services de restauration et d'événementiel\n3/Transport de meubles");
                            choix = int.Parse(Console.ReadLine());

                        } while (choix <= 0 || choix > 3);

                        string cause = null;

                        if (choix == 1) { cause = "Transport de verre pour vitriers"; }
                        if (choix == 2) { cause = "Services de restauration et d'événementiel"; }
                        if (choix == 3) { cause = "Transport de meubles"; }

                        vehicule = Choix_Camionette(date, cause);


                        #endregion
                        break;

                    case 3: //Camion
                        #region POIDS/VOLUME
                        Console.WriteLine("preciser le poids (  1 - 2 ) en tonnes");
                        int poids = int.Parse(Console.ReadLine());
                        Console.WriteLine("preciser le volume (  25 - 40 ) en m3");
                        int volume = int.Parse(Console.ReadLine());
                        #endregion

                        #region Choix Type de Camion
                        int x = 0;
                        do
                        {
                            Console.WriteLine("Quelle type de camion souhaitez vous ?");
                            Console.WriteLine("\t1/Citerne : Pour le transport de liquide ou de Gaz");
                            Console.WriteLine("\t2/Benne : Pour le transport de matériaux (sable, terre, gravier");
                            Console.WriteLine("\t3/Frigorifique : Pour le transport de produits alimentaires");
                            x = int.Parse(Console.ReadLine());
                        } while (x <= 0 || x > 3);

                        switch (x)
                        {
                            case 1: //citerne
                                #region Citerne
                                int choix_matiere;
                                do
                                {
                                    Console.Write("Précisez la matiere transporte:\n1/Matières dangereuses\n2/ Produits pétroliers de grande valeur\n3/Gaz\n4/Eau");
                                    choix_matiere = int.Parse(Console.ReadLine());
                                } while (choix_matiere <= 0 || choix_matiere > 4);

                                string matiere = null;
                                if (choix_matiere == 1) { matiere = "Matières dangereuses"; }
                                if (choix_matiere == 2) { matiere = "Produits pétroliers de grande valeur"; }
                                if (choix_matiere == 3) { matiere = "Gaz"; }
                                if (choix_matiere == 4) { matiere = "Eau"; }

                                vehicule = Choix_Citerne(date, poids, volume, matiere);

                                #endregion
                                break;

                            case 2://Benne

                                #region Nombre de Bennes
                                int nb = 0;
                                do
                                {
                                    Console.Write("Selectionner le nombre de benne souhaite: 1 - 2 - 3");
                                    nb = int.Parse(Console.ReadLine());
                                } while (nb <= 0 || nb > 3);
                                #endregion

                                #region Grue
                                bool g = false;
                                int a;
                                do
                                {
                                    Console.Write("Voulez-vous une grue supllementaire?\n0/Non\n1/Oui");
                                    a = int.Parse(Console.ReadLine());
                                } while (a != 0 && a != 1);

                                if (a == 1) { g = true; }

                                #endregion

                                #region Choix Matieres

                                int m1, m2, m3;
                                string matiere1 = null; string matiere2 = null; string matiere3 = null;
                                Console.WriteLine("Choisir la/les matieres (1 matiere par benne)");
                                Console.WriteLine("\n1/Sable\n2/Terre\n3/Gravier");

                                do
                                {
                                    m1 = int.Parse(Console.ReadLine());

                                } while (m1 < 1 || m1 > 3);

                                if (m1 == 1) { matiere1 = "Sable"; }
                                if (m1 == 2) { matiere1 = "Terre"; }
                                if (m1 == 3) { matiere1 = "Gravier"; }

                                if (nb == 2)
                                {
                                    do
                                    {
                                        m2 = int.Parse(Console.ReadLine());

                                    } while (m2 < 1 || m2 > 3);

                                    if (m2 == 1) { matiere2 = "Sable"; }
                                    if (m2 == 2) { matiere2 = "Terre"; }
                                    if (m2 == 3) { matiere2 = "Gravier"; }

                                    vehicule = Choix_Benne2(date, poids, volume, matiere1, matiere2, g);
                                }
                                else if (nb == 3)
                                {
                                    do
                                    {
                                        m2 = int.Parse(Console.ReadLine());

                                    } while (m2 < 1 || m2 > 3);

                                    if (m2 == 1) { matiere2 = "Sable"; }
                                    if (m2 == 2) { matiere2 = "Terre"; }
                                    if (m2 == 3) { matiere2 = "Gravier"; }

                                    do
                                    {
                                        m3 = int.Parse(Console.ReadLine());

                                    } while (m3 < 1 || m3 > 3);

                                    if (m3 == 1) { matiere3 = "Sable"; }
                                    if (m3 == 2) { matiere3 = "Terre"; }
                                    if (m3 == 3) { matiere3 = "Gravier"; }

                                    vehicule = Choix_Benne3(date, poids, volume, matiere1, matiere2, matiere3, g);
                                }
                                else
                                {
                                    vehicule = Choix_Benne1(date, poids, volume, matiere1, g);
                                }

                                #endregion

                                break;

                            case 3: //Frigorifique
                                #region Frigorifique
                                vehicule = Choix_Frigorifique(date, poids, volume);
                                #endregion
                                break;

                        }
                        #endregion

                        break;
                }
                #endregion

                #region Choix Chauffeur
                chauffeur = Choix_Chauffeur(date);
                #endregion

                if (chauffeur == null) Console.WriteLine("Aucun chauffeur n'est disponible pour le " + date.ToString() + " ,veuillez indiquer une autre date de livraison");
                if (vehicule == null) Console.WriteLine("Aucun vehicule n'est disponible pour le " + date.ToString() + " ,veuillez indiquer une autre date de livraison");



            } while (vehicule == null || chauffeur == null);

            #region Creation Commande et Calcul Prix

            Commande nouvelle_commande = new Commande(client, depart, arrivee, date, vehicule, chauffeur);
            nouvelle_commande.Prix = nouvelle_commande.Calcul_Prix(distance);

            #endregion

            #region Enregistrement de la Commande

            Console.WriteLine(nouvelle_commande.ToString());

            if (this.ensemble_commande == null)
            {
                this.ensemble_commande = new List<Commande> { nouvelle_commande };
            }
            else
            {
                this.ensemble_commande.Add(nouvelle_commande);
            }
            #endregion

        }

        /// <summary>
        /// cette fonction permet se supprimer une commande de la base de donnee de l'entreprise
        /// </summary>
        public void SupprimerCommande()
        {
            Console.WriteLine("========== Supprimer une Commande ==========");

            //demande utilisateur donnee du client a supprimer
            Console.WriteLine("Identifiant de la commande à supprimer : ");
            int id;
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Veuillez entrer un identifiant valide.");
                return;
            }

            Commande c = this.RechercheCommande_ID(id);

            if (this.ensemble_commande != null && c != null)
            {
                this.ensemble_commande.Remove(c);
                Console.WriteLine("La commande à été supprimer avec succès !");
                this.MiseAJour_FichierClient();

            }
            else { Console.WriteLine("La commande n'a pas pu etre supprimer"); }
        }

        /// <summary>
        /// cette fonction permet de modifier les commande qui sont encore en attente
        /// l'utilisateur a le choix entre modifier la date, la ville de depart ou d'arrivee
        /// </summary>
        public void ModifierCommande()
        {
            Console.WriteLine("=========== Modifier une Commande ===========\n");

            #region Choix commande
            Console.WriteLine("=========== Liste de commandes modifiables ===========\n");
            foreach (Commande c in this.ensemble_commande)
            {
                Console.WriteLine(c.ToString());
            }

            Console.WriteLine("\n\nSaisir l'identifiant (numero) de la commande a modifier: ");
            int id = int.Parse(Console.ReadLine());

            Commande c_modif = RechercheCommande_ID(id);
            #endregion

            if (c_modif != null && c_modif.Terminee() == false)
            {
                int choix = 0;
                do
                {
                    Console.WriteLine("Que voulez vous modifier pour la commande " + c_modif.Id.ToString());
                    Console.WriteLine("\t1/Date de livraison");
                    Console.WriteLine("\t2/Ville de depart");
                    Console.WriteLine("\t3/Ville d'arrivée");
                    Console.WriteLine("\t0/Quitter");
                    choix = int.Parse(Console.ReadLine());
                } while (choix > 3 || choix < 0);

                switch (choix)
                {
                    case 0:
                        break;

                    case 1:
                        #region Date
                        c_modif.Chauf = null;
                        do
                        {
                            Console.WriteLine("Nouvelle date de livraison: ");
                            c_modif.Date = Convert.ToDateTime(Console.ReadLine());
                            c_modif.Chauf = Choix_Chauffeur(c_modif.Date);
                        } while (c_modif.Chauf == null);
                        Graph map = new Graph();
                        map = map.Construire_graph();
                        
                        int distance = map.Distance_Chemain_court(map.Matrice_adj(), map.Labels(), c_modif.Depart, c_modif.Arrivee); //affiche chemin + retourne distance
                        c_modif.Prix = c_modif.Calcul_Prix(distance);
                        break;
                    #endregion
                    case 2:
                        #region Depart
                        Console.WriteLine("Nouvelle ville de départ: ");
                        string depart = Console.ReadLine();
                        string arrivee = c_modif.Arrivee;

                        Graph map1 = new Graph();
                        map1 = map1.Construire_graph();
                        int distance1 = map1.Distance_Chemain_court(map1.Matrice_adj(), map1.Labels(), depart, arrivee); //affiche chemin + retourne distance

                        if (distance1 > 0)
                        {
                            c_modif.Depart = depart;
                            c_modif.Prix = c_modif.Calcul_Prix(distance1);

                        }

                        break;
                    #endregion
                    case 3:
                        #region Arrivee
                        Console.WriteLine("Nouvelle ville d'arrivee: ");
                        string a = Console.ReadLine();
                        string d = c_modif.Depart;

                        Graph map2 = new Graph();
                        map2 = map2.Construire_graph();
                        int dist = map2.Distance_Chemain_court(map2.Matrice_adj(), map2.Labels(), d, a); //affiche chemin + retourne distance

                        if (dist > 0)
                        {
                            c_modif.Arrivee = a;
                            c_modif.Prix = c_modif.Calcul_Prix(dist);

                        }
                        
                        break;
                        #endregion

                }
                Console.WriteLine("Commande modifiee !");
                Console.WriteLine(c_modif.ToString());
            }

        }

        #endregion

        #endregion

        #region IId-STATISTIQUES

        /// <summary>
        /// cette fonction compte le nombre de livraisons faites par un chauffeur
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public int Compter_Livraisons(Chauffeur c)
        {
            int cpt = 0;
            foreach (Commande co in this.ensemble_commande)
            {
                if (co.Chauf == c) { cpt++; }
            }
            return cpt;
        }

        /// <summary>
        /// cette fonction le nombre de livraisons effectuées affiche par chauffeur  
        /// </summary>
        public void Nb_Livraison_Chauffeur()
        {
            int n;
            Console.WriteLine("============ Nombre de Livraison Par Chauffeurs ============");
            foreach (Chauffeur c in this.ensemble_chauffeurs)
            {
                n = Compter_Livraisons(c);
                Console.WriteLine(c.Nom + " " + c.Prenom + " : " + n);
            }
        }

        /// <summary>
        /// cette fonction affiche les commandes selon une période de temps
        /// </summary>
        /// <param name="debut"></param>
        /// <param name="fin"></param>
        public void Commande_Periode()
        {
            Console.WriteLine("=========== Commande sur une Periode ============");
            
            Console.WriteLine("\nSaisire date debut: ");
            string date1 = Console.ReadLine();
            DateTime debut = new DateTime();
            if (date1 != "") debut = Convert.ToDateTime(date1);

            Console.WriteLine("\nSaisir date fin:");
            string date2 = Console.ReadLine();
            DateTime fin = new DateTime();
            if (date2 != "") fin = Convert.ToDateTime(date2);

            foreach (Commande c in this.ensemble_commande)
            {
                if (c.Date >= debut && c.Date <= fin)
                {
                    Console.WriteLine(c.ToString());
                }
            }

            Console.WriteLine("\nFin de la liste.");

        }

        /// <summary>
        /// Calcule et retourne la moyenne des prix commande
        /// </summary>
        /// <returns></returns>
        public void MoyennePrixCommande()
        {
            Console.WriteLine("============ Moyenne des prix des commandes ============\n");
            double s = 0;
            int len = this.ensemble_commande.Count;

            foreach (Commande c in this.ensemble_commande)
            {
                s += c.Prix;
            }
            Console.WriteLine("\nLa Moyenne est de : " + s / len);

        }

        /// <summary>
        /// cette fonction affiche la moyenne des comptes clients (pour chaque client)
        /// </summary>
        public void MoyCompteClients()
        {
            Console.WriteLine("============ Moyenne des comptes client ============\n");

            if (this.ensemble_clients != null)
            {
                
                foreach (Client c in this.ensemble_clients)
                {
                    double somme = 0;
                    int cpt = 0;

                    if (this.ensemble_commande != null)
                    {
                        foreach (Commande com in this.ensemble_commande)
                        {
                            if(com.Client == c) { somme += com.Prix; cpt++; }
                        }
                    }

                    Console.WriteLine(c.Nom + " " + c.Prenom + " : " + somme / cpt + " euros");
                }
            }

        }

        /// <summary>
        /// cette fonction affiche les commandes faite par un client donnee
        /// </summary>
        /// <param name="c"></param>
        public void Commande_parClient()
        {
            Console.WriteLine("============ Commandes d'un Client ============\n");

            Console.WriteLine("saisir l'identifiant du client: ");
            int id=int.Parse(Console.ReadLine());

            Client c= RechercheClient_ID(id);

            foreach(Commande co in  this.ensemble_commande)
            {
                if(co.Client == c) { Console.WriteLine(c.ToString()); }
            }

            Console.WriteLine("Fin de la liste");
        }

        #endregion

        #region IIe-MODULE SUPPLEMENTAIRE

        /*Pour le module supplémentaire et afin d'ajouter de l'originalité au projet, 
         * j'ai choisi de faire dépendre le prix du type de véhicule choisi, non seulement de la distance et du tarif du chauffeur. 
         * J'ai décidé de mettre en œuvre la fonction "achat d'un véhicule et ajout à la flotte de l'entreprise", 
         * ainsi qu'une fonction permettant de calculer le bilan comptable de l'entreprise.
         */

        /// <summary>
        /// cette fonction permet l'enregistrement d'une nouveau vehicule achetee par l'entreprise
        /// elle met a jour aussi les depenses de l'entreprise
        /// </summary>
        public void Achat_Vehicule()
        {
            Console.WriteLine("============ Enregistrement d'un nouveau vehicule ============\n");

            #region type vehicule
            Console.WriteLine("Choisir le type de vehicule a acheter");
            Console.WriteLine("1/Voiture\n2/Camionette\n3/Camion Citerne\n4/Camion Benne\n5/Camion Frigorifique\n0/Quitter");
            int choix;
            do
            {
                choix=int.Parse(Console.ReadLine());
            } while(choix<1 || choix>5);

            #endregion

            switch (choix)
            {
                case 0:
                    break;
                case 1:
                    #region voiture
                    Console.WriteLine("saisir le nombre de passager (1 a 7): ");
                    int n = int.Parse(Console.ReadLine());
                    do
                    {
                        Console.WriteLine("Invalide. Reessayer:  ");
                        n=int.Parse(Console.ReadLine());    
                    } while(n<1 || n>8);

                    Voiture v=new Voiture(n);

                    if (this.flotte_vehicules == null)
                    {
                        List<Vehicule> l = new List<Vehicule> { v };
                        this.flotte_vehicules = l;
                    }
                    else { this.flotte_vehicules.Add(v); }

                    Console.WriteLine("\nEnregistrement fini! ");
                    Console.WriteLine("\nNouveau Vehicule: \n");
                    Console.WriteLine(v.ToString());

                    #endregion
                    break;
                case 2:
                    #region camionette
                    int x;
                    do
                    {
                        Console.WriteLine("Précisez l'usage de la camionette:\n1/Transport de verre pour vitriers\n2/Services de restauration et d'événementiel\n3/Transport de meubles");
                        x = int.Parse(Console.ReadLine());

                    } while (x <= 0 || x > 3);

                    string cause = null;

                    if (choix == 1) { cause = "Transport de verre pour vitriers"; }
                    if (choix == 2) { cause = "Services de restauration et d'événementiel"; }
                    if (choix == 3) { cause = "Transport de meubles"; }

                    Camionette v1 = new Camionette(cause);

                    if (this.flotte_vehicules == null)
                    {
                        List<Vehicule> l = new List<Vehicule> { v1 };
                        this.flotte_vehicules = l;
                    }
                    else { this.flotte_vehicules.Add(v1); }
                    Console.WriteLine("\nEnregistrement fini! ");
                    Console.WriteLine("\nNouveau Vehicule: \n");
                    Console.WriteLine(v1.ToString());
                    #endregion
                    break;
                case 3:
                    #region Camion Citerne
                    Console.WriteLine("preciser le poids (  1 - 2 ) en tonnes");
                    int poids = int.Parse(Console.ReadLine());
                    Console.WriteLine("preciser le volume (  25 - 40 ) en m3");
                    int volume = int.Parse(Console.ReadLine());

                    int choix_matiere;
                    do
                    {
                        Console.Write("Précisez la matiere transporte:\n1/Matières dangereuses\n2/ Produits pétroliers de grande valeur\n3/Gaz\n4/Eau");
                        choix_matiere = int.Parse(Console.ReadLine());
                    } while (choix_matiere <= 0 || choix_matiere > 4);

                    string matiere = null;
                    if (choix_matiere == 1) { matiere = "Matières dangereuses"; }
                    if (choix_matiere == 2) { matiere = "Produits pétroliers de grande valeur"; }
                    if (choix_matiere == 3) { matiere = "Gaz"; }
                    if (choix_matiere == 4) { matiere = "Eau"; }

                    Citerne c = new Citerne (poids, volume, matiere);

                    if (this.flotte_vehicules == null)
                    {
                        List<Vehicule> l = new List<Vehicule> { c };
                        this.flotte_vehicules = l;
                    }
                    else { this.flotte_vehicules.Add(c); }
                    Console.WriteLine("\nEnregistrement fini! ");
                    Console.WriteLine("\nNouveau Vehicule: \n");
                    Console.WriteLine(c.ToString());
                    #endregion
                    break;
                case 4:
                    #region Camion Benne
                    Console.WriteLine("preciser le poids (  1 - 2 ) en tonnes");
                    int poids1= int.Parse(Console.ReadLine());
                    Console.WriteLine("preciser le volume (  25 - 40 ) en m3");
                    int volume1 = int.Parse(Console.ReadLine());

                    #region nb_benne
                    int nb = 0;
                    do
                    {
                        Console.Write("Selectionner le nombre de benne souhaite: 1 - 2 - 3");
                        nb = int.Parse(Console.ReadLine());
                    } while (nb <= 0 || nb > 3);

                    #endregion

                    #region Choix Matieres

                    int m1, m2, m3;
                    string matiere1 = null; string matiere2 = null; string matiere3 = null;
                    Benne b = null;
                    Console.WriteLine("Choisir la/les matieres (1 matiere par benne)");
                    Console.WriteLine("\n1/Sable\n2/Terre\n3/Gravier");

                    do
                    {
                        m1 = int.Parse(Console.ReadLine());

                    } while (m1 < 1 || m1 > 3);

                    if (m1 == 1) { matiere1 = "Sable"; }
                    if (m1 == 2) { matiere1 = "Terre"; }
                    if (m1 == 3) { matiere1 = "Gravier"; }

                    if (nb == 2)
                    {
                        do
                        {
                            m2 = int.Parse(Console.ReadLine());

                        } while (m2 < 1 || m2 > 3);

                        if (m2 == 1) { matiere2 = "Sable"; }
                        if (m2 == 2) { matiere2 = "Terre"; }
                        if (m2 == 3) { matiere2 = "Gravier"; }

                        b = new Benne(poids1, volume1, nb, matiere1, matiere2, false);
                    }
                    else if (nb == 3)
                    {
                        do
                        {
                            m2 = int.Parse(Console.ReadLine());

                        } while (m2 < 1 || m2 > 3);

                        if (m2 == 1) { matiere2 = "Sable"; }
                        if (m2 == 2) { matiere2 = "Terre"; }
                        if (m2 == 3) { matiere2 = "Gravier"; }

                        do
                        {
                            m3 = int.Parse(Console.ReadLine());

                        } while (m3 < 1 || m3 > 3);

                        if (m3 == 1) { matiere3 = "Sable"; }
                        if (m3 == 2) { matiere3 = "Terre"; }
                        if (m3 == 3) { matiere3 = "Gravier"; }

                        b = new Benne(poids1, volume1, nb, matiere1, matiere2, matiere3, false);
                    }
                    else
                    {
                        b = new Benne(poids1, volume1, nb, matiere1, false);
                    }

                    #endregion

                    if (this.flotte_vehicules == null)
                    {
                        List<Vehicule> l = new List<Vehicule> { b };
                        this.flotte_vehicules = l;
                    }
                    else { this.flotte_vehicules.Add(b); }
                    Console.WriteLine("\nEnregistrement fini! ");
                    Console.WriteLine("\nNouveau Vehicule: \n");
                    Console.WriteLine(b.ToString());
                    #endregion
                    break;
                case 5:
                    #region Camion Frigorifique
                    Console.WriteLine("preciser le poids (  1 - 2 ) en tonnes");
                    int poids2 = int.Parse(Console.ReadLine());
                    Console.WriteLine("preciser le volume (  25 - 40 ) en m3");
                    int volume2 = int.Parse(Console.ReadLine());

                    Frigorifique f = new Frigorifique(poids2, volume2);

                    if (this.flotte_vehicules == null)
                    {
                        List<Vehicule> l = new List<Vehicule> { f };
                        this.flotte_vehicules = l;
                    }
                    else { this.flotte_vehicules.Add(f); }
                    Console.WriteLine("\nEnregistrement fini! ");
                    Console.WriteLine("\nNouveau Vehicule: \n");
                    Console.WriteLine(f.ToString());
                    #endregion
                    break;
            }

            #region Prix
            Console.WriteLine("\nA quelle prix a-t-il etait achete ? ");
            int prix=int.Parse(Console.ReadLine());
            this.Ajout_Depenses(prix);
            Console.WriteLine("Depense enregistree. ");
            #endregion

            

        }

        /// <summary>
        /// cette fonction affiche la flotte de vehicules de l'entreprise
        /// </summary>
        public void Afficher_Flotte_Vehicule()
        {
            Console.WriteLine("============ Flottes de Vehicules ============\n");

            for (int i=0; i<this.flotte_vehicules.Count; i++)
            {
                Console.WriteLine(this.flotte_vehicules[i].ToString());
            }
        }

        /// <summary>
        /// cette fonction rajoute les depenses faites par l'entreprise
        /// </summary>
        /// <param name="p"></param>
        public void Ajout_Depenses(double p)
        {
            this.depense += p;
        }

        /// <summary>
        /// cette fonction fait le bilan de l'entreprise
        /// gain-somme des salaires-les depenses
        /// </summary>
        public void Bilan_compte()
        {
            Console.WriteLine("============ Bilan Comptable ============\n");

            double somme_salaire = 0;
            double somme_depense = this.depense;
            double somme_gain = 0;

            foreach(Salarie s in this.ensemble_salaries)
            {
                somme_salaire += s.Salaire;
            }

            foreach(Commande c in this.ensemble_commande)
            {
                somme_gain += c.Prix;
            }

            double Bilan = somme_gain - somme_salaire - somme_depense;

            Console.WriteLine("Somme des gains: " + somme_gain + " euros");
            Console.WriteLine("Somme des salaires: " + somme_salaire + " euros");
            Console.WriteLine("Somme des dpenses: " + somme_depense + " euros");
            Console.WriteLine("Bilan: " + Bilan + " euros");



        }

        #endregion


        #endregion
    }
}
