﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IronXL;
using System.Net;
using System.IO;

/*
 * Agenda:
 * **************************************************************************************************************************************************
 * Do this for the workshops list before all others --> The code around line 720, where the parseContactWithKeywordLocation method is called, needs to parse the correct info, not necessarily the first contact info. (Maybe compile a list of possible contact info and then go through them, checking validity somehow?)
 * The lower methods need corrections so that the webcrawler only collects the info corresponding to the selections in the GUI
 * If a source URL is empty, the crawler should just ignore it, not spend like 20 seconds "thinking about it"
 * When the crawler reads source URLs from workbook, make it so it reads hyperlinks, then the text if a hyperlink is unavailable, see IronXL docs for help: https://ironsoftware.com/csharp/excel/object-reference/api/IronXL.Cell.html
 * Refine contact search keywords after previous stuff to make searches more accurate
 * Step 3 gives the URL indices of url's instead of row numbers and step 3 needs to stop after reading to the number of entries
 * Integrate the GUI
 * Refactor code to convert CONTACTS_PAGE_SEARCH_KEYWORDS to 2D ArrayLists or C# analogs
 * Refactor code to give checkBoxIsChecked and contactInfo dynamic lengths (modifiable by GUI events, such as changing gathering method)
*/

namespace First_Webcrawler
{
    public partial class GUI : Form
    {
        //                                                                  File Reading Class Variables
        //Row counting vars
        public static int NUMBER_OF_ENTRIES = 20;
        //default offset should be 1, for headers
        public static int rowOffset = 1;
        //set URLIndex to rowOffset so that's where it starts
        public static int URLIndex = 0;

        //Row/column reading vars
        public static int NAMES_COLUMN = 1;
        public static int READING_COLUMN = 1;
        public static String MAIN_URL_WRITING_COLUMN = "G"; //K
        public static String CONTACT_URL_WRITING_COLUMN = "F"; //L
        //email = cost
        public static String EMAIL_WRITING_COLUMN = "E"; //E
        //phone = yes or no lodging
        public static String PHONE_WRITING_COLUMN = "H"; //F
        //address = destination
        public static String ADDRESS_WRITING_COLUMN = "C"; //C
        //other = available
        public static String OTHER_WRITING_COLUMN = "D"; //A

        //File path vars
        public static String SHEET_NAME = "Sheet1";
        public static String NAME_OF_IO_DOC = "Hyperlink to URL to Info.xlsx";//"Workshop Listing URLs Prepped for Webcrawler.xlsx";//
        public static String PATH_OF_IO_DOC = "C:\\Users\\Owner\\Desktop\\Use to Improve Webcrawler & Parser\\" + NAME_OF_IO_DOC; //



