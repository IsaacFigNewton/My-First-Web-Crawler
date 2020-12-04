using System;
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

namespace First_Webcrawler
{
    public partial class GUI : Form
    {
        //class variables
        //-80 just for testing porpoises
        public static int NUMBER_OF_ENTRIES = 10;
        public static int READING_COLUMN = 7;
        public static String CONTACT_URL_WRITING_COLUMN = "G";
        public static String MAIN_URL_WRITING_COLUMN = "H";
        public static String EMAIL_WRITING_COLUMN = "E";
        public static String PHONE_WRITING_COLUMN = "J";
        public static String ADDRESS_WRITING_COLUMN = "C";
        public static String MEETING_LOCATION_WRITING_COLUMN = "I";

        public static int URLIndex = 1;
        public static string[] UKURLs = { "https://sinwp.com/camera_clubs/Pinner-Camera-Club-1122.htm", "https://sinwp.com/camera_clubs/Plymouth-Athenaeum-Photographic-Society-1571.htm", "https://sinwp.com/camera_clubs/Plymouth-Camera-Club-1441.htm", "https://sinwp.com/camera_clubs/Plymstock-Camera-Club-1016.htm", "https://sinwp.com/camera_clubs/Pocklington-Camera-Club-487.htm", "https://sinwp.com/camera_clubs/Pontefract-Camera-Club-488.htm", "https://sinwp.com/camera_clubs/Ponteland-Photographic-Society-489.htm", "https://sinwp.com/camera_clubs/Poole-and-District-Camera-Club-490.htm", "https://sinwp.com/camera_clubs/Portishead-Camera-Club-1017.htm", "https://sinwp.com/camera_clubs/Portland-Camera-Club-1559.htm", "https://sinwp.com/camera_clubs/Portsmouth-Camera-Club-492.htm", "https://sinwp.com/camera_clubs/Portsmouth-Imaging-Club-1251.htm", "https://sinwp.com/camera_clubs/Positive-Image-1972.htm", "https://sinwp.com/camera_clubs/Potters-Bar-District-Photographic-Society-1769.htm", "https://sinwp.com/camera_clubs/Potters-Bar-Distreict-Photographic-Society-494.htm", "https://sinwp.com/camera_clubs/Poulton-le-Fylde-Photographic-Society-495.htm", "https://sinwp.com/camera_clubs/Preston-Photographic-Club-Paignton-1572.htm", "https://sinwp.com/camera_clubs/Preston-Photographic-Society-496.htm", "https://sinwp.com/camera_clubs/Prestwich-Camera-Club-1827.htm", "https://sinwp.com/camera_clubs/Princes-Risborough-Photographic-Society-1123.htm", "https://sinwp.com/camera_clubs/Print-Project-Group-1924.htm", "https://sinwp.com/camera_clubs/Prismatic-Photographic-Club-1956.htm", "https://sinwp.com/camera_clubs/Pudsey-Camera-Club-499.htm", "https://sinwp.com/camera_clubs/Queensberry-Camera-Club-502.htm", "https://sinwp.com/camera_clubs/Quekett-Microscopical-Club-792.htm", "https://sinwp.com/camera_clubs/RPR-Camera-Club-504.htm", "https://sinwp.com/camera_clubs/Rayleigh-Camera-Club-506.htm", "https://sinwp.com/camera_clubs/Raynes-Park-Camera-Club-793.htm", "https://sinwp.com/camera_clubs/RB-Camera-Club-Lincoln-and-District-507.htm", "https://sinwp.com/camera_clubs/Reading-Camera-Club-508.htm", "https://sinwp.com/camera_clubs/Reading-University-Photographic-Society-1534.htm", "https://sinwp.com/camera_clubs/Redditch-Photographic-Society-509.htm", "https://sinwp.com/camera_clubs/Reepham-District-Photographic-Club-1770.htm", "https://sinwp.com/camera_clubs/Reepham-and-District-Photographic-Club-510.htm", "https://sinwp.com/camera_clubs/Reflex-Camera-Club-1513.htm", "https://sinwp.com/camera_clubs/Reflex-Photographic-Club-1256.htm", "https://sinwp.com/camera_clubs/Reigate-Photographic-Society-794.htm", "https://sinwp.com/camera_clubs/Resound-Camera-Club-1573.htm", "https://sinwp.com/camera_clubs/Retford-and-District-Photographic-Society-512.htm", "https://sinwp.com/camera_clubs/Richmond-Twickenham-Photographic-Society-1257.htm", "https://sinwp.com/camera_clubs/Richmond-Camera-Club-1258.htm", "https://sinwp.com/camera_clubs/Riding-Mill-Photographic-Society-1640.htm", "https://sinwp.com/camera_clubs/Ringwood-Camera-Club-1602.htm", "https://sinwp.com/camera_clubs/Ripon-City-Photographic-Society-518.htm", "https://sinwp.com/camera_clubs/Riverside-Camera-Club-1096.htm", "https://sinwp.com/camera_clubs/Roby-Mill-Community-Camera-Club-1831.htm", "https://sinwp.com/camera_clubs/Rocester-and-District-Camera-Club-1642.htm", "https://sinwp.com/camera_clubs/Rochdale-and-District-Camera-Club-1830.htm", "https://sinwp.com/camera_clubs/Rochdale-Photographic-Society-1829.htm", "https://sinwp.com/camera_clubs/Rochdale-Photographic-Society-520.htm", "https://sinwp.com/camera_clubs/Roehampton-Photography-Society-1583.htm", "https://sinwp.com/camera_clubs/Rolls-Royce-Derby-Photographic-Society-722.htm", "https://sinwp.com/camera_clubs/Romford-Camera-Club-1771.htm", "https://sinwp.com/camera_clubs/Romford-Photographic-Society-521.htm", "https://sinwp.com/camera_clubs/Romiley-Camera-Club-1832.htm", "https://sinwp.com/camera_clubs/Romiley-Photo-And-Digital-Club-1176.htm", "https://sinwp.com/camera_clubs/Romney-Marsh-Photographic-Club-1281.htm", "https://sinwp.com/camera_clubs/Ross-on-Wye-Photographic-Society-1443.htm", "https://sinwp.com/camera_clubs/Rossington-Camera-Club-522.htm", "https://sinwp.com/camera_clubs/Rothbury-and-District-Photographic-Society-524.htm", "https://sinwp.com/camera_clubs/Rotherham-Photographic-Society-525.htm", "https://sinwp.com/camera_clubs/Rottingdean-Camera-Club-796.htm", "https://sinwp.com/camera_clubs/Royal-Holloways-Photography-Society-1592.htm", "https://sinwp.com/camera_clubs/Royston-Photographic-Society-526.htm", "https://sinwp.com/camera_clubs/Rugeley-and-Armitage-Camera-Club-1863.htm", "https://sinwp.com/camera_clubs/Runcorn-Phoenix-Photographic-Society-527.htm", "https://sinwp.com/camera_clubs/Rushcliffe-Photographic-Society-528.htm", "https://sinwp.com/camera_clubs/Rushden-and-District-Photographic-Society-529.htm", "https://sinwp.com/camera_clubs/Ruskin-Camera-Club-530.htm", "https://sinwp.com/camera_clubs/Russel-Street-Photographic-Society-531.htm", "https://sinwp.com/camera_clubs/Ryburn-Photographic-Studios-532.htm", "https://sinwp.com/camera_clubs/Ryde-Imaging-Group-1933.htm", "https://sinwp.com/camera_clubs/Rye-and-District-Camera-Club-1097.htm", "https://sinwp.com/camera_clubs/Ryton-and-District-Camera-Club-534.htm", "https://sinwp.com/camera_clubs/Saddleworth-Camera-Club-535.htm", "https://sinwp.com/camera_clubs/Saffron-Walden-Camera-Club-536.htm", "https://sinwp.com/camera_clubs/Sale-Photographic-Society-537.htm", "https://sinwp.com/camera_clubs/Salford-Photographic-Society-538.htm", "https://sinwp.com/camera_clubs/Salisbury-Camera-Club-1263.htm", "https://sinwp.com/camera_clubs/Saltash-and-District-Camera-Club-1019.htm", "https://sinwp.com/camera_clubs/Sandbach-Photographic-Society-1833.htm", "https://sinwp.com/camera_clubs/Sandown-Shanklin-and-District-Camera-Club-542.htm", "https://sinwp.com/camera_clubs/Scarborough-Photographic-Society-1973.htm", "https://sinwp.com/camera_clubs/Scartho-Village-Community-Centre-1913.htm", "https://sinwp.com/camera_clubs/Scunthorpe-Camera-Club-543.htm", "https://sinwp.com/camera_clubs/Seaford-Photographic-society-1264.htm", "https://sinwp.com/camera_clubs/Sedgemoor-Camera-Club-1957.htm", "https://sinwp.com/camera_clubs/Sedgley-Camera-Club-544.htm", "https://sinwp.com/camera_clubs/Seham-Photographic-Society-545.htm", "https://sinwp.com/camera_clubs/Selby-Camera-Club-546.htm", "https://sinwp.com/camera_clubs/Selsey-Camera-Club-1945.htm", "https://sinwp.com/camera_clubs/Senior-Photographers-1267.htm", "https://sinwp.com/camera_clubs/Settle-Photographic-Group-1974.htm", "https://sinwp.com/camera_clubs/Seven-Sisters-Camera-Club-1552.htm", "https://sinwp.com/camera_clubs/Sevenoaks-Camera-Club-1268.htm", "https://sinwp.com/camera_clubs/Shaftesbury-Camera-Club-1575.htm", "https://sinwp.com/camera_clubs/Shafton-and-District-Photographic-Society-549.htm", "https://sinwp.com/camera_clubs/Sheffield-Photographic-Society-550.htm", "https://sinwp.com/camera_clubs/Shefford-and-District-Camera-Club-551.htm", "https://sinwp.com/camera_clubs/Shell-Club-Photographic-Section-552.htm", "https://sinwp.com/camera_clubs/Shepshed-and-District-Camera-Club-554.htm", "https://sinwp.com/camera_clubs/Sherborne-Bradford-Abbas-Camera-Club-1021.htm", "https://sinwp.com/camera_clubs/Sherburn-Camera-Club-555.htm", "https://sinwp.com/camera_clubs/Shillington-District-Camera-Club-1772.htm", "https://sinwp.com/camera_clubs/Shillington-and-District-Camera-Club-556.htm", "https://sinwp.com/camera_clubs/Shirley-Photographic-Society-557.htm", "https://sinwp.com/camera_clubs/Shropshire-Photographic-Society-558.htm", "https://sinwp.com/camera_clubs/Sidmouth-District-Photographic-Club-1022.htm", "https://sinwp.com/camera_clubs/Sileby-Photographic-Society-559.htm", "https://sinwp.com/camera_clubs/Sittingbourne-Photographic-Society-1099.htm", "https://sinwp.com/camera_clubs/Skipton-Camera-Club-560.htm", "https://sinwp.com/camera_clubs/Sleaford-District-Photographic-Group-1927.htm", "https://sinwp.com/camera_clubs/SLIC-Photography-and-Digital-Imaging-Club-1838.htm", "https://sinwp.com/camera_clubs/Smethwick-Camera-Club-855.htm", "https://sinwp.com/camera_clubs/Smethwick-Photographic-Society-562.htm", "https://sinwp.com/camera_clubs/Sodbury-Yate-Photographic-Society-1023.htm", "https://sinwp.com/camera_clubs/Solent-Camera-Club-563.htm", "https://sinwp.com/camera_clubs/Solihull-Photographic-Society-565.htm", "https://sinwp.com/camera_clubs/South-Birmingham-Photographic-Society-566.htm", "https://sinwp.com/camera_clubs/South-Derbyshire-Camera-Club-567.htm", "https://sinwp.com/camera_clubs/South-Liverpool-Photographic-Society-568.htm", "https://sinwp.com/camera_clubs/South-London-Photographic-Society-814.htm", "https://sinwp.com/camera_clubs/South-Manchester-Camera-Club-569.htm", "https://sinwp.com/camera_clubs/South-Normanton-Camera-Club-570.htm", "https://sinwp.com/camera_clubs/South-Petherton-Photographic-Society-1024.htm", "https://sinwp.com/camera_clubs/South-Reading-Camera-Club-1275.htm", "https://sinwp.com/camera_clubs/South-Shields-Photographic-Society-572.htm", "https://sinwp.com/camera_clubs/South-Woodham-Ferrers-Camera-Club-573.htm", "https://sinwp.com/camera_clubs/South-Yorkshire-Photographic-Society-1635.htm", "https://sinwp.com/camera_clubs/Southampton-Camera-Club-574.htm", "https://sinwp.com/camera_clubs/Southampton-Students-Union-Photo-Society-1586.htm", "https://sinwp.com/camera_clubs/Southend-On-Sea-Photographic-Society-575.htm", "https://sinwp.com/camera_clubs/Southern-Electric-S-SC-Photographic-Society-576.htm", "https://sinwp.com/camera_clubs/Southern-Photographic-Society-1834.htm", "https://sinwp.com/camera_clubs/Southgate-Photographic-Society-740.htm", "https://sinwp.com/camera_clubs/Southport-Photographic-Society-577.htm", "https://sinwp.com/camera_clubs/Southwick-Camera-Club-800.htm", "https://sinwp.com/camera_clubs/Sowerby-Bridge-and-District-Photographic-Society-578.htm", "https://sinwp.com/camera_clubs/Spalding-Photographic-Society-579.htm", "https://sinwp.com/camera_clubs/Spencer-Dallington-Camera-Club-581.htm", "https://sinwp.com/camera_clubs/Sphinx-Photographic-Club-1864.htm", "https://sinwp.com/camera_clubs/Spires-Photographic-Society-582.htm", "https://sinwp.com/camera_clubs/Spondon-Camera-Club-723.htm", "https://sinwp.com/camera_clubs/Spratton-Photography-Group-1865.htm", "https://sinwp.com/camera_clubs/Springfield-Camera-Club-583.htm", "https://sinwp.com/camera_clubs/SRGB-Photo-Group-1839.htm", "https://sinwp.com/camera_clubs/St-Agnes-Photographic-Club-1574.htm", "https://sinwp.com/camera_clubs/St-Austell-Camera-Club-1541.htm", "https://sinwp.com/camera_clubs/St-Ives-Photographic-Club-1278.htm", "https://sinwp.com/camera_clubs/St-Neots-District-CC-1279.htm", "https://sinwp.com/camera_clubs/St-Albans-District-Photographic-Society-1124.htm", "https://sinwp.com/camera_clubs/St-Helens-Camera-Club-1484.htm", "https://sinwp.com/camera_clubs/St-Helens-Camera-Club-585.htm", "https://sinwp.com/camera_clubs/St-Ives-Photographic-Club-587.htm", "https://sinwp.com/camera_clubs/Stafford-Camera-Club-588.htm", "https://sinwp.com/camera_clubs/Stafford-Photographic-Society-590.htm", "https://sinwp.com/camera_clubs/Staffordshire-Audio-Visual-Group-1866.htm", "https://sinwp.com/camera_clubs/Stagecoach-Camera-Club-1138.htm", "https://sinwp.com/camera_clubs/Stalybridge-Photographic-Club-591.htm", "https://sinwp.com/camera_clubs/Stamford-Photographic-Society-1928.htm", "https://sinwp.com/camera_clubs/Stanhope-Photographic-Society-1895.htm", "https://sinwp.com/camera_clubs/Stanhope-Photographic-Society-592.htm", "https://sinwp.com/camera_clubs/Stanley-Camera-Club-593.htm", "https://sinwp.com/camera_clubs/Stapleford-Community-Association-Camera-Club-594.htm", "https://sinwp.com/camera_clubs/Staplehurst-Photographic-Society-1082.htm", "https://sinwp.com/camera_clubs/Stead-McAlpine-Camera-Club-595.htm", "https://sinwp.com/camera_clubs/Stevenage-Photographic-Society-1282.htm", "https://sinwp.com/camera_clubs/Steyning-Camera-Club-1283.htm", "https://sinwp.com/camera_clubs/Stockport-Photographic-Society-599.htm", "https://sinwp.com/camera_clubs/Stockport-Photographic-Society-1836.htm", "https://sinwp.com/camera_clubs/Stocksbridge-Photographic-Society-821.htm", "https://sinwp.com/camera_clubs/Stockton-Camera-Club-1896.htm", "https://sinwp.com/camera_clubs/Stockton-On-Tees-Photo-Colour-Society-752.htm", "https://sinwp.com/camera_clubs/Stoke-Poges-Photographic-Club-1284.htm", "https://sinwp.com/camera_clubs/Stoke-On-Trent-Camera-Club-601.htm", "https://sinwp.com/camera_clubs/Stokenchurch-Camera-Club-1126.htm", "https://sinwp.com/camera_clubs/Stokesley-Photographic-Society-602.htm", "https://sinwp.com/camera_clubs/Storrington-Camera-Club-1285.htm", "https://sinwp.com/camera_clubs/Stothert-Pitt-Camera-Club-1026.htm", "https://sinwp.com/camera_clubs/Stourbridge-Photographic-Society-603.htm", "https://sinwp.com/camera_clubs/Stourport-Camera-Club-604.htm", "https://sinwp.com/camera_clubs/Stowe-and-District-Photographic-Club-605.htm", "https://sinwp.com/camera_clubs/Stowmarket-District-Camera-Club-606.htm", "https://sinwp.com/camera_clubs/Stratford-upon-Avon-Photographic-Club-607.htm", "https://sinwp.com/camera_clubs/Stroud-Camera-Club-1027.htm", "https://sinwp.com/camera_clubs/Sudbury-and-District-Camera-Club-610.htm", "https://sinwp.com/camera_clubs/Suffolk-Creative-Photographic-Group-1531.htm", "https://sinwp.com/camera_clubs/Suffolk-Monochrome-Group-1775.htm", "https://sinwp.com/camera_clubs/Sunderland-Photographic-Association-611.htm", "https://sinwp.com/camera_clubs/Sunningdale-Ascot-Camera-Club-1287.htm", "https://sinwp.com/camera_clubs/Sutton-Coldfield-Photographic-Society-612.htm", "https://sinwp.com/camera_clubs/Sutton-Photographic-Group-1776.htm", "https://sinwp.com/camera_clubs/Swaffham-Camera-Club-1777.htm", "https://sinwp.com/camera_clubs/Swavesey-Camera-Club-613.htm", "https://sinwp.com/camera_clubs/Sway-Camera-Club-1338.htm", "https://sinwp.com/camera_clubs/Swindon-Imaging-Group-1444.htm", "https://sinwp.com/camera_clubs/Swindon-Photographic-Society-1028.htm", "https://sinwp.com/camera_clubs/Swinton-and-District-Photographic-Society-1837.htm", "https://sinwp.com/camera_clubs/Tadley-District-Photography-Club-616.htm", "https://sinwp.com/camera_clubs/Tame-Photographic-Society-617.htm", "https://sinwp.com/camera_clubs/Tamworth-Photographic-Club-618.htm", "https://sinwp.com/camera_clubs/Tandridge-Photographic-Society-802.htm", "https://sinwp.com/camera_clubs/Taunton-Camera-Club-859.htm", "https://sinwp.com/camera_clubs/Taurus-Photographic-Club-234.htm", "https://sinwp.com/camera_clubs/Tavistock-Camera-Club-1031.htm", "https://sinwp.com/camera_clubs/Teesdale-Photographic-Society-1135.htm", "https://sinwp.com/camera_clubs/Temeside-Camera-Club-619.htm", "https://sinwp.com/camera_clubs/Tettenhall-Wood-Photographic-Club-621.htm", "https://sinwp.com/camera_clubs/Thame-Camera-Club-1127.htm", "https://sinwp.com/camera_clubs/Thatcham-Photographic-Club-1445.htm", "https://sinwp.com/camera_clubs/The-Practical-Camera-Club-of-Southampton-1334.htm", "https://sinwp.com/camera_clubs/The-35-Mill-Camera-Club-623.htm", "https://sinwp.com/camera_clubs/The-Ashby-Photographic-Group-1904.htm", "https://sinwp.com/camera_clubs/The-Barnes-Photographic-Society-1633.htm", "https://sinwp.com/camera_clubs/The-Brelu-Brelu-Photographic-Group-1903.htm", "https://sinwp.com/camera_clubs/The-British-Society-of-Underwater-Photographers-1446.htm", "https://sinwp.com/camera_clubs/The-Camera-Club-1291.htm", "https://sinwp.com/camera_clubs/The-City-Camera-Club-1292.htm", "https://sinwp.com/camera_clubs/The-Darlington-Association-of-Photographers-1880.htm", "https://sinwp.com/camera_clubs/The-Disabled-Photographers-Society-1419.htm", "https://sinwp.com/camera_clubs/The-East-London-Fun-Photography-Club-TELFPC-1582.htm", "https://sinwp.com/camera_clubs/The-Evolve-Group-1801.htm", "https://sinwp.com/camera_clubs/The-f4-Photographic-Group-1964.htm", "https://sinwp.com/camera_clubs/The-Fosse-Co-op-Camera-Club-269.htm", "https://sinwp.com/camera_clubs/The-Hill-Camera-Club-1621.htm", "https://sinwp.com/camera_clubs/The-Icon-Group-1293.htm", "https://sinwp.com/camera_clubs/The-Leeds-Photographic-Society-1634.htm", "https://sinwp.com/camera_clubs/The-Leica-Society-1448.htm", "https://sinwp.com/camera_clubs/The-Northern-Audio-Visual-Group-1890.htm", "https://sinwp.com/camera_clubs/The-Northolt-District-Photographic-Society-1294.htm", "https://sinwp.com/camera_clubs/The-Portfolio-Group-1576.htm", "https://sinwp.com/camera_clubs/The-Stereoscopic-Society-1449.htm", "https://sinwp.com/camera_clubs/The-Western-Isle-of-Man-Photographic-Society-1842.htm", "https://sinwp.com/camera_clubs/The-Woodberry-Camera-Club-1597.htm", "https://sinwp.com/camera_clubs/Third-Dimension-Society-627.htm", "https://sinwp.com/camera_clubs/Thornbury-Camera-Club-1033.htm", "https://sinwp.com/camera_clubs/Thornton-Heath-Camera-Club-803.htm", "https://sinwp.com/camera_clubs/Thurrock-Camera-Club-628.htm", "https://sinwp.com/camera_clubs/Tipton-Camera-Club-630.htm", "https://sinwp.com/camera_clubs/Tipton-Photographic-Society-1868.htm", "https://sinwp.com/camera_clubs/Tiverton-Heathcoat-Photographic-Club-1034.htm", "https://sinwp.com/camera_clubs/Tividale-Camera-Club-631.htm", "https://sinwp.com/camera_clubs/Todmorden-Photographic-Society-632.htm", "https://sinwp.com/camera_clubs/Tonbridge-Camera-Club-823.htm", "https://sinwp.com/camera_clubs/Torbay-Photographic-Society-1035.htm", "https://sinwp.com/camera_clubs/Totton-and-Eling-Camera-Club-633.htm", "https://sinwp.com/camera_clubs/Tovil-Camera-Club-1100.htm", "https://sinwp.com/camera_clubs/Towcester-Camera-Club-1545.htm", "https://sinwp.com/camera_clubs/Trent-Valley-Photographic-Society-1869.htm", "https://sinwp.com/camera_clubs/Tring-and-District-Camera-Club-1298.htm", "https://sinwp.com/camera_clubs/Trinity-Photography-Group-1563.htm", "https://sinwp.com/camera_clubs/Trowbridge-Camera-Club-1036.htm", "https://sinwp.com/camera_clubs/Tyndale-Photography-Club-1577.htm", "https://sinwp.com/camera_clubs/Tynemouth-Photographic-Society-638.htm", "https://sinwp.com/camera_clubs/Tyneside-Camera-Club-639.htm", "https://sinwp.com/camera_clubs/Uckfield-Photographic-Society-804.htm", "https://sinwp.com/camera_clubs/UCLU-Photographic-Society-1580.htm", "https://sinwp.com/camera_clubs/Ulverston-Photographic-Society-640.htm", "https://sinwp.com/camera_clubs/Unicam-Photographic-Club-641.htm", "https://sinwp.com/camera_clubs/United-Photographic-Postfolios-of-Great-Britain-1172.htm", "https://sinwp.com/camera_clubs/UWE-Students-Union-Photo-Society-1585.htm", "https://sinwp.com/camera_clubs/Vale-of-Evesham-Camera-Club-1870.htm", "https://sinwp.com/camera_clubs/Vange-Camera-Club-645.htm", "https://sinwp.com/camera_clubs/Vauxhall-in-Ellesmere-Port-Photographic-1059.htm", "https://sinwp.com/camera_clubs/Vauxhall-Motors-Luton-Photographic-Society-646.htm", "https://sinwp.com/camera_clubs/Viewfinder-Photographic-Society-i-1979.htm", "https://sinwp.com/camera_clubs/Viewfinders-of-Romsey-Camera-Club-1947.htm", "https://sinwp.com/camera_clubs/Wadebridge-District-Camera-Club-1038.htm", "https://sinwp.com/camera_clubs/Waitrose-Photographic-Society-648.htm", "https://sinwp.com/camera_clubs/Wakefield-Camera-Club-649.htm", "https://sinwp.com/camera_clubs/Wall-Heath-Camera-Club-650.htm", "https://sinwp.com/camera_clubs/Wallasey-Amateur-Photographic-Society-1840.htm", "https://sinwp.com/camera_clubs/Wallingford-Photographic-Club-1129.htm", "https://sinwp.com/camera_clubs/Wallsend-Photographic-Society-1899.htm", "https://sinwp.com/camera_clubs/Walsall-Photographic-Society-653.htm", "https://sinwp.com/camera_clubs/Walsall-Wood-Camera-Club-654.htm", "https://sinwp.com/camera_clubs/Walthamstow-and-District-Photographic-Society-743.htm", "https://sinwp.com/camera_clubs/Wantage-Camera-Club-824.htm", "https://sinwp.com/camera_clubs/Ware-and-District-Photographic-Society-655.htm", "https://sinwp.com/camera_clubs/Wareham-Camera-Club-1451.htm", "https://sinwp.com/camera_clubs/Warminster-Camera-Club-1039.htm", "https://sinwp.com/camera_clubs/Warrington-and-District-Camera-Club-656.htm", "https://sinwp.com/camera_clubs/Warrington-Camera-Club-1047.htm", "https://sinwp.com/camera_clubs/Warrington-Photographic-Society-657.htm", "https://sinwp.com/camera_clubs/Warsop-and-District-Camera-Club-658.htm", "https://sinwp.com/camera_clubs/Washington-Camera-Club-659.htm", "https://sinwp.com/camera_clubs/Watford-Camera-Club-825.htm", "https://sinwp.com/camera_clubs/Wath-and-District-Camera-Club-660.htm", "https://sinwp.com/camera_clubs/Wayland-District-Photographic-Club-1474.htm", "https://sinwp.com/camera_clubs/Wdaebridge-District-Camera-Cub-1454.htm", "https://sinwp.com/camera_clubs/Wearside-Photo-Imaging-Club-661.htm", "https://sinwp.com/camera_clubs/Webheath-Digital-Photography-Club-1456.htm", "https://sinwp.com/camera_clubs/Wednesbury-Photographic-Society-662.htm", "https://sinwp.com/camera_clubs/Wellingborough-and-District-Camera-Club-663.htm", "https://sinwp.com/camera_clubs/Wellington-District-Camera-Club-1040.htm", "https://sinwp.com/camera_clubs/Welwyn-Garden-City-Photographic-Club-1307.htm", "https://sinwp.com/camera_clubs/West-Cornwall-Camera-Club-952.htm", "https://sinwp.com/camera_clubs/West-Cumbria-Photo-Group-1900.htm", "https://sinwp.com/camera_clubs/West-Malling-Camera-Club-1328.htm", "https://sinwp.com/camera_clubs/West-Wickham-Photographic-Society-817.htm", "https://sinwp.com/camera_clubs/Western-Isle-of-Man-Photographic-Society-668.htm", "https://sinwp.com/camera_clubs/Western-Audio-Visual-Enthusiasts-WAVES-1579.htm", "https://sinwp.com/camera_clubs/Westfield-Photographic-Club-1976.htm", "https://sinwp.com/camera_clubs/Weston-Photographic-Society-1457.htm", "https://sinwp.com/camera_clubs/Wetherby-and-District-Camera-Club-670.htm", "https://sinwp.com/camera_clubs/Weymouth-Camera-Club-1042.htm", "https://sinwp.com/camera_clubs/Wharf-Camera-Club-1581.htm", "https://sinwp.com/camera_clubs/Whickham-Photographic-Club-671.htm", "https://sinwp.com/camera_clubs/Whitby-Photographic-Society-1977.htm", "https://sinwp.com/camera_clubs/Whitchurch-Hill-Camera-Club-862.htm", "https://sinwp.com/camera_clubs/Whitchurch-Photographic-Society-673.htm", "https://sinwp.com/camera_clubs/White-River-Digital-Camera-Club-St-Austell-1578.htm", "https://sinwp.com/camera_clubs/Whitley-Bay-Photographic-Society-1901.htm", "https://sinwp.com/camera_clubs/Whitstable-Photographic-Group-1329.htm", "https://sinwp.com/camera_clubs/Whitton-Camera-Club-806.htm", "https://sinwp.com/camera_clubs/Whitworth-Photographic-Society-1843.htm", "https://sinwp.com/camera_clubs/Wickford-Camera-Club-676.htm", "https://sinwp.com/camera_clubs/Wickham-Market-Photographic-Club-1779.htm", "https://sinwp.com/camera_clubs/Wigan-10-Foto-Club-1841.htm", "https://sinwp.com/camera_clubs/Wigan-Photographic-Society-679.htm", "https://sinwp.com/camera_clubs/Wigan-Strobist-Club-1458.htm", "https://sinwp.com/camera_clubs/Wight-Balance-Photography-1643.htm", "https://sinwp.com/camera_clubs/Willfield-Camera-Club-1514.htm", "https://sinwp.com/camera_clubs/Wilmington-Camera-Club-1101.htm", "https://sinwp.com/camera_clubs/Wilmslow-Guild-Photographic-Society-680.htm", "https://sinwp.com/camera_clubs/Wiltshire-Swindon-Photography-1537.htm", "https://sinwp.com/camera_clubs/Wimborne-Camera-Club-681.htm", "https://sinwp.com/camera_clubs/Wincanton-Camera-Club-1043.htm", "https://sinwp.com/camera_clubs/Winchester-Photographic-Society-1311.htm", "https://sinwp.com/camera_clubs/Windlesham-Camberley-Camera-Club-1312.htm", "https://sinwp.com/camera_clubs/Windsor-Photographic-Society-1313.htm", "https://sinwp.com/camera_clubs/Wingrave-Photographic-Interest-Club-1314.htm", "https://sinwp.com/camera_clubs/Winlaton-Camera-Club-684.htm", "https://sinwp.com/camera_clubs/Witham-Camera-Club-686.htm", "https://sinwp.com/camera_clubs/Witney-Photo-Group-1743.htm", "https://sinwp.com/camera_clubs/Wittering-Camera-Club-1341.htm", "https://sinwp.com/camera_clubs/Witterings-Camera-Club-1948.htm", "https://sinwp.com/camera_clubs/Woking-Photographic-Society-1316.htm", "https://sinwp.com/camera_clubs/Wokingham-East-Berkshire-Camera-Club-1949.htm", "https://sinwp.com/camera_clubs/Wolds-Photographic-Society-1978.htm", "https://sinwp.com/camera_clubs/Wolverhampton-Photographic-Society-689.htm", "https://sinwp.com/camera_clubs/Wonston-Worthys-Camera-Club-690.htm", "https://sinwp.com/camera_clubs/Woodbridge-Camera-Club-1532.htm", "https://sinwp.com/camera_clubs/Woodford-Wanstead-Photographic-Society-691.htm", "https://sinwp.com/camera_clubs/Woodford-and-Wanstead-Camera-Club-1781.htm", "https://sinwp.com/camera_clubs/Woodley-Photographic-Club-1132.htm", "https://sinwp.com/camera_clubs/Wooler-District-Camera-Club-1902.htm", "https://sinwp.com/camera_clubs/Woolston-Camera-Club-692.htm", "https://sinwp.com/camera_clubs/Wootton-Bassett-Camera-Club-1044.htm", "https://sinwp.com/camera_clubs/Worcestershire-Camera-Club-693.htm", "https://sinwp.com/camera_clubs/Workington-Camera-Club-694.htm", "https://sinwp.com/camera_clubs/Wrekin-Arts-Photographic-Club-696.htm", "https://sinwp.com/camera_clubs/Wycombe-Photographic-Society-1744.htm", "https://sinwp.com/camera_clubs/Wymondham-and-Attleborough-Camera-Club-697.htm", "https://sinwp.com/camera_clubs/Wyre-Forest-Camera-Club-864.htm", "https://sinwp.com/camera_clubs/Wythall-Photographic-Society-699.htm", "https://sinwp.com/camera_clubs/Wyvern-Camera-Club-1045.htm", "https://sinwp.com/camera_clubs/XRR-Photographic-Society-1133.htm", "https://sinwp.com/camera_clubs/Yardley-Photographic-Society-700.htm", "https://sinwp.com/camera_clubs/Yateley-Camera-Club-701.htm", "https://sinwp.com/camera_clubs/Yeovil-Camera-Club-1046.htm", "https://sinwp.com/camera_clubs/York-Camera-Club-702.htm", "https://sinwp.com/camera_clubs/York-Photographic-Society-703.htm", "https://sinwp.com/camera_clubs/Yoxall-Camera-Club-1871.htm" };
        public static string[] URLs = new String [NUMBER_OF_ENTRIES];
        //public static string[] URLs = UKURLs;
        public static string[] contactURLs = new String[NUMBER_OF_ENTRIES];
        //when you inevitably increase the number of things in the array below, you'll have to make the one below it a 2D array and alter lines 137-145 to allow subsequent known URLs
        public static string[] KNOWN_CONTACT_URLS = { "https://sinwp.com/camera_clubs/", "http://www.n4c.us/", "https://www.caccaphoto.org/", "https://cameracouncil.org/member-clubs/", "https://www.facebook.com/" };
        public static string[] KNOWN_CONTACT_URLS_LOCATOR_KEYWORDS = {"web address:- <a href="};
        //2-dimensional array of contact info in String form
        //ex: int[,] array2D = new int[,] { {email1, phone1, other1}, {email2, phone2, other2}, {email3, phone3, other3}};
        public static String[,] contactInfo = new String[NUMBER_OF_ENTRIES, 3];
        public static String NAME_OF_IO_DOC = "placeholder camera club adds.xlsx";
        public static String SHEET_NAME = "Sheet1";
        public static String PATH_OF_IO_DOC = "C:\\Users\\Owner\\Desktop\\" + NAME_OF_IO_DOC;

