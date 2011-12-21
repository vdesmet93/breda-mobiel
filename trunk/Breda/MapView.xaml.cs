using System;
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

namespace View
{
    public partial class MapView : PhoneApplicationPage
    {
        internal GeocodeResult[] geocodeResults;
        private Pushpin myPushpin;
        public Color themeColor = ((App)Application.Current).themeColor;
        public List<POI> Route;

        /// <summary>
        /// Initializes a new instance of the <see cref="MapView"/> class.
        /// </summary>
        public MapView()
        {
            Route = new List<POI>();
            InitializeComponent();
            Controller.Controller control = Breda.App.control;
            control.LocationChanged +=new Controller.Controller.OnLocationChanged(OnLocationChanged);
            map1.Center = control.getLocation();

            foreach (DatabaseTable row in control.DatabaseTables)
            {
                GeoCoordinate geo = new GeoCoordinate() 
                        { Latitude = row.Latitude, Longitude = row.Longitude };

                addWaypoint(geo, row.Naam, row.isUitgaan, row.Beschrijving, row.Nummer);
            }
            geocodeResults = new GeocodeService.GeocodeResult[control.getRowCount()];
            
            
            GeocodeResultToWaypoint(control.getWayPoints());
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
                routeLine.Opacity = 0.65;
                routeLine.StrokeThickness = 5.0;

                // Retrieve the route points that define the shape of the route.
                foreach (Location p in e.Result.Result.RoutePath.Points)
                {
                    routeLine.Locations.Add(new GeoCoordinate(p.Latitude, p.Longitude));
                }

                // Add a map layer in which to draw the route.
                MapLayer myRouteLayer = new MapLayer();
                map1.Children.Add(myRouteLayer);

                // Add the route line to the new layer.
                myRouteLayer.Children.Add(routeLine);

                // Figure the rectangle which encompasses the route. This is used later to set the map view.

                LocationRect rect = new LocationRect(new GeoCoordinate(routeLine.Locations[0].Latitude, routeLine.Locations[0].Longitude), 10, 10);

          

                // Set the map view using the rectangle which bounds the rendered route.
               map1.SetView(rect);
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
            zoomOnLocation(l);
            

        }




        /// <summary>
        /// zooms your map to your current locatien and adds a pushpin at your location.
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
                Fill = new SolidColorBrush(Colors.Blue),
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
        public void addWaypoint(GeoCoordinate g, string naam, bool isUitgaan, string info, int nummer)
        {
            POI poi = new POI(g, this, naam, isUitgaan, info, nummer);
            map1.Children.Add(poi.pushpin);
            Route.Add(poi);
        }
    }
}