        //                                                                  URL Gathering Class Variables
        //Main URL vars
        //Empty cells not included in list of sites
        public static string[] UKURLs = {"https://sinwp.com/camera_clubs/Bottisham-and-Burwell-Photographic-Club-171.htm", "https://sinwp.com/camera_clubs/Bournemouth-Electric-Camera-Club-1932.htm", "https://sinwp.com/camera_clubs/Bournemouth-Natural-Science-Society-81.htm", "https://sinwp.com/camera_clubs/Bracknell-Camera-Club-84.htm", "https://sinwp.com/camera_clubs/Bradford-Camera-Club-85.htm", "https://sinwp.com/camera_clubs/Bradford-Photographic-Society-86.htm", "https://sinwp.com/camera_clubs/Bradford-Telephone-Area-Camera-Club-73.htm", "https://sinwp.com/camera_clubs/Braintree-Camera-Club-75.htm", "https://sinwp.com/camera_clubs/Bramhall-Photographic-Society-76.htm", "https://sinwp.com/camera_clubs/Brandon-District-Photographic-Club-1464.htm", "https://sinwp.com/camera_clubs/Braunton-Camera-Club-963.htm", "https://sinwp.com/camera_clubs/Breckland-Imaging-Group-1465.htm", "https://sinwp.com/camera_clubs/Brentwood-and-District-Photographic-Club-79.htm", "https://sinwp.com/camera_clubs/Bridgnorth-and-District-Camera-Club-70.htm", "https://sinwp.com/camera_clubs/Bridgwater-Photographic-Society-1029.htm", "https://sinwp.com/camera_clubs/Bridport-Camera-Club-964.htm", "https://sinwp.com/camera_clubs/Brierley-Hill-Photographic-Club-71.htm", "https://sinwp.com/camera_clubs/Brighton-Hove-Camera-Club-755.htm", "https://sinwp.com/camera_clubs/Bristol-Photographic-Society-965.htm", "https://sinwp.com/camera_clubs/British-Aerospace-Photo-Club-1050.htm", "https://sinwp.com/camera_clubs/British-Airways-Camera-Club-756.htm", "https://sinwp.com/camera_clubs/British-Telecom-Research-Photographic-Club-72.htm", "https://sinwp.com/camera_clubs/Bromley-Camera-Club-Bromley-827.htm", "https://sinwp.com/camera_clubs/Bromsgrove-Photographic-Society-828.htm", "https://sinwp.com/camera_clubs/Brooklands-Photographic-Society-1962.htm", "https://sinwp.com/camera_clubs/Broughton-Astley-Photographic-Society-1846.htm", "https://sinwp.com/camera_clubs/Brunel-Photography-Society-1589.htm", "https://sinwp.com/camera_clubs/Bryn-Photographic-Club-67.htm", "https://sinwp.com/camera_clubs/Buckingham-Camera-Club-829.htm", "https://sinwp.com/camera_clubs/Bungay-Camera-Club-1103.htm", "https://sinwp.com/camera_clubs/Bunwell-District-Camera-Club-1411.htm", "https://sinwp.com/camera_clubs/Burghfield-Camera-Club-1186.htm", "https://sinwp.com/camera_clubs/Burnham-On-Sea-Camera-Club-966.htm", "https://sinwp.com/camera_clubs/Burnley-Camera-Club-1412.htm", "https://sinwp.com/camera_clubs/Burton-Latimer-Camera-Club-69.htm", "https://sinwp.com/camera_clubs/Burton-upon-Trent-Photographic-Society-1847.htm", "https://sinwp.com/camera_clubs/Bury-Photographic-Society-63.htm", "https://sinwp.com/camera_clubs/Bury-St-Edmunds-Photographic-Society-64.htm", "https://sinwp.com/camera_clubs/Buxton-Camera-Club-65.htm", "https://sinwp.com/camera_clubs/Byker-Photographic-Workshop-59.htm", "https://sinwp.com/camera_clubs/Caister-Camera-Club-61.htm", "https://sinwp.com/camera_clubs/Calne-Camera-Club-967.htm", "https://sinwp.com/camera_clubs/Calverton-Camera-Club-56.htm", "https://sinwp.com/camera_clubs/Cambois-Camera-Club-1644.htm", "https://sinwp.com/camera_clubs/Camborne-and-Redruth-Camera-Club-969.htm", "https://sinwp.com/camera_clubs/Cambridge-Camera-Club-50.htm", "https://sinwp.com/camera_clubs/Cambridge-University-Photographic-Society-%5bPhoCUS%5d-1587.htm", "https://sinwp.com/camera_clubs/Camping-and-Caravanning-Club-Photographic-Group-831.htm", "https://sinwp.com/camera_clubs/Cannock-Photographic-Society-51.htm", "https://sinwp.com/camera_clubs/Canterbury-Photographic-Society-1075.htm", "https://sinwp.com/camera_clubs/Canvey-Camera-Club-52.htm", "https://sinwp.com/camera_clubs/Capel-Camera-Club-758.htm", "https://sinwp.com/camera_clubs/Carlisle-Camera-Club-172.htm", "https://sinwp.com/camera_clubs/Carnkie-Camera-Club-1361.htm", "https://sinwp.com/camera_clubs/Carshalton-Camera-Club-759.htm", "https://sinwp.com/camera_clubs/Castleford-Camera-Club-176.htm", "https://sinwp.com/camera_clubs/Castleside-District-Photo-Club-1876.htm", "https://sinwp.com/camera_clubs/Caston-Camera-Club-1414.htm", "https://sinwp.com/camera_clubs/Chalfonts-and-Gerrards-Cross-Camera-Club-1108.htm", "https://sinwp.com/camera_clubs/Challney-Camera-Club-178.htm", "https://sinwp.com/camera_clubs/Chapel-Camera-Club-1799.htm", "https://sinwp.com/camera_clubs/Chapel-en-le-Firth-Camera-Club-179.htm", "https://sinwp.com/camera_clubs/Chard-Camera-Club-970.htm", "https://sinwp.com/camera_clubs/Charnwood-Imaging-Group-1362.htm", "https://sinwp.com/camera_clubs/Cheam-Camera-Club-1187.htm", "https://sinwp.com/camera_clubs/Chelmsford-Camera-Club-181.htm", "https://sinwp.com/camera_clubs/Cheltenham-Camera-Club-182.htm", "https://sinwp.com/camera_clubs/Chertsey-Camera-Club-1460.htm", "https://sinwp.com/camera_clubs/Chesham-Photographic-Society-1520.htm", "https://sinwp.com/camera_clubs/Chester-Photographic-Society-1174.htm", "https://sinwp.com/camera_clubs/Chester-Le-Street-Camera-Club-745.htm", "https://sinwp.com/camera_clubs/Chesterfield-Photographic-Society-1415.htm", "https://sinwp.com/camera_clubs/Chet-Valley-Photography-Club-1524.htm", "https://sinwp.com/camera_clubs/Chichester-Camera-Club-1330.htm", "https://sinwp.com/camera_clubs/Chigwell-Camera-Club-1416.htm", "https://sinwp.com/camera_clubs/Chingford-Photographic-Society-1188.htm", "https://sinwp.com/camera_clubs/Chorley-Photographic-Society-709.htm", "https://sinwp.com/camera_clubs/Cirencester-Camera-Club-971.htm", "https://sinwp.com/camera_clubs/City-Camera-Club-762.htm", "https://sinwp.com/camera_clubs/City-Of-London-Cripplegate-Photographic-Society-1189.htm", "https://sinwp.com/camera_clubs/Clacton-Camera-Club-1752.htm", "https://sinwp.com/camera_clubs/Clay-Cross-and-Biwater-Photographic-Society-188.htm", "https://sinwp.com/camera_clubs/Cleethorpes-Camera-Club-1909.htm", "https://sinwp.com/camera_clubs/Clevedon-Camera-Club-972.htm", "https://sinwp.com/camera_clubs/Club-Abington-Photographic-1848.htm", "https://sinwp.com/camera_clubs/Club-Focus-Camera-Club-1955.htm", "https://sinwp.com/camera_clubs/Coalville-and-District-Photographic-Society-189.htm", "https://sinwp.com/camera_clubs/Coast-Camera-Club-190.htm", "https://sinwp.com/camera_clubs/Colchester-Creative-Shots-1751.htm", "https://sinwp.com/camera_clubs/Colchester-Photographic-Society-191.htm", "https://sinwp.com/camera_clubs/Coleford-Photographic-Club-1714.htm", "https://sinwp.com/camera_clubs/Colne-Camera-Club-1798.htm", "https://sinwp.com/camera_clubs/Colour-Reversal-Club-833.htm", "https://sinwp.com/camera_clubs/Colyford-District-Photographic-Club-2529.htm", "https://sinwp.com/camera_clubs/Compton-G-Camera-Club-1849.htm", "https://sinwp.com/camera_clubs/Consett-and-District-Photographic-Society-192.htm", "https://sinwp.com/camera_clubs/Cookhill-and-Studley-Camera-Club-194.htm", "https://sinwp.com/camera_clubs/Cookridge-Camera-Club-195.htm", "https://sinwp.com/camera_clubs/Copeland-Photographic-Society-196.htm", "https://sinwp.com/camera_clubs/Corby-Photogrpahic-Club-197.htm", "https://sinwp.com/camera_clubs/Corley-and-Fillongley-Photographic-Club-1561.htm", "https://sinwp.com/camera_clubs/Corsham-Photographic-Club-973.htm", "https://sinwp.com/camera_clubs/Cosham-Camera-Club-198.htm", "https://sinwp.com/camera_clubs/Cotswold-Monochrome-Print-Group-1052.htm", "https://sinwp.com/camera_clubs/Courtaulds-Camera-Club-199.htm", "https://sinwp.com/camera_clubs/Coventry-Photographic-Society-200.htm", "https://sinwp.com/camera_clubs/Cramlington-Camera-Club-202.htm", "https://sinwp.com/camera_clubs/Cranleigh-Camera-Club-763.htm", "https://sinwp.com/camera_clubs/Crawley-Camera-Club-764.htm", "https://sinwp.com/camera_clubs/Crayford-Camera-Club-1083.htm", "https://sinwp.com/camera_clubs/Creda-Camera-Club-203.htm", "https://sinwp.com/camera_clubs/Crediton-Photography-Club-974.htm", "https://sinwp.com/camera_clubs/Crewe-Photographic-Society-1797.htm", "https://sinwp.com/camera_clubs/Crosby-Camera-Club-206.htm", "https://sinwp.com/camera_clubs/Crossbow-Camera-Club-975.htm", "https://sinwp.com/camera_clubs/Crown-Monochrome-Group-976.htm", "https://sinwp.com/camera_clubs/Croxley-Camera-Club-1109.htm", "https://sinwp.com/camera_clubs/Croydon-Camera-Club-765.htm", "https://sinwp.com/camera_clubs/Cuffley-Camera-Club-208.htm", "https://sinwp.com/camera_clubs/Cyanamid-Camera-Club-213.htm", "https://sinwp.com/camera_clubs/DSS-Newcastle-CO-Camera-Club-214.htm", "https://sinwp.com/camera_clubs/Dagenham-Camera-Club-215.htm", "https://sinwp.com/camera_clubs/Danson-and-District-Amateur-Camera-Club-1190.htm", "https://sinwp.com/camera_clubs/Darlington-Camera-Club-216.htm", "https://sinwp.com/camera_clubs/Dartford-District-Photographic-Society-1084.htm", "https://sinwp.com/camera_clubs/Dartmouth-Camera-Club-977.htm", "https://sinwp.com/camera_clubs/Darwen-Camera-Club-217.htm", "https://sinwp.com/camera_clubs/Daventry-Photographic-Society-1850.htm", "https://sinwp.com/camera_clubs/Davyhulme-Camera-Club-1626.htm", "https://sinwp.com/camera_clubs/Dawlish-Teignmouth-Camera-Club-The-978.htm", "https://sinwp.com/camera_clubs/Deal-District-Camera-Club-1782.htm", "https://sinwp.com/camera_clubs/Dean-Photographic-Club-219.htm", "https://sinwp.com/camera_clubs/Dearne-Valley-Camera-Club-1364.htm", "https://sinwp.com/camera_clubs/Deepings-Camera-Club-1540.htm", "https://sinwp.com/camera_clubs/Derby-City-Photographic-Club-185.htm", "https://sinwp.com/camera_clubs/Derby-Photographic-Society-223.htm", "https://sinwp.com/camera_clubs/Dereham-Photographic-Club-224.htm", "https://sinwp.com/camera_clubs/Desborough-Rothwell-Photographic-Society-1851.htm", "https://sinwp.com/camera_clubs/Devizes-Camera-Club-979.htm", "https://sinwp.com/camera_clubs/Dewsbury-and-District-Photographic-Society-227.htm", "https://sinwp.com/camera_clubs/Digibox-Camera-Club-1852.htm", "https://sinwp.com/camera_clubs/Digital-Photographer-Learning-Together-1191.htm", "https://sinwp.com/camera_clubs/Diss-and-District-Camera-Club-231.htm", "https://sinwp.com/camera_clubs/Doncaster-Camera-Club-232.htm", "https://sinwp.com/camera_clubs/Dorchester-Camera-Club-980.htm", "https://sinwp.com/camera_clubs/Dorking-Camera-Club-766.htm", "https://sinwp.com/camera_clubs/Dorset-Light-1931.htm", "https://sinwp.com/camera_clubs/Downend-Camera-Club-981.htm", "https://sinwp.com/camera_clubs/Dowty-Staverton-Camera-Club-233.htm", "https://sinwp.com/camera_clubs/Driffield-Photographic-Society-1963.htm", "https://sinwp.com/camera_clubs/Droitwich-Camera-Club-1853.htm", "https://sinwp.com/camera_clubs/Dronfield-Camera-Club-236.htm", "https://sinwp.com/camera_clubs/Droylesden-Camera-Club-237.htm", "https://sinwp.com/camera_clubs/Dudley-Camera-Club-238.htm", "https://sinwp.com/camera_clubs/Dulverton-Camera-Club-982.htm", "https://sinwp.com/camera_clubs/Dunchurch-Photographic-Society-240.htm", "https://sinwp.com/camera_clubs/Dunholme-Camera-Club-1420.htm", "https://sinwp.com/camera_clubs/Dunstable-and-District-Camera-Club-243.htm", "https://sinwp.com/camera_clubs/Durham-Photographic-Society-244.htm", "https://sinwp.com/camera_clubs/Duston-Camera-Club-839.htm", "https://sinwp.com/camera_clubs/Ealing-and-Hampshire-House-Photographic-Society-246.htm", "https://sinwp.com/camera_clubs/Eamont-Camera-Club-247.htm", "https://sinwp.com/camera_clubs/Earby-And-District-Camera-Club-1177.htm", "https://sinwp.com/camera_clubs/Earl-Shilton-Camera-Club-1421.htm", "https://sinwp.com/camera_clubs/East-Grinstead-Camera-Club-768.htm", "https://sinwp.com/camera_clubs/East-Ipswich-Camera-Club-249.htm", "https://sinwp.com/camera_clubs/East-Midlands-Audio-Visual-Group-1422.htm", "https://sinwp.com/camera_clubs/East-Midlands-Monochrome-Group-1854.htm", "https://sinwp.com/camera_clubs/Eastbourne-Photographic-Society-767.htm", "https://sinwp.com/camera_clubs/Eastleigh-Camera-Club-251.htm", "https://sinwp.com/camera_clubs/Eastwood-Photographic-Society-1912.htm", "https://sinwp.com/camera_clubs/Edmonton-Camera-Club-728.htm", "https://sinwp.com/camera_clubs/Eggleston-Camera-Club-1136.htm", "https://sinwp.com/camera_clubs/Electricity-Camera-Club-255.htm", "https://sinwp.com/camera_clubs/Ellesmere-Port-and-District-Photographic-Society-256.htm", "https://sinwp.com/camera_clubs/Ellesmere-Port-Photographic-Society-1058.htm", "https://sinwp.com/camera_clubs/Elmham-Camera-Club-729.htm", "https://sinwp.com/camera_clubs/Elmswell-Photographic-Society-1525.htm", "https://sinwp.com/camera_clubs/Ely-Photographic-Club-730.htm", "https://sinwp.com/camera_clubs/Enfield-Camera-Club-840.htm", "https://sinwp.com/camera_clubs/EPIC-1193.htm", "https://sinwp.com/camera_clubs/Epsom-Camera-Club-769.htm", "https://sinwp.com/camera_clubs/Esh-Winning-Camera-Club-1882.htm", "https://sinwp.com/camera_clubs/Esprit-Photographic-Club-1195.htm", "https://sinwp.com/camera_clubs/Essex-Audio-Visual-Group-1753.htm", "https://sinwp.com/camera_clubs/Essex-Monochrome-Group-1754.htm", "https://sinwp.com/camera_clubs/Evesham-Camera-Club-1365.htm", "https://sinwp.com/camera_clubs/Exeter-Camera-Club-983.htm", "https://sinwp.com/camera_clubs/Exmoor-Camera-Club-1479.htm", "https://sinwp.com/camera_clubs/Exmouth-Photo-Group-954.htm", "https://sinwp.com/camera_clubs/Eynsford-Photographic-Image-Club-1324.htm", "https://sinwp.com/camera_clubs/Failsworth-Camera-Club-711.htm", "https://sinwp.com/camera_clubs/Falmouth-Camera-Club-985.htm", "https://sinwp.com/camera_clubs/Fareham-Portchester-Camera-Club-1196.htm", "https://sinwp.com/camera_clubs/Farnborough-Camera-Club-771.htm", "https://sinwp.com/camera_clubs/Faversham-District-Camera-Club-1086.htm", "https://sinwp.com/camera_clubs/Felixstowe-Photographic-Society-1468.htm", "https://sinwp.com/camera_clubs/Felling-Camera-Club-747.htm", "https://sinwp.com/camera_clubs/Fenland-Camera-Club-1526.htm", "https://sinwp.com/camera_clubs/Fentham-Photographic-Society-257.htm", "https://sinwp.com/camera_clubs/Field-End-Photographic-Society-1197.htm", "https://sinwp.com/camera_clubs/Fleetlands-Photographic-Society-258.htm", "https://sinwp.com/camera_clubs/Fleetwood-Camera-Club-259.htm", "https://sinwp.com/camera_clubs/Focus-Ryde-Imaging-Group-533.htm", "https://sinwp.com/camera_clubs/Focus-2-4-1755.htm", "https://sinwp.com/camera_clubs/Focus-Photographic-Group-1965.htm", "https://sinwp.com/camera_clubs/Folkestone-Camera-Club-1076.htm", "https://sinwp.com/camera_clubs/Ford-Camera-Club-260.htm", "https://sinwp.com/camera_clubs/Ford-Halewood-Camera-Club-261.htm", "https://sinwp.com/camera_clubs/Fordingbridge-Camera-Club-262.htm", "https://sinwp.com/camera_clubs/Forest-of-Dean-Camera-Club-1855.htm", "https://sinwp.com/camera_clubs/Forest-of-Galtres-Camera-Club-264.htm", "https://sinwp.com/camera_clubs/Format-88-Camera-Club-266.htm", "https://sinwp.com/camera_clubs/Formby-Photographic-Group-267.htm", "https://sinwp.com/camera_clubs/Forty-Three-Camera-Club-772.htm", "https://sinwp.com/camera_clubs/Fowey-River-Camera-Club-987.htm", "https://sinwp.com/camera_clubs/Framlingham-Camera-Club-270.htm", "https://sinwp.com/camera_clubs/Friern-Barnet-Totteridge-Camera-Club-841.htm", "https://sinwp.com/camera_clubs/Frodsham-and-District-Photographic-Society-272.htm", "https://sinwp.com/camera_clubs/Frome-Selwood-Photographic-Society-988.htm", "https://sinwp.com/camera_clubs/Frome-Wessex-Camera-Club-989.htm", "https://sinwp.com/camera_clubs/Furness-Foto-Group-1424.htm", "https://sinwp.com/camera_clubs/Gallery-Photo-Group-275.htm", "https://sinwp.com/camera_clubs/Gamlingay-Photographic-Society-276.htm", "https://sinwp.com/camera_clubs/Gamma-Photoforum-1966.htm", "https://sinwp.com/camera_clubs/Garforth-Camera-Club-277.htm", "https://sinwp.com/camera_clubs/Garstang-Camera-Club-278.htm", "https://sinwp.com/camera_clubs/Gateshead-Camera-Club-279.htm", "https://sinwp.com/camera_clubs/Gateway-Camera-Club-1087.htm", "https://sinwp.com/camera_clubs/Gearies-Photographic-Group-818.htm", "https://sinwp.com/camera_clubs/Glaxo-Wellcome-Photographic-Club-1088.htm", "https://sinwp.com/camera_clubs/Glemsford-Photographic-Club-1425.htm", "https://sinwp.com/camera_clubs/Gloucester-Camera-Club-286.htm", "https://sinwp.com/camera_clubs/GMC-Fire-Service-Camera-Club-712.htm", "https://sinwp.com/camera_clubs/Godalming-Photographic-Society-773.htm", "https://sinwp.com/camera_clubs/Godmanchester-Camera-Club-1756.htm", "https://sinwp.com/camera_clubs/Golden-Lane-Camera-Club-1198.htm", "https://sinwp.com/camera_clubs/Goldfish-Bowl-Bournemouth-1367.htm", "https://sinwp.com/camera_clubs/Goring-Gap-Photo-Club-1521.htm", "https://sinwp.com/camera_clubs/Gosforth-Camera-Club-1883.htm", "https://sinwp.com/camera_clubs/Gosport-Camera-Club-288.htm", "https://sinwp.com/camera_clubs/Grange-and-District-Photographic-Society-289.htm", "https://sinwp.com/camera_clubs/Grange-Photographic-Society-280.htm", "https://sinwp.com/camera_clubs/Grantham-District-Camera-Club-290.htm", "https://sinwp.com/camera_clubs/Gravesend-Camera-Club-1077.htm", "https://sinwp.com/camera_clubs/Great-Barr-Photographic-Society-1426.htm", "https://sinwp.com/camera_clubs/Great-Harwood-Camera-Club-1803.htm", "https://sinwp.com/camera_clubs/Great-Notley-Photography-Club-1469.htm", "https://sinwp.com/camera_clubs/Great-Yarmouth-and-District-Photographic-Society-1427.htm", "https://sinwp.com/camera_clubs/Greenwood-Camera-Club-1089.htm", "https://sinwp.com/camera_clubs/Greshams-Photographic-Society-1758.htm", "https://sinwp.com/camera_clubs/Grimsby-Photographic-Society-295.htm", "https://sinwp.com/camera_clubs/Guardian-Camera-Club-1199.htm", "https://sinwp.com/camera_clubs/Guildford-Photographic-Society-774.htm", "https://sinwp.com/camera_clubs/Guisborough-Photo-Group-1369.htm", "https://sinwp.com/camera_clubs/Hadleigh-Camera-Club-1471.htm", "https://sinwp.com/camera_clubs/Hagley-Camera-Club-302.htm", "https://sinwp.com/camera_clubs/Hailsham-Photographic-Society-1200.htm", "https://sinwp.com/camera_clubs/Hainault-Community-Camera-Club-303.htm", "https://sinwp.com/camera_clubs/Halesworth-Camera-Club-819.htm", "https://sinwp.com/camera_clubs/Halifax-Photographic-Society-304.htm", "https://sinwp.com/camera_clubs/Halstead-District-Photographic-Society-1759.htm", "https://sinwp.com/camera_clubs/Halstead-and-District-Camera-Club-1428.htm", "https://sinwp.com/camera_clubs/Halton-Photographic-Society-306.htm", "https://sinwp.com/camera_clubs/Hampstead-Photographic-Society-775.htm", "https://sinwp.com/camera_clubs/Hampton-Hill-Photographic-Society-776.htm", "https://sinwp.com/camera_clubs/Handsworth-Photogrpahic-Society-308.htm", "https://sinwp.com/camera_clubs/Hanham-Photographic-Society-990.htm", "https://sinwp.com/camera_clubs/Harleston-Camera-Club-1527.htm", "https://sinwp.com/camera_clubs/Harlow-Photographic-Society-736.htm", "https://sinwp.com/camera_clubs/Harpenden-Photographic-Society-1201.htm", "https://sinwp.com/camera_clubs/Harrogate-Photographic-Society-1104.htm", "https://sinwp.com/camera_clubs/Harrow-Camera-Club-1512.htm", "https://sinwp.com/camera_clubs/Hartlepool-Camera-Club-1884.htm", "https://sinwp.com/camera_clubs/Hartlepool-Photographic-and-Digital-Group-1429.htm", "https://sinwp.com/camera_clubs/Hartlepool-Photographic-Society-749.htm", "https://sinwp.com/camera_clubs/Harwich-and-Dovercourt-Camera-Club-737.htm", "https://sinwp.com/camera_clubs/Haslemere-Camera-Club-1935.htm", "https://sinwp.com/camera_clubs/Haslemere-Camera-Club-1202.htm", "https://sinwp.com/camera_clubs/Hastings-and-St-Leonards-Camera-Club-1090.htm", "https://sinwp.com/camera_clubs/Havant-Camera-Club-309.htm", "https://sinwp.com/camera_clubs/Hay-On-Wye-Camera-Club-311.htm", "https://sinwp.com/camera_clubs/Hayling-Island-Camera-Club-1203.htm", "https://sinwp.com/camera_clubs/Heacham-Digital-Camera-Club-1528.htm", "https://sinwp.com/camera_clubs/Hebden-Bridge-Camera-Club-312.htm", "https://sinwp.com/camera_clubs/Helston-Camera-Club-991.htm", "https://sinwp.com/camera_clubs/Hemel-Hempstead-Photographic-Society-314.htm", "https://sinwp.com/camera_clubs/Henfield-Camera-Club-777.htm", "https://sinwp.com/camera_clubs/Henley-Photographic-Club-1739.htm", "https://sinwp.com/camera_clubs/Herefordshire-Photographic-Society-315.htm", "https://sinwp.com/camera_clubs/Heritage-Centre-Camera-Group-1204.htm", "https://sinwp.com/camera_clubs/Herne-Bay-Photographic-Club-1091.htm", "https://sinwp.com/camera_clubs/Hertford-District-Camera-Club-1761.htm", "https://sinwp.com/camera_clubs/Hertford-and-District-Camera-Club-316.htm", "https://sinwp.com/camera_clubs/Heswall-Photographic-Society-317.htm", "https://sinwp.com/camera_clubs/Hexham-District-Photographic-Society-1885.htm", "https://sinwp.com/camera_clubs/Hexham-and-District-Photographic-Society-318.htm", "https://sinwp.com/camera_clubs/High-Wycombe-and-District-Camera-Club-820.htm", "https://sinwp.com/camera_clubs/Highcliffe-and-Infinity-Camera-Club-319.htm", "https://sinwp.com/camera_clubs/Highworth-Camera-Club-992.htm", "https://sinwp.com/camera_clubs/Hinckley-District-Photographic-Society-1856.htm", "https://sinwp.com/camera_clubs/Hinkley-District-Photographic-Society-1205.htm", "https://sinwp.com/camera_clubs/Hitchin-Camera-Club-321.htm", "https://sinwp.com/camera_clubs/Hoddesdon-Camera-Club-1472.htm", "https://sinwp.com/camera_clubs/Holme-and-District-Photographic-Society-323.htm", "https://sinwp.com/camera_clubs/Holmes-Chapel-Camera-Club-1430.htm", "https://sinwp.com/camera_clubs/Holmfirth-Camera-Club-324.htm", "https://sinwp.com/camera_clubs/Honiton-Camera-Club-993.htm", "https://sinwp.com/camera_clubs/Hordle-Photographic-Club-1206.htm", "https://sinwp.com/camera_clubs/Horley-Photographic-Club-778.htm", "https://sinwp.com/camera_clubs/Hornchurch-Photographic-Society-22.htm", "https://sinwp.com/camera_clubs/Horndean-Camera-Club-23.htm", "https://sinwp.com/camera_clubs/Horsforth-Photographic-Club-24.htm", "https://sinwp.com/camera_clubs/Horsforth-Photographic-Society-327.htm", "https://sinwp.com/camera_clubs/Horsham-Photographic-Society-779.htm", "https://sinwp.com/camera_clubs/Howden-Camera-Club-328.htm", "https://sinwp.com/camera_clubs/Hoylake-Photographic-Society-329.htm", "https://sinwp.com/camera_clubs/Huddersfield-Photographic-Society-331.htm", "https://sinwp.com/camera_clubs/Huddesfield-Camera-club-330.htm", "https://sinwp.com/camera_clubs/Hull-Photographic-Society-332.htm", "https://sinwp.com/camera_clubs/Hull-YPI-Camera-Club-333.htm", "https://sinwp.com/camera_clubs/Hungerford-Camera-Club-1936.htm", "https://sinwp.com/camera_clubs/Hunmanby-Camera-Club-1967.htm", "https://sinwp.com/camera_clubs/Hunstanton-District-Photographic-Society-26.htm", "https://sinwp.com/camera_clubs/Hyde-Photographic-Society-27.htm", "https://sinwp.com/camera_clubs/ICI-Merseyside-Photographic-Society-715.htm", "https://sinwp.com/camera_clubs/ICU-PhotoSoc-Imperial-College-London-1591.htm", "https://sinwp.com/camera_clubs/Ilkeston-Photo-2000-Club-1915.htm", "https://sinwp.com/camera_clubs/Ilkeston-Photographic-Club-28.htm", "https://sinwp.com/camera_clubs/Ilkley-Camera-Club-1431.htm", "https://sinwp.com/camera_clubs/ImageZ-Camera-Club-1208.htm", "https://sinwp.com/camera_clubs/Imprerial-Camera-Club-1567.htm", "https://sinwp.com/camera_clubs/Ingatestone-District-Camera-Club-1762.htm", "https://sinwp.com/camera_clubs/Ingatestone-and-District-Camera-Club-31.htm", "https://sinwp.com/camera_clubs/Invicta-Photographic-Club-1094.htm", "https://sinwp.com/camera_clubs/Ipswich-and-District-Photographic-Society-1601.htm", "https://sinwp.com/camera_clubs/Irwell-Vale-Photo-Club-1808.htm", "https://sinwp.com/camera_clubs/Isle-of-Man-Photographic-Society-35.htm", "https://sinwp.com/camera_clubs/Isle-of-Sheppey-Camera-Club-1092.htm", "https://sinwp.com/camera_clubs/Isle-of-Thanet-Photographic-Society-1093.htm", "https://sinwp.com/camera_clubs/Isle-of-Wight-Digital-Imaging-Group-1332.htm", "https://sinwp.com/camera_clubs/Isle-of-Wight-Photographic-Society-36.htm", "https://sinwp.com/camera_clubs/Isles-of-Scilly-Camera-Club-1654.htm", "https://sinwp.com/camera_clubs/Ivybridge-Camera-Club-995.htm", "https://sinwp.com/camera_clubs/Jaguar-Photographic-Society-37.htm", "https://sinwp.com/camera_clubs/James-Maude-Camera-Club-1543.htm", "https://sinwp.com/camera_clubs/John-Lewis-Camera-Club-1110.htm", "https://sinwp.com/camera_clubs/Kboro-Camera-Club-1481.htm", "https://sinwp.com/camera_clubs/KCLSU-Photosoc-1588.htm", "https://sinwp.com/camera_clubs/Kegworth-and-District-Photographic-Society-39.htm", "https://sinwp.com/camera_clubs/Keighley-and-District-Photographic-Association-40.htm", "https://sinwp.com/camera_clubs/Kempsey-Camera-Club-1857.htm", "https://sinwp.com/camera_clubs/Kempston-Camera-Club-41.htm", "https://sinwp.com/camera_clubs/Kendal-Photo-Club-1809.htm", "https://sinwp.com/camera_clubs/Kensington-Photographic-Society-44.htm", "https://sinwp.com/camera_clubs/Kent-Digital-Photographers-Group-1078.htm", "https://sinwp.com/camera_clubs/Keswick-Photographic-Society-45.htm", "https://sinwp.com/camera_clubs/Kettering-and-District-Photographic-Society-46.htm", "https://sinwp.com/camera_clubs/Kettering-Hospitals-Camera-Club-47.htm", "https://sinwp.com/camera_clubs/Keynsham-Photographic-Society-996.htm", "https://sinwp.com/camera_clubs/Keyworth-Camera-Club-48.htm", "https://sinwp.com/camera_clubs/kidderminster-Camera-Club-1370.htm", "https://sinwp.com/camera_clubs/Kidlington-Camera-Club-1111.htm", "https://sinwp.com/camera_clubs/Killamarsh-Amateur-Photographic-Society-1916.htm", "https://sinwp.com/camera_clubs/Killamarsh-Amateur-Photographic-Society-1432.htm", "https://sinwp.com/camera_clubs/Kineton-Camera-Club-1858.htm", "https://sinwp.com/camera_clubs/Kings-Lynn-Photography-Group-1536.htm", "https://sinwp.com/camera_clubs/Kings-Lynn-and-District-Camera-Club-337.htm", "https://sinwp.com/camera_clubs/Kings-Norton-Photographic-Society-338.htm", "https://sinwp.com/camera_clubs/Kingsbridge-District-Camera-Club-1210.htm", "https://sinwp.com/camera_clubs/Kingsclere-Photo-Club-1211.htm", "https://sinwp.com/camera_clubs/Kingsnorth-and-Grain-Camera-Club-1095.htm", "https://sinwp.com/camera_clubs/Kingston-Camera-Club-1212.htm", "https://sinwp.com/camera_clubs/Kingswood-Photographic-Society-705.htm", "https://sinwp.com/camera_clubs/Kinson-Camera-Club-1072.htm", "https://sinwp.com/camera_clubs/Kirkby-Photographic-Society-1810.htm", "https://sinwp.com/camera_clubs/Kirkbymoorside-District-Camera-Club-341.htm", "https://sinwp.com/camera_clubs/Kitty-Dancy-Room-1951.htm", "https://sinwp.com/camera_clubs/Knaresborough-Camera-Club-344.htm", "https://sinwp.com/camera_clubs/Knowle-Camera-Club-1475.htm", "https://sinwp.com/camera_clubs/Knutsford-Photographic-Society-346.htm", "https://sinwp.com/camera_clubs/Kodak-Works-Photographic-Society-1112.htm", "https://sinwp.com/camera_clubs/LESSA-Wandle-Camera-Club-816.htm", "https://sinwp.com/camera_clubs/LICS-Photographic-Society-347.htm", "https://sinwp.com/camera_clubs/Lacock-Positive-Camera-Club-1568.htm", "https://sinwp.com/camera_clubs/Lakenheath-Camera-Club-348.htm", "https://sinwp.com/camera_clubs/Laleham-Camera-Club-1343.htm", "https://sinwp.com/camera_clubs/Lancashire-Monochrome-1817.htm", "https://sinwp.com/camera_clubs/Lancaster-Photographic-Society-350.htm", "https://sinwp.com/camera_clubs/Launceston-Camera-Club-848.htm", "https://sinwp.com/camera_clubs/Lavenham-Camera-Club-1214.htm", "https://sinwp.com/camera_clubs/Lee-Valley-Nature-Photographers-353.htm", "https://sinwp.com/camera_clubs/Leeds-Co-operative-Photographic-Societys-1105.htm", "https://sinwp.com/camera_clubs/Leeds-Telephone-Area-Photographic-Society-355.htm", "https://sinwp.com/camera_clubs/Leek-Photographic-Club-356.htm", "https://sinwp.com/camera_clubs/Leica-Historical-Society-1433.htm", "https://sinwp.com/camera_clubs/Leicester-and-Leicestershire-Photographic-Society-357.htm", "https://sinwp.com/camera_clubs/Leicester-Forest-Photographic-Society-358.htm", "https://sinwp.com/camera_clubs/Leigh-and-District-Camera-Club-359.htm", "https://sinwp.com/camera_clubs/Leigh-on-Sea-Camera-Club-1216.htm", "https://sinwp.com/camera_clubs/Leighton-Buzzard-Photographic-Club-1113.htm", "https://sinwp.com/camera_clubs/Leominster-Photographic-Club-1218.htm", "https://sinwp.com/camera_clubs/Letchworth-Camera-Club-1219.htm", "https://sinwp.com/camera_clubs/Letchworth-Garden-City-Camera-Club-1764.htm", "https://sinwp.com/camera_clubs/Lewes-Camera-Club-781.htm", "https://sinwp.com/camera_clubs/Leyland-Photographic-Society-1813.htm", "https://sinwp.com/camera_clubs/Leyton-Camera-Club-738.htm", "https://sinwp.com/camera_clubs/Lichfield-Camera-Club-365.htm", "https://sinwp.com/camera_clubs/Lincoln-Camera-Club-366.htm", "https://sinwp.com/camera_clubs/Lincombe-Barn-Camera-Club-1000.htm", "https://sinwp.com/camera_clubs/Linton-Camera-Club-1542.htm", "https://sinwp.com/camera_clubs/Liskeard-District-Camera-Club-1048.htm", "https://sinwp.com/camera_clubs/Little-Common-Photographic-Club-1325.htm", "https://sinwp.com/camera_clubs/Littlehampton-and-District-Camera-Club-782.htm", "https://sinwp.com/camera_clubs/Locks-Heath-and-Sarisbury-Camera-Club-373.htm", "https://sinwp.com/camera_clubs/Locksheath-Sarisbury-Camera-Club-1337.htm", "https://sinwp.com/camera_clubs/London-Photography-Meetup-Group-1385.htm", "https://sinwp.com/camera_clubs/London-Strobist-1384.htm", "https://sinwp.com/camera_clubs/Long-Eaton-Camera-Club-374.htm", "https://sinwp.com/camera_clubs/Long-Mynd-Camera-Club-375.htm", "https://sinwp.com/camera_clubs/Loughborough-Photographic-Society-377.htm", "https://sinwp.com/camera_clubs/Loughton-Camera-Club-378.htm", "https://sinwp.com/camera_clubs/Louth-Photographic-Society-1917.htm", "https://sinwp.com/camera_clubs/Lowedges-Photographic-Society-379.htm", "https://sinwp.com/camera_clubs/Lowestoft-Camera-Club-380.htm", "https://sinwp.com/camera_clubs/Lowestoft-Photographic-Club-1220.htm", "https://sinwp.com/camera_clubs/Ludlow-Photographic-Club-382.htm", "https://sinwp.com/camera_clubs/Ludshott-Photographic-Club-783.htm", "https://sinwp.com/camera_clubs/Lunsdale-Camera-Club-1816.htm", "https://sinwp.com/camera_clubs/Luton-District-Camera-Club-1222.htm", "https://sinwp.com/camera_clubs/Luton-and-dunstable-Photographic-club-742.htm", "https://sinwp.com/camera_clubs/Lutterworth-Photographic-Society-385.htm", "https://sinwp.com/camera_clubs/Lyme-Bay-Photographic-Club-1002.htm", "https://sinwp.com/camera_clubs/Lymington-Camera-Club-386.htm", "https://sinwp.com/camera_clubs/Lymm-Photographic-Society-1637.htm", "https://sinwp.com/camera_clubs/Lytham-St-Annes-1815.htm", "https://sinwp.com/camera_clubs/Macclesfield-Camera-Club-388.htm", "https://sinwp.com/camera_clubs/Maghull-Photography-Club-389.htm", "https://sinwp.com/camera_clubs/Maidenhead-Camera-Club-1114.htm", "https://sinwp.com/camera_clubs/Maidstone-Camera-Club-877.htm", "https://sinwp.com/camera_clubs/Malden-Camera-Club-1547.htm", "https://sinwp.com/camera_clubs/Maldon-Camera-Club-392.htm", "https://sinwp.com/camera_clubs/Malling-Photographic-Society-1079.htm", "https://sinwp.com/camera_clubs/Malton-and-District-Camera-Club-1968.htm", "https://sinwp.com/camera_clubs/Manchester-Amateur-Photographic-Society-394.htm", "https://sinwp.com/camera_clubs/Manningtree-and-District-Photographic-Society-1529.htm", "https://sinwp.com/camera_clubs/Mansfield-and-District-Photographic-Society-395.htm", "https://sinwp.com/camera_clubs/March-Camera-Club-1765.htm", "https://sinwp.com/camera_clubs/Marconi-Photographic-Society-391.htm", "https://sinwp.com/camera_clubs/Marlow-Camera-Club-1115.htm", "https://sinwp.com/camera_clubs/Marwell-Photographic-Group-1939.htm", "https://sinwp.com/camera_clubs/Marwell-Zoological-Society-Photographic-Group-397.htm", "https://sinwp.com/camera_clubs/Massey-Ferguson-Photographic-Society-399.htm", "https://sinwp.com/camera_clubs/Masterclass-Photography-Herts-Beds-1228.htm", "https://sinwp.com/camera_clubs/Matlock-Camera-Club-721.htm", "https://sinwp.com/camera_clubs/Medway-DSLR-Camera-Club-1562.htm", "https://sinwp.com/camera_clubs/Melbourn-Photographic-Society-402.htm", "https://sinwp.com/camera_clubs/Melbourne-Photographic-Society-1918.htm", "https://sinwp.com/camera_clubs/Melton-Mowbray-Photographic-Society-403.htm", "https://sinwp.com/camera_clubs/Mersea-Island-Photographic-Society-404.htm", "https://sinwp.com/camera_clubs/Mexborough-Photographic-Society-406.htm", "https://sinwp.com/camera_clubs/Mid-Somerset-Camera-Club-1003.htm", "https://sinwp.com/camera_clubs/Mid-Sussex-Camera-Club-785.htm", "https://sinwp.com/camera_clubs/Mid-Cheshire-Camera-Club-1476.htm", "https://sinwp.com/camera_clubs/Mid-Somerset-Camera-Club-1230.htm", "https://sinwp.com/camera_clubs/Middleton-Camera-Club-784.htm", "https://sinwp.com/camera_clubs/Midhurst-Camera-Club-410.htm", "https://sinwp.com/camera_clubs/Midland-Counties-Photographic-Federation-851.htm", "https://sinwp.com/camera_clubs/MidThamesAVGroup-1953.htm", "https://sinwp.com/camera_clubs/Mill-Camera-Group-1766.htm", "https://sinwp.com/camera_clubs/Minehead-and-District-Camera-Club-1004.htm", "https://sinwp.com/camera_clubs/Mirage-Group-786.htm", "https://sinwp.com/camera_clubs/Mitcham-Camera-Club-1231.htm", "https://sinwp.com/camera_clubs/Molesey-Photographic-Club-788.htm", "https://sinwp.com/camera_clubs/Monton-and-Winton-Photographic-Society-416.htm", "https://sinwp.com/camera_clubs/Moore-Camera-Club-1137.htm", "https://sinwp.com/camera_clubs/Morden-Camera-Club-789.htm", "https://sinwp.com/camera_clubs/Morecambe-Camera-Club-418.htm", "https://sinwp.com/camera_clubs/Morley-Camera-Club-419.htm", "https://sinwp.com/camera_clubs/Morpeth-Camera-Club-1887.htm", "https://sinwp.com/camera_clubs/Morton-Photographic-Society-1888.htm", "https://sinwp.com/camera_clubs/Moulsham-Lodge-Camera-Club-424.htm", "https://sinwp.com/camera_clubs/Muswell-Hill-Photographic-Society-739.htm", "https://sinwp.com/camera_clubs/Nantwich-Camera-Club-428.htm", "https://sinwp.com/camera_clubs/Natural-World-Photographic-Society-1344.htm", "https://sinwp.com/camera_clubs/Nelson-Camera-Club-430.htm", "https://sinwp.com/camera_clubs/New-City-Photographic-Society-1740.htm", "https://sinwp.com/camera_clubs/New-Earswick-Camera-Club-432.htm", "https://sinwp.com/camera_clubs/New-Forest-Camera-Club-1235.htm", "https://sinwp.com/camera_clubs/New-Image-Camera-Club-1969.htm", "https://sinwp.com/camera_clubs/Newark-District-Photographic-Society-1920.htm", "https://sinwp.com/camera_clubs/Newbury-Camera-Club-1236.htm", "https://sinwp.com/camera_clubs/Newcastle-Shropshire-Photographic-Society-437.htm", "https://sinwp.com/camera_clubs/Newcastle-Camera-Club-1859.htm", "https://sinwp.com/camera_clubs/NEWDIG-North-East-Wessex-Digital-Imaging-Group-1237.htm", "https://sinwp.com/camera_clubs/Newport-Photographic-Club-440.htm", "https://sinwp.com/camera_clubs/Newton-Abbot-Photographic-Club-1005.htm", "https://sinwp.com/camera_clubs/Nikon-Owners-Club-International-1434.htm", "https://sinwp.com/camera_clubs/Niton-and-District-Camera-Club-1331.htm", "https://sinwp.com/camera_clubs/Norfolk-Photographic-Group-425.htm", "https://sinwp.com/camera_clubs/Normanton-Camera-Club-441.htm", "https://sinwp.com/camera_clubs/North-Birmingham-Photographic-Society-1861.htm", "https://sinwp.com/camera_clubs/North-Cheshire-Photographic-Society-442.htm", "https://sinwp.com/camera_clubs/North-East-Lincolnshire-Photographic-Society-443.htm", "https://sinwp.com/camera_clubs/North-Essex-Photographic-Workshop-1603.htm", "https://sinwp.com/camera_clubs/North-Fylde-Photographic-Society-1824.htm", "https://sinwp.com/camera_clubs/North-Manchester-Camera-Club-444.htm", "https://sinwp.com/camera_clubs/North-Norfolk-Camera-Club-445.htm", "https://sinwp.com/camera_clubs/North-Norfolk-Photographic-Society-1435.htm", "https://sinwp.com/camera_clubs/North-Reading-Photographic-Club-1118.htm", "https://sinwp.com/camera_clubs/North-Romford-Photographic-Society-446.htm", "https://sinwp.com/camera_clubs/North-Shields-Photographic-Society-447.htm", "https://sinwp.com/camera_clubs/North-Walsham-Photographic-Group-448.htm", "https://sinwp.com/camera_clubs/North-West-Bristol-Camera-Club-1006.htm", "https://sinwp.com/camera_clubs/North-West-Monochrome-Group-1060.htm", "https://sinwp.com/camera_clubs/Northallerton-Camera-Club-449.htm", "https://sinwp.com/camera_clubs/Northampton-Camera-Club-1539.htm", "https://sinwp.com/camera_clubs/Northampton-Natural-History-Society-1862.htm", "https://sinwp.com/camera_clubs/Northfields-Camera-Club-1117.htm", "https://sinwp.com/camera_clubs/Northwich-Photographic-Society-717.htm", "https://sinwp.com/camera_clubs/Norton-Radstock-Photographic-Society-1007.htm", "https://sinwp.com/camera_clubs/Norwich-and-District-Photographic-Society-453.htm", "https://sinwp.com/camera_clubs/Norwich-Photographic-Workshop-454.htm", "https://sinwp.com/camera_clubs/Notting-Hill-Photographic-Club-1436.htm", "https://sinwp.com/camera_clubs/Nottingham-Nottinghamshire-Photographic-Soc-455.htm", "https://sinwp.com/camera_clubs/Nottingham-City-Transport-Photographic-Society-456.htm", "https://sinwp.com/camera_clubs/Nottingham-Co-Op-Camera-Club-457.htm", "https://sinwp.com/camera_clubs/Nottingham-Outlaws-Photographic-Society-458.htm", "https://sinwp.com/camera_clubs/Nottinghamshire-Wildlife-Photography-Society-1560.htm", "https://sinwp.com/camera_clubs/Nuneaton-Photographic-Society-459.htm", "https://sinwp.com/camera_clubs/Oadby-Camera-Club-460.htm", "https://sinwp.com/camera_clubs/Oakham-Photography-Club-1655.htm", "https://sinwp.com/camera_clubs/Oakley-Camera-Club-281.htm", "https://sinwp.com/camera_clubs/Oare-and-St-Helens-Camera-Club-1326.htm", "https://sinwp.com/camera_clubs/Okehampton-District-Camera-Club-1008.htm", "https://sinwp.com/camera_clubs/Old-Coulsdon-Camera-Club-1240.htm", "https://sinwp.com/camera_clubs/Oldham-Camera-Club-463.htm", "https://sinwp.com/camera_clubs/Oldham-Camera-Club-1826.htm", "https://sinwp.com/camera_clubs/Oldham-Photographic-Society-464.htm", "https://sinwp.com/camera_clubs/Oldham-Photographic-Society-1825.htm", "https://sinwp.com/camera_clubs/Olney-Camera-Club-1530.htm", "https://sinwp.com/camera_clubs/Olympus-Camera-Club-1437.htm", "https://sinwp.com/camera_clubs/OMT-Photographic-Society-1119.htm", "https://sinwp.com/camera_clubs/Ongar-Photographic-Society-465.htm", "https://sinwp.com/camera_clubs/Ordnance-Survey-Photographic-Society-466.htm", "https://sinwp.com/camera_clubs/Ore-and-St-Helens-Camera-Club-1783.htm", "https://sinwp.com/camera_clubs/Ormskirk-Camera-Club-467.htm", "https://sinwp.com/camera_clubs/Orpington-Photographic-Society-811.htm", "https://sinwp.com/camera_clubs/Ossett-District-Camera-Club-1970.htm", "https://sinwp.com/camera_clubs/Oswestry-Photgraphic-Society-469.htm", "https://sinwp.com/camera_clubs/Otley-Camera-Club-470.htm", "https://sinwp.com/camera_clubs/Overton-Photographic-Club-1242.htm", "https://sinwp.com/camera_clubs/Oxford-Brookes-Photographic-Society-1639.htm", "https://sinwp.com/camera_clubs/Oxford-Photographic-Society-1243.htm", "https://sinwp.com/camera_clubs/OXPHO-Photographic-Society-1523.htm", "https://sinwp.com/camera_clubs/PACC-Photo-Adventure-Camera-Club-1438.htm", "https://sinwp.com/camera_clubs/Padiham-and-District-Photographic-Society-718.htm", "https://sinwp.com/camera_clubs/Paignton-Photographic-Club-1009.htm", "https://sinwp.com/camera_clubs/Park-Street-Camera-Club-1244.htm", "https://sinwp.com/camera_clubs/Parkwood-Camera-Club-1245.htm", "https://sinwp.com/camera_clubs/Pen-and-Camera-Club-of-Methodism-475.htm", "https://sinwp.com/camera_clubs/Penge-Photographic-Club-812.htm", "https://sinwp.com/camera_clubs/Penistone-Camera-Club-477.htm", "https://sinwp.com/camera_clubs/Penrith-District-Camera-Club-1891.htm", "https://sinwp.com/camera_clubs/Penryn-Camera-Club-1010.htm", "https://sinwp.com/camera_clubs/Penwith-Photographic-Group-1011.htm", "https://sinwp.com/camera_clubs/Penzance-Camera-Club-1570.htm", "https://sinwp.com/camera_clubs/Perkins-Photographic-Club-1473.htm", "https://sinwp.com/camera_clubs/Peterborough-Photographic-Society-480.htm", "https://sinwp.com/camera_clubs/Petersfield-Photographic-Society-482.htm", "https://sinwp.com/camera_clubs/Peugot-Camera-Club-483.htm", "https://sinwp.com/camera_clubs/Phoenix-Camera-Club-Holsworthy-1569.htm", "https://sinwp.com/camera_clubs/Phoenix-Group-1767.htm", "https://sinwp.com/camera_clubs/Phoenix-Group-of-Photographers-1012.htm", "https://sinwp.com/camera_clubs/Photo-Adventuer-Camera-Club-1247.htm", "https://sinwp.com/camera_clubs/Photo-Digital-Club-1327.htm", "https://sinwp.com/camera_clubs/Photo-Group-1249.htm", "https://sinwp.com/camera_clubs/Photocraft-Camera-Club-of-Wallington-791.htm", "https://sinwp.com/camera_clubs/Photographic-Collectors-Club-of-Great-Britain-1439.htm", "https://sinwp.com/camera_clubs/Photographic-Imaging-Co-operative-1768.htm", "https://sinwp.com/camera_clubs/Picture-this-Photo-Club-1971.htm", "https://sinwp.com/camera_clubs/Pinchbeck-Photographic-Group-1923.htm", "https://sinwp.com/camera_clubs/Pinner-Camera-Club-1122.htm", "https://sinwp.com/camera_clubs/Plymouth-Athenaeum-Photographic-Society-1571.htm", "https://sinwp.com/camera_clubs/Plymouth-Camera-Club-1441.htm", "https://sinwp.com/camera_clubs/Plymstock-Camera-Club-1016.htm", "https://sinwp.com/camera_clubs/Pocklington-Camera-Club-487.htm", "https://sinwp.com/camera_clubs/Pontefract-Camera-Club-488.htm", "https://sinwp.com/camera_clubs/Ponteland-Photographic-Society-489.htm", "https://sinwp.com/camera_clubs/Poole-and-District-Camera-Club-490.htm", "https://sinwp.com/camera_clubs/Portishead-Camera-Club-1017.htm", "https://sinwp.com/camera_clubs/Portland-Camera-Club-1559.htm", "https://sinwp.com/camera_clubs/Portsmouth-Camera-Club-492.htm", "https://sinwp.com/camera_clubs/Portsmouth-Imaging-Club-1251.htm", "https://sinwp.com/camera_clubs/Positive-Image-1972.htm", "https://sinwp.com/camera_clubs/Potters-Bar-District-Photographic-Society-1769.htm", "https://sinwp.com/camera_clubs/Potters-Bar-Distreict-Photographic-Society-494.htm", "https://sinwp.com/camera_clubs/Poulton-le-Fylde-Photographic-Society-495.htm", "https://sinwp.com/camera_clubs/Preston-Photographic-Club-Paignton-1572.htm", "https://sinwp.com/camera_clubs/Preston-Photographic-Society-496.htm", "https://sinwp.com/camera_clubs/Prestwich-Camera-Club-1827.htm", "https://sinwp.com/camera_clubs/Princes-Risborough-Photographic-Society-1123.htm", "https://sinwp.com/camera_clubs/Print-Project-Group-1924.htm", "https://sinwp.com/camera_clubs/Prismatic-Photographic-Club-1956.htm", "https://sinwp.com/camera_clubs/Pudsey-Camera-Club-499.htm", "https://sinwp.com/camera_clubs/Queensberry-Camera-Club-502.htm", "https://sinwp.com/camera_clubs/Quekett-Microscopical-Club-792.htm", "https://sinwp.com/camera_clubs/RPR-Camera-Club-504.htm", "https://sinwp.com/camera_clubs/Rayleigh-Camera-Club-506.htm", "https://sinwp.com/camera_clubs/Raynes-Park-Camera-Club-793.htm", "https://sinwp.com/camera_clubs/RB-Camera-Club-Lincoln-and-District-507.htm", "https://sinwp.com/camera_clubs/Reading-Camera-Club-508.htm", "https://sinwp.com/camera_clubs/Reading-University-Photographic-Society-1534.htm", "https://sinwp.com/camera_clubs/Redditch-Photographic-Society-509.htm", "https://sinwp.com/camera_clubs/Reepham-District-Photographic-Club-1770.htm", "https://sinwp.com/camera_clubs/Reepham-and-District-Photographic-Club-510.htm", "https://sinwp.com/camera_clubs/Reflex-Camera-Club-1513.htm", "https://sinwp.com/camera_clubs/Reflex-Photographic-Club-1256.htm", "https://sinwp.com/camera_clubs/Reigate-Photographic-Society-794.htm", "https://sinwp.com/camera_clubs/Resound-Camera-Club-1573.htm", "https://sinwp.com/camera_clubs/Retford-and-District-Photographic-Society-512.htm", "https://sinwp.com/camera_clubs/Richmond-Twickenham-Photographic-Society-1257.htm", "https://sinwp.com/camera_clubs/Richmond-Camera-Club-1258.htm", "https://sinwp.com/camera_clubs/Riding-Mill-Photographic-Society-1640.htm", "https://sinwp.com/camera_clubs/Ringwood-Camera-Club-1602.htm", "https://sinwp.com/camera_clubs/Ripon-City-Photographic-Society-518.htm", "https://sinwp.com/camera_clubs/Riverside-Camera-Club-1096.htm", "https://sinwp.com/camera_clubs/Roby-Mill-Community-Camera-Club-1831.htm", "https://sinwp.com/camera_clubs/Rocester-and-District-Camera-Club-1642.htm", "https://sinwp.com/camera_clubs/Rochdale-and-District-Camera-Club-1830.htm", "https://sinwp.com/camera_clubs/Rochdale-Photographic-Society-1829.htm", "https://sinwp.com/camera_clubs/Rochdale-Photographic-Society-520.htm", "https://sinwp.com/camera_clubs/Roehampton-Photography-Society-1583.htm", "https://sinwp.com/camera_clubs/Rolls-Royce-Derby-Photographic-Society-722.htm", "https://sinwp.com/camera_clubs/Romford-Camera-Club-1771.htm", "https://sinwp.com/camera_clubs/Romford-Photographic-Society-521.htm", "https://sinwp.com/camera_clubs/Romiley-Camera-Club-1832.htm", "https://sinwp.com/camera_clubs/Romiley-Photo-And-Digital-Club-1176.htm", "https://sinwp.com/camera_clubs/Romney-Marsh-Photographic-Club-1281.htm", "https://sinwp.com/camera_clubs/Ross-on-Wye-Photographic-Society-1443.htm", "https://sinwp.com/camera_clubs/Rossington-Camera-Club-522.htm", "https://sinwp.com/camera_clubs/Rothbury-and-District-Photographic-Society-524.htm", "https://sinwp.com/camera_clubs/Rotherham-Photographic-Society-525.htm", "https://sinwp.com/camera_clubs/Rottingdean-Camera-Club-796.htm", "https://sinwp.com/camera_clubs/Royal-Holloways-Photography-Society-1592.htm", "https://sinwp.com/camera_clubs/Royston-Photographic-Society-526.htm", "https://sinwp.com/camera_clubs/Rugeley-and-Armitage-Camera-Club-1863.htm", "https://sinwp.com/camera_clubs/Runcorn-Phoenix-Photographic-Society-527.htm", "https://sinwp.com/camera_clubs/Rushcliffe-Photographic-Society-528.htm", "https://sinwp.com/camera_clubs/Rushden-and-District-Photographic-Society-529.htm", "https://sinwp.com/camera_clubs/Ruskin-Camera-Club-530.htm", "https://sinwp.com/camera_clubs/Russel-Street-Photographic-Society-531.htm", "https://sinwp.com/camera_clubs/Ryburn-Photographic-Studios-532.htm", "https://sinwp.com/camera_clubs/Ryde-Imaging-Group-1933.htm", "https://sinwp.com/camera_clubs/Rye-and-District-Camera-Club-1097.htm", "https://sinwp.com/camera_clubs/Ryton-and-District-Camera-Club-534.htm", "https://sinwp.com/camera_clubs/Saddleworth-Camera-Club-535.htm", "https://sinwp.com/camera_clubs/Saffron-Walden-Camera-Club-536.htm", "https://sinwp.com/camera_clubs/Sale-Photographic-Society-537.htm", "https://sinwp.com/camera_clubs/Salford-Photographic-Society-538.htm", "https://sinwp.com/camera_clubs/Salisbury-Camera-Club-1263.htm", "https://sinwp.com/camera_clubs/Saltash-and-District-Camera-Club-1019.htm", "https://sinwp.com/camera_clubs/Sandbach-Photographic-Society-1833.htm", "https://sinwp.com/camera_clubs/Sandown-Shanklin-and-District-Camera-Club-542.htm", "https://sinwp.com/camera_clubs/Scarborough-Photographic-Society-1973.htm", "https://sinwp.com/camera_clubs/Scartho-Village-Community-Centre-1913.htm", "https://sinwp.com/camera_clubs/Scunthorpe-Camera-Club-543.htm", "https://sinwp.com/camera_clubs/Seaford-Photographic-society-1264.htm", "https://sinwp.com/camera_clubs/Sedgemoor-Camera-Club-1957.htm", "https://sinwp.com/camera_clubs/Sedgley-Camera-Club-544.htm", "https://sinwp.com/camera_clubs/Seham-Photographic-Society-545.htm", "https://sinwp.com/camera_clubs/Selby-Camera-Club-546.htm", "https://sinwp.com/camera_clubs/Selsey-Camera-Club-1945.htm", "https://sinwp.com/camera_clubs/Senior-Photographers-1267.htm", "https://sinwp.com/camera_clubs/Settle-Photographic-Group-1974.htm", "https://sinwp.com/camera_clubs/Seven-Sisters-Camera-Club-1552.htm", "https://sinwp.com/camera_clubs/Sevenoaks-Camera-Club-1268.htm", "https://sinwp.com/camera_clubs/Shaftesbury-Camera-Club-1575.htm", "https://sinwp.com/camera_clubs/Shafton-and-District-Photographic-Society-549.htm", "https://sinwp.com/camera_clubs/Sheffield-Photographic-Society-550.htm", "https://sinwp.com/camera_clubs/Shefford-and-District-Camera-Club-551.htm", "https://sinwp.com/camera_clubs/Shell-Club-Photographic-Section-552.htm", "https://sinwp.com/camera_clubs/Shepshed-and-District-Camera-Club-554.htm", "https://sinwp.com/camera_clubs/Sherborne-Bradford-Abbas-Camera-Club-1021.htm", "https://sinwp.com/camera_clubs/Sherburn-Camera-Club-555.htm", "https://sinwp.com/camera_clubs/Shillington-District-Camera-Club-1772.htm", "https://sinwp.com/camera_clubs/Shillington-and-District-Camera-Club-556.htm", "https://sinwp.com/camera_clubs/Shirley-Photographic-Society-557.htm", "https://sinwp.com/camera_clubs/Shropshire-Photographic-Society-558.htm", "https://sinwp.com/camera_clubs/Sidmouth-District-Photographic-Club-1022.htm", "https://sinwp.com/camera_clubs/Sileby-Photographic-Society-559.htm", "https://sinwp.com/camera_clubs/Sittingbourne-Photographic-Society-1099.htm", "https://sinwp.com/camera_clubs/Skipton-Camera-Club-560.htm", "https://sinwp.com/camera_clubs/Sleaford-District-Photographic-Group-1927.htm", "https://sinwp.com/camera_clubs/SLIC-Photography-and-Digital-Imaging-Club-1838.htm", "https://sinwp.com/camera_clubs/Smethwick-Camera-Club-855.htm", "https://sinwp.com/camera_clubs/Smethwick-Photographic-Society-562.htm", "https://sinwp.com/camera_clubs/Sodbury-Yate-Photographic-Society-1023.htm", "https://sinwp.com/camera_clubs/Solent-Camera-Club-563.htm", "https://sinwp.com/camera_clubs/Solihull-Photographic-Society-565.htm", "https://sinwp.com/camera_clubs/South-Birmingham-Photographic-Society-566.htm", "https://sinwp.com/camera_clubs/South-Derbyshire-Camera-Club-567.htm", "https://sinwp.com/camera_clubs/South-Liverpool-Photographic-Society-568.htm", "https://sinwp.com/camera_clubs/South-London-Photographic-Society-814.htm", "https://sinwp.com/camera_clubs/South-Manchester-Camera-Club-569.htm", "https://sinwp.com/camera_clubs/South-Normanton-Camera-Club-570.htm", "https://sinwp.com/camera_clubs/South-Petherton-Photographic-Society-1024.htm", "https://sinwp.com/camera_clubs/South-Reading-Camera-Club-1275.htm", "https://sinwp.com/camera_clubs/South-Shields-Photographic-Society-572.htm", "https://sinwp.com/camera_clubs/South-Woodham-Ferrers-Camera-Club-573.htm", "https://sinwp.com/camera_clubs/South-Yorkshire-Photographic-Society-1635.htm", "https://sinwp.com/camera_clubs/Southampton-Camera-Club-574.htm", "https://sinwp.com/camera_clubs/Southampton-Students-Union-Photo-Society-1586.htm", "https://sinwp.com/camera_clubs/Southend-On-Sea-Photographic-Society-575.htm", "https://sinwp.com/camera_clubs/Southern-Electric-S-SC-Photographic-Society-576.htm", "https://sinwp.com/camera_clubs/Southern-Photographic-Society-1834.htm", "https://sinwp.com/camera_clubs/Southgate-Photographic-Society-740.htm", "https://sinwp.com/camera_clubs/Southport-Photographic-Society-577.htm", "https://sinwp.com/camera_clubs/Southwick-Camera-Club-800.htm", "https://sinwp.com/camera_clubs/Sowerby-Bridge-and-District-Photographic-Society-578.htm", "https://sinwp.com/camera_clubs/Spalding-Photographic-Society-579.htm", "https://sinwp.com/camera_clubs/Spencer-Dallington-Camera-Club-581.htm", "https://sinwp.com/camera_clubs/Sphinx-Photographic-Club-1864.htm", "https://sinwp.com/camera_clubs/Spires-Photographic-Society-582.htm", "https://sinwp.com/camera_clubs/Spondon-Camera-Club-723.htm", "https://sinwp.com/camera_clubs/Spratton-Photography-Group-1865.htm", "https://sinwp.com/camera_clubs/Springfield-Camera-Club-583.htm", "https://sinwp.com/camera_clubs/SRGB-Photo-Group-1839.htm", "https://sinwp.com/camera_clubs/St-Agnes-Photographic-Club-1574.htm", "https://sinwp.com/camera_clubs/St-Austell-Camera-Club-1541.htm", "https://sinwp.com/camera_clubs/St-Ives-Photographic-Club-1278.htm", "https://sinwp.com/camera_clubs/St-Neots-District-CC-1279.htm", "https://sinwp.com/camera_clubs/St-Albans-District-Photographic-Society-1124.htm", "https://sinwp.com/camera_clubs/St-Helens-Camera-Club-1484.htm", "https://sinwp.com/camera_clubs/St-Helens-Camera-Club-585.htm", "https://sinwp.com/camera_clubs/St-Ives-Photographic-Club-587.htm", "https://sinwp.com/camera_clubs/Stafford-Camera-Club-588.htm", "https://sinwp.com/camera_clubs/Stafford-Photographic-Society-590.htm", "https://sinwp.com/camera_clubs/Staffordshire-Audio-Visual-Group-1866.htm", "https://sinwp.com/camera_clubs/Stagecoach-Camera-Club-1138.htm", "https://sinwp.com/camera_clubs/Stalybridge-Photographic-Club-591.htm", "https://sinwp.com/camera_clubs/Stamford-Photographic-Society-1928.htm", "https://sinwp.com/camera_clubs/Stanhope-Photographic-Society-1895.htm", "https://sinwp.com/camera_clubs/Stanhope-Photographic-Society-592.htm", "https://sinwp.com/camera_clubs/Stanley-Camera-Club-593.htm", "https://sinwp.com/camera_clubs/Stapleford-Community-Association-Camera-Club-594.htm", "https://sinwp.com/camera_clubs/Staplehurst-Photographic-Society-1082.htm", "https://sinwp.com/camera_clubs/Stead-McAlpine-Camera-Club-595.htm", "https://sinwp.com/camera_clubs/Stevenage-Photographic-Society-1282.htm", "https://sinwp.com/camera_clubs/Steyning-Camera-Club-1283.htm", "https://sinwp.com/camera_clubs/Stockport-Photographic-Society-599.htm", "https://sinwp.com/camera_clubs/Stockport-Photographic-Society-1836.htm", "https://sinwp.com/camera_clubs/Stocksbridge-Photographic-Society-821.htm", "https://sinwp.com/camera_clubs/Stockton-Camera-Club-1896.htm", "https://sinwp.com/camera_clubs/Stockton-On-Tees-Photo-Colour-Society-752.htm", "https://sinwp.com/camera_clubs/Stoke-Poges-Photographic-Club-1284.htm", "https://sinwp.com/camera_clubs/Stoke-On-Trent-Camera-Club-601.htm", "https://sinwp.com/camera_clubs/Stokenchurch-Camera-Club-1126.htm", "https://sinwp.com/camera_clubs/Stokesley-Photographic-Society-602.htm", "https://sinwp.com/camera_clubs/Storrington-Camera-Club-1285.htm", "https://sinwp.com/camera_clubs/Stothert-Pitt-Camera-Club-1026.htm", "https://sinwp.com/camera_clubs/Stourbridge-Photographic-Society-603.htm", "https://sinwp.com/camera_clubs/Stourport-Camera-Club-604.htm", "https://sinwp.com/camera_clubs/Stowe-and-District-Photographic-Club-605.htm", "https://sinwp.com/camera_clubs/Stowmarket-District-Camera-Club-606.htm", "https://sinwp.com/camera_clubs/Stratford-upon-Avon-Photographic-Club-607.htm", "https://sinwp.com/camera_clubs/Stroud-Camera-Club-1027.htm", "https://sinwp.com/camera_clubs/Sudbury-and-District-Camera-Club-610.htm", "https://sinwp.com/camera_clubs/Suffolk-Creative-Photographic-Group-1531.htm", "https://sinwp.com/camera_clubs/Suffolk-Monochrome-Group-1775.htm", "https://sinwp.com/camera_clubs/Sunderland-Photographic-Association-611.htm", "https://sinwp.com/camera_clubs/Sunningdale-Ascot-Camera-Club-1287.htm", "https://sinwp.com/camera_clubs/Sutton-Coldfield-Photographic-Society-612.htm", "https://sinwp.com/camera_clubs/Sutton-Photographic-Group-1776.htm", "https://sinwp.com/camera_clubs/Swaffham-Camera-Club-1777.htm", "https://sinwp.com/camera_clubs/Swavesey-Camera-Club-613.htm", "https://sinwp.com/camera_clubs/Sway-Camera-Club-1338.htm", "https://sinwp.com/camera_clubs/Swindon-Imaging-Group-1444.htm", "https://sinwp.com/camera_clubs/Swindon-Photographic-Society-1028.htm", "https://sinwp.com/camera_clubs/Swinton-and-District-Photographic-Society-1837.htm", "https://sinwp.com/camera_clubs/Tadley-District-Photography-Club-616.htm", "https://sinwp.com/camera_clubs/Tame-Photographic-Society-617.htm", "https://sinwp.com/camera_clubs/Tamworth-Photographic-Club-618.htm", "https://sinwp.com/camera_clubs/Tandridge-Photographic-Society-802.htm", "https://sinwp.com/camera_clubs/Taunton-Camera-Club-859.htm", "https://sinwp.com/camera_clubs/Taurus-Photographic-Club-234.htm", "https://sinwp.com/camera_clubs/Tavistock-Camera-Club-1031.htm", "https://sinwp.com/camera_clubs/Teesdale-Photographic-Society-1135.htm", "https://sinwp.com/camera_clubs/Temeside-Camera-Club-619.htm", "https://sinwp.com/camera_clubs/Tettenhall-Wood-Photographic-Club-621.htm", "https://sinwp.com/camera_clubs/Thame-Camera-Club-1127.htm", "https://sinwp.com/camera_clubs/Thatcham-Photographic-Club-1445.htm", "https://sinwp.com/camera_clubs/The-Practical-Camera-Club-of-Southampton-1334.htm", "https://sinwp.com/camera_clubs/The-35-Mill-Camera-Club-623.htm", "https://sinwp.com/camera_clubs/The-Ashby-Photographic-Group-1904.htm", "https://sinwp.com/camera_clubs/The-Barnes-Photographic-Society-1633.htm", "https://sinwp.com/camera_clubs/The-Brelu-Brelu-Photographic-Group-1903.htm", "https://sinwp.com/camera_clubs/The-British-Society-of-Underwater-Photographers-1446.htm", "https://sinwp.com/camera_clubs/The-Camera-Club-1291.htm", "https://sinwp.com/camera_clubs/The-City-Camera-Club-1292.htm", "https://sinwp.com/camera_clubs/The-Darlington-Association-of-Photographers-1880.htm", "https://sinwp.com/camera_clubs/The-Disabled-Photographers-Society-1419.htm", "https://sinwp.com/camera_clubs/The-East-London-Fun-Photography-Club-TELFPC-1582.htm", "https://sinwp.com/camera_clubs/The-Evolve-Group-1801.htm", "https://sinwp.com/camera_clubs/The-f4-Photographic-Group-1964.htm", "https://sinwp.com/camera_clubs/The-Fosse-Co-op-Camera-Club-269.htm", "https://sinwp.com/camera_clubs/The-Hill-Camera-Club-1621.htm", "https://sinwp.com/camera_clubs/The-Icon-Group-1293.htm", "https://sinwp.com/camera_clubs/The-Leeds-Photographic-Society-1634.htm", "https://sinwp.com/camera_clubs/The-Leica-Society-1448.htm", "https://sinwp.com/camera_clubs/The-Northern-Audio-Visual-Group-1890.htm", "https://sinwp.com/camera_clubs/The-Northolt-District-Photographic-Society-1294.htm", "https://sinwp.com/camera_clubs/The-Portfolio-Group-1576.htm", "https://sinwp.com/camera_clubs/The-Stereoscopic-Society-1449.htm", "https://sinwp.com/camera_clubs/The-Western-Isle-of-Man-Photographic-Society-1842.htm", "https://sinwp.com/camera_clubs/The-Woodberry-Camera-Club-1597.htm", "https://sinwp.com/camera_clubs/Third-Dimension-Society-627.htm", "https://sinwp.com/camera_clubs/Thornbury-Camera-Club-1033.htm", "https://sinwp.com/camera_clubs/Thornton-Heath-Camera-Club-803.htm", "https://sinwp.com/camera_clubs/Thurrock-Camera-Club-628.htm", "https://sinwp.com/camera_clubs/Tipton-Camera-Club-630.htm", "https://sinwp.com/camera_clubs/Tipton-Photographic-Society-1868.htm", "https://sinwp.com/camera_clubs/Tiverton-Heathcoat-Photographic-Club-1034.htm", "https://sinwp.com/camera_clubs/Tividale-Camera-Club-631.htm", "https://sinwp.com/camera_clubs/Todmorden-Photographic-Society-632.htm", "https://sinwp.com/camera_clubs/Tonbridge-Camera-Club-823.htm", "https://sinwp.com/camera_clubs/Torbay-Photographic-Society-1035.htm", "https://sinwp.com/camera_clubs/Totton-and-Eling-Camera-Club-633.htm", "https://sinwp.com/camera_clubs/Tovil-Camera-Club-1100.htm", "https://sinwp.com/camera_clubs/Towcester-Camera-Club-1545.htm", "https://sinwp.com/camera_clubs/Trent-Valley-Photographic-Society-1869.htm", "https://sinwp.com/camera_clubs/Tring-and-District-Camera-Club-1298.htm", "https://sinwp.com/camera_clubs/Trinity-Photography-Group-1563.htm", "https://sinwp.com/camera_clubs/Trowbridge-Camera-Club-1036.htm", "https://sinwp.com/camera_clubs/Tyndale-Photography-Club-1577.htm", "https://sinwp.com/camera_clubs/Tynemouth-Photographic-Society-638.htm", "https://sinwp.com/camera_clubs/Tyneside-Camera-Club-639.htm", "https://sinwp.com/camera_clubs/Uckfield-Photographic-Society-804.htm", "https://sinwp.com/camera_clubs/UCLU-Photographic-Society-1580.htm", "https://sinwp.com/camera_clubs/Ulverston-Photographic-Society-640.htm", "https://sinwp.com/camera_clubs/Unicam-Photographic-Club-641.htm", "https://sinwp.com/camera_clubs/United-Photographic-Postfolios-of-Great-Britain-1172.htm", "https://sinwp.com/camera_clubs/UWE-Students-Union-Photo-Society-1585.htm", "https://sinwp.com/camera_clubs/Vale-of-Evesham-Camera-Club-1870.htm", "https://sinwp.com/camera_clubs/Vange-Camera-Club-645.htm", "https://sinwp.com/camera_clubs/Vauxhall-in-Ellesmere-Port-Photographic-1059.htm", "https://sinwp.com/camera_clubs/Vauxhall-Motors-Luton-Photographic-Society-646.htm", "https://sinwp.com/camera_clubs/Viewfinder-Photographic-Society-i-1979.htm", "https://sinwp.com/camera_clubs/Viewfinders-of-Romsey-Camera-Club-1947.htm", "https://sinwp.com/camera_clubs/Wadebridge-District-Camera-Club-1038.htm", "https://sinwp.com/camera_clubs/Waitrose-Photographic-Society-648.htm", "https://sinwp.com/camera_clubs/Wakefield-Camera-Club-649.htm", "https://sinwp.com/camera_clubs/Wall-Heath-Camera-Club-650.htm", "https://sinwp.com/camera_clubs/Wallasey-Amateur-Photographic-Society-1840.htm", "https://sinwp.com/camera_clubs/Wallingford-Photographic-Club-1129.htm", "https://sinwp.com/camera_clubs/Wallsend-Photographic-Society-1899.htm", "https://sinwp.com/camera_clubs/Walsall-Photographic-Society-653.htm", "https://sinwp.com/camera_clubs/Walsall-Wood-Camera-Club-654.htm", "https://sinwp.com/camera_clubs/Walthamstow-and-District-Photographic-Society-743.htm", "https://sinwp.com/camera_clubs/Wantage-Camera-Club-824.htm", "https://sinwp.com/camera_clubs/Ware-and-District-Photographic-Society-655.htm", "https://sinwp.com/camera_clubs/Wareham-Camera-Club-1451.htm", "https://sinwp.com/camera_clubs/Warminster-Camera-Club-1039.htm", "https://sinwp.com/camera_clubs/Warrington-and-District-Camera-Club-656.htm", "https://sinwp.com/camera_clubs/Warrington-Camera-Club-1047.htm", "https://sinwp.com/camera_clubs/Warrington-Photographic-Society-657.htm", "https://sinwp.com/camera_clubs/Warsop-and-District-Camera-Club-658.htm", "https://sinwp.com/camera_clubs/Washington-Camera-Club-659.htm", "https://sinwp.com/camera_clubs/Watford-Camera-Club-825.htm", "https://sinwp.com/camera_clubs/Wath-and-District-Camera-Club-660.htm", "https://sinwp.com/camera_clubs/Wayland-District-Photographic-Club-1474.htm", "https://sinwp.com/camera_clubs/Wdaebridge-District-Camera-Cub-1454.htm", "https://sinwp.com/camera_clubs/Wearside-Photo-Imaging-Club-661.htm", "https://sinwp.com/camera_clubs/Webheath-Digital-Photography-Club-1456.htm", "https://sinwp.com/camera_clubs/Wednesbury-Photographic-Society-662.htm", "https://sinwp.com/camera_clubs/Wellingborough-and-District-Camera-Club-663.htm", "https://sinwp.com/camera_clubs/Wellington-District-Camera-Club-1040.htm", "https://sinwp.com/camera_clubs/Welwyn-Garden-City-Photographic-Club-1307.htm", "https://sinwp.com/camera_clubs/West-Cornwall-Camera-Club-952.htm", "https://sinwp.com/camera_clubs/West-Cumbria-Photo-Group-1900.htm", "https://sinwp.com/camera_clubs/West-Malling-Camera-Club-1328.htm", "https://sinwp.com/camera_clubs/West-Wickham-Photographic-Society-817.htm", "https://sinwp.com/camera_clubs/Western-Isle-of-Man-Photographic-Society-668.htm", "https://sinwp.com/camera_clubs/Western-Audio-Visual-Enthusiasts-WAVES-1579.htm", "https://sinwp.com/camera_clubs/Westfield-Photographic-Club-1976.htm", "https://sinwp.com/camera_clubs/Weston-Photographic-Society-1457.htm", "https://sinwp.com/camera_clubs/Wetherby-and-District-Camera-Club-670.htm", "https://sinwp.com/camera_clubs/Weymouth-Camera-Club-1042.htm", "https://sinwp.com/camera_clubs/Wharf-Camera-Club-1581.htm", "https://sinwp.com/camera_clubs/Whickham-Photographic-Club-671.htm", "https://sinwp.com/camera_clubs/Whitby-Photographic-Society-1977.htm", "https://sinwp.com/camera_clubs/Whitchurch-Hill-Camera-Club-862.htm", "https://sinwp.com/camera_clubs/Whitchurch-Photographic-Society-673.htm", "https://sinwp.com/camera_clubs/White-River-Digital-Camera-Club-St-Austell-1578.htm", "https://sinwp.com/camera_clubs/Whitley-Bay-Photographic-Society-1901.htm", "https://sinwp.com/camera_clubs/Whitstable-Photographic-Group-1329.htm", "https://sinwp.com/camera_clubs/Whitton-Camera-Club-806.htm", "https://sinwp.com/camera_clubs/Whitworth-Photographic-Society-1843.htm", "https://sinwp.com/camera_clubs/Wickford-Camera-Club-676.htm", "https://sinwp.com/camera_clubs/Wickham-Market-Photographic-Club-1779.htm", "https://sinwp.com/camera_clubs/Wigan-10-Foto-Club-1841.htm", "https://sinwp.com/camera_clubs/Wigan-Photographic-Society-679.htm", "https://sinwp.com/camera_clubs/Wigan-Strobist-Club-1458.htm", "https://sinwp.com/camera_clubs/Wight-Balance-Photography-1643.htm", "https://sinwp.com/camera_clubs/Willfield-Camera-Club-1514.htm", "https://sinwp.com/camera_clubs/Wilmington-Camera-Club-1101.htm", "https://sinwp.com/camera_clubs/Wilmslow-Guild-Photographic-Society-680.htm", "https://sinwp.com/camera_clubs/Wiltshire-Swindon-Photography-1537.htm", "https://sinwp.com/camera_clubs/Wimborne-Camera-Club-681.htm", "https://sinwp.com/camera_clubs/Wincanton-Camera-Club-1043.htm", "https://sinwp.com/camera_clubs/Winchester-Photographic-Society-1311.htm", "https://sinwp.com/camera_clubs/Windlesham-Camberley-Camera-Club-1312.htm", "https://sinwp.com/camera_clubs/Windsor-Photographic-Society-1313.htm", "https://sinwp.com/camera_clubs/Wingrave-Photographic-Interest-Club-1314.htm", "https://sinwp.com/camera_clubs/Winlaton-Camera-Club-684.htm", "https://sinwp.com/camera_clubs/Witham-Camera-Club-686.htm", "https://sinwp.com/camera_clubs/Witney-Photo-Group-1743.htm", "https://sinwp.com/camera_clubs/Wittering-Camera-Club-1341.htm", "https://sinwp.com/camera_clubs/Witterings-Camera-Club-1948.htm", "https://sinwp.com/camera_clubs/Woking-Photographic-Society-1316.htm", "https://sinwp.com/camera_clubs/Wokingham-East-Berkshire-Camera-Club-1949.htm", "https://sinwp.com/camera_clubs/Wolds-Photographic-Society-1978.htm", "https://sinwp.com/camera_clubs/Wolverhampton-Photographic-Society-689.htm", "https://sinwp.com/camera_clubs/Wonston-Worthys-Camera-Club-690.htm", "https://sinwp.com/camera_clubs/Woodbridge-Camera-Club-1532.htm", "https://sinwp.com/camera_clubs/Woodford-Wanstead-Photographic-Society-691.htm", "https://sinwp.com/camera_clubs/Woodford-and-Wanstead-Camera-Club-1781.htm", "https://sinwp.com/camera_clubs/Woodley-Photographic-Club-1132.htm", "https://sinwp.com/camera_clubs/Wooler-District-Camera-Club-1902.htm", "https://sinwp.com/camera_clubs/Woolston-Camera-Club-692.htm", "https://sinwp.com/camera_clubs/Wootton-Bassett-Camera-Club-1044.htm", "https://sinwp.com/camera_clubs/Worcestershire-Camera-Club-693.htm", "https://sinwp.com/camera_clubs/Workington-Camera-Club-694.htm", "https://sinwp.com/camera_clubs/Wrekin-Arts-Photographic-Club-696.htm", "https://sinwp.com/camera_clubs/Wycombe-Photographic-Society-1744.htm", "https://sinwp.com/camera_clubs/Wymondham-and-Attleborough-Camera-Club-697.htm", "https://sinwp.com/camera_clubs/Wyre-Forest-Camera-Club-864.htm", "https://sinwp.com/camera_clubs/Wythall-Photographic-Society-699.htm", "https://sinwp.com/camera_clubs/Wyvern-Camera-Club-1045.htm", "https://sinwp.com/camera_clubs/XRR-Photographic-Society-1133.htm", "https://sinwp.com/camera_clubs/Yardley-Photographic-Society-700.htm", "https://sinwp.com/camera_clubs/Yateley-Camera-Club-701.htm", "https://sinwp.com/camera_clubs/Yeovil-Camera-Club-1046.htm", "https://sinwp.com/camera_clubs/York-Camera-Club-702.htm", "https://sinwp.com/camera_clubs/York-Photographic-Society-703.htm", "https://sinwp.com/camera_clubs/Yoxall-Camera-Club-1871.htm"};
        public static string[] URLs = new String[NUMBER_OF_ENTRIES]; //UKURLs; //

