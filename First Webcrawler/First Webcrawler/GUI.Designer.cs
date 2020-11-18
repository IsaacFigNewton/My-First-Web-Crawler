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
        public static int NUMBER_OF_ENTRIES = 91-80;
        public static int READING_COLUMN = 1;
        public static String EMAIL_WRITING_COLUMN = "F";
        public static String PHONE_WRITING_COLUMN = "D";
        public static String OTHER_WRITING_COLUMN = "G";

        public static int URLIndex = 1;
        public static string [] URLs = new String [NUMBER_OF_ENTRIES];
        public static string[] contactURLs = new String[NUMBER_OF_ENTRIES];
        public static string[] knownContactURLs = {"https://sinwp.com/camera_clubs/"/*,  "https://sinwp.com/camera_clubs/"*/};
        //2-dimensional array of contact info in String form
        //ex: int[,] array2D = new int[,] { {email1, phone1, other1}, {email2, phone2, other2}, {email3, phone3, other3}};
        public static String[,] contactInfo = new String[NUMBER_OF_ENTRIES, 3];
        public static String PATH_OF_IO_DOC = "C:\\Users\\Owner\\Desktop\\List of Camera Clubs.xlsx";
        public static String SHEET_NAME = "Midwest (ND,SD,NE,KS,OK,TX,MN,I";

        public static Boolean endOfBody = false; //
        public static String[] MAIN_PAGE_SEARCH_KEYWORDS = {"Contact ", "contact ", "CONTACT" };
        public static String[,] CONTACTS_PAGE_SEARCH_KEYWORDS = { {"Email:", "Email-", "email:", "mailto:" }, { "Phone:", "tel:-", "phone:", "phone-" }, { "Other:", "Other-", "other:", "web address:- " } };
        //# of types of contact information in the array above
        public static String[] URL_EXTENSIONS = {"contact_us", "contact-us", "contact", "map-and-contact", "info/contact" };
        public static String[] URL_TYPE_EXTENSIONS = {"", ".htm", ".html", ".php", ".aspx"};
        public static int NUMBER_OF_CONTACT_TYPES = 3;
        //# of keyword items in each array within the array of contact keywords above
        public static int NUMBER_OF_CONTACT_KEYWORDS = 4;

        //Contact phrase html segment
        public static int CONTACT_SEGMENT_SIZE = 200;

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

        //screen 1

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
            Console.WriteLine("Edit contact page search section to just try all combos of URL_EXTENSIONS and URL_TYPE_EXTENSIONS appended to original URL for contact page");
            Console.WriteLine("Also, focus on boundary cases instead of all URLs");
            Console.WriteLine("Also, go to sites linked by sinwp to look for contact page");

        }

        private void buttonLocateContacts_Click(object sender, EventArgs e)
        {
            while (URLIndex < URLs.Length)
            {
                try
                {
                    while (URLIndex < URLs.Length)
                    {
                        string html;
                        string url = URLs[URLIndex];

                        //make sure the url is valid
                        if (!(url == null || url == ""))
                        {
                            //if the url does not start with the url for the contacts page of a club or is too short to read (but not empty) then read the HTML
                            if (((url.Length > 0) && (url.Length < knownContactURLs[0].Length)) || !((url.Substring(0, knownContactURLs[0].Length) == knownContactURLs[0]) /* || (url.Substring(0, knownContactURLs[1].Length) == knownContactURLs[1])*/))
                            {
                                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                                request.UserAgent = "C# console client";

                                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                                using (Stream stream = response.GetResponseStream())
                                using (StreamReader reader = new StreamReader(stream))
                                {
                                    html = reader.ReadToEnd();
                                }

                                getContactURLs(html);
                            }
                            //otherwise if the url is a known contact URL, set the contactURLs entry to the respective url
                            else if ((url.Substring(0, knownContactURLs[0].Length) == knownContactURLs[0])/* || (url.Substring(0, knownContactURLs[1].Length) == knownContactURLs[1])*/)
                            {
                                contactURLs[URLIndex] = url;

                                Console.WriteLine(URLIndex);
                                Console.WriteLine(url.Substring(8, 22));
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
                        //increment the starting URLindex after an exception is seen so that it is incremented properly in the exception handlers
                        URLIndex++;
                    }

                    Console.WriteLine("");
                    Console.WriteLine("Finished getting sites' HTML");
                    Console.WriteLine("");
                }
                //catch the null argument exception and let user try again, starting at the next URL
                catch (ArgumentNullException ex)
                {
                    Console.WriteLine("Null Argument Exception caught, try again.");
                    Console.WriteLine("");
                    getContactURLs("");
                    URLIndex++;
                }
                //catch the web exception and let user start again, starting at the next URL
                catch (WebException ex)
                {
                    Console.WriteLine("Web (unable to resolve host name) Exception caught, try again.");
                    Console.WriteLine("");
                    getContactURLs("");
                    URLIndex++;
                }
            }
        }

        private static void getContactURLs (string html)
        {
            try
            {
                endOfBody = false;
                //make sure the input is not empty
                if (html != "")
                {
                    //Show the webpage currently being read
                    Console.WriteLine(URLIndex);
                    //print first few chars of HTML as indication of proper functioning
                    Console.WriteLine(html.Substring(0, 15+20));

                    int i = 0;
                    //read through html until it 
                    while (!endOfBody)
                    {
                        //read through all of MAIN_PAGE_SEARCH_KEYWORDS
                        for (int j = 0; j < MAIN_PAGE_SEARCH_KEYWORDS.Length; j++)
                        {
                            //if the site's HTML includes the keywords somewhere, look nearby it for the URL of the contacts page
                            if (html.Substring(i, MAIN_PAGE_SEARCH_KEYWORDS[j].Length).StartsWith(MAIN_PAGE_SEARCH_KEYWORDS[j]))
                            {
                                //debugging
                                Console.WriteLine("Found contacts page phrase in HTML at character #" + i);
                                if (i - CONTACT_SEGMENT_SIZE >= 0)
                                    Console.WriteLine(html.Substring(i - CONTACT_SEGMENT_SIZE, CONTACT_SEGMENT_SIZE + MAIN_PAGE_SEARCH_KEYWORDS[j].Length));
                                else
                                    Console.WriteLine(html.Substring(0, CONTACT_SEGMENT_SIZE + MAIN_PAGE_SEARCH_KEYWORDS[j].Length));

                                //look through nearby html for contacts page URL

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
                else
                {
                    Console.WriteLine(URLIndex);
                    Console.WriteLine("Somehow there was no html at this URL");
                    Console.WriteLine("");
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine("Looked for keyphrases outside of html for some reason.");
                Console.WriteLine(ex);
                Console.WriteLine("");
            }
        }

        private void buttonReadSites_Click(object sender, EventArgs e)
        {
            //read the information on the new site URL
            //basically the same as buttonLocateContacts_Click(), but it stores the contact data collected
            try {
                URLIndex = 0;

                while (URLIndex < URLs.Length)
                {
                    string html;
                    string url = contactURLs[URLIndex];

                    //make sure the url is there
                    if (!(url == null || url == "" || url.Length == 0))
                    {
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                        request.UserAgent = "C# console client";

                        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                        using (Stream stream = response.GetResponseStream())
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            html = reader.ReadToEnd();
                        }

                        getContacts(html);
                    }

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
                getContacts("");
                URLIndex++;
            }
            //catch the web exception and let user start again, starting at the next URL
            catch (WebException ex)
            {
                Console.WriteLine(URLIndex);
                Console.WriteLine("Web (unable to resolve host name) Exception caught, try again.");
                getContacts("");
                URLIndex++;
            }
        }

        private static void getContacts(string html)
        {
            try
            {
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
                                        contactInfo[URLIndex, 0] = getContactFromHTMLSegment(contactHTMLSegment);
                                        Console.WriteLine("Found EMAIL contact phrase in HTML at character #" + i);
                                        Console.WriteLine(contactHTMLSegment);
                                    }
                                    //phone
                                    if (j == 1)
                                    {
                                        contactInfo[URLIndex, 1] = getContactFromHTMLSegment(contactHTMLSegment);
                                        Console.WriteLine("Found PHONE contact phrase in HTML at character #" + i);
                                        Console.WriteLine(contactHTMLSegment);
                                    }
                                    //other
                                    if (j == 2)
                                    {
                                        contactInfo[URLIndex, 2] = getContactFromHTMLSegment(contactHTMLSegment);
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

        private static String getContactFromHTMLSegment (String segment)
        {
            String contact = "";
            int startIndex = 0;
            int endIndex = 0;

            for (int i = 0; i < segment.Length - 5; i++)
            {
                if (segment.Substring(i, 5) == "href=")
                {
                    startIndex = i + 6;

                    char temp = 'j';
                    int j = startIndex;
                    while (temp != '"')
                    {
                        temp = segment[j];
                        j++;
                    }
                    endIndex = j;

                    contact = segment.Substring(startIndex, endIndex - startIndex);
                    break;
                }
                else if (segment.Substring(i, 4) == "src=")
                {
                    startIndex = i + 5;

                    char temp = 'j';
                    int j = startIndex;
                    while (temp != '"')
                    {
                        temp = segment[j];
                        j++;
                    }
                    endIndex = j;

                    contact = segment.Substring(startIndex, endIndex - startIndex);
                    break;
                }
            }

            return contact;
        }

        private void buttonWriteContacts_Click(object sender, EventArgs e)
        {
            //read the URLs from the excel doc to an array of strings
            WorkBook workbook = WorkBook.Load(PATH_OF_IO_DOC);
            WorkSheet worksheet = workbook.GetWorkSheet(SHEET_NAME);

            int rowCount = NUMBER_OF_ENTRIES;
            //start at row 2 to skip the first header
            for (int i = 0; i < rowCount; i++)
            {
                //check to make sure correct values for correct column are written
                Console.WriteLine(i);

                //set value by cell address
                //set value by row and column indexing
                worksheet[EMAIL_WRITING_COLUMN + i].Value = contactInfo[i, 0];
                Console.WriteLine(contactInfo[i, 0]);
                worksheet[PHONE_WRITING_COLUMN + i].Value = contactInfo[i, 1];
                Console.WriteLine(contactInfo[i, 1]);
                worksheet[OTHER_WRITING_COLUMN + i].Value = contactInfo[i, 2];
                Console.WriteLine(contactInfo[i, 2]);

                Console.WriteLine("");
            }
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
    http://howtouseexcel.net/how-to-extract-a-url-from-a-hyperlink-on-excel
    http://zetcode.com/csharp/readwebpage/
    https://docs.microsoft.com/en-us/dotnet/api/system.string.startswith?view=net-5.0#System_String_StartsWith_System_String_

*/