        public static Boolean endOfBody = false; //
        public static String[] MAIN_PAGE_SEARCH_KEYWORDS = {"Contact ", "contact ", "CONTACT", "meetings", "MEETINGS", "Meetings"};
        public static String[,] CONTACTS_PAGE_SEARCH_KEYWORDS = { {"president", "Email", "email", "mailto:", "Email Address:", "@" }, { "PHONE", "Phone", "phone", "tel:-", "-", "" }, { "Address", "address", "at", "address:", "Ave", "Rd" }, { "Location", "meet", "location", "Ave", "Rd", "Ln" } };
        //# of types of contact information in the array above
        public static int NUMBER_OF_CONTACT_TYPES = 3;
        //# of keyword items in each array within the array of contact keywords above
        public static int NUMBER_OF_CONTACT_KEYWORDS = 4;

        //if the website's link goes to something including one of the following phrases(paired with an extension from the URL_TYPE_EXTENSIONS list), just remove it and then brute force the contact URL with the following extension words/phrases
        public static String[] URL_REMOVE_EXTENSIONS = {"default", "index", "Default" };
        //in case the scraper can't get the contact page for whatever reason, use the below information to brute force the contact URL
        public static String[] URL_PRE_EXTENSIONS = {"", "about/", "Club/", "info/", "page/" };
        public static String[] URL_EXTENSIONS = {"contact", "contact-us", "contact_us",  "Contact", "about-us", "about", "contact_us", "contact-form", "join-us", "contactus", "About-Us"};
        public static String[] URL_EXTENSION_EXTENSIONS = { "", "2", "-2" };
        public static String[] URL_TYPE_EXTENSIONS = {"", ".html", ".htm", ".aspx", ".php", ".shtml", ".asp"};
        //if unable to find contact page this way, look for facebook link, go there, and then append "about"