        //Contact URL vars
        public static string[] contactURLs = new String[NUMBER_OF_ENTRIES];
        //when you inevitably increase the number of things in the array below, you'll have to make the one below it a 2D array and alter lines 137-145 to allow subsequent known URLs
        public static string[] KNOWN_CONTACT_URLS = { "https://sinwp.com/", "https://www.facebook.com/" };
        public static String[] LINK_SEARCH_TAGS_START = { "<a ", "<button ", "< a ", "< button " };
        public static String[] LINK_SEARCH_TAGS_END = { ">", "/>", "</" };
        //all lowercase to expedite searches
        public static String[] LINK_SEARCH_KEYWORDS = { "contact", "about", "meet", "join", "board", "coordinators" };
        public static string[] KNOWN_CONTACT_URLS_LOCATOR_KEYPHRASES = { "web address:- <a href=", "" };
        public static string[] CONTACT_URL_PARSING_KEYWORDS = { "href=", "src=" };

        //For dictionary guessing contact page URLs or going to local URLs
        //if the website's link goes to something including one of the following phrases(paired with an extension from the URL_TYPE_EXTENSIONS list), just remove it and then brute force the contact URL with the following extension words/phrases
        public static String[] URL_REMOVE_EXTENSIONS = { "Index", "index", "Home", "home", "Default", "default", "Welcome", "welcome" };
        //in case the scraper can't get the contact page for whatever reason, use the below information to brute force the contact URL
        public static String[] URL_PRE_EXTENSIONS = { "/", "#", "about/", "Club/", "info/", "page/" };
        public static String[] URL_MAIN_EXTENSION = { "contact", "Contact", "about", "About", "join" };
        public static String[] URL_EXTENSION1 = { "", "-", "_",};
        public static String[] URL_EXTENSION2 = { "", "us", "Us", "form" };
        public static String[] URL_EXTENSION3 = { "", "2" };
        public static String[] URL_TYPE_EXTENSIONS = { "", ".html", ".htm", ".aspx", ".php", ".shtml", ".asp" };
        //if unable to find contact page this way, look for facebook link, go there, and then append "about"



