using System;

namespace View
{
    /// <summary>The POI class represents a POI where it holds the location, the infomation about it and what number it is in route.</summary>
    public class POI
    {
        private Double lengteGraad { get; set; }
        private Double breedteGraad { get; set; }
        private String info { get; set; }
        private int nummer { get; set; }

        /// <summary>Initializes a new instance of the <see cref="POI"/> class.</summary>
        /// <param name="l">The longtitude.</param>
        /// <param name="b">The latitude.</param>
        /// <param name="i">The exra information.</param>
        /// <param name="n">The number.</param>
        public POI(Double l, Double b, String i, int n)
        {
            lengteGraad = l;
            breedteGraad = b;
            info = i;
            nummer = n;
        }

        public String getInfo()
        {
            return info;
        }
    }
}
