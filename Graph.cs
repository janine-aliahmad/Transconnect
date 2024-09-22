using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace TransConnect
{
    internal class Graph
    {
        #region attribut - constructeurs - proprietes
        Sommet racine;
        List<Sommet> sommets = new List<Sommet>();

        public Graph() { }

        public Graph(Sommet racine)
        {
            this.racine = racine;
        }

        public Sommet Racine
        {
            get { return racine; }
            set { racine = value; }
        }

        public List<Sommet> Sommets
        {
            get { return sommets; }
            set { sommets = value; }
        }

        #endregion

        #region fonctions utiles

        /// <summary>
        /// associe une racine
        /// </summary>
        /// <param name="r"></param>
        public void Ajouter_Racine(Sommet r)
        {
            this.racine = r;
        }

        /// <summary>
        /// ajoute un sommer
        /// </summary>
        /// <param name="s"></param>
        public void Ajouter_Sommet(Sommet s)
        {
            this.Sommets.Add(s);
        }

        /// <summary>
        /// affiche tout les sommets du graphs
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string affiche = "";

            if (this.Sommets == null) { return "EMPTY"; }
            foreach (Sommet s in this.sommets)
            {
               affiche=affiche + s.Ville +" - ";
            }
            return affiche;
        }

        /// <summary>
        /// revoie un tableau avec les labels des sommets : les villes
        /// </summary>
        /// <returns></returns>
        public string[] Labels()
        {
            string[] l=new string[this.Sommets.Count];
            int i = 0;
            foreach(Sommet s in this.sommets)
            {
                l[i] = s.Ville; i++;
            }
            return l;
        }

        #endregion

        #region Construction Graphs + Matrice adj
        /// <summary>
        /// cette fonction renvoie la matrice d'adjacence du graph
        /// </summary>
        /// <returns></returns>
        public int[,] Matrice_adj()
        {
            int[,] adj = new int[this.sommets.Count, this.sommets.Count]; // crceation de la matrice taille
            for(int i = 0; i < this.sommets.Count; i++)
            {
                Sommet s1 = this.sommets[i];
                for(int j = 0; j < this.sommets.Count; j++)
                {
                    Sommet s2= this.sommets[j];
                    Arrete ar = s1.Arretes.FirstOrDefault(a => a.Enfant == s2);
                    if (ar != null) { adj[i, j] = ar.Distance; }
                    else { adj[i, j] = 0; }
                }
            }

            return adj;
        }

        /// <summary>
        /// cette fonction affiche la matrice d'adjacence
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="labels"></param>
        /// <param name="count"></param>
        public void AfficherMatrice(int[,] matrice)
        {
            int rows = matrice.GetLength(0);
            int cols = matrice.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(matrice[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// cette fonction construit un graph a partie d'un fichier CSV, elle n'attribut pas de racine au graph
        /// </summary>
        /// <param name="nomfichier"></param>
        /// <returns></returns>
        public Graph Construire_graph(string path = "D:\\Janine's Documents\\ESILV\\S6\\C#\\TransConnect\\TransConnect\\Distances.csv")
        {
            Graph graph = new Graph();

            // Read the CSV file
            var lines = File.ReadAllLines(path);

            foreach (var line in lines) 
            {
                var data = line.Split(',');
                string pointA = data[0].Trim();
                string pointB = data[1].Trim();
                int distance = int.Parse(data[2].Trim()); 

                // Create or get vertices
                Sommet sommetA = graph.Sommets.FirstOrDefault(s => s.Ville == pointA);
                if (sommetA == null)
                {
                    sommetA = new Sommet(pointA);
                    graph.Ajouter_Sommet(sommetA);
                }

                Sommet sommetB = graph.Sommets.FirstOrDefault(s => s.Ville == pointB);
                if (sommetB == null)
                {
                    sommetB = new Sommet(pointB);
                    graph.Ajouter_Sommet(sommetB);
                }

                // Create edge
                sommetA.Ajouter_arrete(sommetB, distance);
                sommetB.Ajouter_arrete(sommetA, distance);


            }
            return graph;
        }

        #endregion

        #region Dijkstra

        /// <summary>
        /// cette fonction trouve le sommet avec la distance minim parmi l'ensemble des sommets qui n'ont pas encore etait parcourure
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="tset"></param>
        /// <returns></returns>
        public int miniDist(int[] distance, bool[] tset)
        {
            int minimum = int.MaxValue; //infini
            int index = 0;
            for (int k = 0; k < distance.Length; k++)
            {
                if (!tset[k] && distance[k] <= minimum)
                {
                    minimum = distance[k];
                    index = k;
                }
            }
            return index;
        }


        /// <summary>
        /// cette  fonction applique l'arlgorithm de dijkstra sur un graph a partitr de sa matrice d'adjacence, un depart et une arrivee
        /// 
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="depart"></param>
        /// <param name="arrivee"></param>
        /// <returns></returns>
        public List<int> Dijkstar(int[,] adj, int depart, int arrivee)
        {
            int nb_sommet = adj.GetLength(0); //nombre de sommets
            int[] distance = new int[nb_sommet];
            bool[] parcourue = new bool[nb_sommet]; //sommet parcourue
            int[] prec = new int[nb_sommet]; //sommet precedant

            //initialisation
            for (int i = 0; i < nb_sommet; i++)
            {
                distance[i] = int.MaxValue;
                parcourue[i] = false;
                prec[i] = -1;
            }
            distance[depart] = 0;

            //debut de l'algo
            for (int k = 0; k < nb_sommet - 1; k++)
            {
                int minsommet = miniDist(distance, parcourue);
                parcourue[minsommet] = true;
                for (int i = 0; i < nb_sommet; i++)
                {
                    if (adj[minsommet, i] > 0)
                    {
                        int shortestToMinSommet = distance[minsommet];
                        int distanceToSommetSuiv = (int)adj[minsommet, i];
                        int Distacetotal = shortestToMinSommet + distanceToSommetSuiv;
                        if (Distacetotal < distance[i])
                        {
                            distance[i] = (int)Distacetotal;
                            prec[i] = minsommet;
                        }
                    }
                }
            }
            if (distance[arrivee] == int.MaxValue)
            {
                return new List<int>();
            }
            var chemin = new LinkedList<int>();
            int Sommet_courant = arrivee;
            while (Sommet_courant != -1)
            {
                chemin.AddFirst(Sommet_courant);
                Sommet_courant = prec[Sommet_courant];
            }
            return chemin.ToList(); //retourn donc une liste d'entier representant les indexes des neouds dans l'ordre du chemin a prendre
        }

        /// <summary>
        /// cette fonction AFFICHE le chemin le plus cours et RETOURNE la DISTANCE 
        /// </summary>
        /// <param name="adj"></param>
        /// <param name="labels"></param>
        /// <param name="depart"></param>
        /// <param name="arrivee"></param>
        public int Distance_Chemain_court(int[,] adj, string[] labels, string depart, string arrivee)
        {
            int d = Array.IndexOf(labels, depart);
            int a = Array.IndexOf(labels, arrivee);

            Console.Write($" le Chemain le plus court de [{depart} -> {arrivee}] est : ");
            var chemin = Dijkstar(adj, d, a);

            if (chemin.Count > 0)
            {
                int path_length = 0;
                for (int i = 0; i < chemin.Count - 1; i++)
                {
                    int length = (int)adj[chemin[i], chemin[i + 1]];
                    path_length += length;
                    Console.Write($"{labels[chemin[i]]} [{length}] -> ");
                }
                Console.WriteLine($"{labels[a]} (Distance {path_length})");
                return path_length;

            }
            else
            {
                Console.WriteLine("Aucun chemain");
                return 0;
            }

        }

        #endregion
    }
}