        //                                                                  Contact Gathering Class Variables
        //Contact info storage vars
        //# of types of contact information in the array below
        public static int NUMBER_OF_CONTACT_TYPES = 4;
        //2-dimensional array of contact info in String form
        //ex: int[,] array2D = new int[,] { {email1, phone1, other1}, {email2, phone2, other2}, {email3, phone3, other3}};
        public static String[,] contactInfo = new String[NUMBER_OF_ENTRIES, NUMBER_OF_CONTACT_TYPES];

        //Contact detection vars
        // [0] = Email, [1] = Phone, [2] = Address, [3] = Other
        public static Boolean[] checkBoxIsChecked = { true, true, true, true };
        public static Boolean endOfBody = false;
        public static int MAX_CONTACT_KEYWORD_LENGTH = 8;
        //Contact phrase html segment (for debugging porpoises only)
        public static int CONTACT_SEGMENT_SIZE = 100;
        public static String impossibleSearchPhrase = "Hopefully this will never show up in a site's HTML";

        //Contact info searching vars
        //# of keyword items in each array within the array of contact keywords above
        public static int NUMBER_OF_CONTACT_PAGE_SEARCH_KEYWORDS = 4;
        //the next link or info-specific indicator should be searched for after one of these search keywords is found
        //the below array can be empty because it's "filled" by the resetContactsSearchKeywordsArray(); method
        public static String[,] CONTACTS_PAGE_SEARCH_KEYWORDS = new String[NUMBER_OF_CONTACT_TYPES, NUMBER_OF_CONTACT_PAGE_SEARCH_KEYWORDS];
        //maximum search keyword length to tell when to stop when looking for contact keywords


