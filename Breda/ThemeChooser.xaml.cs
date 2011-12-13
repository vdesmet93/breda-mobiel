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
        MapView map = new MapView();
        String Theme = "";

        public ThemeChooser()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainMenu.xaml", UriKind.Relative));
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void historisbutton_Click(object sender, RoutedEventArgs e)
        {
            SolidColorBrush sBrush = (SolidColorBrush)historisbutton.Foreground;
            sBrush.Color = Colors.Blue;
        }

        private void uitgangbutton_Click(object sender, RoutedEventArgs e)
        {
            SolidColorBrush sBrush = (SolidColorBrush)historisbutton.Foreground;
            sBrush.Color = Colors.Red;;
        }

        private void okbutton_Click(object sender, RoutedEventArgs e)
        {
            if(Theme!="")
            {
                NavigationService.Navigate(new Uri("/MapView.xaml", UriKind.Relative));
            }
        }
    }
}