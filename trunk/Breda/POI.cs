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

        /// <summary>
        /// Initializes a new instance of the <see cref="POI"/> class.
        /// </summary>
        /// <param name="l">The l.</param>
        /// <param name="b">The b.</param>
        /// <param name="i">The i.</param>
        public POI(Double l, Double b, String i)
        {
            this.lengteGraad = l;
            this.breedteGraad = b;
            this.info = i;
            
        }
    }
}
