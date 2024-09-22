using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    abstract class Camion : Vehicule
    {
        //attribut
        protected int poids;
        protected int volume;

        //constructeur

        public Camion (int poids, int volume) : base()
        {
            this.poids = poids;
            this.volume = volume;
        }

        //propriete

        public int Poids { get { return this.poids; } }
        public int Volume { get { return this.volume; } }

        //methode

        public override string ToString()
        {
            return "\nPoids: "+this.poids+" kg \nVolume: "+this.volume+" L" + base.ToString();
        }

    }
}