        //                                                                               End of class variable instantiation, beginning of GUI event assignment and scary code
        //****************************************************************************************************************************************************************************************
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public GUI()
        {
            InitializeComponent();
            buttonScrapeURLs.Click += new EventHandler(this.buttonScrapeURLs_Click);
            checkBoxEmail.CheckedChanged += new EventHandler(this.checkBoxEmail_CheckedChanged);
            checkBoxPhone.CheckedChanged += new EventHandler(this.checkBoxPhone_CheckedChanged);
            checkBoxAddress.CheckedChanged += new EventHandler(this.checkBoxAddress_CheckedChanged);
            checkBoxOther.CheckedChanged += new EventHandler(this.checkBoxOther_CheckedChanged);
            buttonGetURLs.Click += new EventHandler(this.buttonGetURLs_Click);
            buttonLocateContacts.Click += new EventHandler(this.buttonLocateContacts_Click);
            buttonReadSites.Click += new EventHandler(this.buttonReadSites_Click);
            buttonWriteContacts.Click += new EventHandler(this.buttonWriteContacts_Click);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code


        //                                                                               End of GUI event assignment and scary code, beginning of GUI updating area
        //****************************************************************************************************************************************************************************************

        private void resetContactsSearchKeywordsArray()
        {
            //set all CONTACTS_PAGE_SEARCH_KEYWORDs to their "off" states
            for (int i = 0; i < NUMBER_OF_CONTACT_PAGE_SEARCH_KEYWORDS; i++)
                for (int j = 0; j < NUMBER_OF_CONTACT_PAGE_SEARCH_KEYWORDS; j++)
                    CONTACTS_PAGE_SEARCH_KEYWORDS[i, j] = impossibleSearchPhrase;

            //alter respective email search word entries
            if (checkBoxIsChecked[0])
            {
                CONTACTS_PAGE_SEARCH_KEYWORDS[0, 0] = "Email";
                CONTACTS_PAGE_SEARCH_KEYWORDS[0, 1] = "email";
                CONTACTS_PAGE_SEARCH_KEYWORDS[0, 2] = "mailto:";
                CONTACTS_PAGE_SEARCH_KEYWORDS[0, 3] = "@";
                //CONTACTS_PAGE_SEARCH_KEYWORDS[0, 0] = "$";
                //CONTACTS_PAGE_SEARCH_KEYWORDS[0, 1] = "price";
                //CONTACTS_PAGE_SEARCH_KEYWORDS[0, 2] = "cost";
                //CONTACTS_PAGE_SEARCH_KEYWORDS[0, 3] = "per person";
            }

            //alter respective phone search word entries
            if (checkBoxIsChecked[1])
            {
                CONTACTS_PAGE_SEARCH_KEYWORDS[1, 0] = "Phone";
                CONTACTS_PAGE_SEARCH_KEYWORDS[1, 1] = "phone";
                CONTACTS_PAGE_SEARCH_KEYWORDS[1, 2] = "tel:-";
                CONTACTS_PAGE_SEARCH_KEYWORDS[1, 3] = "1(";
                //CONTACTS_PAGE_SEARCH_KEYWORDS[1, 0] = "lodging";
                //CONTACTS_PAGE_SEARCH_KEYWORDS[1, 1] = "occupancy";
                //CONTACTS_PAGE_SEARCH_KEYWORDS[1, 2] = "overnight";
                //CONTACTS_PAGE_SEARCH_KEYWORDS[1, 3] = "stay";
            }

            //alter respective address search word entries
            if (checkBoxIsChecked[2])
            {
                CONTACTS_PAGE_SEARCH_KEYWORDS[2, 0] = "Address";
                CONTACTS_PAGE_SEARCH_KEYWORDS[2, 1] = "address";
                CONTACTS_PAGE_SEARCH_KEYWORDS[2, 2] = "Location";
                CONTACTS_PAGE_SEARCH_KEYWORDS[2, 3] = "location";
                //CONTACTS_PAGE_SEARCH_KEYWORDS[2, 0] = "yellowstone";
                //CONTACTS_PAGE_SEARCH_KEYWORDS[2, 1] = "glacier national park";
                //CONTACTS_PAGE_SEARCH_KEYWORDS[2, 2] = "colorado";
                //CONTACTS_PAGE_SEARCH_KEYWORDS[2, 3] = "coast";
            }

            //alter respective other search word entries
            if (checkBoxIsChecked[3])
            {
                CONTACTS_PAGE_SEARCH_KEYWORDS[3, 0] = "meet";
                CONTACTS_PAGE_SEARCH_KEYWORDS[3, 1] = "Ave";
                CONTACTS_PAGE_SEARCH_KEYWORDS[3, 2] = "Rd";
                CONTACTS_PAGE_SEARCH_KEYWORDS[3, 3] = "Ln";
                //CONTACTS_PAGE_SEARCH_KEYWORDS[3, 0] = "2020";
                //CONTACTS_PAGE_SEARCH_KEYWORDS[3, 1] = "2019";
                //CONTACTS_PAGE_SEARCH_KEYWORDS[3, 2] = "2018";
                //CONTACTS_PAGE_SEARCH_KEYWORDS[3, 3] = "2017";
            }

            ////alter respective other search word entries
            //if (checkBoxIsChecked[4])
            //{
            //    CONTACTS_PAGE_SEARCH_KEYWORDS[4, 0] = "meet";
            //    CONTACTS_PAGE_SEARCH_KEYWORDS[4, 1] = "Ave";
            //    CONTACTS_PAGE_SEARCH_KEYWORDS[4, 2] = "Rd";
            //    CONTACTS_PAGE_SEARCH_KEYWORDS[4, 3] = "Ln";
            //    //CONTACTS_PAGE_SEARCH_KEYWORDS[4, 0] = "maximum";
            //    //CONTACTS_PAGE_SEARCH_KEYWORDS[4, 1] = "participants";
            //    //CONTACTS_PAGE_SEARCH_KEYWORDS[4, 2] = "size";
            //    //CONTACTS_PAGE_SEARCH_KEYWORDS[4, 3] = "group";
            //}

            //set all CONTACTS_PAGE_SEARCH_KEYWORDs to lowercase
            for (int i = 0; i < NUMBER_OF_CONTACT_PAGE_SEARCH_KEYWORDS; i++)
                for (int j = 0; j < NUMBER_OF_CONTACT_PAGE_SEARCH_KEYWORDS; j++)
                    CONTACTS_PAGE_SEARCH_KEYWORDS[i, j] = CONTACTS_PAGE_SEARCH_KEYWORDS[i, j].ToLower();

            //print updated keyword arrays
            Console.WriteLine("");
            for (int i = 0; i < NUMBER_OF_CONTACT_PAGE_SEARCH_KEYWORDS; i++)
            {
                //if the checkbox in question is unchecked, print to the console that it's unchecked
                if (!checkBoxIsChecked[i])
                    Console.WriteLine("Checkbox number " + (i + 1) + " is unchecked." + "\n");
                //if for whatever reason the list hasn't been updated yet, say so
                else if (CONTACTS_PAGE_SEARCH_KEYWORDS[i, 0] == null)
                    Console.WriteLine("Checkbox number " + (i + 1) + " is somehow not updated, or is broken." + "\n");
                //otherwise the entry is considered to be checked
                else
                    Console.WriteLine("Checkbox number " + (i + 1) + " is checked." + "\n");
            }
        }

        //Added contact search keyphrase changing functionality

        private void checkBoxEmail_CheckedChanged(object sender, EventArgs e)
        {
            //reset the check state
            checkBoxIsChecked[0] = checkBoxEmail.Checked;
            //checkBoxEmail.Checked = !checkBoxEmail.Checked;

            resetContactsSearchKeywordsArray();

            Console.WriteLine("Changed CONTACTS_PAGE_SEARCH_KEYWORDS");
        }

        private void checkBoxPhone_CheckedChanged(object sender, EventArgs e)
        {
            //reset the check state
            checkBoxIsChecked[1] = checkBoxPhone.Checked;
            //checkBoxPhone.Checked = !checkBoxPhone.Checked;

            resetContactsSearchKeywordsArray();

            Console.WriteLine("Changed CONTACTS_PAGE_SEARCH_KEYWORDS");
        }

        private void checkBoxAddress_CheckedChanged(object sender, EventArgs e)
        {
            //reset the check state
            checkBoxIsChecked[2] = checkBoxAddress.Checked;
            //checkBoxOther.Checked = !checkBoxOther.Checked;

            resetContactsSearchKeywordsArray();

            Console.WriteLine("Changed CONTACTS_PAGE_SEARCH_KEYWORDS");
        }

        private void checkBoxOther_CheckedChanged(object sender, EventArgs e)
        {
            //reset the check state
            checkBoxIsChecked[3] = checkBoxOther.Checked;
            //checkBoxOther.Checked = !checkBoxOther.Checked;

            resetContactsSearchKeywordsArray();

            Console.WriteLine("Changed CONTACTS_PAGE_SEARCH_KEYWORDS");
        }

        //                                                              End of GUI updating area, beginning of scraping from urls section
        //*****************************************************************************************************************************************************************************************

        private void buttonScrapeURLs_Click(object sender, EventArgs e)
        {
            //read the URLs from the excel doc to an array of strings
            WorkBook workbook = WorkBook.Load(PATH_OF_IO_DOC);
            WorkSheet worksheet = workbook.GetWorkSheet(SHEET_NAME);

            //make sure to instantiate the contact search keywords array if it's not already been instantiated by changing the check state of a checkbox
            resetContactsSearchKeywordsArray();

            int rowCount = NUMBER_OF_ENTRIES + rowOffset;
            //start at rowOffset to skip the header
            for (int i = rowOffset; i < rowCount; i++)
            {
                //get value by row and column indexing
                string index_val = worksheet.Rows[i].Columns[READING_COLUMN].ToString();

                ////check to make sure correct values are collected
                //Console.WriteLine(i + "'{0}'", index_val);

                //scrape the info. from the url
                getContactsFromURL(index_val);
            }
            Console.WriteLine("Finished scraping URLs");
            Console.WriteLine("");

        }

        //                                                              End of scraping from URLs section, beginning of source url gathering section
        //*****************************************************************************************************************************************************************************************

        private void buttonGetURLs_Click(object sender, EventArgs e)
        {
            //read the URLs from the excel doc to an array of strings
            WorkBook workbook = WorkBook.Load(PATH_OF_IO_DOC);
            WorkSheet worksheet = workbook.GetWorkSheet(SHEET_NAME);

            int rowCount = NUMBER_OF_ENTRIES + rowOffset;
            //start at rowOffset to skip the header
            for (int i = rowOffset; i < rowCount; i++)
            {
                //get value by cell address
                //string address_val = ws["A" + rowCount].ToString();
                //get value by row and column indexing
                string index_val = worksheet.Rows[i].Columns[READING_COLUMN].ToString();

                //read each cell's value to the array of URLs
                URLs[i - rowOffset] = index_val;
                //set the URL to an empty string if it's originally a null value
                if (URLs[i - rowOffset] == null)
                    URLs[i - rowOffset] = "";

                //check to make sure correct values are collected
                Console.WriteLine(i + "'{0}'", index_val);
            }
            Console.WriteLine("Finished getting site URLs");
            Console.WriteLine("");

        }

        //                                                              End of source url gathering section, beginning of main page and contact page url scraping section
        //*****************************************************************************************************************************************************************************************

        private void buttonLocateContacts_Click(object sender, EventArgs e)
        {
            //make sure to instantiate the contact search keywords array if it's not already been instantiated by changing the check state of a checkbox
            resetContactsSearchKeywordsArray();

            URLIndex = 0;
            try
            {
                //loop through all the entries
                while (URLIndex < NUMBER_OF_ENTRIES)
                {
                    string url = URLs[URLIndex];
                    string html = getHTML(url);
                    String[] urlSearchPhrases = {""};

                    //make sure the url is valid
                    if (!(url == null || url == "") && (url.Length > 0))
                    {
                        urlSearchPhrases[0] = KNOWN_CONTACT_URLS_LOCATOR_KEYPHRASES[0];

                        //Show the webpage currently being read
                        Console.WriteLine("");
                        Console.WriteLine(URLIndex + rowOffset);
                        Console.WriteLine("What a strange new site to see!");
                        //since it's an unknown, and thus, assumed to be the main page of the club, searching for the URL isn't necessary, as we already have it
                        Console.WriteLine("Main page URL = " + URLs[URLIndex]);

                        //try to get the contact page URL specifically, and if unavailable, then try to get another likely contact page URL
                        //set the contacts page URL to the one found in the new webpage's HTML
                        contactURLs[URLIndex] = getURLFromHTML(getHTML(URLs[URLIndex]), LINK_SEARCH_TAGS_START);
                        //if the scraped contact page URL doesn't have the keyword "contact" in it
                        if (contactURLs[URLIndex].ToLower().LastIndexOf("contact") < 0)
                        {
                            //try the brute force method, stopping when it reaches the list entry after the last version of the "contact" keyword
                            String bruteForcedURL = tryBruteForce(URLs[URLIndex], 2);
                            //if the brute force method returns a url that contains the keyword, use that as the contact page url
                            if (bruteForcedURL.ToLower().LastIndexOf("contact") > 0)
                                contactURLs[URLIndex] = bruteForcedURL;
                            //otherwise leave it as is and accept that you can't get 'em all right
                        }

                        Console.WriteLine("Contact page URL = " + contactURLs[URLIndex]);

                        Console.WriteLine("");
                    }
                    else
                    {
                        contactURLs[URLIndex] = url;

                        Console.WriteLine("Empty source URL");
                        Console.WriteLine("");

                        //i think I'm doing this bit right
                        URLs[URLIndex] = assembleGoogleURL();
                    }
                    //increment the starting URLindex after an exception is seen so that it is incremented properly in the exception handlers
                    URLIndex++;
                }

                Console.WriteLine("");
                Console.WriteLine("Finished getting sites' URLs");
                Console.WriteLine("");
            }
            ////catch the null argument exception and let user try again, starting at the next URL
            //catch (ArgumentNullException ex)
            //{
            //    Console.WriteLine("Null Argument Exception caught, try again.");
            //    Console.WriteLine("");
            //    contactURLs[URLIndex] = getURLFromHTML(-1, "", LINK_SEARCH_TAGS_START);
            //    URLIndex++;
            //}
            //catch the web exception and let user start again, starting at the next URL
            catch (WebException)
            {
                Console.WriteLine("WebException caused by url being: " + contactURLs[URLIndex]);
                contactURLs[URLIndex] = getURLFromHTML("", LINK_SEARCH_TAGS_START);
                URLIndex++;
                Console.WriteLine("");
            }
        }

        private static string getHTML(string url)
        {
            try
            {
                //make sure that the url provided isn't empty
                if (url != null)
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    request.UserAgent = "C# console client";

                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    using (Stream stream = response.GetResponseStream())
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        return reader.ReadToEnd();  // can't do .ToLower(); because it ruins the urls, I've tested it
                        /*
                            maybe return 2 strings one of the standard and one of the .ToLower-ed html, so you can search the .ToLower-ed HTML
                            and then go to that character in the unaltered html for the url?
                            that's just for later on, when efficiency and not debugging is my main focus
                         */
                    }
                //if the url provided was null, just return an empty string
                } else {
                    Console.WriteLine("the url I was given was null");
                    return "";
                }
            }
            catch (WebException)
            {
                //if the URL is invalid, try googling the invalid url string maybe?
                Console.WriteLine("couldn't resolve the site host name because the url is '" + url + "'");
                return "";
            }
            catch (NotSupportedException)
            {
                //if the URL is invalid, try googling the invalid url string

                Console.WriteLine("exception caused by url being '" + url + "'");
                Console.WriteLine("");

                return "";
            }
            catch (UriFormatException)
            {
                Console.WriteLine("exception caused by url being '" + url + "'");
                Console.WriteLine("");

                return "";
            }
        }

