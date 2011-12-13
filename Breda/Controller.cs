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

        /// <summary>
        /// Initializes a new instance of the <see cref="Controller"/> class.
        /// </summary>
        /// <param name="breda">The breda.</param>
        public Controller(Breda.App breda)
        {
            
            bredamobiel = breda;
            fileIO = new Model.FileManager();

            hasLocation = false;
            watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.Default)
            {
                MovementThreshold = 2
            };

            watcher.PositionChanged += this.watcher_PositionChanged;
            watcher.StatusChanged += this.watcher_StatusChanged;
            watcher.Start();
        }

        /// <summary>
        /// Handles the StatusChanged event of the watcher control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Device.Location.GeoPositionStatusChangedEventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// Handles the PositionChanged event of the watcher control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Device.Location.GeoPositionChangedEventArgs&lt;System.Device.Location.GeoCoordinate&gt;"/> instance containing the event data.</param>
        private void watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            hasLocation = true;
            var epl = e.Position.Location;
            latitude = epl.Latitude;
            latitude = epl.Longitude;
        }
        /// <summary>
        /// Calcs the specifications.
        /// </summary>
        /// <param name="tohere">The tohere.</param>
        public void calcSpecifications(POI tohere)
        {

        }

        /// <summary>
        /// Changes the color of the POI.
        /// </summary>
        public void changePOIColor()
        {

        }

        /// <summary>
        /// Generates the route.
        /// </summary>
        /// <param name="newloc">The newloc.</param>
        public void generateRoute(POI newloc)
        {

        }

        /// <summary>
        /// Gets the help data.
        /// </summary>
        /// <returns></returns>
        public string getHelpData()
        {
            return "";
        }

        /// <summary>
        /// Gets the info.
        /// </summary>
        /// <param name="info">The info.</param>
        /// <returns></returns>
        public string getInfo(POI info)
        {
            return "";
        }

        /// <summary>
        /// Gets the map.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the file IO.
        /// </summary>
        /// <returns></returns>
        internal object getFileIO()
        {
            return fileIO;
        }
    }
}