        //Contact phrase html segment
        public static int CONTACT_SEGMENT_SIZE = 400;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public GUI()
        {
            InitializeComponent();
            buttonReadSites.Click += new EventHandler(this.buttonReadSites_Click);
            checkBoxEmail.CheckedChanged += new EventHandler(this.checkBoxEmail_CheckedChanged);
            checkBoxPhone.CheckedChanged += new EventHandler(this.checkBoxPhone_CheckedChanged);
            checkBoxOther.CheckedChanged += new EventHandler(this.checkBoxOther_CheckedChanged);
            buttonLocateContacts.Click += new EventHandler(this.buttonLocateContacts_Click);
            buttonGetURLs.Click += new EventHandler(this.buttonGetURLs_Click);
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

        //                                                              Beginning of source url locating/gathering section
        //*****************************************************************************************************************************************************************************************

        private void buttonGetURLs_Click(object sender, EventArgs e)
        {
            //read the URLs from the excel doc to an array of strings
            WorkBook workbook = WorkBook.Load(PATH_OF_IO_DOC);
            WorkSheet worksheet = workbook.GetWorkSheet(SHEET_NAME);

            int rowCount = NUMBER_OF_ENTRIES;
            //start at row 2 to skip the first header
            for (int i = 0; i < rowCount; i++)
            {
                //get value by cell address
                //string address_val = ws["A" + rowCount].ToString();
                //get value by row and column indexing
                string index_val = worksheet.Rows[i].Columns[READING_COLUMN].ToString();

                //read each cell's value to the array of URLs
                URLs[i] = index_val;

                //check to make sure correct values are collected
                Console.WriteLine(i + "'{0}'", index_val);
            }
            Console.WriteLine("Finished getting site URLs");
            Console.WriteLine("Focus on getting/reading contact URLs");
            Console.WriteLine("");

        }

        //                                                              End of source url locating/gathering section, beginning of main page url locating/gathering section
        //*****************************************************************************************************************************************************************************************

        private void buttonLocateContacts_Click(object sender, EventArgs e)
        {
            while (URLIndex < NUMBER_OF_ENTRIES)
            {
                try
                {
                    while (URLIndex < NUMBER_OF_ENTRIES)
                    {
                        string url = URLs[URLIndex];
                        string html = getHTML(url);

                        //make sure the url is valid
                        if (!(url == null || url == ""))
                        {
                            //if the url does not start with the url for a known club coalition site or is too short to read (but not empty) then read the HTML
                            if (((url.Length > 0) && (url.Length < KNOWN_CONTACT_URLS[0].Length)) || !((url.Substring(0, KNOWN_CONTACT_URLS[0].Length) == KNOWN_CONTACT_URLS[0]) /* || (url.Substring(0, knownContactURLs[1].Length) == knownContactURLs[1])*/))
                            {
                                contactURLs[URLIndex] = getURLFromHTML(-1, html, MAIN_PAGE_SEARCH_KEYWORDS);
                            }
                            //otherwise if the url is a known contact URL, set the contactURLs entry to the url provided by the known site
                            else if ((url.Substring(0, KNOWN_CONTACT_URLS[0].Length) == KNOWN_CONTACT_URLS[0])/* || (url.Substring(0, knownContactURLs[1].Length) == knownContactURLs[1])*/)
                            {
                                Console.WriteLine("Oooh! I know this site!");
                                //set the URL at the current spot to that found at the known weppage
                                URLs[URLIndex] = getURLFromHTML(0, html, KNOWN_CONTACT_URLS_LOCATOR_KEYWORDS);
                                //set the contacts page URL to the one found in the new webpage's HTML
                                contactURLs[URLIndex] = getURLFromHTML(0, getHTML(URLs[URLIndex]), KNOWN_CONTACT_URLS_LOCATOR_KEYWORDS);

                                Console.WriteLine(URLIndex);
                                Console.WriteLine("");
                                Console.WriteLine(contactURLs[URLIndex]);
                                Console.WriteLine("");
                                Console.WriteLine("");
                            }
                            //otherwise, print something else
                            else if ((url.Length == 0)/* || (url.Substring(0, knownContactURLs[1].Length) == knownContactURLs[1])*/)
                            {
                                contactURLs[URLIndex] = url;

                                Console.WriteLine("Empty URL");
                                Console.WriteLine("");

                            }
                        }
                        else if (url == "")
                        {
                            //i think I'm doing this bit right
                            tryGoogling();
                        }
                        //increment the starting URLindex after an exception is seen so that it is incremented properly in the exception handlers
                        URLIndex++;
                    }

                    Console.WriteLine("");
                    Console.WriteLine("Finished getting sites' HTML/main page URLs");
                    Console.WriteLine("Try limiting the HTML segment analyzed for the contacts page link to the first <a> tag before the keyphrase");
                    Console.WriteLine("");
                }
                //catch the null argument exception and let user try again, starting at the next URL
                catch (ArgumentNullException ex)
                {
                    Console.WriteLine("Null Argument Exception caught, try again.");
                    Console.WriteLine("");
                    contactURLs[URLIndex] = getURLFromHTML(-1, "", MAIN_PAGE_SEARCH_KEYWORDS);
                    URLIndex++;
                }
                //catch the web exception and let user start again, starting at the next URL
                catch (WebException ex)
                {
                    Console.WriteLine("Web (unable to resolve host name) Exception caught, try again.");
                    Console.WriteLine("");
                    contactURLs[URLIndex] = getURLFromHTML(-1, "", MAIN_PAGE_SEARCH_KEYWORDS);
                    URLIndex++;
                }
            }
        }

        private static string getHTML(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.UserAgent = "C# console client";

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();  // can't do .ToLower(); because it ruins the urls, I've tested it
                    //maybe return 2 strings one of the standard and one of the .ToLower-ed html, so you can search the .ToLower-ed HTML and then go to that character in the unaltered html for the url
                    //that's just for later on, when efficiency and not debugging is my main focus
                }
            }
            catch (UriFormatException ex)
            {
                Console.WriteLine("exception caused by url being '" + url + "'");
                Console.WriteLine("");

                return "";
            }
        }

