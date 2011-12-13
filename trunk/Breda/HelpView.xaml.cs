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
    public partial class HelpView : PhoneApplicationPage
    {
        private Model.FileManager fileIO;

        public HelpView(Model.FileManager io)
        {
            InitializeComponent();
            fileIO = io;
            showHelp();
        }

        private void showHelp()
        {

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainMenu.xaml", UriKind.Relative));
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}