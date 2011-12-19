using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Controls.Maps;
using System.Device.Location;
using System.Windows.Media.Imaging;

namespace View
{
    public partial class MapView : PhoneApplicationPage
    {
        private Pushpin myPushpin;
        private int id;
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            
            string ids = "";
            if (NavigationContext.QueryString.TryGetValue("canvas", out ids))
            {
               
                if (Int32.TryParse("canvas", out id))
                {
                 showPOICanvas();
                }
                else
                {
                    // als id niet bestaat hier fout melden.
                  showPOICanvas();
                }
            }
            else
            {
                hidePOICanvas(); 
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="MapView"/> class.
        /// </summary>
        public MapView()
        {
            InitializeComponent();
            Controller.Controller control = Breda.App.control;
            control.LocationChanged +=new Controller.Controller.OnLocationChanged(OnLocationChanged);
            map1.Center = control.getLocation();

            foreach (DatabaseTable row in control.DatabaseTables)
            {
                GeoCoordinate geo = new GeoCoordinate() 
                        { Latitude = row.Latitude, Longitude = row.Longitude };

                addWaypoint(geo);
            }
        }

        /// <summary>
        /// shows the POICanvas
        /// </summary>
        public void showPOICanvas()
        {
            POICanvas.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// hides the POICanvas
        /// </summary>
        public void hidePOICanvas()
        {
            POICanvas.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Handles the Click event of the button1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainMenu.xaml", UriKind.Relative));
        }

        /// <summary>
        /// Handles the Click event of the button2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        /// <summary>
        /// POs the i_ tap.
        /// </summary>
        private void POI_Tap()
        {
 
        }

        /// <summary>
        /// Called when [location changed].
        /// </summary>
        /// <param name="l">The l.</param>
        public void OnLocationChanged(GeoCoordinate l)
        {
            map1.Center = l;
            map1.ZoomLevel = 17;
            if (myPushpin != null) map1.Children.Remove(myPushpin);
            myPushpin = new Pushpin();
            myPushpin.Template = null;
            myPushpin.Content = new Ellipse()
            {
                Fill = new SolidColorBrush(Colors.Blue),
                StrokeThickness =0,
                Opacity = .8,
                Height = 25,
                Width = 25
            };
            myPushpin.Location = l;
            
            map1.Children.Add(myPushpin);
            
        }


        /// <summary>
        /// Adds the waypoint.
        /// </summary>
        /// <param name="g">The g.</param>
        public void addWaypoint(GeoCoordinate g)
        {
            bool a = false;
            if (a)
            {
                Image image = new Image();
                image.Source = new BitmapImage(
                new Uri("red-dot.png", UriKind.Relative));
                MapLayer mylayer = new MapLayer();
                mylayer.AddChild(image, new LocationRect()
                {
                    Northeast = new GeoCoordinate(g.Latitude + 0.0005, g.Longitude + 0.00025),
                    Southwest = new GeoCoordinate(g.Latitude, g.Longitude - 0.00025)
                });
                map1.Children.Add(mylayer);
            }
            else
            {
                POI poi = new POI(g, this);
                map1.Children.Add(poi.pushpin);
            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            POIinfoScreen wnd = new POIinfoScreen("started from button");
            wnd.Show();
        }

        private void sluitenboutton_Click(object sender, RoutedEventArgs e)
        {
            hidePOICanvas();
        }

        private void meerbutton_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}