        private static string getURLFromHTML(string html, string[] searchTagsStart)
        {
            string[] links = getHTMLSegments(html, searchTagsStart, LINK_SEARCH_TAGS_END);

            Console.WriteLine("Found " + links.Length + " links");
            Console.WriteLine("first 3 values of links array:");
            Console.WriteLine(links[0] + ", " + links[1] + ", " + links[2]);

            string foundURL = parseItemFromList(LINK_SEARCH_KEYWORDS, CONTACT_URL_PARSING_KEYWORDS, links);

            //Reset endOfBody so that other methods can reuse the variable
            endOfBody = false;

            //return the url
            return foundURL;
            //}
            //catch (ArgumentOutOfRangeException ex)
            //{
            //    Console.WriteLine("Looked for keyphrases outside of html for some reason.");
            //    Console.WriteLine(ex);
            //    Console.WriteLine("");

            //    //return the url
            //    return foundURL;
            //}
        }


        //Note: You can't break search for contacts from html SEGMENTS because contact info isn't virtually always contained in an "a" tag or a "button" tag like contact page urls are
        public static string[] getHTMLSegments (string html, String[] searchTagsStart, String[] searchTagsEnd)
        {
            //initialize a string array of empty strings
            string[] segments = new string[10000];
            //initialize segments list
            for (int i = 0; i < segments.Length; i++)
                segments[i] = "empty";

            if (html.Length > 0)
            {
                int segmentCounter = 0;
                endOfBody = false;

                //print first few chars of HTML as indication of proper functioning
                Console.WriteLine(html.Substring(0, 15));

                //for counting segments
                int l = 0;
                //index for finding the starting spot of each link
                int i = 0;
                bool foundContact = false;
                //read through html until it reaches the end of the body or finds the contact
                while (!endOfBody && !foundContact && segmentCounter < segments.Length)
                {
                    //read through all of the search keywords
                    for (int j = 0; j < searchTagsEnd.Length; j++)
                    {
                        //makes a list (segments array) of <a> blocks (<a> and <button> blocks) to go through and store them in the segments array
                        //if the site's HTML includes the keywords somewhere, look nearby it for the URL of the contacts page
                        if (j < searchTagsStart.Length && i < html.Length - searchTagsStart[j].Length && html.Substring(i, searchTagsStart[j].Length) == (searchTagsStart[j]))
                        {
                            //i is the starting spot of each link and k is the ending spot

                            //find the <a> and <button> blocks
                            //look through subsequent html for closing <a> or <button> tag, and when it's found, set k to it
                            int k = i+0;
                            Console.WriteLine("ArgumentOutOfRange exception caused by the line of code below (Line 560)");
                            while (j < searchTagsEnd.Length && html.Substring(k, searchTagsEnd[j].Length) != (searchTagsEnd[j]))
                                k++;

                            //add another link as an entry to the array of segments to go through
                            //this is not the cause of null value exceptions either
                            if (html.Substring(i, k - i) != null && html.Substring(i, k - i) != "")
                                segments[segmentCounter] = html.Substring(i, k - i);
                            segmentCounter++;

                            //debugging
                            l++;
                            //Console.WriteLine("Found a link at character #" + i);
                            //if (i - CONTACT_SEGMENT_SIZE >= 0)
                            //    Console.WriteLine(html.Substring(i - CONTACT_SEGMENT_SIZE, 2 * CONTACT_SEGMENT_SIZE + searchTagsStart[j].Length));
                            //else
                            //    Console.WriteLine(html.Substring(0, CONTACT_SEGMENT_SIZE + searchTagsStart[j].Length));
                        }

                        //move on to the next HTML once it's finished reading through this HTML
                        if ((i < html.Length - 7 && html.Substring(i, 7) == ("</body>")) || i == html.Length - 1)
                        {
                            endOfBody = true;
                            break;
                        }
                    }

                    i++;
                }
                return segments;
            }
            else
            {
                Console.WriteLine("Somehow there was no html at this URL");
                return segments;
            }
        }

