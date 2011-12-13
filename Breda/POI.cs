using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace View
{
    public class POI
    {
        public Double lengteGraad;
        public Double breedteGraad;
        public String info;

        public POI(Double l, Double b, String i)
        {
            this.lengteGraad = l;
            this.breedteGraad = b;
            this.info = i;
        }
    }
}
