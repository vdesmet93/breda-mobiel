﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Controls.Maps;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Controls.Maps.Platform;
using System.Device.Location;
using View.RouteService;
using View.GeocodeService;
using Breda;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Threading;

namespace View
{
    public partial class MapView : PhoneApplicationPage
    {
        internal GeocodeResult[] geocodeResults;
        private Pushpin myPushpin;
        public bool fromto = false;
        private Controller.Controller control;
        public Color themeColor = ((App)Application.Current).themeColor;
        public List<POI> Route;
        public Boolean GotLocation = false;
        POIinfoScreen wnd = new POIinfoScreen(0, "NoClose", "Er is nog geen GPS-fix gevonden. De applicatie is druk bezig om een nauwkeurige locatie te krijgen.\n even geduld A.U.B.");

        /// <summary>
        /// Initializes a new instance of the <see cref="MapView"/> class.
        /// </summary>
        public MapView()
        {
            Route = new List<POI>();
            InitializeComponent();
            control = Breda.App.control;
            control.LocationChanged +=new Controller.Controller.OnLocationChanged(OnLocationChanged);
            map1.Center = control.getLocation();
            map1.ZoomLevel = 20;

            foreach (DatabaseTable row in control.DatabaseTables)
            {
                GeoCoordinate geo = new GeoCoordinate() 
                        { Latitude = row.Latitude, Longitude = row.Longitude };
                addWaypoint(geo, row.Naam, row.isUitgaan, row.Uitleg, row.Nummer,row.Foto);
            }
            geocodeResults = new GeocodeService.GeocodeResult[control.getRowCount()];
            GeocodeResultToWaypoint(control.getWayPoints());
            wnd.Show();
            
        }

        public static class UIThread
        {
            private static readonly Dispatcher Dispatcher;

            static UIThread()
            {
                // Store a reference to the current Dispatcher once per application
                Dispatcher = Deployment.Current.Dispatcher;
            }

            /// <summary>
            ///   Invokes the given action on the UI thread - if the current thread is the UI thread this will just invoke the action directly on
            ///   the current thread so it can be safely called without the calling method being aware of which thread it is on.
            /// </summary>
            public static void Invoke(Action action)
            {
                if (Dispatcher.CheckAccess())
                    action.Invoke();
                else
                    Dispatcher.BeginInvoke(action);
            }
        }

        private RouteService.Waypoint GeocodeResultToWaypoint(Location[] result)
        {
            // Create the service variable and set the callback method using the CalculateRouteCompleted property.
            RouteService.RouteServiceClient routeService = new RouteService.RouteServiceClient("BasicHttpBinding_IRouteService");
            routeService.CalculateRouteCompleted += new EventHandler<RouteService.CalculateRouteCompletedEventArgs>(routeService_CalculateRouteCompleted);

            // Set the token.
            RouteService.RouteRequest routeRequest = new RouteService.RouteRequest();
            routeRequest.Credentials = new Credentials();
            routeRequest.Credentials.ApplicationId = ((ApplicationIdCredentialsProvider)map1.CredentialsProvider).ApplicationId;

            // Return the route points so the route can be drawn.
            routeRequest.Options = new RouteService.RouteOptions();
            routeRequest.Options.RoutePathType = RouteService.RoutePathType.Points;
            routeRequest.Waypoints = new System.Collections.ObjectModel.ObservableCollection<RouteService.Waypoint>();
            routeRequest.Options.Mode = TravelMode.Walking;
            int i = 0;
            foreach (Location l in result)
            {
                Waypoint point = new RouteService.Waypoint();
                point.Location = l;
                routeRequest.Waypoints.Add(point);
                geocodeResults[i] = new GeocodeResult();
                View.GeocodeService.GeocodeLocation loc = new View.GeocodeService.GeocodeLocation();
                loc.Latitude = l.Latitude;
                loc.Longitude = l.Longitude;
                geocodeResults[i].Locations = new System.Collections.ObjectModel.ObservableCollection<GeocodeService.GeocodeLocation>();
                geocodeResults[i].Locations.Add(loc);
                i++;
            }
            routeService.CalculateRouteAsync(routeRequest);
            return null;
        }