        //                                                              End of main page and contact page url scraping section, beginning of contact scraping section
        //*****************************************************************************************************************************************************************************************

        private void buttonReadSites_Click(object sender, EventArgs e)
        {
            //read the information on the new site URL
            //basically the same as buttonLocateContacts_Click(), but it stores the contact data collected
            try
            {
                //Reset URLIndex so that the while loop isn't just immediately skipped because URLIndex == NUMBER_OF_ENTRIES at the start of this method
                URLIndex = 0;
                //Reset endOfBody so that other methods can reuse the variable (maybe unnecessary at the beginning of this method, but you can never be too sure)
                endOfBody = false;


                while (URLIndex < NUMBER_OF_ENTRIES)
                {
                    Console.WriteLine("");
                    Console.WriteLine(URLIndex + rowOffset);

                    string url = contactURLs[URLIndex];

                    //make sure the url is defined
                    if (url != null && url != "")
                        getContactsFromURL(url);
                    
                    Console.WriteLine("");
                    //increment the starting URLindex after an exception is seen so that it is incremented properly in the exception handlers
                    URLIndex++;
                }

                Console.WriteLine("");
                Console.WriteLine("Finished getting sites' contact information");
                Console.WriteLine("");
            }
            ////catch the null argument exception and let user try again, starting at the next URL
            //catch (ArgumentNullException ex)
            //{
            //    Console.WriteLine(URLIndex + rowOffset);
            //    Console.WriteLine("Null Argument Exception caught, try again.");
            //    getContactsFromURL("");
            //    URLIndex++;
            //}
            //catch the web exception and let user start again, starting at the next URL
            catch (WebException)
            {
                Console.WriteLine(URLIndex + rowOffset);
                Console.WriteLine("WebException caused by URL being '" + contactURLs[URLIndex] + "'");
                getContactsFromURL(contactURLs[URLIndex]);
                URLIndex++;
            }
        }

        //Note: You can't break search for contacts from html SEGMENTS because contact info isn't virtually always contained in an "a" tag or a "button" tag like contact page urls are
        private static void getContactsFromURL(string url)
        {
            try
            {
                //Reset endOfBody so that other methods can reuse the variable (maybe unnecessary at the beginning of this method, but you can never be too sure)
                endOfBody = false;

                //make sure the url is not empty
                if (url != "")
                {
                    string html = getHTML(url);
                    //all lowercase for searching porpoises
                    string lowercaseHTML = html.ToLower();

                    //make sure the input is not empty
                    if (html != "")
                    {
                        //print first 4 chars of HTML as indication of proper functioning
                        Console.WriteLine(html.Substring(0, 15));

                        //the index of some character in the HTML
                        int i = 0;
                        //read through html until it gets to the end of the html or body
                        while (!(i >= html.Length - MAX_CONTACT_KEYWORD_LENGTH || endOfBody))
                        {
                            //read through all of CONTACTS_PAGE_SEARCH_KEYWORDS arrays
                            for (int j = 0; j < NUMBER_OF_CONTACT_TYPES; j++)
                            {
                                //skip values of j that correspond to unwanted contact types (==false instead of ! for purposes of legibility)
                                if (j == 0 && checkBoxIsChecked[0] == false)
                                    j++;
                                if (j == 1 && checkBoxIsChecked[1] == false)
                                    j++;
                                if (j == 2 && checkBoxIsChecked[2] == false)
                                    j++;
                                if (j == 3 && checkBoxIsChecked[3] == false)
                                    break;

                                //read through the items of the arrays of the array (ex, CONTACTS_PAGE_SEARCH_KEYWORDS array 1, item 3)
                                for (int k = 0; k < NUMBER_OF_CONTACT_PAGE_SEARCH_KEYWORDS; k++)                                         //problem here
                                {
                                    //if the site's HTML includes the keywords somewhere, look nearby it for the contacts information
                                    //IndexOutOfRangeException in the below substring statement because you need to update NUMBER_OF_CONTACT_TYPES and NUMBER_OF_CONTACT_PAGE_SEARCH_KEYWORDS
                                    if (lowercaseHTML.Substring(i, CONTACTS_PAGE_SEARCH_KEYWORDS[j, k].Length).Equals(CONTACTS_PAGE_SEARCH_KEYWORDS[j, k]))
                                    {
                                        //This is where the various contact types are looked for in different places around each keyword
                                        String contact = "No " + CONTACTS_PAGE_SEARCH_KEYWORDS[j, 0].ToUpper() + " found";

                                        //maybe modify contact text in case extra text further is included in parsing
                                        //*********************************************************************************************************                   CONTINUE HERE (SEE BELOW STATEMENT FOR FURTHER GUIDANCE)
                                        //needs to find ALL possible contact information, not just one, then decide which one is the most likely contact info.
                                        contact = parseContactWithKeywordLocation(html, CONTACTS_PAGE_SEARCH_KEYWORDS[j, k], i);
                                        contactInfo[URLIndex, j] = contact;

                                        Console.WriteLine("Found " + CONTACTS_PAGE_SEARCH_KEYWORDS[j, 0].ToUpper() + " contact phrase in HTML at character #" + i);
                                        Console.WriteLine("Contact phrase: '" + contact + "'");
                                        
                                        //add the length of the search keyword found to i so it can rule that phrase out
                                        i += CONTACTS_PAGE_SEARCH_KEYWORDS[j, k].Length;
                                    }
                                }
                            }

                            //move on to the next HTML once it's finished reading through this HTML (up to the point of the last place to read the contact keywords from)
                            if (html.Substring(i + MAX_CONTACT_KEYWORD_LENGTH, 7) == ("</body>"))
                            {
                                endOfBody = true;
                                break;
                            }

                            i++;
                        }

                        Console.WriteLine("");
                    }
                    else
                    {
                        //set contact info of the contact to show that there is no contact info for it
                        //email
                        contactInfo[URLIndex, 0] = "There was no html at this URL";
                        //phone
                        contactInfo[URLIndex, 1] = "";
                        //other
                        contactInfo[URLIndex, 2] = "";

                        //debugging
                        Console.WriteLine("There was no html at this URL");
                    }
                }
                //if the url provided is empty
                else if (url != "Sorry, I couldn't find a contacts page.")
                {
                    //getting the url:
                    //check for a facebook link
                    contactURLs[URLIndex] = checkForFacebookLink(url);
                
                    //disabled for first rounds of testing
                    //if that fails, try brute-forcing the contact page url
                    //contactURLs[URLIndex] = tryBruteForce(url);

                    ////if that fails, try googling the club based on the url stored in the respective URLs[] index
                    //contactURLs[URLIndex] = assembleGoogleURL();

                    //getting the contacts from the url or giving up
                    if (!(contactURLs[URLIndex] == "" || contactURLs[URLIndex].Length == 0))
                        getContactsFromURL(url);
                    else
                        contactURLs[URLIndex] = "Sorry, I couldn't find a contacts page.";
                }
                else
                {
                    //it should do this if no contacts page url is provided or found
                    //set contact info of the contact to show that there is no contact info for it
                    //email
                    contactInfo[URLIndex, 0] = "Somehow there was no html at this URL";
                    //phone
                    contactInfo[URLIndex, 1] = "Somehow there was no html at this URL";
                    //other
                    contactInfo[URLIndex, 2] = "Somehow there was no html at this URL";
                }

                //Reset endOfBody so that other methods can reuse the variable
                endOfBody = false;

            }
            catch (UriFormatException)
            {
                //if the URL is invalid, try googling the invalid url string maybe?
                Console.WriteLine("exception caused by url being '" + url + "'");
            }
}

        //Note: You can't break search for contacts from html SEGMENTS because contact info isn't virtually always contained in an "a" tag or a "button" tag like contact page urls are
        private static String parseItemFromList(String [] itemSearchKeywords, String[] parsingKeyWords, String [] listItems)
        {
            try {
                String item = "";
                int startIndex = 0;
                int endIndex = 0;
                String listItem = "";
                Boolean doBreakception = false;

                //loop through all listItems accumulated
                for (int i = 0; i < listItems.Length; i++)
                {
                    //listItems[i] should be a segment of HTML
                    listItem = listItems[i];

                    //fuck null values of listItems, all my homies use empty strings
                    if (listItem == null)
                        listItem = "";

                        for (int q = 0; q < itemSearchKeywords.Length; q++)
                        {

                            //if the searched phrase is within the listItem string (.ToLower to speed up search)
                            if (listItem.ToLower().LastIndexOf(itemSearchKeywords[q]) > 0)
                            {
                                //look for first listItem tag by going through detected html backwards
                                for (int j = listItem.Length - 5 - 1; j >= 0; j--)
                                {
                                    foreach (String parsingKeyWord in parsingKeyWords)
                                    {
                                        //"href=" and "src=" originally, lengths may need to be refactored further
                                        if (listItem.Substring(j, parsingKeyWord.Length) == parsingKeyWord)
                                        {
                                            //start reading the URL after the phrase above
                                            startIndex = j + parsingKeyWord.Length + 1;

                                            int k = 0;
                                            while (k < listItem.Length - startIndex - 1 && listItem[startIndex + k + 1] != null)
                                            {
                                                //break at the closing " in the html, signifying the end of the HTML
                                                //I'm leaving in the null test because I think the compiler gets mad at me when it's not there
                                                if (listItem[startIndex + k + 1] != null && listItem[startIndex + k + 1] == '"')
                                                    break;
                                                k++;
                                            }
                                            //if this is being written a ton of times, something is wrong
                                            Console.WriteLine("Found item " + item);
                                            item = listItem.Substring(startIndex, k + 1);
                                            doBreakception = true;
                                            break;
                                        }
                                    }
                                    if (doBreakception)
                                        break;
                                }
                            }
                            if (doBreakception)
                                break;
                        }
                        if (doBreakception)
                            break;
                }

                string tempItem = item;
                string baseURL = URLs[URLIndex];

                //check to make sure the url about to be returned is valid, and if it isn't make the returned url reflect that
                if (item.Length >= 4 && item.Substring(0, 4) != "http") {
                    //check all possible listItem extensions
                    for (int i = 0; i < URL_TYPE_EXTENSIONS.Length; i++)
                    {
                        //if the item ends with a URL_TYPE_EXTENSION from URL_TYPE_EXTENSIONS
                        if (item.Substring(item.Length - URL_TYPE_EXTENSIONS[i].Length, URL_TYPE_EXTENSIONS[i].Length) == URL_TYPE_EXTENSIONS[i]) {
                            tempItem = item;
                            break;
                        }
                        tempItem = "The items page url that I was going to return was not valid";
                    }

                    //check if the item url is a local listItem and convert it to a URL if it is, making sure to add a "/"
                    //if (item != "The items page url that I was going to return was not valid")
                    //    tempItem = baseURL.Substring(0, baseURL.LastIndexOf("")) + tempItem;
                    if (item.LastIndexOf("/") == -1)
                        return prepBaseURL(baseURL) + "/" + item;
                    else if (item.Substring(0, 1) == "/")
                        tempItem = prepBaseURL(baseURL);
                    else if (item.Substring(0, 2) == "./")
                        tempItem = prepBaseURL(baseURL) + item.Substring(1);
                }

                item = tempItem;

                return item;
            }
            catch (IndexOutOfRangeException)
            {
                return "listItem index went out of bounds on line 707";
            }
            catch (NullReferenceException)
            {
                return "listItem value at index was null on line 722";
            }
        }

        private static String prepBaseURL(String baseURL)
        {
            //if the baseURL doesn't have "/", or only has one at the end of "https://"
            if (baseURL.LastIndexOf("/") == -1 || baseURL.Substring(0, baseURL.LastIndexOf("/")+1).Equals("https://"))
                //return the original, unchanged baseURL
                return baseURL;


            String newBaseURL = baseURL.Substring(0, baseURL.LastIndexOf("/"));

            //otherwise, if the last bit on the url is actually a home page, remove that bit and return only the first part of the URL 
            foreach (String homePageKeyword in URL_REMOVE_EXTENSIONS)
                foreach (String extension in URL_REMOVE_EXTENSIONS)
                    //if the end of the baseURL is a homepage URL tidbit with the "/"
                    //the part where I did (newBaseURL.Length-1) was to get the last index of newBaseURL
                    if (newBaseURL.Substring((newBaseURL.Length-1) - (homePageKeyword + extension + 1).Length).Equals(homePageKeyword + extension + "/"))
                        //set the newBaseURL to the baseURL without that homepage bit
                        newBaseURL = newBaseURL.Substring(0, (newBaseURL.Length - 1) - (homePageKeyword + extension + 1).Length);
                    //no need to check for URLs that have the home page stuff withOUT the "/" at the end because they're gotten rid of near the top of this method, where newBaseURL is instantiated

            return newBaseURL;
        }

