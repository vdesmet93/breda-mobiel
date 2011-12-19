﻿using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Device.Location;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Microsoft.Phone.Controls.Maps;
using View;

namespace Controller
{
    public class Controller
    {
        GeoCoordinateWatcher watcher;
        private Breda.App bredamobiel;
        private Model.FileManager fileIO;
        private Database db;
        private  bool hasLocation { get; set; }
        private  double latitude { get; set; }
        private  double longtitude { get; set; }
        public event OnLocationChanged LocationChanged;
        public delegate void OnLocationChanged(GeoCoordinate l);
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

        /// <summary>
        /// Fills the database.
        /// </summary>
        private void fillDatabase()
        {

            DatabaseTable item = new DatabaseTable { Nummer = 1, Latitude = 51.5938D, Longitude = 4.77963D, Naam = "VVV Breda" };
            db.databaseTables.InsertOnSubmit(item);

            item = new DatabaseTable { Nummer = 2, Latitude = 51.59327833333333D, Longitude = 4.779388333333333D, Naam = "Liefdeszuster" };
            db.databaseTables.InsertOnSubmit(item);

            item = new DatabaseTable { Nummer = 3, Latitude = 51.59250D, Longitude = 4.779695D, Naam = "Nassau Baronie Monument" };
            db.databaseTables.InsertOnSubmit(item);

            item = new DatabaseTable { Nummer = 4, Latitude = 51.59327833333333D, Longitude = 4.779388333333333D, Naam = "Liefdeszuster" };
            db.databaseTables.InsertOnSubmit(item);

            item = new DatabaseTable { Nummer = 5, Latitude = 51.59283333333333D, Longitude = 4.7784716666666665D, Naam = "The Light house" };
            db.databaseTables.InsertOnSubmit(item);

            item = new DatabaseTable { Nummer = 6, Latitude = 51.59061166666667D, Longitude = 4.776166666666667D, Naam = "Kasteel van Breda" };
            db.databaseTables.InsertOnSubmit(item);

            item = new DatabaseTable { Nummer = 7, Latitude = 51.589695D, Longitude = 4.776138333333334D, Naam = "Stadhouderspoort" };
            db.databaseTables.InsertOnSubmit(item);

            item = new DatabaseTable { Nummer = 8, Latitude = 51.590028333333336D, Longitude = 4.774361666666667D, Naam = "Huis van Brecht" };
            db.databaseTables.InsertOnSubmit(item);

            item = new DatabaseTable { Nummer = 9, Latitude = 51.590195D, Longitude = 4.773445D, Naam = "Spanjaardsgat" };
            db.databaseTables.InsertOnSubmit(item);

            item = new DatabaseTable { Nummer = 10, Latitude = 51.58983333333333D, Longitude = 4.773333333333333D, Naam = "Vismarkt" };
            db.databaseTables.InsertOnSubmit(item);

            item = new DatabaseTable { Nummer = 11, Latitude = 51.58936166666667D, Longitude = 4.774445D, Naam = "Havermarkt" };
            db.databaseTables.InsertOnSubmit(item);

            item = new DatabaseTable { Nummer = 12, Latitude = 51.58883333333333D, Longitude = 4.7752783333333335D, Naam = "Grote Kerk" };
            db.databaseTables.InsertOnSubmit(item);

            item = new DatabaseTable { Nummer = 13, Latitude = 51.588195D, Longitude = 4.7751383333333335D, Naam = "Het Poortje" };
            db.databaseTables.InsertOnSubmit(item);

            item = new DatabaseTable { Nummer = 14, Latitude = 51.58708333333333D, Longitude = 4.77575D, Naam = "Ridderstraat" };
            db.databaseTables.InsertOnSubmit(item);

            item = new DatabaseTable { Nummer = 15, Latitude = 51.58741666666667D, Longitude = 4.776555D, Naam = "Grote Markt" };
            db.databaseTables.InsertOnSubmit(item);

            item = new DatabaseTable { Nummer = 16, Latitude = 51.588028333333334D, Longitude = 4.7763333333333335D, Naam = "Bevrijdingsmonument" };
            db.databaseTables.InsertOnSubmit(item);

            item = new DatabaseTable { Nummer = 17, Latitude = 51.58875D, Longitude = 4.776111666666667D, Naam = "StadHuis" };
            db.databaseTables.InsertOnSubmit(item);

            item = new DatabaseTable { Nummer = 18, Latitude = 51.58763833333333D, Longitude = 4.77725D, Naam = "Antonius van Paduakerk" };
            db.databaseTables.InsertOnSubmit(item);

            item = new DatabaseTable { Nummer = 19, Latitude = 51.588D, Longitude = 4.778945D, Naam = "Bibliotheek" };
            db.databaseTables.InsertOnSubmit(item);

            item = new DatabaseTable { Nummer = 20, Latitude = 51.58772166666667D, Longitude = 4.781028333333333D, Naam = "Kloosterkazerne" };
            db.databaseTables.InsertOnSubmit(item);

            item = new DatabaseTable { Nummer = 21, Latitude = 51.58775D, Longitude = 4.782D, Naam = "Chasse theater" };
            db.databaseTables.InsertOnSubmit(item);

            item = new DatabaseTable { Nummer = 22, Latitude = 51.58775D, Longitude = 4.78125D, Naam = "Binding van Isaac" };
            db.databaseTables.InsertOnSubmit(item);

            item = new DatabaseTable { Nummer = 23, Latitude = 51.589666666666666D, Longitude = 4.781D, Naam = "Beyerd" };
            db.databaseTables.InsertOnSubmit(item);

            item = new DatabaseTable { Nummer = 24, Latitude = 51.589555D, Longitude = 4.78D, Naam = "Gasthuispoort" };
            db.databaseTables.InsertOnSubmit(item);

            item = new DatabaseTable { Nummer = 25, Latitude = 51.58911166666667D, Longitude = 4.777945D, Naam = "Willem Merkxtuin" };
            db.databaseTables.InsertOnSubmit(item);

            item = new DatabaseTable { Nummer = 26, Latitude = 51.589695D, Longitude = 4.778361666666667D, Naam = "Begijnenhof" };
            db.databaseTables.InsertOnSubmit(item);

            item = new DatabaseTable { Nummer = 27, Latitude = 51.5895D, Longitude = 4.77625D, Naam = "Einde stadswandeling" };
            db.databaseTables.InsertOnSubmit(item);


            db.SubmitChanges();

            var DB = from DatabaseTable databasetable in db.databaseTables select databasetable;
            // Execute query and place results into a collection.
            DatabaseTables = new ObservableCollection<DatabaseTable>(DB);
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
            longtitude = epl.Longitude;
            if(LocationChanged != null) LocationChanged(getLocation());
        }
        /// <summary>
        /// Calcs the specifications.
        /// </summary>
        /// <param name="tohere">The POI to g.</param>
        public void calcSpecifications(POI tohere)
        {
            
        }

        /// <summary>
        /// Changes the color of the POI after the user has passed the POI while using the application.
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
            return info.informatie;
        }

        /// <summary>
        /// Gets the map.
        /// </summary>
        /// <returns></returns>
        public Object getMap()
        {
            return null;
        }

        /// <summary>
        /// Corrects the route.
        /// </summary>
        /// <returns></returns>
        private Boolean correctRoute()
        {
            return false;
        }

        /// <summary>
        /// Detects the POI.
        /// </summary>
        private void detectPOI()
        {

        }

        /// <summary>
        /// Gets the location.
        /// </summary>
        /// <returns></returns>
        public GeoCoordinate getLocation()
        {
            GeoCoordinate l = new GeoCoordinate();
            l.Latitude = latitude;
            l.Longitude = longtitude;
            return l;

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