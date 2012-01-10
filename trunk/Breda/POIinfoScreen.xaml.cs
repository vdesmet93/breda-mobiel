using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Phone.Tasks;
using System.Windows.Navigation;
using System.Windows.Media.Imaging;

namespace View
{
    public partial class POIinfoScreen : ChildWindow
    {
        private String naam;
        public string HostName { get; set; }

        public POIinfoScreen(int imageID,string naam, String info)
        {
            InitializeComponent();
            if (info == null) info = String.Empty;
            if (imageID > 0)
            {
                BitmapImage img = new BitmapImage(new Uri(String.Format("/Photos/{0}.jpg", imageID), UriKind.Relative));
                image1.Source = img;
            }
           // InitializeComponent();
            this.naam = naam;
            Textbox.Text = info;
            if(naam.Equals("NoClose"))
            {
                closeButton.Width=0;
            }
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