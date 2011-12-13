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
    public partial class MainMenu : PhoneApplicationPage
    {
        // Constructor
        public MainMenu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new Uri("/MapView.xaml", UriKind.Relative));
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MapView.xaml", UriKind.Relative));
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/ThemeChooser.xaml", UriKind.Relative));
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            throw new ArgumentNullException("Fuck Off!");
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/HelpView.xaml", UriKind.Relative));
        }


      
    }
}