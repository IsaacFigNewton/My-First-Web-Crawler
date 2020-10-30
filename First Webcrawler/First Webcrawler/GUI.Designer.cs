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

namespace First_Webcrawler
{
    public partial class GUI : Form
    {
        //class variables
        public static int NUMBER_OF_ENTRIES = 90;
        public static String [] URLs = new String [NUMBER_OF_ENTRIES];
        public static String[] contactURLs = new String[NUMBER_OF_ENTRIES];
        //2-dimensional array of contact info in String form
        //ex: int[,] array2D = new int[,] { {email1, phone1, other1}, {email2, phone2, other2}, {email3, phone3, other3}};
        public static String[,] contactInfo = new String[NUMBER_OF_ENTRIES, NUMBER_OF_ENTRIES];
        public static String PATH_OF_IO_DOC = "C:\\Users\\Owner\\Desktop\\List of Camera Clubs.xlsx";
        public static String SHEET_NAME = "Midwest (ND,SD,NE,KS,OK,TX,MN,I";


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
            for (int i = 2; i < rowCount; i++)
            {
                //skip header lines
                if (!((rowCount == 13) || (rowCount == 16) || (rowCount == 31) || (rowCount == 32) || (rowCount == 33)))
                {
                    //get value by cell address
                    //string address_val = ws["A" + rowCount].ToString();
                    //get value by row and column indexing
                    string index_val = ws.Rows[rowCount].Columns[1].ToString();

                    //read each cell's value to the array of URLs
                    URLs[rowCount] = index_val;
                    
                    //check to make sure correct values are collected
                    Console.WriteLine(", '{0}'", index_val);
                }
            }
        }

        private void buttonLocateContacts_Click(object sender, EventArgs e)
        {
            //look for contacts page
            String[] siteElementInfo;
        }

        private void buttonReadSites_Click(object sender, EventArgs e)
        {
            //read the information on the new site URL
            //basically the same as buttonLocateContacts_Click(), but it stores the contact data collected

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
    }
}

/*
Sources:
    https://stackoverflow.com/questions/16160676/read-excel-data-line-by-line-with-c-sharp-net
    https://www.wfmj.com/story/42504801/c-read-excel-file-example
    https://ironsoftware.com/csharp/excel/tutorials/csharp-open-write-excel-file/

*/

