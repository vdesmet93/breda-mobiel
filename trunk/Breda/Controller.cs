using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Device.Location;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Microsoft.Phone.Controls.Maps;
using View;
using Microsoft.Phone.Controls.Maps.Platform;

namespace Controller
{
    public class Controller
    {
        GeoCoordinateWatcher watcher;
        private Breda.App bredamobiel;
        private Model.FileManager fileIO;
        private Database db;
        private bool hasLocation { get; set; }
        private double latitude { get; set; }
        private double longtitude { get; set; }
        public event OnLocationChanged LocationChanged;
        private ObservableCollection<DatabaseTable> _DatabaseTables;
        /// <summary>This delegate is called when the location has changed.</summary>
        /// <param name="l">The changed lcoation.</param>
        public delegate void OnLocationChanged(GeoCoordinate l);
        /// <summary>Gets or sets the database tables.</summary>
        /// <value>The database tables.</value>
        public ObservableCollection<DatabaseTable> DatabaseTables
        {
            get
            {
                return _DatabaseTables;
            }
            set
            {
                if (_DatabaseTables != value)
                {
                    _DatabaseTables = value;
                    NotifyPropertyChanged("DatabaseTable");
                }
            }
        }

        /// <summary>Initializes a new instance of the <see cref="Controller"/> class.</summary>
        /// <remarks>The constructor intializes the database, GPS and the FileManager used for FileIO</remarks>
        /// <param name="breda">The breda app.</param>
        public Controller(Breda.App breda)
        {
            _DatabaseTables = new ObservableCollection<DatabaseTable>();
            DatabaseTables = new ObservableCollection<DatabaseTable>();
            bredamobiel = breda;
            fileIO = new Model.FileManager();

            hasLocation = false;
            watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.Default)
            {
                MovementThreshold = 2
            };
            watcher.PositionChanged += this.watcher_PositionChanged;
            watcher.StatusChanged += this.watcher_StatusChanged;
            watcher.Start();
            // Create the database if it does not exist.
            db = new Database();
            //Create the database if necessary
            if (!db.DatabaseExists())
            {
                db.CreateDatabase();
                fillDatabase();
            }
            // read data?
            var DB = from DatabaseTable databasetable in db.databaseTables select databasetable;
            // Execute query and place results into a collection.
            DatabaseTables = new ObservableCollection<DatabaseTable>(DB);
            String text = String.Format("{0}, {1}, {2}, {3} \n", DatabaseTables[0].Nummer, DatabaseTables[0].Longitude, DatabaseTables[0].Latitude, DatabaseTables[0].Naam);
            text += String.Format("{0}, {1}, {2}, {3} \n", DatabaseTables[1].Nummer, DatabaseTables[1].Longitude, DatabaseTables[1].Latitude, DatabaseTables[1].Naam);
            text += String.Format("{0}, {1}, {2}, {3}", DatabaseTables[2].Nummer, DatabaseTables[2].Longitude, DatabaseTables[2].Latitude, DatabaseTables[2].Naam);
        }