        //Note: following method gathers multiple starting and ending indices for potential contact information and returns the two corresponding with the shortest length contact information string (Yes I know this only works for short segments of contact info. but it should work well enough to get emails)
        private static String parseContactWithKeywordLocation(String html, String keyword, int keywordIndexInHTML )
        {
            //reset the class variable so no old values carry over
            string [] contactSegments = new string[10000];

            String[] parsingPhrasesStart = { " ", "mailto:", "\"", ">" };
            String[] parsingPhrasesEnd = { "\"", "<", "?subject=" };
            ////offset so that the contact info. text phrase starts on the right word/phrase
            //int offset = 4;
            //Lists of possible contact start and end indices (Lengths correspond to the number of possible indices)
            int[] contactIndicesStart = new int[parsingPhrasesStart.Length];
            for (int i = 0; i < contactIndicesStart.Length; i++)
                contactIndicesStart[i] = keywordIndexInHTML;
            int[] contactIndicesEnd = new int[parsingPhrasesEnd.Length];
            for (int i = 0; i < contactIndicesEnd.Length; i++)
                contactIndicesEnd[i] = -1;

            //keyword location
            //A type 1 statistical error in this case would be less severe than a type 2, as we'd still get the contact info if it were unclear whether or not the keyword was in the contact info
            Boolean keywordAtStartOfContact = false;
            //determine if the keyword is at the start of the contact info, by determining if the character before the keyword is empty or not
            //(primitive, I know, but it should be good enough for now at least)
            //if the character before the keywordIndexInHTML is empty, we assert that the scraper is starting at the start of the contact info
            if (html[keywordIndexInHTML-1].Equals(" "))
                keywordAtStartOfContact = true;

            //go through all start parsing phrases to find the contact info start indices if the keyword is in the middle of the contact info (ex: example@domain.com)
            if (!keywordAtStartOfContact)
                //go through all start parsing phrases to determine if the end of the contact info has been found
                for (int i = 0; i < parsingPhrasesStart.Length; i++)
                {
                    //go through all characters in html before the keyword where a keyword can still be searched
                    for(int j = keywordIndexInHTML - 1; j > keyword.Length; j--)
                    {
                        //any starting index exceptions
                        ////exception for keyword "$" [0, 0]
                        //if (keyword.Equals("$"))
                        //    //set the start index to the index of the keyword, since that's how it should be formatted (+0 so memory location definitions aren't changed)
                        //    contactStartIndex = keywordIndexInHTML + 0;

                        //if the string at index i is equal to one of the parsing phrases from list (.ToLower() to speed up search)
                        if (html.ToLower().Substring(j, parsingPhrasesStart[i].Length).Equals(parsingPhrasesStart[i]))
                        {
                            //set the end index to the index after the current parsing phrase
                            contactIndicesStart[i] = j;// + parsingPhrasesStart[i].Length - 1;
                            break;
                        }
                    }
                }

            //go through all end parsing phrases to find the contact info end indices
            for (int i = 0; i < parsingPhrasesEnd.Length; i++)
            {
                //go through all characters in html after the keyword
                for (int j = keywordIndexInHTML + keyword.Length; j < html.Length - parsingPhrasesEnd[i].Length; j++)
                {
                    //any ending index exceptions
                    ////exception for keyword "$" [0, 0]
                    //if (keyword.Equals("$"))
                    //    //set the start index to the index of the keyword, since that's how it should be formatted (+0 so memory location definitions aren't changed)
                    //    contactStartIndex = keywordIndexInHTML + 0;

                    //if the phrase at index i is equal to one of the parsing phrases from list (.ToLower() to speed up search)
                    if (html.ToLower().Substring(j, parsingPhrasesEnd[i].Length).Equals(parsingPhrasesEnd[i]))
                    {
                        //set the end index to the index at the current parsing phrase
                        contactIndicesEnd[i] = j+0;
                        break;
                    }
                }
            }

            //get the largest valid start index and smallest valid end index around the contact information
            int startIndex = -1;
            foreach (int contactIndexStart in contactIndicesStart)
                //if the start of the contact information is at the index where or before where the keyword was found and is greater than the previous startIndex value, set it as the new startIndex
                if (contactIndexStart <= keywordIndexInHTML && contactIndexStart > startIndex)
                    startIndex = contactIndexStart + 0;
            int endIndex = html.Length;
            foreach (int contactIndexEnd in contactIndicesEnd)
                //if the end of the contact information is after the startIndex and is less than the previous startIndex value, set it as the new endIndex
                if (contactIndexEnd > startIndex && contactIndexEnd < endIndex)
                    endIndex = contactIndexEnd + 0;

            //add 1 to exclode
            startIndex++;

            //return the contact info
            //Note: there was an issue where the previous code would get a good string, but for whatever reason contactIndexEnd < contactIndexStart
            //&& contactIndexEnd > contactIndexStart
            //if (contactIndexEnd > 0 )
            //{
                Console.WriteLine("Character at contactIndexStart (" + startIndex + "): " + html[startIndex]);
                Console.WriteLine("Character at contactIndexEnd (" + endIndex + "): " + html[endIndex]);
                Console.WriteLine("");
                return html.Substring(startIndex, endIndex - startIndex);
            //}

            //if it couldn't find an end index
            return "Couldn't parse the text after the contact information keyword.";
        }

        private static String checkForFacebookLink(String url)
        {
            String[] searchKeywords = { "https://www.facebook.com/" };
            string html = getHTML(url);
            string foundURL = getURLFromHTML(html, searchKeywords);

            return foundURL;
        }

        private static String tryBruteForce(String url, int stopAtMainExtensionIndex)
        {
            //find contacts page url given the main page url
            string baseURL = url;
            string foundURL = url;

            //check for/remove any removable phrases in the url
            for (int i = 0; i < URL_REMOVE_EXTENSIONS.Length; i++)
            {
                //if the last bit of the home page URL is a known removable phrase
                if (baseURL.Length - URL_REMOVE_EXTENSIONS[i].Length >= 0 && baseURL.Substring(baseURL.Length - URL_REMOVE_EXTENSIONS[i].Length) == URL_REMOVE_EXTENSIONS[i])
                {
                    //for each removable extension, reset the baseURL and set the foundURL to the baseURL before the removable phrase
                    baseURL = baseURL.Substring(0, baseURL.Length - URL_REMOVE_EXTENSIONS[i].Length);
                    foundURL = baseURL.Substring(0, baseURL.Length - URL_REMOVE_EXTENSIONS[i].Length);
                    //break statement just to speed things up and quit if/when a removable extension is found
                    break;
                }
            }

            //brute force all possible contact urls until one works
            for (int i = 0; i < URL_PRE_EXTENSIONS.Length; i++)
            {
                for (int j = 0; j < URL_MAIN_EXTENSION.Length; j++)
                {
                    for (int k = 0; k < URL_EXTENSION1.Length; k++)
                    {
                        for (int l = 0; l < URL_EXTENSION2.Length; l++)
                        {
                            for (int m = 0; m < URL_EXTENSION3.Length; m++)
                            {
                                for (int n = 0; n < URL_TYPE_EXTENSIONS.Length; n++)
                                {
                                    //if NOT(one of the intermediate string from the URL_EXTENSION1 list is currently chosen and the string from the URL_EXTENSION2 list is empty) DO this url check
                                    if (!(k > 0 && l == 0))
                                    {
                                        foundURL = prepBaseURL(baseURL) + URL_PRE_EXTENSIONS[i] + URL_MAIN_EXTENSION[j] + URL_EXTENSION1[k] + URL_EXTENSION2[l] + URL_EXTENSION3[m] + URL_TYPE_EXTENSIONS[n];
                                        //if the stopping location is hit and no correct contact url is found
                                        if (j == stopAtMainExtensionIndex)
                                            return "";
                                        //if the attempted URL is correct
                                        if (!(getHTML(foundURL) == ""))
                                            return foundURL;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //if a correct contact url is never found
            return "";
        }

        private static String checkLenseLab()
        {
            //check to make sure you're not going with the sinwp page for the club url

            if (URLIndex < NUMBER_OF_ENTRIES)
            {
                //get club text
                WorkBook workbook = WorkBook.Load(PATH_OF_IO_DOC);
                WorkSheet worksheet = workbook.GetWorkSheet(SHEET_NAME);
                //get seach words by looking at the name of the club and what state it's located in
                string clubGooglePhrase = worksheet.Rows[URLIndex].Columns[NAMES_COLUMN].ToString() + " " + worksheet.Rows[URLIndex].Columns[READING_COLUMN + 1].ToString();
                //add if statement to change any " CC" string to " Camera Club"
                //if the word club isn't already included, append it

                //check to make sure correct value is collected
                Console.WriteLine(URLIndex + "'{0}'", clubGooglePhrase);

                //then assemble search URL
                String url = "https://www.lenslab.co.uk/clublist.php";

                //then look for the first search result's URL
                String[] searchKeywords = { clubGooglePhrase };
                string html = getHTML(url);
                string foundURL = "";
                getURLFromHTML(html, searchKeywords);

                return foundURL;
            }
            else
            {
                return "The URLIndex value was out of bounds";
            }
        }

        private static String checkFacebook()
        {
            //check to make sure you're not going with the sinwp page for the club url

            if (URLIndex < NUMBER_OF_ENTRIES)
            {
                //get club text
                WorkBook workbook = WorkBook.Load(PATH_OF_IO_DOC);
                WorkSheet worksheet = workbook.GetWorkSheet(SHEET_NAME);
                //get seach words by looking at the name of the club and what state it's located in
                string clubGooglePhrase = worksheet.Rows[URLIndex].Columns[NAMES_COLUMN].ToString() + " " + worksheet.Rows[URLIndex].Columns[READING_COLUMN + 1].ToString();
                //add if statement to change any " CC" string to " Camera Club"
                //if the word club isn't already included, append it

                //check to make sure correct value is collected
                Console.WriteLine(URLIndex + "'{0}'", clubGooglePhrase);

                //then assemble search URL
                String url = assembleFacebookURL();

                //then look for the first search result's URL
                String[] searchKeywords = { };
                string html = getHTML(url);
                string foundURL = "";
                getURLFromHTML(html, searchKeywords);

                return foundURL;
            }
            else
            {
                return "The URLIndex value was out of bounds";
            }
        }

        private static String assembleFacebookURL()
        {
            if (URLIndex < NUMBER_OF_ENTRIES)
            {
                //get club text
                WorkBook workbook = WorkBook.Load(PATH_OF_IO_DOC);
                WorkSheet worksheet = workbook.GetWorkSheet(SHEET_NAME);
                //get seach words by looking at the name of the club and what state it's located in
                string clubPhrase = worksheet.Rows[URLIndex].Columns[NAMES_COLUMN].ToString() + " " + worksheet.Rows[URLIndex].Columns[READING_COLUMN + 1].ToString();
                //add if statement to change any " CC" string to " Camera Club"
                //if the word club isn't already included, append it
                String[] clubPhraseWords = { "Camera Club" };
                for (int i = 0; i < clubPhrase.Length; i++)
                {
                    //parse each clubPhraseWords entry by the ' ' (space) character
                }

                //assemble google query url
                String url = "https://www.google.com/search?q=" + '"';
                for (int i = 0; i < clubPhraseWords.Length; i++)
                {
                    url += clubPhraseWords[i] + "+";
                }
                url = url.Substring(0, url.Length - 1) + '"';

                return url;
            }
            return "The URLIndex was larger than the number of entries being read";
        }

        private static String assembleGoogleURL()
        {
            if (URLIndex < NUMBER_OF_ENTRIES)
            {
                //get club text
                WorkBook workbook = WorkBook.Load(PATH_OF_IO_DOC);
                WorkSheet worksheet = workbook.GetWorkSheet(SHEET_NAME);
                //get seach words by looking at the name of the club and what state it's located in
                string clubPhrase = worksheet.Rows[URLIndex].Columns[NAMES_COLUMN].ToString() + " " + worksheet.Rows[URLIndex].Columns[READING_COLUMN + 1].ToString();
                
                String[] clubPhraseArray = clubPhrase.Split(' ');
                String[] clubPhraseWords = new String [clubPhraseArray.Length+2];
                //add if statement to change any " CC" string to " Camera Club"

                //append the words "camera" and "club" just in case
                clubPhraseWords[clubPhraseWords.Length - 2] = "Camera";
                clubPhraseWords[clubPhraseWords.Length - 1] = "Club";

                //assemble google query url
                String url = "https://www.google.com/search?q=" + '"';
                for (int i = 0; i < clubPhraseWords.Length; i++)
                {
                    url += clubPhraseWords[i] + "+";
                }
                url = url.Substring(0, url.Length - 1) + '"';

                return url;
            }
            return "The URLIndex was larger than the number of entries being read";
        }

        //                                                                               End of contact scraping section, beginning of contact info writing section
        //*************************************************************************************************************************************************************************

        private void buttonWriteContacts_Click(object sender, EventArgs e)
        {
            //read the URLs from the excel doc to an array of strings
            WorkBook workbook = WorkBook.Load(PATH_OF_IO_DOC);
            WorkSheet worksheet = workbook.GetWorkSheet(SHEET_NAME);

            int rowCount = NUMBER_OF_ENTRIES + rowOffset;
            int offset = rowOffset + 1;
            //start at rowOffset + 1 to skip the first header
            for (int i = offset; i < rowCount; i++)
            {
                //check to make sure correct values for correct column are written
                Console.WriteLine(i - offset);

                //set value by cell address
                //set value by row and column indexing
                worksheet[MAIN_URL_WRITING_COLUMN + i].Value = URLs[i - offset];
                Console.WriteLine(URLs[i - offset]);
                worksheet[CONTACT_URL_WRITING_COLUMN + i].Value = contactURLs[i - offset];
                Console.WriteLine(contactURLs[i - offset]);
                //only write information if it's selected in the GUI
                if (checkBoxIsChecked[0])
                {
                    worksheet[EMAIL_WRITING_COLUMN + i].Value = contactInfo[i - offset, 0];
                    Console.WriteLine(contactInfo[i - offset, 0]);
                }
                else
                {
                    Console.WriteLine("No email was written to the workbook.");
                }
                if (checkBoxIsChecked[1])
                {
                    worksheet[PHONE_WRITING_COLUMN + i].Value = contactInfo[i - offset, 1];
                    Console.WriteLine(contactInfo[i - offset, 1]);
                }
                else
                {
                    Console.WriteLine("No phone number was written to the workbook.");
                }
                if (checkBoxIsChecked[2])
                {
                    worksheet[ADDRESS_WRITING_COLUMN + i].Value = contactInfo[i - offset, 2];
                    Console.WriteLine(contactInfo[i - offset, 2]);
                }
                else
                {
                    Console.WriteLine("No address was written to the workbook.");
                }
                if (checkBoxIsChecked[3])
                {
                    worksheet[OTHER_WRITING_COLUMN + i].Value = contactInfo[i - offset, 2];
                    Console.WriteLine(contactInfo[i - offset, 2]);
                }
                else
                {
                    Console.WriteLine("No additional information was written to the workbook.");
                }

                Console.WriteLine("");
            }

            //save the altered workbook
            workbook.Save();
            Console.WriteLine("Finished writing contact information to workbook.");
        }

        //End of code

        #endregion

        private System.Windows.Forms.TabControl pageControl;
        private System.Windows.Forms.TabPage pageMain;
        private Label label2;
        private Label label1;
        private TabControl tabControlGatherMethod;
        private TabPage tabPageContactInfo;
        private CheckBox checkBoxAddress;
        private CheckBox checkBoxEmail;
        private CheckBox checkBoxPhone;
        private CheckBox checkBoxOther;
        private Button buttonLocateContacts;
        private Button buttonReadSites;
        private TabPage tabPageScraping;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private CheckBox checkBox4;
        private CheckBox checkBox3;
        private Button buttonScrapeURLs;
        private TabPage tabPageCustom;
        private RadioButton radioButtonBefore;
        private Label label7;
        private Label label6;
        private TextBox textBox4;
        private TextBox textBox3;
        private TextBox textBox2;
        private TextBox textBox1;
        private Label label5;
        private Label label4;
        private Label label3;
        private ListBox listBoxGatherMethod;
        private Button buttonWriteContacts;
        private Button buttonGetURLs;
        private Label labelInfoToGather;
        private Label title1;
        private Button buttonCustomBack;
        private Button buttonCustomNext;
        private TabControl tabControlCustom;
        private TabPage tabPageKeywords;
        private TabPage tabPageBasicQuestions;
        private Label label9;
        private RadioButton radioButton1;
        private RadioButton radioButtonAfter;
        private RadioButton radioButton2;
        private TabPage tabPageCrawl;
        private Button button1;
        private Button button2;
        private Label label8;
        private TextBox textBox5;
        private TabPage tabPageScrape;
        private Button button3;
    }
}

/*
Sources:
                                                https://stackoverflow.com/questions/16160676/read-excel-data-line-by-line-with-c-sharp-net
                                                https://www.wfmj.com/story/42504801/c-read-excel-file-example
                                                https://ironsoftware.com/csharp/excel/tutorials/csharp-open-write-excel-file/
    Prepping the Excel sheets (Add " " to the new cell values, use the =CONCAT function to concatenate all the urls into one cell, then use a word processor to replace the spaces with "", "", finally, copy+paste the string of urls into this program):
    Extracting URLs from hyperlinks (alt+f11):  https://excel.tips.net/T003281_Extracting_URLs_from_Hyperlinks.html
                                                http://zetcode.com/csharp/readwebpage/
                                                https://docs.microsoft.com/en-us/dotnet/api/system.string == ?view=net-5.0#System_String_StartsWith_System_String_
    An excel sheet from a friend to get some practice and testing data for the program
*/