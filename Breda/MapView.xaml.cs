using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Controls.Maps;
using System.Device.Location;

namespace View
{
    public partial class MapView : PhoneApplicationPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MapView"/> class.
        /// </summary>
        public MapView()
        {
            InitializeComponent();
            Controller.Controller control = Breda.App.control;
            control.LocationChanged +=new Controller.Controller.OnLocationChanged(OnLocationChanged);
            map1.Center = control.getLocation();
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

        public void OnLocationChanged(GeoCoordinate l)
        {
            map1.Center = l;
            map1.ZoomLevel = 15;
            Pushpin myPushpin = new Pushpin();
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
            if(map1.Children.Count != 0)       map1.Children.RemoveAt(0);
            map1.Children.Add(myPushpin);
        }

    }
}