        /// <summary>Fills the database with all the POI's used in the application.</summary>
        private void fillDatabase()
        {
            DatabaseTable item = new DatabaseTable { Nummer = 1, Latitude = 51.5938D, Longitude = 4.77963D, Naam = "VVV Breda" , isUitgaan = false};
      //      db.databaseTables.InsertOnSubmit(item);
       //     item = new DatabaseTable { Nummer = 2, Latitude = 51.59327833333333D, Longitude = 4.779388333333333D, Naam = "Liefdeszuster" };
       //     db.databaseTables.InsertOnSubmit(item);
            item = new DatabaseTable { Nummer = 3, Latitude = 51.59250D, Longitude = 4.779695D, Naam = "Nassau Baronie Monument", Uitleg = "Bij de ingang van het stadspark, het Valkenberg, staat een monument dat u iets vertelt over de wordings-geschiedenis van de stad Breda, maar vooral over de Heren van de stad uit het Huis van Nassau en de 500-jarige band tussen Breda en het Huis van Nassau.Op 3 juli 1905 werd het Nassau-Baronie-monument, zoals het officieel heet, met veel feestelijk vertoon door Koningin Wilhelmina onthuld. Het beeld herinnert aan het feit, dat in 1404 Graaf Engelbert, de eerste Bredase Nassau en zijn gemalin, Johanna van Polanen, werden ingehuldigd als Heer en Vrouwe van Breda. De ontwerper is de welbekende dr. P.J.H. Cuypers, die o.m. het Rijksmuseum en het Centraal Station in Amsterdam ontwierp. Op dit monument zijn niet alleen de wapenschilden van twintig gemeenten in en rond de Baronie aangebracht maar ook de Leeuw van Nassau die boven alles uittorent met koningskroon, zwaard en wapenschild. In de drie reliëfs is de 'blijde incomste' van Graaf Engelbert en zijn gemalin afgebeeld. De poorters bieden de sleutel van de stad aan." , isUitgaan = false };
            db.databaseTables.InsertOnSubmit(item);
            item = new DatabaseTable { Nummer = 4, Latitude = 51.59327833333333D, Longitude = 4.779388333333333D, Naam = "Liefdeszuster" };
            db.databaseTables.InsertOnSubmit(item);
            item = new DatabaseTable { Nummer = 5, Latitude = 51.59283333333333D, Longitude = 4.7784716666666665D, Naam = "The Light house" , isUitgaan = false };
            db.databaseTables.InsertOnSubmit(item);
            item = new DatabaseTable { Nummer = 6, Latitude = 51.59061166666667D, Longitude = 4.776166666666667D, Naam = "Kasteel van Breda", Uitleg = "Dit plein bevindt zich aan de zuiderzijde van het kasteel. U heeft zo een mooi uitzicht op de monumentale poort, de zgn. Stadhouderspoort met het wapen van stadhouder Willem de Vijfde, dat overigens pas later is aangebracht. Achter u ziet u het ruiterstandbeeld van stadhouder koning Willem III geplaatst. Voor dit standbeeld werd onder de burgerij van Breda een inzameling gehouden die f 47.000,-- opbracht. Voor de oude Nassaustad Breda is de stadhouderkoning van grote betekenis geweest. Hij voltooide na anderhalve eeuw de verbouwing van het Kasteel. Hendrik de Derde van Nassau en zijn (drede) vrouw Mencia de Mendoza hebben veel in Breda vertoefd, maar verbleven dan toch bij voorkeur in de vertrekken boven de overigens al lang geleden gesloopte watermolen op het terrein van het Kasteel. Rechts van de poort bevindt zich het zgn. Blokhuis, de ambtswoning van de gouveneur van de KMA. Willem van Oranje woonde daar in een aangrenzende ruimte, maar Prins Maurits prefereerde de watermolen als dagelijks verblijf. Prins Philips Willem (1554-1618), de oudste en roomskatholieke zoon van Willem van Oranje, die vele jaren in Spaanse ballingschap verbleef en in Diest werd begraven, is de eerste Oranje geweest, die met zijn vrouw ook in het Kasteel ging wonen. Hij liet het park Valkenberg verfraaien en het Kasteel echt als een paleis inrichten. Bij zijn dood in 1618 werden in Breda tweeënveertig dagen achtereen de kerkklokken geluid. Op de plaats van de vensters, links en rechts van de poort, bevonden zich in de zestiende en zeventiende eeuw sierlijke open galerijen. Het muisgrijze gebouw links van de kasteelpoort werd gebouwd in 1867. Het maakt thans ook deel uit van het KMA gebouwen complex. Meer geschiedenis schuilt et achter de witte pui van het gebouw" , isUitgaan = false};
            db.databaseTables.InsertOnSubmit(item);
            item = new DatabaseTable { Nummer = 7, Latitude = 51.589695D, Longitude = 4.776138333333334D, Naam = "Stadhouderspoort", isUitgaan = false };
            db.databaseTables.InsertOnSubmit(item);
            item = new DatabaseTable { Nummer = 8, Latitude = 51.590028333333336D, Longitude = 4.774361666666667D, Naam = "Huis van Brecht" };
            db.databaseTables.InsertOnSubmit(item);
            item = new DatabaseTable { Nummer = 9, Latitude = 51.590195D, Longitude = 4.773445D, Naam = "Spanjaardsgat" , isUitgaan = false };
            db.databaseTables.InsertOnSubmit(item);
            item = new DatabaseTable { Nummer = 10, Latitude = 51.58983333333333D, Longitude = 4.773333333333333D, Naam = "Vismarkt", Uitleg = "Van de plaats waar de vishal (1725) nog aangeeft dat hier vroeger een levendige handel in zeevis werd bedreven, wandelen we door de Vismarktstraat naar de Havermarkt. Deze markt is vanaf de jaren zestig tot op de dag van vandaag het uitgaanscentrum van Breda. De naam Havermarkt ten spijt moet worden vastgesteld, dat de granen niet hier; maar op de Grote Markt werden verhandeld. Wel werden hier door de boeren uit de omtrek groenten, eieren en boter aangevoerd en verkocht. In de zeventiende eeuw werd hier ook een leermarkt gehouden. De oude benamingen van dit pleintje luidden Groenmarkt, Botermarkt en Korenmarkt hetgeen de vroegere functies iets beter aanduidt. Rond 1490 werd dit aangelegd, als een verbreding van de Visserstraat. Wie op dit verrukkelijke pleintje in de zomer op een terrasje een pilsje pakt, ontdekt (overigens ook zonder dat pilsje) al heel gauw, dat dit plein het mooiste uitzicht biedt op de grote toren van de Grote Kerk. Vanaf deze plek kunt u ook de mooie brede onderbouw van die toren bewonderen. Er is veel te zien op dit pleintje. Links op de hoek van de Havermarkt en Reigerstraat ziet u het huis 'De Arent', gebouwd rond ca. 1490. Het heeft nu een zeventiende eeuwse trapgevel en is in 1966 geheel gerestaureerd en verbouwd tot restaurant. Tegenover u op de Havermarkt bevinden zich nog twee panden die uw aandacht verdienen. Havermarkt nummer 5 dateert uit de zestiende en zeventiend eeuw. 'De Vogelstruys' op Havermarkt 21 is een opmerkelijk zeventiende eeuws monument met een hoge, niet symmetrische trapgevel. Ook dit huis heeft in de loop de eeuwen allerlei functies en bestemmingen gehad. Het was onder meer refugiehuis voor de zusters van Catharinadal in de zeventiende eeuw. Voor het beeldje 'De Troubadour', dat aan het eind van de Havermarkt prijkt, zijn vier exemplaren vervaardigd. De andere drie werden onthuld in Diest, Orange en Dillenburg, de zustersteden van Breda in de in 1963 tot stand gekomen Unie van Oranjesteden. Een stukje verder in de Visserstraat (nr. 31), aan de rechterkant, staat een fraai bakstenen huis uit de 17e eeuw met overblijfselen van een zestiende eeuwse gothische woning, de Drie Moren geheten.", isUitgaan = true };
            db.databaseTables.InsertOnSubmit(item);
            item = new DatabaseTable { Nummer = 11, Latitude = 51.58936166666667D, Longitude = 4.774445D, Naam = "Havermarkt" , isUitgaan = true };
            db.databaseTables.InsertOnSubmit(item);
            item = new DatabaseTable { Nummer = 12, Latitude = 51.58883333333333D, Longitude = 4.7752783333333335D, Naam = "Grote Kerk" , isUitgaan = false };
            db.databaseTables.InsertOnSubmit(item);
            item = new DatabaseTable { Nummer = 13, Latitude = 51.588195D, Longitude = 4.7751383333333335D, Naam = "Het Poortje" };
            db.databaseTables.InsertOnSubmit(item);
            item = new DatabaseTable { Nummer = 14, Latitude = 51.58708333333333D, Longitude = 4.77575D, Naam = "Ridderstraat" };
            db.databaseTables.InsertOnSubmit(item);
            item = new DatabaseTable { Nummer = 15, Latitude = 51.58741666666667D, Longitude = 4.776555D, Naam = "Grote Markt" , isUitgaan = true };
            db.databaseTables.InsertOnSubmit(item);
            item = new DatabaseTable { Nummer = 16, Latitude = 51.588028333333334D, Longitude = 4.7763333333333335D, Naam = "Bevrijdingsmonument" , isUitgaan = false };
            db.databaseTables.InsertOnSubmit(item);
            item = new DatabaseTable { Nummer = 17, Latitude = 51.58875D, Longitude = 4.776111666666667D, Naam = "StadHuis", Uitleg = "De hoofdingang van het standhuis bevindt zich boven het bordes, dat bewaakt wordt door twee zandstenen leeuwen. Het stadhuis, zoals dat er nu uitziet, kwam pas in 1767 tot stand. Het zijn eigenlijk vier huizen, die toen door de bouwmeester van Oranje, Philips Willem Schonck, achter één gevel werden verborgen. Het oudste deel is de grote hal. Al in de vijftiende eeuw werd daarnaast ook een zgn. 'Cleyn raedthuys' in gebruik genomen. In 1898 kwam het meest rechtse huis, het 'Liggend Hert' erbij, dat nog steeds een aparte gevel. heeft. Ondanks het feit dat het stadhuis slechts beperkt geopend is voor het publiek, willen we u de volgende informatie over de binnen zijde ervan toch niet onthouden. Vrouwe Justitia boven de hoofdingang en het houten beeld achter in de hal geven aan dat in het stadhuis vroeger ook recht gesproken werd. De balie van de vroegere rechtbank, eens staande tegen de achtermuur van de hal, wordt nu in het Breda's Museum bewaard. Links hangt een grote kopie van het beroemde schilderij van Velasques 'Las Lanzas', dat de overgave van Breda aan de Spaanse bevelhebber Spinola (1625) in beeld brengt. Het oorspronkelijke schilderij hangt in het Prado te Madrid.Het stadhuis wordt nog gebruikt voor openbare raadsvergaderingen en voor het sluiten van huwelijken. De burgemeester, wethouders en gemeete-ambtenaren huizen sinds februari 1992 in het nieuwe Stadskantoor aan de Claudius Prinsenlaan in Breda. Door het poortgebouw rechts van het stadhuis lopen we het Stadserf op. Midden op dit pleintje herinnert het beeldje De Turfschipper van Gerarda Rueb, aan de legendarische overval in 1590 van Adriaan van Bergen met zijn Turfschip. (De VVV Breda verkoopt hiervan een replica)." isUitgaan = false};
            db.databaseTables.InsertOnSubmit(item);
            item = new DatabaseTable { Nummer = 18, Latitude = 51.58763833333333D, Longitude = 4.77725D, Naam = "Antonius van Paduakerk", Uitleg = "Op de hoek van de Halstraat / St. Janstraat staan we voor het gebouw met de pilaren: het garnizoenscommando. Vroeger werd het gebouwtje 'hoofdwacht' genoemd. Voordat die hoofdwacht daar introk, stond op deze plaats de St. Janskapel met daar weer achter de gebouwen van de Ridderorde van St. Jan. De leden van deze orde verzorgden hier zieken en armen. De straatnaam herinnert aan deze ridders van St. Jan. in de kapel begon de grote stadsbrand van 1534. De St. Janstraat heeft ooit ook de Veterstraat geheten, waarschijnlijk naar de huydvetters of leerlooiers, die in de St. Janstraat hun beroep uitoefenden. Aan uw rechterhand in de St. Janstraat staat de Antonius van Paduakerk, een opvallend rijk uitgevoerde waterstaatskerk. Deze kerk is in 1836 gebouwd als eerste katholieke kerk na de schuilkerkenperiode, die officieel gebouwd mocht worden. In 1853 werd het de eerste bisschopskerk (kathedraal) van het nieuwe bisdom in Breda. De zetel van de bisschop van Breda keerde na een afwezigheid van 32 jaar aan het begin van 2001 terug naar de Antoniuskerk die daarmee de status van kathedraal opnieuw verwierf. Aan de voorgevel zijn duidelijk de drie soorten antieke zuilen te zien. Van onder naar boven: Dorische, Ionische en Corinthische zuilen. Het beeld boven de ingang stelt 'de Godsdienst' voor. Binnen valt direct de schitterende houten preekstoel op. Hierin is in verschillende panelen het leven van H. Antonius uitgebeeld.Even verder bevinden zich restanten of alleen maar herinneringen aan twee zeer voorname hofhuizen, het Huis Ocrum, waarin tot 1994 de Kunstacademie St. Joost was gevestigd en het Huis Hersbeeck, thans pastorie. In 1667 verbleven in beide huizen de Engelse gezanten, die deelnamen aan de Vredesonderhandelingen van Breda. De afgevaardigden van de Raad van State gebruikten het pand (nr. 16) wanneer zij in de stad op dienstreis waren, maar ook koning Lodewijk Napoleon heeft er gelogeerd. De erker van dit pand had een controlerende functie: de pastoor kon goed in de gaten houden wie er naar de kerk ging. Het Huis Ocrum (nr. 18) was van 1848 tot 1952 rooms burgerweeshuis. Dat kunt u nog zien aan de kinderkopjes die aan de 19e eeuwse voorgevel van het academiegebouw zijn aangebracht. De rode kleur van dit huis (na de restauratie aangebracht) zal door de loop der jaren een mooie grijs rode kleur van dit huis (na de restauratie aangebracht) zal door de loop der jaren een mooie grijs rode kleur krijgen." , isUitgaan = false};
            db.databaseTables.InsertOnSubmit(item);
            item = new DatabaseTable { Nummer = 19, Latitude = 51.588D, Longitude = 4.778945D, Naam = "Bibliotheek", Uitleg = "In de Molenstraat ziet u recht de Bibliotheek, ontworpen door architect Herman Hertzberger. Omdat de Molenstraat vrij smal is, heeft de architect ruimte gecreëerd door de bibliotheek schuin oplopende wanden te geven. Op de plaats van de bibliotheek (op de hoek met de Oude Vest) bevond zich vroeger een poort. Boven deze poort bevond zich 's Heeren Gevangenhuys, waar de zwaarst gestraften werden ondergebracht. De plaats is nog met keitjes in het asfalt aangegeven; de asfaltweg is de plaats van de oude stadsgracht." , isUitgaan = false};
            db.databaseTables.InsertOnSubmit(item);
            item = new DatabaseTable { Nummer = 20, Latitude = 51.58772166666667D, Longitude = 4.781028333333333D, Naam = "Kloosterkazerne", Uitleg = "Op deze hoek ziet u aan de overkant een voornaam monument uit de geschiedenis van Breda: de kloosterkazerne. Het is een deel van het vroegere zusterklooster St. Catharinadal, dat hier sinds 1295 gevestigd was. Het huidige gebouw dateert uit 1504. In 1645 werden de zusters Norbertinessen vanuit Breda naar Oosterhout verdreven." isUitgaan = false};
            db.databaseTables.InsertOnSubmit(item);
            item = new DatabaseTable { Nummer = 21, Latitude = 51.58775D, Longitude = 4.782D, Naam = "Chasse theater" , isUitgaan = true };
            db.databaseTables.InsertOnSubmit(item);
            item = new DatabaseTable { Nummer = 22, Latitude = 51.58775D, Longitude = 4.78125D, Naam = "Binding van Isaac" };
            db.databaseTables.InsertOnSubmit(item);
            item = new DatabaseTable { Nummer = 23, Latitude = 51.589666666666666D, Longitude = 4.781D, Naam = "Beyerd" , isUitgaan = true };
            db.databaseTables.InsertOnSubmit(item);
            item = new DatabaseTable { Nummer = 24, Latitude = 51.589555D, Longitude = 4.78D, Naam = "Gasthuispoort" };
            db.databaseTables.InsertOnSubmit(item);
            item = new DatabaseTable { Nummer = 25, Latitude = 51.58911166666667D, Longitude = 4.777945D, Naam = "Willem Merkxtuin" };
            db.databaseTables.InsertOnSubmit(item);
            item = new DatabaseTable { Nummer = 26, Latitude = 51.589695D, Longitude = 4.778361666666667D, Naam = "Begijnenhof" , isUitgaan = true };
            db.databaseTables.InsertOnSubmit(item);
            item = new DatabaseTable { Nummer = 27, Latitude = 51.5895D, Longitude = 4.77625D, Naam = "Einde stadswandeling" };
            db.databaseTables.InsertOnSubmit(item);
            db.SubmitChanges();
            var DB = from DatabaseTable databasetable in db.databaseTables select databasetable;
            // Execute query and place results into a collection.
            DatabaseTables = new ObservableCollection<DatabaseTable>(DB);
        }

