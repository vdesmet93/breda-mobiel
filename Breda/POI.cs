﻿using System;
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
        public String informatie { get; private set; }
        public int nummer { get; private set; }
        public Pushpin pushpin { get; private set; }
        public MapView m { get; private set; }
        /// <summary>Initializes a new instance of the <see cref="POI"/> class.</summary>
        /// <param name="l">The longtitude.</param>
        /// <param name="b">The latitude.</param>
        /// <param name="i">The exra information.</param>
        /// <param name="n">The number.</param>
        
        public POI(GeoCoordinate g, MapView m, string info, int nummer)
        {
            this.m = m;
            this.nummer = nummer;
            intializePushpin(g);
            informatie = info;
            this.nummer = nummer;
        }

        private void intializePushpin(GeoCoordinate g)
        {
            pushpin = new Pushpin();
            pushpin.Location = g;
            pushpin.Template = null;
            Color a;
            if(m.themeColor == Colors.White)
            {
                a = Colors.Black;
            }
            else
            {
                a = m.themeColor;
            }

            pushpin.Content = new Ellipse()
            {
                Fill = new SolidColorBrush(a),
                Stroke = new SolidColorBrush(a),
                StrokeThickness = 5,
                Opacity = .8,
                Height = 20,
                Width = 20
            };
            pushpin.MouseLeftButtonUp += pushpinClickedEvent;
        }

        public void pushpinClickedEvent(object sender, MouseButtonEventArgs e)
        {
            POIinfoScreen wnd = new POIinfoScreen(informatie);
            m.map1.Center = ((Pushpin) sender).Location;
            if (m.map1.ZoomLevel < 16)
            {
                m.map1.ZoomLevel = 16;
            };
            wnd.Show();
        }
    }
}