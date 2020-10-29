using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace First_Webcrawler
{
    public partial class GUI : Form
    {
        //public GUI()
        //{
        //    InitializeComponent();
        //}

        private void InitializeComponent()
        {
            this.pageControl = new System.Windows.Forms.TabControl();
            this.page1 = new System.Windows.Forms.TabPage();
            this.contactTypes = new System.Windows.Forms.CheckedListBox();
            this.buttonReadSites = new System.Windows.Forms.Button();
            this.buttonLocateContacts = new System.Windows.Forms.Button();
            this.buttonGetURLs = new System.Windows.Forms.Button();
            this.labelInfoToGather = new System.Windows.Forms.Label();
            this.title1 = new System.Windows.Forms.Label();
            this.page2 = new System.Windows.Forms.TabPage();
            this.pageControl.SuspendLayout();
            this.page1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pageControl
            // 
            this.pageControl.Controls.Add(this.page1);
            this.pageControl.Controls.Add(this.page2);
            this.pageControl.Location = new System.Drawing.Point(-16, -45);
            this.pageControl.Name = "pageControl";
            this.pageControl.SelectedIndex = 0;
            this.pageControl.Size = new System.Drawing.Size(1267, 730);
            this.pageControl.TabIndex = 0;
            // 
            // page1
            // 
            this.page1.Controls.Add(this.contactTypes);
            this.page1.Controls.Add(this.buttonReadSites);
            this.page1.Controls.Add(this.buttonLocateContacts);
            this.page1.Controls.Add(this.buttonGetURLs);
            this.page1.Controls.Add(this.labelInfoToGather);
            this.page1.Controls.Add(this.title1);
            this.page1.Location = new System.Drawing.Point(4, 22);
            this.page1.Name = "page1";
            this.page1.Padding = new System.Windows.Forms.Padding(3);
            this.page1.Size = new System.Drawing.Size(1259, 704);
            this.page1.TabIndex = 0;
            this.page1.Text = "page1";
            this.page1.UseVisualStyleBackColor = true;
            // 
            // contactTypes
            // 
            this.contactTypes.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contactTypes.FormattingEnabled = true;
            this.contactTypes.Items.AddRange(new object[] {
            "Phone",
            "Email",
            "Other"});
            this.contactTypes.Location = new System.Drawing.Point(105, 162);
            this.contactTypes.Name = "contactTypes";
            this.contactTypes.Size = new System.Drawing.Size(274, 264);
            this.contactTypes.TabIndex = 5;
            // 
            // buttonReadSites
            // 
            this.buttonReadSites.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonReadSites.Location = new System.Drawing.Point(866, 362);
            this.buttonReadSites.Name = "buttonReadSites";
            this.buttonReadSites.Size = new System.Drawing.Size(227, 73);
            this.buttonReadSites.TabIndex = 4;
            this.buttonReadSites.Text = "Read Websites";
            this.buttonReadSites.UseVisualStyleBackColor = true;
            // 
            // buttonLocateContacts
            // 
            this.buttonLocateContacts.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLocateContacts.Location = new System.Drawing.Point(866, 250);
            this.buttonLocateContacts.Name = "buttonLocateContacts";
            this.buttonLocateContacts.Size = new System.Drawing.Size(227, 73);
            this.buttonLocateContacts.TabIndex = 3;
            this.buttonLocateContacts.Text = "Locate Contact Information";
            this.buttonLocateContacts.UseVisualStyleBackColor = true;
            // 
            // buttonGetURLs
            // 
            this.buttonGetURLs.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonGetURLs.Location = new System.Drawing.Point(866, 121);
            this.buttonGetURLs.Name = "buttonGetURLs";
            this.buttonGetURLs.Size = new System.Drawing.Size(227, 73);
            this.buttonGetURLs.TabIndex = 2;
            this.buttonGetURLs.Text = "Load Source URLs";
            this.buttonGetURLs.UseVisualStyleBackColor = true;
            // 
            // labelInfoToGather
            // 
            this.labelInfoToGather.AutoSize = true;
            this.labelInfoToGather.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInfoToGather.Location = new System.Drawing.Point(100, 121);
            this.labelInfoToGather.Name = "labelInfoToGather";
            this.labelInfoToGather.Size = new System.Drawing.Size(293, 25);
            this.labelInfoToGather.TabIndex = 1;
            this.labelInfoToGather.Text = "Contact Information to Gather";
            // 
            // title1
            // 
            this.title1.AutoSize = true;
            this.title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title1.Location = new System.Drawing.Point(330, 32);
            this.title1.Name = "title1";
            this.title1.Size = new System.Drawing.Size(642, 37);
            this.title1.TabIndex = 0;
            this.title1.Text = "Contact Information Gathering Web Crawler";
            // 
            // page2
            // 
            this.page2.Location = new System.Drawing.Point(4, 22);
            this.page2.Name = "page2";
            this.page2.Padding = new System.Windows.Forms.Padding(3);
            this.page2.Size = new System.Drawing.Size(1259, 704);
            this.page2.TabIndex = 1;
            this.page2.Text = "page2";
            this.page2.UseVisualStyleBackColor = true;
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1176, 538);
            this.Controls.Add(this.pageControl);
            this.Name = "GUI";
            this.Text = "Form1";
            this.pageControl.ResumeLayout(false);
            this.page1.ResumeLayout(false);
            this.page1.PerformLayout();
            this.ResumeLayout(false);


            InitializeComponent();
        }
    }
}
