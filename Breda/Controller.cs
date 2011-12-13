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
using View;

namespace Controller
{
    public class Controller
    {
        private Breda.App bredamobiel;
        private Model.FileManager fileIO;

        public Controller(Breda.App breda)
        {
            bredamobiel = breda;
            fileIO = new Model.FileManager();
        }

        public void calcSpecifications(POI tohere)
        {

        }

        public void changePOIColor()
        {

        }

        public void generateRoute(POI newloc)
        {

        }

        public string getHelpData()
        {
            return "";
        }

        public string getInfo(POI info)
        {
            return "";
        }

        public Object getMap()
        {
            return null;
        }

        private Boolean correctRoute()
        {
            return false;
        }

        private void detectPOI()
        {

        }
    }
}
