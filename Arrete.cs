using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    internal class Arrete
    {
        //attribut
        int distance;
        Sommet parent; //d'ou l'arc part
        Sommet enfant; //ou l'arc arrive

        //constructeur
        public Arrete(int distance, Sommet parent, Sommet enfant)
        {
            this.distance = distance;
            this.parent = parent;
            this.enfant = enfant;
        }

        //proprietes
        public int Distance
        {
            get { return distance; }
            set { distance = value; }
        }

        public Sommet Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public Sommet Enfant
        {
            get { return enfant; }
            set { enfant = value; }
        }
    }
}
