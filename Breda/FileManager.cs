using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Model
{
    public class FileManager
    {
        private Breda.App bredamobiel;

        public FileManager(Breda.App breda)
        {
            bredamobiel = breda;
        }

        /// <summary>
        /// Saves the specified data at the specified location.
        /// </summary>
        /// <param name="location">The location where the data will be stored.</param>
        /// <param name="data">The data which will be stored.</param>
        /// <returns>true if data is saved successfully, otherwise false</returns>
        public bool Save(string location, string data)
        {
            // failed to save data
            return false;
        }
        /// <summary>
        /// Loads data from the specified location.
        /// </summary>
        /// <param name="location">The location where the data is stored.</param>
        /// <returns>The loaded data</returns>
        public string Load(string location)
        {
            // no data
            return "";
        }
    
    }
}