        public int getRowCount()
        {
            if (DatabaseTables != null) return DatabaseTables.Count;
            else return 0;
        }
        public Location[] getWayPoints()
        {
            Location[] locations = new Location[DatabaseTables.Count];

            for (int i = 0; i < DatabaseTables.Count; i++)
            {
                locations[i] = new Location();
                locations[i].Latitude = DatabaseTables[i].Latitude;
                locations[i].Longitude = DatabaseTables[i].Longitude;
            }
            return locations;
        }
        /// <summary>Handles the StatusChanged event of the watcher control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Device.Location.GeoPositionStatusChangedEventArgs"/> instance containing the event data.</param>
        private void watcher_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {
            switch (e.Status)
            {
                case GeoPositionStatus.Disabled:
                    // location is unsupported on this device
                    hasLocation = false;
                    break;
                case GeoPositionStatus.NoData:
                    // data unavailable
                    hasLocation = false;
                    break;
            }
        }

        /// <summary>Handles the PositionChanged event of the watcher control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Device.Location.GeoPositionChangedEventArgs&lt;System.Device.Location.GeoCoordinate&gt;"/> instance containing the event data.</param>
        private void watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            hasLocation = true;
            var epl = e.Position.Location;
            latitude = epl.Latitude;
            longtitude = epl.Longitude;
            if (LocationChanged != null) LocationChanged(getLocation());
        }

        /// <summary>Calculates how far the current location is away from the rotue.</summary>
        /// <param name="tohere">The POI to go to.</param>
        public void calcSpecifications(POI tohere)
        {

        }

        /// <summary>Changes the color of the POI after the user has passed the POI while using the application.</summary>
        public void changePOIColor()
        {

        }

        /// <summary>Calculates the route to the selected POI.</summary>
        /// <remarks>Will not be implemented in this version of the application.</remarks>
        /// <param name="newloc">The new location to navigate to.</param>
        public void generateRoute(POI newloc)
        {
            //Get the list of POI's and create a route passing all of them.
        }

        /// <summary>Gets the extra information about a POI.</summary>
        /// <param name="info">The POI.</param>
        /// <returns>The extra information as a string.</returns>
        public string getInfo(POI info)
        {
            return info.informatie;
        }

        /// <summary>Checks if the user is on the route to navigate.</summary>
        /// <returns>Returns true if the user is on the route, otherwise false</returns>
        private Boolean correctRoute()
        {
            return false;
        }

        /// <summary>Checks if the user has passed a POI.</summary>
        /// <returns>Returns true if the user passed a POI, otherwise false.</returns>
        private Boolean detectPOI()
        {
            return false;
        }

        /// <summary>Retrieves the current or last known location.</summary>
        /// <returns>The Coordinates.</returns>
        public GeoCoordinate getLocation()
        {
            GeoCoordinate l = new GeoCoordinate();
            l.Latitude = latitude;
            l.Longitude = longtitude;
            return l;

        }

        /// <summary>Returns the FileManager which handles FileIO.</summary>
        /// <returns>The FIleManager.</returns>
        public Model.FileManager getFileIO()
        {
            return fileIO;
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify Silverlight that a property has changed.
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}