using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Controls.Maps;
//using System.Device.Location;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Controls.Maps.Platform;
using System.Device.Location;
using View.RouteService;
using View.GeocodeService;
using Breda;

namespace View
{
    public partial class MapView : PhoneApplicationPage
    {
        internal GeocodeService.GeocodeResult[] geocodeResults;
        private Pushpin myPushpin;
        public Color themeColor = ((App)Application.Current).themeColor;

        /// <summary>
        /// Initializes a new instance of the <see cref="MapView"/> class.
        /// </summary>
        public MapView()
        {
            InitializeComponent();
            Controller.Controller control = Breda.App.control;
            control.LocationChanged +=new Controller.Controller.OnLocationChanged(OnLocationChanged);
            map1.Center = control.getLocation();

            foreach (DatabaseTable row in control.DatabaseTables)
            {
                GeoCoordinate geo = new GeoCoordinate() 
                        { Latitude = row.Latitude, Longitude = row.Longitude };

                addWaypoint(geo, row.Beschrijving);
            }
            geocodeResults = new GeocodeService.GeocodeResult[control.getRowCount()];
             
            //Geocode("Seattle, WA" , 0);
            //Geocode("Redmond, WA" , 1);
            
            
            
            
            GeocodeResultToWaypoint(control.getWayPoints());
        }
        /**
        // This method accepts a geocode query string as well as a ‘waypoint index’, which will be used to track each asynchronous geocode request.
        private void Geocode(string strAddress, int waypointIndex)
        {
            // Create the service variable and set the callback method using the GeocodeCompleted property.
            GeocodeService.GeocodeServiceClient geocodeService = new GeocodeService.GeocodeServiceClient("BasicHttpBinding_IGeocodeService");
            geocodeService.GeocodeCompleted += new EventHandler<GeocodeService.GeocodeCompletedEventArgs>(geocodeService_GeocodeCompleted);

            // Set the credentials and the geocode query, which could be an address or location.
            GeocodeService.GeocodeRequest geocodeRequest = new GeocodeService.GeocodeRequest();
            geocodeRequest.Credentials = new Credentials();
            geocodeRequest.Credentials.ApplicationId = ((ApplicationIdCredentialsProvider)map1.CredentialsProvider).ApplicationId;
            geocodeRequest.Query = strAddress;

            // Make the asynchronous Geocode request, using the ‘waypoint index’ as 
            //   the user state to track this request and allow it to be identified when the response is returned.
            geocodeService.GeocodeAsync(geocodeRequest, waypointIndex);
        }

        private void geocodeService_GeocodeCompleted(object sender, GeocodeService.GeocodeCompletedEventArgs e)
        {
            // Retrieve the user state of this response (the ‘waypoint index’) to identify which geocode request 
            //   it corresponds to.
            int waypointIndex = System.Convert.ToInt32(e.UserState);

            // Retrieve the GeocodeResult for this response and store it in the global variable geocodeResults, using
            //   the waypoint index to position it in the array.
            geocodeResults[waypointIndex] = e.Result.Results[0];

            // Look at each element in the global gecodeResults array to figure out if more geocode responses still 
            //   need to be returned.

            bool doneGeocoding = true;

            foreach (GeocodeService.GeocodeResult gr in geocodeResults)
            {
                if (gr == null)
                {
                    doneGeocoding = false;
                }
            }

            // If the geocodeResults array is totally filled, then calculate the route.
            if (doneGeocoding)
                CalculateRoute(geocodeResults);

        }

        private void CalculateRoute(GeocodeService.GeocodeResult[] results)
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

            // Set the waypoints of the route to be calculated using the Geocode Service results stored in the geocodeResults variable.
            routeRequest.Waypoints = new System.Collections.ObjectModel.ObservableCollection<RouteService.Waypoint>();
            foreach (GeocodeService.GeocodeResult result in results)
            {
           /**     var  e =new RouteService.Waypoint();
                RouteService.Cala
                routeRequest.Waypoints.Add(GeocodeResultToWaypoint(result));
            }

            // Make the CalculateRoute asnychronous request.
            routeService.CalculateRouteAsync(routeRequest);
        }**/

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
        /// shows the POICanvas
        /// </summary>
        public void showPOICanvas()
        {
            POICanvas.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// hides the POICanvas
        /// </summary>
        public void hidePOICanvas()
        {
            POICanvas.Visibility = Visibility.Collapsed;
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
        /// POs the i_ tap.
        /// </summary>
        private void POI_Tap()
        {
 
        }

        /// <summary>
        /// Called when [location changed].
        /// </summary>
        /// <param name="l">The l.</param>
        public void OnLocationChanged(GeoCoordinate l)
        {
            map1.Center = l;
            map1.ZoomLevel = 17;
            if (myPushpin != null) map1.Children.Remove(myPushpin);
            myPushpin = new Pushpin();
            myPushpin.Template = null;
            myPushpin.Content = new Ellipse()
            {
                Fill = new SolidColorBrush(Colors.Blue),
                StrokeThickness =0,
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
        public void addWaypoint(GeoCoordinate g, string info)
        {
            bool a = false;
            if (a)
            {
                Image image = new Image();
                image.Source = new BitmapImage(
                new Uri("red-dot.png", UriKind.Relative));
                MapLayer mylayer = new MapLayer();
                mylayer.AddChild(image, new LocationRect()
                {
                    Northeast = new GeoCoordinate(g.Latitude + 0.0005, g.Longitude + 0.00025),
                    Southwest = new GeoCoordinate(g.Latitude, g.Longitude - 0.00025)
                });
                map1.Children.Add(mylayer);
            }
            else
            {
                POI poi = new POI(g, this, info);
                map1.Children.Add(poi.pushpin);
            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            POIinfoScreen wnd = new POIinfoScreen("started from button");
            wnd.Show();
        }

        private void sluitenboutton_Click(object sender, RoutedEventArgs e)
        {
            hidePOICanvas();
        }

        private void meerbutton_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}