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
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace View
{
    [Table]
    public class DatabaseTable : INotifyPropertyChanged, INotifyPropertyChanging
    {
        // Define ID: private field, public property and database column.
        private int _Nummer;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int Nummer
        {
            get
            {
                return _Nummer;
            }
            set
            {
                if (_Nummer != value)
                {
                    NotifyPropertyChanging("Nummer");
                    _Nummer = value;
                    NotifyPropertyChanged("Nummer");
                }
            }
        }

        private double _Latitude;

        [Column(CanBeNull = false)]
        public double Latitude
        {
            get
            {
                return _Latitude;
            }
            set
            {
                if (_Latitude != value)
                {
                    NotifyPropertyChanging("Latitude");
                    _Latitude = value;
                    NotifyPropertyChanged("Latitude");
                }
            }
        }

        private double _Longitude;

        [Column(CanBeNull = false)]
        public double Longitude
        {
            get
            {
                return _Longitude;
            }
            set
            {
                if (_Longitude != value)
                {
                    NotifyPropertyChanging("Longitude");
                    _Longitude = value;
                    NotifyPropertyChanged("Longitude");
                }
            }
        }
        // Define item name: private field, public property and database column.
        private string _Naam;

        [Column]
        public string Naam
        {
            get
            {
                return _Naam;
            }
            set
            {
                if (_Naam != value)
                {
                    NotifyPropertyChanging("Naam");
                    _Naam = value;
                    NotifyPropertyChanged("Naam");
                }
            }
        }

        // define beschrijving
        private string _Beschrijving;

        [Column]
        public string Beschrijving
        {
            get
            {
                return _Beschrijving;
            }
            set
            {
                if (_Beschrijving != value)
                {
                    NotifyPropertyChanging("Beschrijving");
                    _Beschrijving = value;
                    NotifyPropertyChanged("Beschrijving");
                }
            }
        }

        // define Uitleg
        private string _Uitleg;

        [Column]
        public string Uitleg
        {
            get
            {
                return _Uitleg;
            }
            set
            {
                if (_Uitleg != value)
                {
                    NotifyPropertyChanging("Uitleg");
                    _Uitleg = value;
                    NotifyPropertyChanged("Uitleg");
                }
            }
        }

        // define isUitgaan
        private bool _isUitgaan;

        [Column]
        public bool isUitgaan
        {
            get
            {
                return _isUitgaan;
            }
            set
            {
                if (_isUitgaan != value)
                {
                    NotifyPropertyChanging("Uitleg");
                    _isUitgaan = value;
                    NotifyPropertyChanged("Uitleg");
                }
            }
        }

       #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify the page that a data context property changed
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region INotifyPropertyChanging Members

        public event PropertyChangingEventHandler PropertyChanging;

        // Used to notify the data context that a data context property is about to change
        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion

        
    }

}
