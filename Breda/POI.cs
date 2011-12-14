using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace View
{
    public class POI
    {
        private Double lengteGraad { get; set; }
        private Double breedteGraad { get; set; }
        private String info { get; set; }
        private int nummer { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="POI"/> class.
        /// </summary>
        /// <param name="l">The longtitude.</param>
        /// <param name="b">The latitude.</param>
        /// <param name="i">The info.</param>
        public POI(Double l, Double b, String i, int n)
        {
            lengteGraad = l;
            breedteGraad = b;
            info = i;
            nummer = n;
        }
    }
}
