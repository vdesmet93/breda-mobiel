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

namespace View
{
    public partial class ThemeChooser : PhoneApplicationPage
    {
        String Theme = "";

        /// <summary>
        /// Initializes a new instance of the <see cref="ThemeChooser"/> class.
        /// </summary>
        public ThemeChooser()
        {
            InitializeComponent();
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
        /// Handles the Click event of the historisbutton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void historisbutton_Click(object sender, RoutedEventArgs e)
        {
            SolidColorBrush sBrush = (SolidColorBrush)historisbutton.Foreground;
            sBrush.Color = Colors.Blue;
            Theme = "historis";
        }

        /// <summary>
        /// Handles the Click event of the uitgangbutton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void uitgangbutton_Click(object sender, RoutedEventArgs e)
        {
            SolidColorBrush sBrush = (SolidColorBrush)historisbutton.Foreground;
            sBrush.Color = Colors.Red;
            Theme = "uitgang";
        }

        /// <summary>
        /// Handles the Click event of the Allebutton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void Allebutton_Click(object sender, RoutedEventArgs e)
        {
            SolidColorBrush sBrush = (SolidColorBrush)historisbutton.Foreground;
            sBrush.Color = Colors.White;
            Theme = "Alle";
        }

        /// <summary>
        /// Handles the Click event of the okbutton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void okbutton_Click(object sender, RoutedEventArgs e)
        {
            if(Theme!="")
            {
                NavigationService.Navigate(new Uri("/MapView.xaml", UriKind.Relative));
            }
        }
    }
}