        private static string getURLFromHTML(int knownURLIndex, string html, String[] searchKeywords)
        {
            string foundURL = "";
            try
            {
                endOfBody = false;
                //make sure the input is not empty
                if (html != "" && knownURLIndex < 0)
                {
                    //Show the webpage currently being read
                    Console.WriteLine(URLIndex);
                    //print first few chars of HTML as indication of proper functioning
                    Console.WriteLine(html.Substring(0, 15 + 20));

                    int i = 0;
                    bool foundContact = false;
                    //read through html until it reaches the end of the body
                    while (!endOfBody && !foundContact)
                    {
                        //read through all of the search keywords
                        for (int j = 0; j < searchKeywords.Length; j++)
                        {
                            //if the site's HTML includes the keywords somewhere, look nearby it for the URL of the contacts page
                            if (html.Substring(i, searchKeywords[j].Length).StartsWith(searchKeywords[j]))
                            {
                                //find the URL then set foundURL to it
                                //look through nearby html for contacts page URL then set foundURL to that string
                                foundURL = getContactURLFromHTMLSegment(html.Substring(i - 200, 400));

                                //debugging
                                Console.WriteLine("Found desired page phrase in HTML at character #" + i);
                                if (i - CONTACT_SEGMENT_SIZE >= 0)
                                    Console.WriteLine(html.Substring(i - CONTACT_SEGMENT_SIZE, CONTACT_SEGMENT_SIZE + searchKeywords[j].Length));
                                else
                                    Console.WriteLine(html.Substring(0, CONTACT_SEGMENT_SIZE + searchKeywords[j].Length));


                                Console.WriteLine("Contacts page URL = '" + foundURL + "'");

                                foundContact = true;
                                break;
                            }
                            //move on to the next HTML once it's finished reading through this HTML
                            if (html.Substring(i, 7).StartsWith("</body>"))
                            {
                                endOfBody = true;
                                break;
                            }
                        }

                        i++;
                    }

                    Console.WriteLine("");
                    //it turns out that .Substring() in C# is not the same as .substring() in Java
                }
                //basically copy+paste with slight modifications for other known URL indices
                else if (knownURLIndex == 0)
                {
                    //Show the webpage currently being read
                    Console.WriteLine(URLIndex);
                    //print first few chars of HTML as indication of proper functioning
                    Console.WriteLine(html.Substring(0, 15 + 20));

                    bool foundContact = false;
                    int startIndex = 0;
                    int endIndex = 0;
                    int i = 0;
                    //read through html until it finds the right keyword
                    while (!endOfBody && !foundContact)
                    {
                        //read through all of MAIN_PAGE_SEARCH_KEYWORDS
                        for (int j = 0; j < searchKeywords.Length; j++)
                        {
                            //look through nearby html for contacts page URL then set foundURL to that string
                            startIndex = i + searchKeywords[j].Length + 1;
                            //if the site's HTML includes the keywords somewhere, look nearby it for the URL of the contacts page
                            if (html.Substring(i, searchKeywords[j].Length).StartsWith(searchKeywords[j]))
                            {
                                int k = 0;
                                while (k < 500)
                                {
                                    //break at the closing " in the html, signifying the end of the HTML
                                    if (html[startIndex + k + 1] == '"')
                                        break;
                                    k++;
                                }
                                endIndex = startIndex + k + 1;

                                foundURL = html.Substring(startIndex, endIndex - startIndex);

                                //check to make sure the url linked by sinwp is valid, and if it is then set the main url to the respective main page url
                                if (foundURL.Substring(0, 4) == "http")
                                    URLs[URLIndex] = foundURL;

                                foundURL = getURLFromHTML(-1, getHTML(foundURL), MAIN_PAGE_SEARCH_KEYWORDS);

                                //if there is no url given, then google the club
                                //if (foundURL != "")
                                //    tryGoogling();

                                //debugging
                                Console.WriteLine("Found desired page phrase in sinwp page HTML at character #" + i);
                                Console.WriteLine("Main page URL = " + foundURL);
                                //if (i - CONTACT_SEGMENT_SIZE >= 0)
                                //    Console.WriteLine(html.Substring(i - CONTACT_SEGMENT_SIZE, CONTACT_SEGMENT_SIZE + searchKeywords[j].Length));
                                //else
                                //    Console.WriteLine(html.Substring(0, CONTACT_SEGMENT_SIZE + searchKeywords[j].Length));

                                foundContact = true;
                                break;
                            }
                            //move on to the next HTML once it's finished reading through this HTML
                            if (html.Substring(i, 7).StartsWith("</body>"))
                            {
                                endOfBody = true;
                                break;
                            }
                        }

                        i++;
                    }
                }
                else if (knownURLIndex == 1)
                {
                    //check this page: http://www.n4c.us/camera_clubs.htm for the desired club
                    //if it isn't there, try googling the club, and if that fails do the next comment
                    //set all contact info to that available on this page: http://www.n4c.us/contact_us.htm

                }
                else
                {

                    Console.WriteLine(URLIndex);
                    Console.WriteLine("Somehow there was no html at this URL");
                    Console.WriteLine("");
                }

                //return the url
                return foundURL;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine("Looked for keyphrases outside of html for some reason.");
                Console.WriteLine(ex);
                Console.WriteLine("");

                //return the url
                return foundURL;
            }
        }

