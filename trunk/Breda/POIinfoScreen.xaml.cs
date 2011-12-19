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

        public POIinfoScreen(String info)
        {
            InitializeComponent();
            Textbox1.Text = info;
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ChildWindow_Closed(object sender, EventArgs e)
        {

        }
    }
}