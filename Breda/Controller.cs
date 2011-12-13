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
using System.Device.Location;

namespace Controller
{
    public class Controller
    {
        GeoCoordinateWatcher watcher;
        private Breda.App bredamobiel;
        private Model.FileManager fileIO;
        //private POI[] POIs = new POI[] { new POI(51.3, 19.2, ""), new POI(51.3, 19.2, ""), new POI(51.3, 19.2, "") };

        public bool hasLocation { get; private set; }
        public double latitude { get; private set; }
        public double longtitude { get; private set; }

        public Controller(Breda.App breda)
        {
            
            bredamobiel = breda;
            fileIO = new Model.FileManager(breda);

            hasLocation = false;
            watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.Default)
            {
                MovementThreshold = 2
            };

            watcher.PositionChanged += this.watcher_PositionChanged;
            watcher.StatusChanged += this.watcher_StatusChanged;
            watcher.Start();
        }
            
        private void watcher_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {
            switch (e.Status)
            {
                case GeoPositionStatus.Disabled:
                    // location is unsupported on this device
                    hasLocation = false;
                    break;
                case GeoPositionStatus.NoData:
                    // data unavailable
                    hasLocation = false;
                    break;
            }
        }

        private void watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            hasLocation = true;
            var epl = e.Position.Location;
            latitude = epl.Latitude;
            latitude = epl.Longitude;
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