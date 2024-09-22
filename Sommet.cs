using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    internal class Sommet
    {
        string ville;
        public List<Arrete> arretes = new List<Arrete>();

        public Sommet(string ville)
        {
            this.ville = ville;
        }

        public string Ville
        {
            get { return this.ville; }
            set { this.ville = value; }
        }

        public List<Arrete> Arretes
        {
            get { return this.arretes; }
            set { this.arretes = value; }
        }

        /// <summary>
        /// cette fonction associe une arette entre 2 sommet avec son poids
        /// </summary>
        /// <param name="enfant"></param>
        /// <param name="poids"></param>

        public void Ajouter_arrete(Sommet enfant, int poids)
        {
            Arrete arrete = new Arrete(poids, this, enfant);
            this.Arretes.Add(arrete);
        }
    }
}
