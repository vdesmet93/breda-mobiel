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
using System.Threading;

namespace View
{
    public partial class MainMenu : PhoneApplicationPage
    {
        // Constructor
        private Breda.App bredamobiel;
        MapView map;
        HelpView help;
        ThemeChooser theme;

        public MainMenu(Breda.App breda)
        {
            bredamobiel = breda;
            theme = new ThemeChooser(breda);
            map = new MapView(breda);
            help = new HelpView(breda.getControl().getFileIO());
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
            
            //Thread.CurrentThread.Abort();
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/HelpView.xaml", UriKind.Relative));
        }


      
    }
}