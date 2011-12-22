using System;
using System.Device.Location;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.Phone.Controls.Maps;
using System.Windows.Input;
using System.Diagnostics;

namespace View
{
    /// <summary>The POI class represents a POI where it holds the location, the infomation about it and what number it is in route.</summary>
    public class POI
    {
        public int foto { get; private set; }
        private bool isUitgaan;
        public bool isBezocht{ get; set; }
        private string naam;
        public String informatie { get; private set; }
        public int nummer { get; private set; }
        public Pushpin pushpin { get; private set; }
        public MapView m { get; private set; }
        /// <summary>Initializes a new instance of the <see cref="POI"/> class.</summary>
        /// <param name="l">The longtitude.</param>
        /// <param name="b">The latitude.</param>
        /// <param name="i">The exra information.</param>
        /// <param name="n">The number.</param>
        
        public POI(GeoCoordinate g, MapView m, string naam, bool isUitgaan, string info, int nummer,int foto)
        {
            isBezocht = false;
            this.m = m;
            this.nummer = nummer;
            this.naam = naam;
            this.isUitgaan = isUitgaan;
            this.foto = foto;
            intializePushpin(g);
            informatie = info ?? "";
            if (String.IsNullOrEmpty(informatie))
            { informatie = naam + "\n \n" + "geen informatie beschikbaar, klik op meer voor informatie op het internet"; }
            else
            { informatie = naam + "\n \n" + informatie; }
            this.nummer = nummer;
            Debug.WriteLine(nummer);
        }

        private void intializePushpin(GeoCoordinate g)
        {
            pushpin = new Pushpin();
            pushpin.Location = g;
            pushpin.Template = null;
            Color a;
            Color b;
            if(m.themeColor == Colors.White)
            {
                a = Colors.Cyan;
            }
            else
            {
                if (m.themeColor == Colors.Red && isUitgaan)
                { a = m.themeColor;}
                else if (m.themeColor == Colors.Blue && !isUitgaan)
                { a = m.themeColor;  }
                else
                { a = Color.FromArgb(250, 150, 150, 150); }   
            }
            b = a;
            if (isBezocht)
            {
                b.R -= 60; b.G -= 60; b.B -= 60;
            }

            pushpin.Content = new Ellipse()
            {
                Fill = new SolidColorBrush(a),
                Stroke = new SolidColorBrush(b),
                StrokeThickness = 5,
                Opacity = .8,
                Height = 20,
                Width = 20
            };
            pushpin.MouseLeftButtonUp += pushpinClickedEvent;
        }

        public void showInfoScreen()
        {
            POIinfoScreen wnd = new POIinfoScreen( this.foto,this.naam, this.informatie);
            wnd.Show();
        }

        public void pushpinClickedEvent(object sender, MouseButtonEventArgs e)
        {
            showInfoScreen();
            m.map1.Center = ((Pushpin) sender).Location;
            if (m.map1.ZoomLevel < 16)
            {
                m.map1.ZoomLevel = 16;
            };
        }

        public Double getDistance(GeoCoordinate g)
        {
            return pushpin.Location.GetDistanceTo(g);
        }
    }
}
