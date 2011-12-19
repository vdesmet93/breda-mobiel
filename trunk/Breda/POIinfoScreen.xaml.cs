using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace View
{
    public partial class POIinfoScreen : ChildWindow
    {
        public string HostName { get; set; }

        public POIinfoScreen()
        {
            InitializeComponent();
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}