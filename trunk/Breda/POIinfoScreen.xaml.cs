using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Phone.Tasks;
using System.Windows.Navigation;

namespace View
{
    public partial class POIinfoScreen : ChildWindow
    {
        private String naam;
        public string HostName { get; set; }

        public POIinfoScreen(string naam, String info)
        {
            if (info == null) info = String.Empty;
            InitializeComponent();
            this.naam = naam;
            Textbox.Text = info;
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            //this.Visibility = Visibility.Collapsed;
            this.Close();
        }

        private void ChildWindow_Closed(object sender, EventArgs e)
        {

        }

        private void Meerbutton_Click(object sender, RoutedEventArgs e)
        {
            WebBrowserTask task = new WebBrowserTask();
            task.URL = "http://lmbtfy.com/?q=" + naam;
            task.Show();
            
        }
    }
}