        //                                                              End of main page url locating/gathering section, beginning of contact locating/gathering section
        //*****************************************************************************************************************************************************************************************

        private void buttonReadSites_Click(object sender, EventArgs e)
        {
            //read the information on the new site URL
            //basically the same as buttonLocateContacts_Click(), but it stores the contact data collected
            try
            {
                URLIndex = 0;

                while (URLIndex < NUMBER_OF_ENTRIES)
                {
                    string url = contactURLs[URLIndex];

                    //make sure the url is defined
                    if (url != null)
                        getContactsFromURL(url);

                    //increment the starting URLindex after an exception is seen so that it is incremented properly in the exception handlers
                    URLIndex++;
                }

                Console.WriteLine("");
                Console.WriteLine("Finished getting sites' contact information");
                Console.WriteLine("");
            }
            //catch the null argument exception and let user try again, starting at the next URL
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(URLIndex);
                Console.WriteLine("Null Argument Exception caught, try again.");
                getContactsFromURL("");
                URLIndex++;
            }
            //catch the web exception and let user start again, starting at the next URL
            catch (WebException ex)
            {
                Console.WriteLine(URLIndex);
                Console.WriteLine("Web (unable to resolve host name) Exception caught, try again.");
                getContactsFromURL("");
                URLIndex++;
            }
        }

        private static void getContactsFromURL(string url)
        {

            try
            {
                //make sure the url is not empty
                if (url != "")
                {
                    string html = getHTML(url);
                    //make sure the input is not empty
                    if (html != "")
                    {
                        //Show the webpage currently being read
                        Console.WriteLine(URLIndex);
                        //print first 4 chars of HTML as indication of proper functioning
                        Console.WriteLine(html.Substring(0, 15));

                        int i = 0;
                        //read through html until it 
                        while (!endOfBody)
                        {
                            //read through all of CONTACTS_PAGE_SEARCH_KEYWORDS arrays
                            for (int j = 0; j < NUMBER_OF_CONTACT_TYPES; j++)
                            {
                                //read through the items of the arrays of the array (ex, CONTACTS_PAGE_SEARCH_KEYWORDS array 1, item 3)
                                for (int k = 0; k < NUMBER_OF_CONTACT_KEYWORDS; k++)                                         //problem here
                                {
                                    //if the site's HTML includes the keywords somewhere, look nearby it for the contacts information
                                    if (html.Substring(i, CONTACTS_PAGE_SEARCH_KEYWORDS[j, k].Length).StartsWith(CONTACTS_PAGE_SEARCH_KEYWORDS[j, k]))
                                    {
                                        //look through nearby html for contacts
                                        String contactHTMLSegment;
                                        if (i - CONTACT_SEGMENT_SIZE >= 0)
                                            contactHTMLSegment = html.Substring(i - CONTACT_SEGMENT_SIZE, CONTACT_SEGMENT_SIZE + MAIN_PAGE_SEARCH_KEYWORDS[j].Length);
                                        else
                                            contactHTMLSegment = html.Substring(0, CONTACT_SEGMENT_SIZE + MAIN_PAGE_SEARCH_KEYWORDS[j].Length);

                                        //set the contact info to that found, checking to make sure that it's entering the right type of info
                                        //email
                                        if (j == 0)
                                        {
                                            contactInfo[URLIndex, 0] = getContactURLFromHTMLSegment(contactHTMLSegment);
                                            Console.WriteLine("Found EMAIL contact phrase in HTML at character #" + i);
                                            Console.WriteLine(contactHTMLSegment);
                                        }
                                        //phone
                                        if (j == 1)
                                        {
                                            contactInfo[URLIndex, 1] = getContactURLFromHTMLSegment(contactHTMLSegment);
                                            Console.WriteLine("Found PHONE contact phrase in HTML at character #" + i);
                                            Console.WriteLine(contactHTMLSegment);
                                        }
                                        //other
                                        if (j == 2)
                                        {
                                            contactInfo[URLIndex, 2] = getContactURLFromHTMLSegment(contactHTMLSegment);
                                            Console.WriteLine("Found OTHER contact phrase in HTML at character #" + i);
                                            Console.WriteLine(contactHTMLSegment);
                                        }
                                    }
                                }
                            }

                            //move on to the next HTML once it's finished reading through this HTML
                            if (html.Substring(i, 14).StartsWith("</body>"))
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
                        contactInfo[URLIndex, 0] = "Somehow there was no html at this URL";
                        //phone
                        contactInfo[URLIndex, 1] = "Somehow there was no html at this URL";
                        //other
                        contactInfo[URLIndex, 2] = "Somehow there was no html at this URL";

                        //debugging
                        Console.WriteLine(URLIndex);
                        Console.WriteLine("Somehow there was no html at this URL");
                        Console.WriteLine("");
                    }
                }
                //if the url provided is empty
                else if (url != "Sorry, I couldn't find a contacts page.")
                {
                    //getting the url:
                    //check for a facebook link
                    contactURLs[URLIndex] = checkForFacebookLink(url);

                    //if that fails, try brute-forcing the contact page url
                    contactURLs[URLIndex] = tryBruteForce(url);

                    //disabled for first round of testing
                    ////if that fails, try googling the club based on the url stored in the respective URLs[] index
                    //contactURLs[URLIndex] = tryGoogling();

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
            }
            catch (IndexOutOfRangeException ex)
            {
                //set contact info of the contact to show that it couldn't get contact info for it
                //email
                contactInfo[URLIndex, 0] = "The index was out of bounds I guess";
                //phone
                contactInfo[URLIndex, 1] = "The index was out of bounds I guess";
                //other
                contactInfo[URLIndex, 2] = "The index was out of bounds I guess";

                //debugging
                Console.WriteLine(URLIndex);
                Console.WriteLine("The index was out of bounds I guess");
                Console.WriteLine(ex);
                Console.WriteLine("");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                //set contact info of the contact to show that it couldn't get contact info for it
                //email
                contactInfo[URLIndex, 0] = "Looked for keyphrases outside of html for some reason";
                //phone
                contactInfo[URLIndex, 1] = "Looked for keyphrases outside of html for some reason";
                //other
                contactInfo[URLIndex, 2] = "Looked for keyphrases outside of html for some reason";

                //debugging
                Console.WriteLine("Looked for keyphrases outside of html for some reason");
                Console.WriteLine(ex);
                Console.WriteLine("");
            }
        }

        private static String getContactURLFromHTMLSegment(String segment)
        {
            String contact = "";
            int startIndex = 0;
            int endIndex = 0;

            for (int i = 0; i < segment.Length - 5; i++)
            {
                if (segment.Substring(i, 5) == "href=")
                {
                    startIndex = i + 6;

                    int k = 0;
                    while (k < segment.Length)
                    {
                        //break at the closing " in the html, signifying the end of the HTML
                        if (segment[startIndex + k + 1] == '"')
                            break;
                        k++;
                    }
                    endIndex = startIndex + k + 1;

                    contact = segment.Substring(startIndex, endIndex - startIndex);
                    break;
                }
                else if (segment.Substring(i, 4) == "src=")
                {
                    startIndex = i + 5 + 1;

                    int k = 0;
                    while (k < segment.Length)
                    {
                        //break at the closing " in the html, signifying the end of the URL
                        if (segment[startIndex + k + 1] == '"')
                            break;
                        k++;
                    }
                    endIndex = startIndex + k + 1;

                    contact = segment.Substring(startIndex, endIndex);
                    break;
                }
            }

            ////check to make sure the url about to be returned is valid, and if it isn't make the returned url reflect that
            //if (contact.Substring(0, 4) != "http")
            //    contact = "The contacts page url that I was going to return was not valid";

            return contact;
        }

        private static String checkForFacebookLink(String url)
        {
            String[] searchKeywords = { "https://www.facebook.com/" };
            string html = getHTML(url);
            string foundURL = getURLFromHTML(-1, html, searchKeywords);

            return foundURL;
        }

        private static String tryBruteForce(String url)
        {
            //find contacts page url given the main page url
            string baseURL = url;
            string foundURL = url;

            //check for/remove any removable phrases in the url
            for (int i = 0; i < URL_REMOVE_EXTENSIONS.Length; i++)
            {
                //if the last bit of the home page URL is a known removable phrase
                if (baseURL.Substring(baseURL.Length - URL_REMOVE_EXTENSIONS[i].Length) == URL_REMOVE_EXTENSIONS[i])
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
                for (int j = 0; j < URL_EXTENSIONS.Length; j++)
                {
                    for (int k = 0; k < URL_EXTENSION_EXTENSIONS.Length; k++)
                    {
                        for (int l = 0; l < URL_TYPE_EXTENSIONS.Length; l++)
                        {
                            foundURL = baseURL + URL_PRE_EXTENSIONS[i] + URL_EXTENSIONS[j] + URL_EXTENSION_EXTENSIONS[k] + URL_TYPE_EXTENSIONS[l];
                            if (!(getHTML(foundURL) == ""))
                                break;
                        }
                    }
                }
            }

            return foundURL;
        }

        private static String tryGoogling()
        {
            //get club text
            WorkBook workbook = WorkBook.Load(PATH_OF_IO_DOC);
            WorkSheet worksheet = workbook.GetWorkSheet(SHEET_NAME);
            //get seach words by looking at the name of the club and what state it's located in
            string clubGooglePhrase = worksheet.Rows[URLIndex].Columns[READING_COLUMN - 1].ToString() + " " + worksheet.Rows[URLIndex].Columns[READING_COLUMN + 1].ToString();
            //check to make sure correct value is collected
            Console.WriteLine(URLIndex + "'{0}'", clubGooglePhrase);

            //then assemble search URL
            String url = assembleGoogleURL(clubGooglePhrase);

            //then look for the first search result's URL
            String[] searchKeywords = { };
            string html = getHTML(url);
            string foundURL = "";
            getURLFromHTML(-1, html, searchKeywords);

            return foundURL;
        }

        private static String assembleGoogleURL(String clubPhrase)
        {
            String[] clubPhraseWords = { };
            for (int i = 0; i < clubPhraseWords.Length; i++)
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

        //                                                                               End of contact locating/gathering section, beginning of contact info writing section
        //*************************************************************************************************************************************************************************

        private void buttonWriteContacts_Click(object sender, EventArgs e)
        {
            //read the URLs from the excel doc to an array of strings
            WorkBook workbook = WorkBook.Load(PATH_OF_IO_DOC);
            WorkSheet worksheet = workbook.GetWorkSheet(SHEET_NAME);

            int rowCount = NUMBER_OF_ENTRIES;
            //start at row 2 to skip the first header
            for (int i = 2; i < rowCount; i++)
            {
                //check to make sure correct values for correct column are written
                Console.WriteLine(i);

                //set value by cell address
                //set value by row and column indexing
                worksheet[MAIN_URL_WRITING_COLUMN + i].Value = URLs[i];
                Console.WriteLine(URLs[i]);
                worksheet[CONTACT_URL_WRITING_COLUMN + i].Value = contactURLs[i];
                Console.WriteLine(contactURLs[i]);
                worksheet[EMAIL_WRITING_COLUMN + i].Value = contactInfo[i, 0];
                Console.WriteLine(contactInfo[i, 0]);
                worksheet[PHONE_WRITING_COLUMN + i].Value = contactInfo[i, 1];
                Console.WriteLine(contactInfo[i, 1]);
                worksheet[ADDRESS_WRITING_COLUMN + i].Value = contactInfo[i, 2];
                Console.WriteLine(contactInfo[i, 2]);
                worksheet[MEETING_LOCATION_WRITING_COLUMN + i].Value = contactInfo[i, 2];
                Console.WriteLine(contactInfo[i, 2]);

                Console.WriteLine("");
            }

            //save the altered workbook
            workbook.Save();
            Console.WriteLine("Finished writing contact information to workbook.");
        }

        private void checkBoxEmail_CheckedChanged(object sender, EventArgs e)
        {
            //do something here
        }

        private void checkBoxPhone_CheckedChanged(object sender, EventArgs e)
        {
            //do something here
        }

        private void checkBoxOther_CheckedChanged(object sender, EventArgs e)
        {
            //do something here
        }

        #endregion

        private System.Windows.Forms.TabControl pageControl;
        private System.Windows.Forms.TabPage page1;
        private System.Windows.Forms.TabPage page2;
        private System.Windows.Forms.Button buttonReadSites;
        private System.Windows.Forms.Button buttonLocateContacts;
        private System.Windows.Forms.Button buttonGetURLs;
        private System.Windows.Forms.Label labelInfoToGather;
        private System.Windows.Forms.Label title1;
        private CheckBox checkBoxOther;
        private CheckBox checkBoxPhone;
        private CheckBox checkBoxEmail;
        private Button buttonWriteContacts;
        private Label label2;
        private Label label1;
    }
}

/*
Sources:
                                https://stackoverflow.com/questions/16160676/read-excel-data-line-by-line-with-c-sharp-net
                                https://www.wfmj.com/story/42504801/c-read-excel-file-example
                                https://ironsoftware.com/csharp/excel/tutorials/csharp-open-write-excel-file/
    Prepping the Excel sheets (Add " " to the new cell values, use the =CONCAT function to concatenate all the urls into one cell, then use a word processor to replace the spaces with "", "", finally, copy+paste the string of urls into this program):
                                http://howtouseexcel.net/how-to-extract-a-url-from-a-hyperlink-on-excel
                                http://zetcode.com/csharp/readwebpage/
                                https://docs.microsoft.com/en-us/dotnet/api/system.string.startswith?view=net-5.0#System_String_StartsWith_System_String_
    An excel sheet from a friend to get some practice and testing data for the program

*/

