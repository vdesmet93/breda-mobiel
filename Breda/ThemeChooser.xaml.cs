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
using Model;
using Breda;

namespace View
{
    public partial class ThemeChooser : PhoneApplicationPage
    {
        public Double lengteGraad { get; private set; }
        public Double breedteGraad { get; private set; }
        public String informatie { get; private set; }
        public int nummer { get; private set; }
        public MapView mapView;
        public POI Poi;
        String Theme = "";
        /// <summary>
        /// Initializes a new instance of the <see cref="ThemeChooser"/> class.
        /// </summary>
        public MainMenu mainMenu { get; set; }
        public ThemeChooser()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Click event of the historisbutton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void historisbutton_Click(object sender, RoutedEventArgs e)
        {
            resetColor();
            SolidColorBrush sBrush = (SolidColorBrush)historisbutton.Foreground;
            sBrush.Color = Colors.Blue;
            ((App)Application.Current).themeColor = Colors.Blue;
            Theme = "historis";
        }

        /// <summary>
        /// Handles the Click event of the uitgangbutton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void uitgangbutton_Click(object sender, RoutedEventArgs e)
        {
            resetColor();
            SolidColorBrush sBrush = (SolidColorBrush)uitgangbutton.Foreground;
            sBrush.Color = Colors.Red;
            ((App)Application.Current).themeColor = Colors.Red;
            Theme = "uitgaan";
        }

        /// <summary>
        /// Handles the Click event of the Allebutton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void Allebutton_Click(object sender, RoutedEventArgs e)
        {
            resetColor();
            SolidColorBrush sBrush = (SolidColorBrush)Allebutton.Foreground;

            sBrush.Color = Colors.Cyan;
            ((App)Application.Current).themeColor = Colors.White;
            Theme = "Alle";
        }

        /// <summary>
        /// Handles the Click event of the okbutton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void okbutton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MapView.xaml", UriKind.Relative));
        }

        private void helpbutton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/HelpView.xaml", UriKind.Relative));
        }

        private void homebutton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainMenu.xaml", UriKind.Relative));
        }

        private void resetColor()
        {
            SolidColorBrush sBrush1 = (SolidColorBrush)historisbutton.Foreground;
            SolidColorBrush sBrush2 = (SolidColorBrush)uitgangbutton.Foreground;
            SolidColorBrush sBrush3 = (SolidColorBrush)Allebutton.Foreground;
            sBrush1.Color = Colors.Black;
            sBrush2.Color = Colors.Black;
            sBrush3.Color = Colors.Black;
           
        }
    }
}