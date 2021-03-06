﻿using System;
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
using System.Threading;

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
            DatabaseTable item = new DatabaseTable { Nummer = 1, Latitude = 51.5938D, Longitude = 4.77963D, Naam = "VVV Breda", isUitgaan = false, Foto = 2 };
            //      db.databaseTables.InsertOnSubmit(item);
            //     item = new DatabaseTable { Nummer = 2, Latitude = 51.59327833333333D, Longitude = 4.779388333333333D, Naam = "Liefdeszuster" };
            //     db.databaseTables.InsertOnSubmit(item);
            item = new DatabaseTable { Nummer = 3, Latitude = 51.59250D, Longitude = 4.779695D, Naam = "Nassau Baronie Monument", Uitleg = "Bij de ingang van het stadspark, het Valkenberg, staat een monument dat u iets vertelt over de wordings-geschiedenis van de stad Breda, maar vooral over de Heren van de stad uit het Huis van Nassau en de 500-jarige band tussen Breda en het Huis van Nassau.Op 3 juli 1905 werd het Nassau-Baronie-monument, zoals het officieel heet, met veel feestelijk vertoon door Koningin Wilhelmina onthuld. Het beeld herinnert aan het feit, dat in 1404 Graaf Engelbert, de eerste Bredase Nassau en zijn gemalin, Johanna van Polanen, werden ingehuldigd als Heer en Vrouwe van Breda. De ontwerper is de welbekende dr. P.J.H. Cuypers, die o.m. het Rijksmuseum en het Centraal Station in Amsterdam ontwierp. Op dit monument zijn niet alleen de wapenschilden van twintig gemeenten in en rond de Baronie aangebracht maar ook de Leeuw van Nassau die boven alles uittorent met koningskroon, zwaard en wapenschild. In de drie reliëfs is de 'blijde incomste' van Graaf Engelbert en zijn gemalin afgebeeld. De poorters bieden de sleutel van de stad aan.", isUitgaan = false, Foto = 4 };
            db.databaseTables.InsertOnSubmit(item);
            item = new DatabaseTable { Nummer = 4, Latitude = 51.59327833333333D, Longitude = 4.779388333333333D, Naam = "Liefdeszuster", isUitgaan = false, Foto = 3 };
            db.databaseTables.InsertOnSubmit(item);
            item = new DatabaseTable { Nummer = 5, Latitude = 51.59283333333333D, Longitude = 4.7784716666666665D, Naam = "The Light house", isUitgaan = false, Foto = 5 };
            db.databaseTables.InsertOnSubmit(item);
            item = new DatabaseTable { Nummer = 6, Latitude = 51.59061166666667D, Longitude = 4.776166666666667D, Naam = "Kasteel van Breda", Uitleg = "Dit plein bevindt zich aan de zuiderzijde van het kasteel. U heeft zo een mooi uitzicht op de monumentale poort, de zgn. Stadhouderspoort met het wapen van stadhouder Willem de Vijfde, dat overigens pas later is aangebracht. Achter u ziet u het ruiterstandbeeld van stadhouder koning Willem III geplaatst. Voor dit standbeeld werd onder de burgerij van Breda een inzameling gehouden die f 47.000,-- opbracht. Voor de oude Nassaustad Breda is de stadhouderkoning van grote betekenis geweest. Hij voltooide na anderhalve eeuw de verbouwing van het Kasteel. Hendrik de Derde van Nassau en zijn (drede) vrouw Mencia de Mendoza hebben veel in Breda vertoefd, maar verbleven dan toch bij voorkeur in de vertrekken boven de overigens al lang geleden gesloopte watermolen op het terrein van het Kasteel. Rechts van de poort bevindt zich het zgn. Blokhuis, de ambtswoning van de gouveneur van de KMA. Willem van Oranje woonde daar in een aangrenzende ruimte, maar Prins Maurits prefereerde de watermolen als dagelijks verblijf. Prins Philips Willem (1554-1618), de oudste en roomskatholieke zoon van Willem van Oranje, die vele jaren in Spaanse ballingschap verbleef en in Diest werd begraven, is de eerste Oranje geweest, die met zijn vrouw ook in het Kasteel ging wonen. Hij liet het park Valkenberg verfraaien en het Kasteel echt als een paleis inrichten. Bij zijn dood in 1618 werden in Breda tweeënveertig dagen achtereen de kerkklokken geluid. Op de plaats van de vensters, links en rechts van de poort, bevonden zich in de zestiende en zeventiende eeuw sierlijke open galerijen. Het muisgrijze gebouw links van de kasteelpoort werd gebouwd in 1867. Het maakt thans ook deel uit van het KMA gebouwen complex. Meer geschiedenis schuilt et achter de witte pui van het gebouw", isUitgaan = false, Foto = 9 };
            db.databaseTables.InsertOnSubmit(item);
            item = new DatabaseTable { Nummer = 7, Latitude = 51.589695D, Longitude = 4.776138333333334D, Naam = "Stadhouderspoort", Uitleg = "De Stadhouderspoort is één van de toegangspoorten van het Kasteel van Breda in het centrum van Breda. Het Kasteel is voorzien van een slotgracht, maar de Stadhouderspoort kan bereikt worden over een loopbrug, na eerst de Kraanpoort met poortgebouw door te zijn gegaan. De Stadhouderspoort is voorzien van een driehoekig fronton en draagt het wapen van stadhouder Willem V. De poort is in 1959 gerestaureerd.", isUitgaan = false, Foto = 11 };
            db.databaseTables.InsertOnSubmit(item);
            item = new DatabaseTable { Nummer = 8, Latitude = 51.590028333333336D, Longitude = 4.774361666666667D, Naam = "Huis van Brecht", Uitleg = "Het Huis van Brecht is een gebouw in het centrum van Breda op het terrein van het Kasteel van Breda. Het is gemaakt van steen en dateert uit de tweede helft van de veertiende eeuw. Gouvaert van Brecht kocht het in 1530 en vergrootte de woning met een galerij. Na de list met het Turfschip van Breda vlucht de Spaanse familie naar Luik en werd het huis van het Bredase stadsbestuur. Tot 1605 heeft het gebouw verschillende functies vervuld, waaronder smidse; vanaf 1794 tot 1940 was het als militair hospitaal in gebruik. Tegenwoordig dient het gebouw als onderkomen voor de bibliotheek van de Koninklijke Militaire Academie (KMA). Op de eerste verdieping is de Puffiuszaal gevestigd, genoemd naar een gouverneur van de KMA. De zaal heeft zijden behang en wordt ook wel de Vlaamse Zaal genoemd naar het Vlaamse balkenplafond. De zaal is tegenwoordig in gebruik als vergaderzaal en als ruimte voor de Aanname- en Advies-Commissie (AAC) voor het voeren van 'sollicitatiegesprekken' met potentiële nieuwe cadetten. " ,isUitgaan = false, Foto = 7 };
            db.databaseTables.InsertOnSubmit(item);
            item = new DatabaseTable { Nummer = 9, Latitude = 51.590195D, Longitude = 4.773445D, Naam = "Spanjaardsgat", Uitleg = "Het Spanjaardsgat is een waterpoort die ligt tussen de Granaattoren en de Duiventoren van het Kasteel van Breda in het centrum van Breda. De waterpoort is alleen vanaf de buitenkant te zien. Het ligt bij de Haven. Het Spanjaardsgat symboliseert het gat dat de Spanjaarden in hun verdediging lieten vallen in 1590. Schipper Adriaen van Bergen zou hier een aantal soldaten de stad in hebben gesmokkeld om de Spanjaarden te verdrijven (in werkelijkheid is dit echter elders gebeurd). Van Bergen heeft met zijn turfschip verder gevaren naar de waterpoort van het Kasteel van Breda aan de huidige Academiesingel. Het Spanjaardsgat is 20 jaar na de gebeurtenis met het Turfschip van Breda gebouwd. Het Spanjaardsgat, en met name de nabijgelegen 'brug', zijn bekend als 'rendez-vous' (afspreekpunt). In 2007 is de oude brug vervangen door een nieuwe Hoge Brug. Werkzaamheden om dit stadgedeelte in zijn oude staat terug te brengen zijn in 2004 begonnen en in december 2008 afgerond; zowel de rivier de Mark als de oude haven vormen weer een wezenlijk onderdeel van het centrum van de stad. Tegenwoordig varen er rondvaartboten vanuit de haven langs het Spanjaardsgat over de singels. " ,isUitgaan = false, Foto = 21 };
            db.databaseTables.InsertOnSubmit(item);
            item = new DatabaseTable { Nummer = 10, Latitude = 51.58983333333333D, Longitude = 4.773333333333333D, Naam = "Vismarkt", Uitleg = "Van de plaats waar de vishal (1725) nog aangeeft dat hier vroeger een levendige handel in zeevis werd bedreven, wandelen we door de Vismarktstraat naar de Havermarkt. Deze markt is vanaf de jaren zestig tot op de dag van vandaag het uitgaanscentrum van Breda. De naam Havermarkt ten spijt moet worden vastgesteld, dat de granen niet hier; maar op de Grote Markt werden verhandeld. Wel werden hier door de boeren uit de omtrek groenten, eieren en boter aangevoerd en verkocht. In de zeventiende eeuw werd hier ook een leermarkt gehouden. De oude benamingen van dit pleintje luidden Groenmarkt, Botermarkt en Korenmarkt hetgeen de vroegere functies iets beter aanduidt. Rond 1490 werd dit aangelegd, als een verbreding van de Visserstraat. Wie op dit verrukkelijke pleintje in de zomer op een terrasje een pilsje pakt, ontdekt (overigens ook zonder dat pilsje) al heel gauw, dat dit plein het mooiste uitzicht biedt op de grote toren van de Grote Kerk. Vanaf deze plek kunt u ook de mooie brede onderbouw van die toren bewonderen. Er is veel te zien op dit pleintje. Links op de hoek van de Havermarkt en Reigerstraat ziet u het huis 'De Arent', gebouwd rond ca. 1490. Het heeft nu een zeventiende eeuwse trapgevel en is in 1966 geheel gerestaureerd en verbouwd tot restaurant. Tegenover u op de Havermarkt bevinden zich nog twee panden die uw aandacht verdienen. Havermarkt nummer 5 dateert uit de zestiende en zeventiend eeuw. 'De Vogelstruys' op Havermarkt 21 is een opmerkelijk zeventiende eeuws monument met een hoge, niet symmetrische trapgevel. Ook dit huis heeft in de loop de eeuwen allerlei functies en bestemmingen gehad. Het was onder meer refugiehuis voor de zusters van Catharinadal in de zeventiende eeuw. Voor het beeldje 'De Troubadour', dat aan het eind van de Havermarkt prijkt, zijn vier exemplaren vervaardigd. De andere drie werden onthuld in Diest, Orange en Dillenburg, de zustersteden van Breda in de in 1963 tot stand gekomen Unie van Oranjesteden. Een stukje verder in de Visserstraat (nr. 31), aan de rechterkant, staat een fraai bakstenen huis uit de 17e eeuw met overblijfselen van een zestiende eeuwse gothische woning, de Drie Moren geheten.", isUitgaan = true, Foto = 20 };
            db.databaseTables.InsertOnSubmit(item);
            item = new DatabaseTable { Nummer = 11, Latitude = 51.58936166666667D, Longitude = 4.774445D, Naam = "Havermarkt", Uitleg = "De Havermarkt is een pleintje in de binnenstad van Breda. Het ligt vlakbij de Grote of Onze-Lieve-Vrouwekerk en de Haven. Het is een pleintje met rondom cafés met terrassen en een bekende uitgaansplek in Breda. Onder meer is er het monumentale pand de Vogel Struys met asymmetrische trapgevel uit 1665. Op de Havermarkt staat het beeld De Troubadour. De Reigerstraat komt, net als de Vismarktstraat, uit op de Havermarkt. Via de Havermarkt is 't Sas bereikbaar. " ,isUitgaan = true, Foto = 19 };
            db.databaseTables.InsertOnSubmit(item);
            item = new DatabaseTable { Nummer = 12, Latitude = 51.58883333333333D, Longitude = 4.7752783333333335D, Naam = "Grote Kerk", Uitleg = "Zowel door haar bouw als door haar ligging wordt de kerk tot de mooiste van Nederland gerekend. Als voorbeeld van de Brabantse gotiek is zij in Nederland het meest gave en elegante, hoewel iets minder bekend dan de Sint-Janskathedraal in 's-Hertogenbosch. De kerk behoort tot de Top 100 der Nederlandse UNESCO-monumenten. \n Wetenswaardigheden \n •	De oudste katholieke school voor voorbereidend hoger en middelbaar onderwijs van Breda is het Onze Lieve Vrouwelyceum (1923). Anders dan de naam wellicht doet vermoeden was dit oorspronkelijk een jongensschool, maar de naam verwees naar de Onze-Lieve-Vrouwekerk, het symbool van Breda. Bovendien voegde de school zich met deze naam in een Vlaams-Brabantse traditie (vergelijkbare gelijknamige scholen zijn er onder meer in Antwerpen, Brugge, Genk, Halle, Ieper, Tienen en Tongeren). •	Pianophasing, een uniek concert met 60 piano's en 120 quatre mains pianisten op 22 april 2007. ", isUitgaan = false, Foto = 10 };
            db.databaseTables.InsertOnSubmit(item);
            item = new DatabaseTable { Nummer = 13, Latitude = 51.588195D, Longitude = 4.7751383333333335D, Naam = "Het Poortje", isUitgaan = false, Foto = 0 };
            db.databaseTables.InsertOnSubmit(item);
            item = new DatabaseTable { Nummer = 14, Latitude = 51.58708333333333D, Longitude = 4.77575D, Naam = "Ridderstraat", isUitgaan = true, Foto = 0 };
            db.databaseTables.InsertOnSubmit(item);
            item = new DatabaseTable { Nummer = 15, Latitude = 51.58741666666667D, Longitude = 4.776555D, Naam = "Grote Markt", Uitleg = "Het plein ligt in de binnenstad en werd vroeger de Plaetse genoemd, hart van de stad, plek van samenkomst in voor- en tegenspoed. In 1313 waren er twee jaarmarkten: de Sinxenmarkt en de Barnismarkt. Het Stadsbestuur van Breda liet misdadigers executeren op de Grote Markt. In de Middeleeuwen waren er drie herbergen gevestigd op de Markt: Herberg de Beer, gerund door de zuster van de schout van Zevenbergen, Herberg de Zwaan en Herberg de Wilderman. Ook was er de vleeshal. De Grote Markt was vroeger niet zo groot als tegenwoordig. Na de grote stadsbrand van 1534 werd een afgebrande huizenrij afgebroken en kreeg de Markt de afmetingen van tegenwoordig. Tot 1898 stond er een reusachtige lindeboom. Deze werd in het kroningsjaar van koningin Wilhelmina omgehakt en maakte plaats voor de Wilhelminafontein die daar tot 1909 heeft gestaan. In dat jaar verhuisde de fontein naar het Sophiaplein, waar deze nu nog staat. Ook stond op de Grote Markt vroeger een muziekkiosk, waar in de zomer muziekuitvoeringen werden gegeven. Op het plein staat nog het oorlogsmunument Judith met het hoofd van Holofernes van Niel Steenbergen.", isUitgaan = true, Foto = 17 };
            db.databaseTables.InsertOnSubmit(item);
            item = new DatabaseTable { Nummer = 16, Latitude = 51.588028333333334D, Longitude = 4.7763333333333335D, Naam = "Bevrijdingsmonument", isUitgaan = false, Foto = 24 };
            db.databaseTables.InsertOnSubmit(item);
            item = new DatabaseTable { Nummer = 17, Latitude = 51.58875D, Longitude = 4.776111666666667D, Naam = "StadHuis", Uitleg = "De hoofdingang van het standhuis bevindt zich boven het bordes, dat bewaakt wordt door twee zandstenen leeuwen. Het stadhuis, zoals dat er nu uitziet, kwam pas in 1767 tot stand. Het zijn eigenlijk vier huizen, die toen door de bouwmeester van Oranje, Philips Willem Schonck, achter één gevel werden verborgen. Het oudste deel is de grote hal. Al in de vijftiende eeuw werd daarnaast ook een zgn. 'Cleyn raedthuys' in gebruik genomen. In 1898 kwam het meest rechtse huis, het 'Liggend Hert' erbij, dat nog steeds een aparte gevel. heeft. Ondanks het feit dat het stadhuis slechts beperkt geopend is voor het publiek, willen we u de volgende informatie over de binnen zijde ervan toch niet onthouden. Vrouwe Justitia boven de hoofdingang en het houten beeld achter in de hal geven aan dat in het stadhuis vroeger ook recht gesproken werd. De balie van de vroegere rechtbank, eens staande tegen de achtermuur van de hal, wordt nu in het Breda's Museum bewaard. Links hangt een grote kopie van het beroemde schilderij van Velasques 'Las Lanzas', dat de overgave van Breda aan de Spaanse bevelhebber Spinola (1625) in beeld brengt. Het oorspronkelijke schilderij hangt in het Prado te Madrid.Het stadhuis wordt nog gebruikt voor openbare raadsvergaderingen en voor het sluiten van huwelijken. De burgemeester, wethouders en gemeete-ambtenaren huizen sinds februari 1992 in het nieuwe Stadskantoor aan de Claudius Prinsenlaan in Breda. Door het poortgebouw rechts van het stadhuis lopen we het Stadserf op. Midden op dit pleintje herinnert het beeldje De Turfschipper van Gerarda Rueb, aan de legendarische overval in 1590 van Adriaan van Bergen met zijn Turfschip. (De VVV Breda verkoopt hiervan een replica).", isUitgaan = false, Foto = 0 };
            db.databaseTables.InsertOnSubmit(item);
            item = new DatabaseTable { Nummer = 18, Latitude = 51.58763833333333D, Longitude = 4.77725D, Naam = "Antonius van Paduakerk", Uitleg = "Op de hoek van de Halstraat / St. Janstraat staan we voor het gebouw met de pilaren: het garnizoenscommando. Vroeger werd het gebouwtje 'hoofdwacht' genoemd. Voordat die hoofdwacht daar introk, stond op deze plaats de St. Janskapel met daar weer achter de gebouwen van de Ridderorde van St. Jan. De leden van deze orde verzorgden hier zieken en armen. De straatnaam herinnert aan deze ridders van St. Jan. in de kapel begon de grote stadsbrand van 1534. De St. Janstraat heeft ooit ook de Veterstraat geheten, waarschijnlijk naar de huydvetters of leerlooiers, die in de St. Janstraat hun beroep uitoefenden. Aan uw rechterhand in de St. Janstraat staat de Antonius van Paduakerk, een opvallend rijk uitgevoerde waterstaatskerk. Deze kerk is in 1836 gebouwd als eerste katholieke kerk na de schuilkerkenperiode, die officieel gebouwd mocht worden. In 1853 werd het de eerste bisschopskerk (kathedraal) van het nieuwe bisdom in Breda. De zetel van de bisschop van Breda keerde na een afwezigheid van 32 jaar aan het begin van 2001 terug naar de Antoniuskerk die daarmee de status van kathedraal opnieuw verwierf. Aan de voorgevel zijn duidelijk de drie soorten antieke zuilen te zien. Van onder naar boven: Dorische, Ionische en Corinthische zuilen. Het beeld boven de ingang stelt 'de Godsdienst' voor. Binnen valt direct de schitterende houten preekstoel op. Hierin is in verschillende panelen het leven van H. Antonius uitgebeeld.Even verder bevinden zich restanten of alleen maar herinneringen aan twee zeer voorname hofhuizen, het Huis Ocrum, waarin tot 1994 de Kunstacademie St. Joost was gevestigd en het Huis Hersbeeck, thans pastorie. In 1667 verbleven in beide huizen de Engelse gezanten, die deelnamen aan de Vredesonderhandelingen van Breda. De afgevaardigden van de Raad van State gebruikten het pand (nr. 16) wanneer zij in de stad op dienstreis waren, maar ook koning Lodewijk Napoleon heeft er gelogeerd. De erker van dit pand had een controlerende functie: de pastoor kon goed in de gaten houden wie er naar de kerk ging. Het Huis Ocrum (nr. 18) was van 1848 tot 1952 rooms burgerweeshuis. Dat kunt u nog zien aan de kinderkopjes die aan de 19e eeuwse voorgevel van het academiegebouw zijn aangebracht. De rode kleur van dit huis (na de restauratie aangebracht) zal door de loop der jaren een mooie grijs rode kleur van dit huis (na de restauratie aangebracht) zal door de loop der jaren een mooie grijs rode kleur krijgen.", isUitgaan = false, Foto = 0 };
            db.databaseTables.InsertOnSubmit(item);
            item = new DatabaseTable { Nummer = 19, Latitude = 51.588D, Longitude = 4.778945D, Naam = "Bibliotheek", Uitleg = "In de Molenstraat ziet u recht de Bibliotheek, ontworpen door architect Herman Hertzberger. Omdat de Molenstraat vrij smal is, heeft de architect ruimte gecreëerd door de bibliotheek schuin oplopende wanden te geven. Op de plaats van de bibliotheek (op de hoek met de Oude Vest) bevond zich vroeger een poort. Boven deze poort bevond zich 's Heeren Gevangenhuys, waar de zwaarst gestraften werden ondergebracht. De plaats is nog met keitjes in het asfalt aangegeven; de asfaltweg is de plaats van de oude stadsgracht.", isUitgaan = false, Foto = 0 };
            db.databaseTables.InsertOnSubmit(item);
            item = new DatabaseTable { Nummer = 20, Latitude = 51.58772166666667D, Longitude = 4.781028333333333D, Naam = "Kloosterkazerne", Uitleg = "Op deze hoek ziet u aan de overkant een voornaam monument uit de geschiedenis van Breda: de kloosterkazerne. Het is een deel van het vroegere zusterklooster St. Catharinadal, dat hier sinds 1295 gevestigd was. Het huidige gebouw dateert uit 1504. In 1645 werden de zusters Norbertinessen vanuit Breda naar Oosterhout verdreven.", isUitgaan = false, Foto = 0 };
            db.databaseTables.InsertOnSubmit(item);
            item = new DatabaseTable { Nummer = 21, Latitude = 51.58775D, Longitude = 4.782D, Naam = "Chasse theater", Uitleg = "Het Chassé Theater heeft de volgende theaterzalen:\n•	Jupiler Zaal (1430 plaatsen)\n•	Finntax Telecom Zaal (650 plaatsen)\n•	MK2 Zaal (200 plaatsen)\nIn het Chassé Theater bevinden zich buiten Chassé Cinema, twee horecagelegenheden en diverse foyers. Bovendien kan vanuit het theater de Bredase vestiging van het Holland Casino bereikt worden. Het biedt elk seizoen een uitgebreid theaterprogramma met veel verschillenden genres en voor diverse doelgroepen en leeftijden. Het Chassé Theater ontvangt jaarlijks ongeveer 400.000 bezoekers. ", isUitgaan = true, Foto = 0 };
            db.databaseTables.InsertOnSubmit(item);
            item = new DatabaseTable { Nummer = 22, Latitude = 51.58775D, Longitude = 4.78125D, Naam = "Binding van Isaac", isUitgaan = false, Foto = 0 };
            db.databaseTables.InsertOnSubmit(item);
            item = new DatabaseTable { Nummer = 23, Latitude = 51.589666666666666D, Longitude = 4.781D, Naam = "Beyerd", Uitleg = "Het museum is gevestigd in het Oudemannenhuis. In de jaren zestig van de vorige eeuw kreeg dat de bestemming van museum voor moderne kunst onder de naam De Beyerd. In de jaren negentig ging de transformatie van De Beyerd tot Graphic Design Museum van start. Hans van Heeswijk is sinds 1985 huisarchitect van De Beyerd en heeft ook de grootschalige verbouwing gerealiseerd. Het Grafisch Museum beschikt sinds de verbouwing over een nieuw gedeelte en heeft onder meer zes grote tentoonstellingszalen en een winkel, café en auditorium.", isUitgaan = true, Foto = 0 };
            db.databaseTables.InsertOnSubmit(item);
            item = new DatabaseTable { Nummer = 24, Latitude = 51.589555D, Longitude = 4.78D, Naam = "Gasthuispoort", isUitgaan = false, Foto = 0 };
            db.databaseTables.InsertOnSubmit(item);
            item = new DatabaseTable { Nummer = 25, Latitude = 51.58911166666667D, Longitude = 4.777945D, Naam = "Willem Merkxtuin", Uitleg = "De tuin is gelegen achter de voormalige hofhuizen in de Catharinastraat verlengde van de Reigerstraat. De tuin is vernoemd naar Willem Merkx, burgemeester van Breda van 1967 tot 1983. Het is een kleine groene oase in het midden van de stad. In de tuin staan enkele beeldhouwwerken. Vanuit een doorgang achter de volière komt men via het Stadserf bij de achterkant van het stadhuis Breda op de Grote Markt Breda. ", isUitgaan = false, Foto = 0 };
            db.databaseTables.InsertOnSubmit(item);
            item = new DatabaseTable { Nummer = 26, Latitude = 51.589695D, Longitude = 4.778361666666667D, Naam = "Begijnenhof", isUitgaan = false, Foto = 0 };
            db.databaseTables.InsertOnSubmit(item);
            item = new DatabaseTable { Nummer = 27, Latitude = 51.5895D, Longitude = 4.77625D, Naam = "Einde stadswandeling", Foto = 0 };
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

        public Location[] getRouteToNextPOI(POI nextPOI)
        {
            Location[] loc = new Location[2];
            loc[0] = new Location();
            loc[0].Latitude = getLocation().Latitude;
            loc[0].Longitude = getLocation().Longitude;
            loc[1] = new Location();
            loc[1].Latitude = nextPOI.latitude;
            loc[1].Longitude = nextPOI.longitude;
            return loc;
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