        private void routeService_CalculateRouteCompleted(object sender, RouteService.CalculateRouteCompletedEventArgs e)
        {
            // If the route calculate was a success and contains a route, then draw the route on the map.
            if ((e.Result.ResponseSummary.StatusCode == RouteService.ResponseStatusCode.Success) & (e.Result.Result.Legs.Count != 0))
            {
                // Set properties of the route line you want to draw.
                Color routeColor = Colors.Blue;
                SolidColorBrush routeBrush = new SolidColorBrush(routeColor);
                MapPolyline routeLine = new MapPolyline();
                routeLine.Locations = new LocationCollection();
                routeLine.Stroke = routeBrush;
                if (fromto)
                {
                    routeLine.StrokeThickness = 10.0;
                    routeLine.Opacity = 1.0;
                    map1.Children.RemoveAt(map1.Children.Count - 2);
                    fromto = false;
                }
                else
                {
                    routeLine.StrokeThickness = 5.0;
                    routeLine.Opacity = 0.65;
                }
                // Retrieve the route points that defines the shape of the route.
                foreach (Location p in e.Result.Result.RoutePath.Points)
                {
                    routeLine.Locations.Add(new GeoCoordinate(p.Latitude, p.Longitude));
                }
                // Add a map layer in which to draw the route.
                MapLayer myRouteLayer = new MapLayer();
                map1.Children.Add(myRouteLayer);
                // Add the route line to the new layer.
                myRouteLayer.Children.Add(routeLine);
            }
        }

        /// <summary>
        /// Handles the Click event of the button1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainMenu.xaml", UriKind.Relative));
        }

        /// <summary>
        /// Handles the Click event of the button2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        /// <summary>
        /// Called when [location changed].
        /// </summary>
        /// <param name="l">your current location</param>
        public void OnLocationChanged(GeoCoordinate l)
        {
            GotLocation = true;
            wnd.Close();
            zoomOnLocation(l);
            HandleRouteChange(l);
        }

        /// <summary>
        /// recalculates route and checks if POI is reached.
        /// </summary>
        /// <param name="l">current location</param>
        private void HandleRouteChange(GeoCoordinate l)
        {
            POI poiToUse = null;

            for (int i = 0; i < Route.Count; )
            {
                if (!Route[i].isBezocht)
                {
                    poiToUse = Route[i];
                    break;
                }
                i++;
            }

            if (poiToUse == null)
            {
                //route is done
            }
            else
            {
                if (poiToUse.getDistance(l) < 20)
                {
                    //doel berrijkt
                    poiToUse.isBezocht = true;
                    poiToUse.showInfoScreen();
                }
                else
                {
                    fromto = true;
                    GeocodeResultToWaypoint(control.getRouteToNextPOI(poiToUse));
                }
            }
        }

        /// <summary>
        /// zooms your map to your current location and adds a pushpin at your location.
        /// </summary>
        /// <param name="l">current location</param>
        public void zoomOnLocation(GeoCoordinate l)
        {
            map1.Center = l;
            map1.ZoomLevel = 17;
            if (myPushpin != null) map1.Children.Remove(myPushpin);
            myPushpin = new Pushpin();
            myPushpin.Template = null;
            myPushpin.Content = new Ellipse()
            {
                Fill = new SolidColorBrush(Colors.Red),
                StrokeThickness = 0,
                Opacity = .8,
                Height = 25,
                Width = 25
            };
            myPushpin.Location = l;
            map1.Children.Add(myPushpin);
        }

        /// <summary>
        /// Adds the waypoint.
        /// </summary>
        /// <param name="g">The g.</param>
        public void addWaypoint(GeoCoordinate g, string naam, bool isUitgaan, string info, int nummer,int foto)
        {
            POI poi = new POI(g, this, naam, isUitgaan, info, nummer,foto);
            map1.Children.Add(poi.pushpin);
            Route.Add(poi);
        }
    }
}