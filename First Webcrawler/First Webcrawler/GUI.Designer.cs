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
        public static int NUMBER_OF_ENTRIES = 91;
        public static int URLIndex = 1;
        public static string [] URLs = new String [NUMBER_OF_ENTRIES];
        public static string[] contactURLs = new String[NUMBER_OF_ENTRIES];
        public static string[] knownContactURLs = {"https://sinwp.com/camera_clubs/"/*,  "https://sinwp.com/camera_clubs/"*/};
        //2-dimensional array of contact info in String form
        //ex: int[,] array2D = new int[,] { {email1, phone1, other1}, {email2, phone2, other2}, {email3, phone3, other3}};
        public static String[,] contactInfo = new String[NUMBER_OF_ENTRIES, NUMBER_OF_ENTRIES];
        public static String PATH_OF_IO_DOC = "C:\\Users\\Owner\\Desktop\\List of Camera Clubs.xlsx";
        public static String SHEET_NAME = "Midwest (ND,SD,NE,KS,OK,TX,MN,I";

        public static String[] MAIN_PAGE_SEARCH_KEYWORDS = { "Contact", "Contact Us", "contact", "CONTACT"};
        public static String[,] CONTACTS_PAGE_SEARCH_KEYWORDS = { { "Email:", "Email-", "email:", "e-mail:- " }, { "Phone:", "Phone-", "phone:", "phone-" }, { "Other:", "Other-", "other:", "web address:- " } };
        //# of types of contact information in the array above
        public static int NUMBER_OF_CONTACT_TYPES = 3;
        //# of keyword items in each array within the array of contact keywords above
        public static int NUMBER_OF_CONTACT_KEYWORDS = 4;

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
            WorkBook wb = WorkBook.Load(PATH_OF_IO_DOC);
            WorkSheet ws = wb.GetWorkSheet(SHEET_NAME);

            int rowCount = NUMBER_OF_ENTRIES;
            //start at row 2 to skip the first header
            for (int i = 0; i < rowCount; i++)
            {
                    //get value by cell address
                    //string address_val = ws["A" + rowCount].ToString();
                    //get value by row and column indexing
                    string index_val = ws.Rows[i].Columns[1].ToString();

                    //read each cell's value to the array of URLs
                    URLs[i] = index_val;

                //check to make sure correct values are collected
                Console.WriteLine(i + "'{0}'", index_val);
            }
            Console.WriteLine("Finished getting site URLs" + ", also an ArgumentOutOfRangeException is caused by line 286");
        }

        private void buttonLocateContacts_Click(object sender, EventArgs e)
        {
            while (URLIndex < URLs.Length)
            {
                lookAtHTML();
            }
        }

        private static void lookAtHTML()
        {
            try
            {                                                  //refactor to new method to run multiple times

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

                            //print first 4 chars of HTML as indication of proper functioning
                            Console.WriteLine(html.Substring(0, 15));
                            Console.WriteLine(URLIndex);
                            Console.WriteLine("");
                        }
                        //otherwise if the url is a known contact URL, set the contactURLs entry to the respective url
                        else if ((url.Substring(0, knownContactURLs[0].Length) == knownContactURLs[0])/* || (url.Substring(0, knownContactURLs[1].Length) == knownContactURLs[1])*/)
                        {
                            contactURLs[URLIndex] = url;

                            Console.WriteLine(url.Substring(8, 22));
                            Console.WriteLine(URLIndex);
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

                Console.WriteLine("Finished getting sites' HTML");
            }
            //catch the null argument exception and let user try again, starting at the next URL
            catch (ArgumentNullException ex)
            {
                Console.WriteLine("Null Argument Exception caught: {0}", ex, ", try again.");
                getContactURLs("");
                URLIndex++;
            }
            //catch the web exception and let user start again, starting at the next URL
            catch (WebException ex)
            {
                Console.WriteLine("Web (unable to resolve host name) Exception caught: {0}", ex, ", try again.");
                getContactURLs("");
                URLIndex++;
            }
        }

        private static void getContactURLs (string html)
        {
            try
            {
                //make sure the input is not empty
                if (html != "")
                {
                    //buffer range included in limiting statement (refactorable but I'm too lazy and this should be good enough)
                    for (int i = 0; i < html.Length - 20; i++)
                    {
                        //read through all of MAIN_PAGE_SEARCH_KEYWORDS
                        for (int j = 0; j < MAIN_PAGE_SEARCH_KEYWORDS.Length; j++)
                        {
                            //if the site's HTML includes the keywords somewhere, look nearby it for the URL of the contacts page
                            if (html.Substring(i, i + MAIN_PAGE_SEARCH_KEYWORDS[j].Length) == MAIN_PAGE_SEARCH_KEYWORDS[j])
                            {
                                //debugging
                                Console.WriteLine("Found contacts phrase in HTML at character #" + i);
                                //look through nearby html for contacts page URL

                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Somehow there was no html at this URL");
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine("Looked for keyphrases outside of html for some reason.");
            }
        }

        private void buttonReadSites_Click(object sender, EventArgs e)
        {
            //read the information on the new site URL
            //basically the same as buttonLocateContacts_Click(), but it stores the contact data collected
            try
            {                                                  //refactor to new method to run multiple times
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

                            //print first 4 chars of HTML as indication of proper functioning
                            Console.WriteLine(html.Substring(0, 15));
                            Console.WriteLine(URLIndex);
                            Console.WriteLine("");
                    }
                    //increment the starting URLindex after an exception is seen so that it is incremented properly in the exception handlers
                    URLIndex++;
                }

                Console.WriteLine("Finished getting sites' contact information");
            }
            //catch the null argument exception and let user try again, starting at the next URL
            catch (ArgumentNullException ex)
            {
                Console.WriteLine("Null Argument Exception caught: {0}", ex, ", try again.");
                getContacts("");
                URLIndex++;
            }
            //catch the web exception and let user start again, starting at the next URL
            catch (WebException ex)
            {
                Console.WriteLine("Web (unable to resolve host name) Exception caught: {0}", ex, ", try again.");
                getContacts("");
                URLIndex++;
            }
        }

        private static void getContacts(string html)
        {
            //try
            //{
                //make sure the input is not empty
                if (html != "")
                {
                    //buffer range included in limiting statement (refactorable but I'm too lazy and this should be good enough)
                    //if there's an ArgumentOutOfRangeException being caused, it's probably because of this next line of code (I usually get around it by adding the buffer range mentioned above)
                    for (int i = 0; i < html.Length - 20; i++)
                    {
                        //read through all of CONTACTS_PAGE_SEARCH_KEYWORDS arrays
                        for (int j = 0; j < NUMBER_OF_CONTACT_TYPES; j++)
                        {
                            //read through the items of the arrays of the array (ex, CONTACTS_PAGE_SEARCH_KEYWORDS array 1, item 3)
                            for (int k = 0; k < NUMBER_OF_CONTACT_KEYWORDS; k++)                                         //problem here
                            {
                                //if the site's HTML includes the keywords somewhere, look nearby it for the contacts information
                                //Apparently the line below also causes ArgumentOutOfRangeExceptions, for reasons unbeknownst to me
                                if (html.Substring(i, i + CONTACTS_PAGE_SEARCH_KEYWORDS[j, k].Length) == CONTACTS_PAGE_SEARCH_KEYWORDS[j, k])
                                {

                                    //look through nearby html for contacts
                                    int contactIndex = i + CONTACTS_PAGE_SEARCH_KEYWORDS[j, k].Length + 9;

                                    //set the contact info to that found, checking to make sure that it's entering the right type of info
                                    //email
                                    if (j == 0)
                                        contactInfo[URLIndex, 0] = "Email for site #" + URLIndex + " at index " + contactIndex;
                                    //phone
                                    if (j == 1)
                                        contactInfo[URLIndex, 1] = "Phone for site #" + URLIndex + " at index " + contactIndex;
                                    //other
                                    if (j == 2)
                                        contactInfo[URLIndex, 2] = "Other for site #" + URLIndex + " at index " + contactIndex;

                                }
                            }
                        }
                    }
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
                    Console.WriteLine("Somehow there was no html at this URL");
                }
            //}
            //catch (IndexOutOfRangeException ex)
            //{
            //    //set contact info of the contact to show that it couldn't get contact info for it
            //    //email
            //    contactInfo[URLIndex, 0] = "The index was out of bounds I guess";
            //    //phone
            //    contactInfo[URLIndex, 1] = "The index was out of bounds I guess";
            //    //other
            //    contactInfo[URLIndex, 2] = "The index was out of bounds I guess";

            //    //debugging
            //    Console.WriteLine("The index was out of bounds I guess");
            //}
            //catch (ArgumentOutOfRangeException ex)
            //{
            //    //set contact info of the contact to show that it couldn't get contact info for it
            //    //email
            //    contactInfo[URLIndex, 0] = "Looked for keyphrases outside of html for some reason";
            //    //phone
            //    contactInfo[URLIndex, 1] = "Looked for keyphrases outside of html for some reason";
            //    //other
            //    contactInfo[URLIndex, 2] = "Looked for keyphrases outside of html for some reason";

            //    //debugging
            //    Console.WriteLine("Looked for keyphrases outside of html for some reason");
            //}
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
        private Button button1;
    }
}

/*
Sources:
    https://stackoverflow.com/questions/16160676/read-excel-data-line-by-line-with-c-sharp-net
    https://www.wfmj.com/story/42504801/c-read-excel-file-example
    https://ironsoftware.com/csharp/excel/tutorials/csharp-open-write-excel-file/
    http://howtouseexcel.net/how-to-extract-a-url-from-a-hyperlink-on-excel
    http://zetcode.com/csharp/readwebpage/

*/

