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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Controller
{
    public class Controller
    {
        //private POI[] POIs = new POI[] { new POI(51.3, 19.2, ""), new POI(51.3, 19.2, ""), new POI(51.3, 19.2, "") };
        GeoCoordinateWatcher watcher;
        private Breda.App bredamobiel;
        private Model.FileManager fileIO;
        private Database db;
        private bool hasLocation { get; set; }
        private double latitude { get; set; }
        private double longtitude { get; set; }
        private ObservableCollection<DatabaseTable> _DatabaseTables;
        public ObservableCollection<DatabaseTable> DatabaseTables
        {
            get
            {
                return _DatabaseTables;
            }
            set
            {
                if (_DatabaseTables != value)
                {
                    _DatabaseTables = value;
                    NotifyPropertyChanged("DatabaseTable");
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Controller"/> class.
        /// </summary>
        /// <param name="breda">The breda app.</param>
        public Controller(Breda.App breda)
        {
            _DatabaseTables = new ObservableCollection<DatabaseTable>();
            DatabaseTables = new ObservableCollection<DatabaseTable>();
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
            // Create the database if it does not exist.
            db = new Database();
            //Create the database if necessary
            if (!db.DatabaseExists())
            {
                db.CreateDatabase();
                fillDatabase();
            }
            // read data?
            var DB = from DatabaseTable databasetable in db.databaseTables select databasetable;
            // Execute query and place results into a collection.
            DatabaseTables = new ObservableCollection<DatabaseTable>(DB);
            
            String text = String.Format("{0}, {1}, {2}, {3} \n", DatabaseTables[0].Nummer, DatabaseTables[0].Longitude, DatabaseTables[0].Latitude, DatabaseTables[0].Naam);
            text += String.Format("{0}, {1}, {2}, {3} \n", DatabaseTables[1].Nummer, DatabaseTables[1].Longitude, DatabaseTables[1].Latitude, DatabaseTables[1].Naam);
            text += String.Format("{0}, {1}, {2}, {3}", DatabaseTables[2].Nummer, DatabaseTables[2].Longitude, DatabaseTables[2].Latitude, DatabaseTables[2].Naam);

        }

        private void fillDatabase()
        {

            DatabaseTable item = new DatabaseTable { Nummer = 1, Latitude = 51.5938D, Longitude = 4.77963D, Naam = "VVV Breda" };
            DatabaseTables.Add(item);
            db.databaseTables.InsertOnSubmit(item);

            item = new DatabaseTable { Nummer = 2, Latitude = 51.59307D, Longitude = 4.77969D, Naam = "Liefdeszuster" };
            DatabaseTables.Add(item);
            db.databaseTables.InsertOnSubmit(item);

            item = new DatabaseTable { Nummer = 3, Latitude = 51.59250D, Longitude = 4.77969D, Naam = "Valkenberg" };
            DatabaseTables.Add(item);
            db.databaseTables.InsertOnSubmit(item);

            db.SubmitChanges();
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

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify Silverlight that a property has changed.
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}