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
            this.pageMain = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControlGatherMethod = new System.Windows.Forms.TabControl();
            this.tabPageContactInfo = new System.Windows.Forms.TabPage();
            this.checkBoxAddress = new System.Windows.Forms.CheckBox();
            this.checkBoxEmail = new System.Windows.Forms.CheckBox();
            this.checkBoxPhone = new System.Windows.Forms.CheckBox();
            this.checkBoxOther = new System.Windows.Forms.CheckBox();
            this.buttonLocateContacts = new System.Windows.Forms.Button();
            this.buttonReadSites = new System.Windows.Forms.Button();
            this.tabPageScraping = new System.Windows.Forms.TabPage();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.buttonScrapeURLs = new System.Windows.Forms.Button();
            this.tabPageCustom = new System.Windows.Forms.TabPage();
            this.buttonCustomBack = new System.Windows.Forms.Button();
            this.buttonCustomNext = new System.Windows.Forms.Button();
            this.tabControlCustom = new System.Windows.Forms.TabControl();
            this.tabPageKeywords = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.tabPageBasicQuestions = new System.Windows.Forms.TabPage();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.radioButtonBefore = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButtonAfter = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.tabPageCrawl = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.tabPageScrape = new System.Windows.Forms.TabPage();
            this.button3 = new System.Windows.Forms.Button();
            this.listBoxGatherMethod = new System.Windows.Forms.ListBox();
            this.buttonWriteContacts = new System.Windows.Forms.Button();
            this.buttonGetURLs = new System.Windows.Forms.Button();
            this.labelInfoToGather = new System.Windows.Forms.Label();
            this.title1 = new System.Windows.Forms.Label();
            this.pageControl.SuspendLayout();
            this.pageMain.SuspendLayout();
            this.tabControlGatherMethod.SuspendLayout();
            this.tabPageContactInfo.SuspendLayout();
            this.tabPageScraping.SuspendLayout();
            this.tabPageCustom.SuspendLayout();
            this.tabControlCustom.SuspendLayout();
            this.tabPageKeywords.SuspendLayout();
            this.tabPageBasicQuestions.SuspendLayout();
            this.tabPageCrawl.SuspendLayout();
            this.tabPageScrape.SuspendLayout();
            this.SuspendLayout();
            // 
            // pageControl
            // 
            this.pageControl.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.pageControl.Controls.Add(this.pageMain);
            this.pageControl.Location = new System.Drawing.Point(4, -3);
            this.pageControl.Name = "pageControl";
            this.pageControl.SelectedIndex = 0;
            this.pageControl.Size = new System.Drawing.Size(1523, 926);
            this.pageControl.TabIndex = 0;
            // 
            // pageMain
            // 
            this.pageMain.Controls.Add(this.label2);
            this.pageMain.Controls.Add(this.label1);
            this.pageMain.Controls.Add(this.tabControlGatherMethod);
            this.pageMain.Controls.Add(this.listBoxGatherMethod);
            this.pageMain.Controls.Add(this.buttonWriteContacts);
            this.pageMain.Controls.Add(this.buttonGetURLs);
            this.pageMain.Controls.Add(this.labelInfoToGather);
            this.pageMain.Controls.Add(this.title1);
            this.pageMain.Location = new System.Drawing.Point(4, 25);
            this.pageMain.Name = "pageMain";
            this.pageMain.Padding = new System.Windows.Forms.Padding(3);
            this.pageMain.Size = new System.Drawing.Size(1515, 897);
            this.pageMain.TabIndex = 1;
            this.pageMain.Text = "Main";
            this.pageMain.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1024, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(472, 31);
            this.label2.TabIndex = 27;
            this.label2.Text = "Step 3: Write Information to Workbook";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(361, 31);
            this.label1.TabIndex = 26;
            this.label1.Text = "Step 1: Import Starting URLs";
            // 
            // tabControlGatherMethod
            // 
            this.tabControlGatherMethod.Controls.Add(this.tabPageContactInfo);
            this.tabControlGatherMethod.Controls.Add(this.tabPageScraping);
            this.tabControlGatherMethod.Controls.Add(this.tabPageCustom);
            this.tabControlGatherMethod.Location = new System.Drawing.Point(439, 165);
            this.tabControlGatherMethod.Name = "tabControlGatherMethod";
            this.tabControlGatherMethod.SelectedIndex = 0;
            this.tabControlGatherMethod.Size = new System.Drawing.Size(559, 579);
            this.tabControlGatherMethod.TabIndex = 25;
            // 
            // tabPageContactInfo
            // 
            this.tabPageContactInfo.BackColor = System.Drawing.Color.Transparent;
            this.tabPageContactInfo.Controls.Add(this.checkBoxAddress);
            this.tabPageContactInfo.Controls.Add(this.checkBoxEmail);
            this.tabPageContactInfo.Controls.Add(this.checkBoxPhone);
            this.tabPageContactInfo.Controls.Add(this.checkBoxOther);
            this.tabPageContactInfo.Controls.Add(this.buttonLocateContacts);
            this.tabPageContactInfo.Controls.Add(this.buttonReadSites);
            this.tabPageContactInfo.Location = new System.Drawing.Point(4, 22);
            this.tabPageContactInfo.Name = "tabPageContactInfo";
            this.tabPageContactInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageContactInfo.Size = new System.Drawing.Size(551, 553);
            this.tabPageContactInfo.TabIndex = 0;
            this.tabPageContactInfo.Text = "Contact Info";
            // 
            // checkBoxAddress
            // 
            this.checkBoxAddress.AutoSize = true;
            this.checkBoxAddress.Checked = true;
            this.checkBoxAddress.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxAddress.Location = new System.Drawing.Point(16, 87);
            this.checkBoxAddress.Name = "checkBoxAddress";
            this.checkBoxAddress.Size = new System.Drawing.Size(110, 29);
            this.checkBoxAddress.TabIndex = 11;
            this.checkBoxAddress.Text = "Address";
            this.checkBoxAddress.UseVisualStyleBackColor = true;
            // 
            // checkBoxEmail
            // 
            this.checkBoxEmail.AutoSize = true;
            this.checkBoxEmail.Checked = true;
            this.checkBoxEmail.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxEmail.Location = new System.Drawing.Point(16, 17);
            this.checkBoxEmail.Name = "checkBoxEmail";
            this.checkBoxEmail.Size = new System.Drawing.Size(84, 29);
            this.checkBoxEmail.TabIndex = 5;
            this.checkBoxEmail.Text = "Email";
            this.checkBoxEmail.UseVisualStyleBackColor = true;
            // 
            // checkBoxPhone
            // 
            this.checkBoxPhone.AutoSize = true;
            this.checkBoxPhone.Checked = true;
            this.checkBoxPhone.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxPhone.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxPhone.Location = new System.Drawing.Point(16, 52);
            this.checkBoxPhone.Name = "checkBoxPhone";
            this.checkBoxPhone.Size = new System.Drawing.Size(93, 29);
            this.checkBoxPhone.TabIndex = 6;
            this.checkBoxPhone.Text = "Phone";
            this.checkBoxPhone.UseVisualStyleBackColor = true;
            // 
            // checkBoxOther
            // 
            this.checkBoxOther.AutoSize = true;
            this.checkBoxOther.Checked = true;
            this.checkBoxOther.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxOther.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxOther.Location = new System.Drawing.Point(16, 122);
            this.checkBoxOther.Name = "checkBoxOther";
            this.checkBoxOther.Size = new System.Drawing.Size(84, 29);
            this.checkBoxOther.TabIndex = 7;
            this.checkBoxOther.Text = "Other";
            this.checkBoxOther.UseVisualStyleBackColor = true;
            // 
            // buttonLocateContacts
            // 
            this.buttonLocateContacts.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLocateContacts.Location = new System.Drawing.Point(16, 178);
            this.buttonLocateContacts.Name = "buttonLocateContacts";
            this.buttonLocateContacts.Size = new System.Drawing.Size(227, 73);
            this.buttonLocateContacts.TabIndex = 3;
            this.buttonLocateContacts.Text = "Locate Contact Information";
            this.buttonLocateContacts.UseVisualStyleBackColor = true;
            // 
            // buttonReadSites
            // 
            this.buttonReadSites.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonReadSites.Location = new System.Drawing.Point(16, 252);
            this.buttonReadSites.Name = "buttonReadSites";
            this.buttonReadSites.Size = new System.Drawing.Size(227, 84);
            this.buttonReadSites.TabIndex = 4;
            this.buttonReadSites.Text = "Read Contact Information";
            this.buttonReadSites.UseVisualStyleBackColor = true;
            // 
            // tabPageScraping
            // 
            this.tabPageScraping.BackColor = System.Drawing.Color.Transparent;
            this.tabPageScraping.Controls.Add(this.checkBox1);
            this.tabPageScraping.Controls.Add(this.checkBox2);
            this.tabPageScraping.Controls.Add(this.checkBox4);
            this.tabPageScraping.Controls.Add(this.checkBox3);
            this.tabPageScraping.Controls.Add(this.buttonScrapeURLs);
            this.tabPageScraping.Location = new System.Drawing.Point(4, 22);
            this.tabPageScraping.Name = "tabPageScraping";
            this.tabPageScraping.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageScraping.Size = new System.Drawing.Size(551, 553);
            this.tabPageScraping.TabIndex = 1;
            this.tabPageScraping.Text = "Scraping";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.Location = new System.Drawing.Point(16, 88);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(103, 29);
            this.checkBox1.TabIndex = 21;
            this.checkBox1.Text = "Thing 3";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox2.Location = new System.Drawing.Point(16, 18);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(103, 29);
            this.checkBox2.TabIndex = 18;
            this.checkBox2.Text = "Thing 1";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Checked = true;
            this.checkBox4.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox4.Location = new System.Drawing.Point(16, 123);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(103, 29);
            this.checkBox4.TabIndex = 20;
            this.checkBox4.Text = "Thing 4";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Checked = true;
            this.checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox3.Location = new System.Drawing.Point(16, 53);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(103, 29);
            this.checkBox3.TabIndex = 19;
            this.checkBox3.Text = "Thing 2";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // buttonScrapeURLs
            // 
            this.buttonScrapeURLs.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonScrapeURLs.Location = new System.Drawing.Point(16, 249);
            this.buttonScrapeURLs.Name = "buttonScrapeURLs";
            this.buttonScrapeURLs.Size = new System.Drawing.Size(227, 73);
            this.buttonScrapeURLs.TabIndex = 12;
            this.buttonScrapeURLs.Text = "Scrape Pages from List of URLs";
            this.buttonScrapeURLs.UseVisualStyleBackColor = true;
            // 
            // tabPageCustom
            // 
            this.tabPageCustom.Controls.Add(this.buttonCustomBack);
            this.tabPageCustom.Controls.Add(this.buttonCustomNext);
            this.tabPageCustom.Controls.Add(this.tabControlCustom);
            this.tabPageCustom.Location = new System.Drawing.Point(4, 22);
            this.tabPageCustom.Name = "tabPageCustom";
            this.tabPageCustom.Size = new System.Drawing.Size(551, 553);
            this.tabPageCustom.TabIndex = 2;
            this.tabPageCustom.Text = "Custom";
            this.tabPageCustom.UseVisualStyleBackColor = true;
            // 
            // buttonCustomBack
            // 
            this.buttonCustomBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCustomBack.Location = new System.Drawing.Point(7, 503);
            this.buttonCustomBack.Name = "buttonCustomBack";
            this.buttonCustomBack.Size = new System.Drawing.Size(159, 41);
            this.buttonCustomBack.TabIndex = 29;
            this.buttonCustomBack.Text = "Back";
            this.buttonCustomBack.UseVisualStyleBackColor = true;
            // 
            // buttonCustomNext
            // 
            this.buttonCustomNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCustomNext.Location = new System.Drawing.Point(404, 503);
            this.buttonCustomNext.Name = "buttonCustomNext";
            this.buttonCustomNext.Size = new System.Drawing.Size(159, 41);
            this.buttonCustomNext.TabIndex = 28;
            this.buttonCustomNext.Text = "Next";
            this.buttonCustomNext.UseVisualStyleBackColor = true;
            // 
            // tabControlCustom
            // 
            this.tabControlCustom.Controls.Add(this.tabPageKeywords);
            this.tabControlCustom.Controls.Add(this.tabPageBasicQuestions);
            this.tabControlCustom.Controls.Add(this.tabPageCrawl);
            this.tabControlCustom.Controls.Add(this.tabPageScrape);
            this.tabControlCustom.Location = new System.Drawing.Point(3, 4);
            this.tabControlCustom.Name = "tabControlCustom";
            this.tabControlCustom.SelectedIndex = 0;
            this.tabControlCustom.Size = new System.Drawing.Size(573, 490);
            this.tabControlCustom.TabIndex = 22;
            // 
            // tabPageKeywords
            // 
            this.tabPageKeywords.Controls.Add(this.label3);
            this.tabPageKeywords.Controls.Add(this.label6);
            this.tabPageKeywords.Controls.Add(this.label4);
            this.tabPageKeywords.Controls.Add(this.label5);
            this.tabPageKeywords.Controls.Add(this.textBox4);
            this.tabPageKeywords.Controls.Add(this.textBox1);
            this.tabPageKeywords.Controls.Add(this.textBox2);
            this.tabPageKeywords.Controls.Add(this.textBox3);
            this.tabPageKeywords.Location = new System.Drawing.Point(4, 22);
            this.tabPageKeywords.Name = "tabPageKeywords";
            this.tabPageKeywords.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageKeywords.Size = new System.Drawing.Size(565, 464);
            this.tabPageKeywords.TabIndex = 0;
            this.tabPageKeywords.Text = "Keyword Selectors";
            this.tabPageKeywords.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(5, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(417, 20);
            this.label3.TabIndex = 15;
            this.label3.Text = "Enter keyword selectors for thing 1, separated by commas";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(5, 195);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(417, 20);
            this.label6.TabIndex = 21;
            this.label6.Text = "Enter keyword selectors for thing 4, separated by commas";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(5, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(417, 20);
            this.label4.TabIndex = 17;
            this.label4.Text = "Enter keyword selectors for thing 2, separated by commas";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(5, 131);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(417, 20);
            this.label5.TabIndex = 19;
            this.label5.Text = "Enter keyword selectors for thing 3, separated by commas";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // textBox4
            // 
            this.textBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox4.Location = new System.Drawing.Point(5, 218);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(304, 22);
            this.textBox4.TabIndex = 20;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(5, 36);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(304, 22);
            this.textBox1.TabIndex = 14;
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(5, 96);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(304, 22);
            this.textBox2.TabIndex = 16;
            // 
            // textBox3
            // 
            this.textBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox3.Location = new System.Drawing.Point(5, 154);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(304, 22);
            this.textBox3.TabIndex = 18;
            // 
            // tabPageBasicQuestions
            // 
            this.tabPageBasicQuestions.Controls.Add(this.label9);
            this.tabPageBasicQuestions.Controls.Add(this.label7);
            this.tabPageBasicQuestions.Controls.Add(this.radioButtonBefore);
            this.tabPageBasicQuestions.Controls.Add(this.radioButton1);
            this.tabPageBasicQuestions.Controls.Add(this.radioButtonAfter);
            this.tabPageBasicQuestions.Controls.Add(this.radioButton2);
            this.tabPageBasicQuestions.Location = new System.Drawing.Point(4, 22);
            this.tabPageBasicQuestions.Name = "tabPageBasicQuestions";
            this.tabPageBasicQuestions.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBasicQuestions.Size = new System.Drawing.Size(565, 464);
            this.tabPageBasicQuestions.TabIndex = 1;
            this.tabPageBasicQuestions.Text = "Basic Questions";
            this.tabPageBasicQuestions.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(8, 81);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(405, 24);
            this.label9.TabIndex = 29;
            this.label9.Text = "Do you want to scrape the URLs or crawl them?";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(8, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(516, 24);
            this.label7.TabIndex = 22;
            this.label7.Text = "Does the key information come before or after the keywords?";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // radioButtonBefore
            // 
            this.radioButtonBefore.AutoSize = true;
            this.radioButtonBefore.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonBefore.Location = new System.Drawing.Point(12, 39);
            this.radioButtonBefore.Name = "radioButtonBefore";
            this.radioButtonBefore.Size = new System.Drawing.Size(75, 24);
            this.radioButtonBefore.TabIndex = 23;
            this.radioButtonBefore.TabStop = true;
            this.radioButtonBefore.Text = "Before";
            this.radioButtonBefore.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton1.Location = new System.Drawing.Point(89, 108);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(66, 24);
            this.radioButton1.TabIndex = 28;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Crawl";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButtonAfter
            // 
            this.radioButtonAfter.AutoSize = true;
            this.radioButtonAfter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonAfter.Location = new System.Drawing.Point(93, 39);
            this.radioButtonAfter.Name = "radioButtonAfter";
            this.radioButtonAfter.Size = new System.Drawing.Size(62, 24);
            this.radioButtonAfter.TabIndex = 24;
            this.radioButtonAfter.TabStop = true;
            this.radioButtonAfter.Text = "After";
            this.radioButtonAfter.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton2.Location = new System.Drawing.Point(8, 108);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(78, 24);
            this.radioButton2.TabIndex = 27;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Scrape";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // tabPageCrawl
            // 
            this.tabPageCrawl.Controls.Add(this.button1);
            this.tabPageCrawl.Controls.Add(this.button2);
            this.tabPageCrawl.Controls.Add(this.label8);
            this.tabPageCrawl.Controls.Add(this.textBox5);
            this.tabPageCrawl.Location = new System.Drawing.Point(4, 22);
            this.tabPageCrawl.Name = "tabPageCrawl";
            this.tabPageCrawl.Size = new System.Drawing.Size(565, 464);
            this.tabPageCrawl.TabIndex = 2;
            this.tabPageCrawl.Text = "Custom Crawl";
            this.tabPageCrawl.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(35, 227);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(227, 73);
            this.button1.TabIndex = 5;
            this.button1.Text = "Locate Contact Information";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(35, 301);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(227, 84);
            this.button2.TabIndex = 6;
            this.button2.Text = "Read Contact Information";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(5, 13);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(510, 20);
            this.label8.TabIndex = 26;
            this.label8.Text = "Enter keyword selectors for the pages to scrape, separated by commas";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // textBox5
            // 
            this.textBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox5.Location = new System.Drawing.Point(5, 36);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(304, 22);
            this.textBox5.TabIndex = 25;
            // 
            // tabPageScrape
            // 
            this.tabPageScrape.BackColor = System.Drawing.Color.Transparent;
            this.tabPageScrape.Controls.Add(this.button3);
            this.tabPageScrape.Location = new System.Drawing.Point(4, 22);
            this.tabPageScrape.Name = "tabPageScrape";
            this.tabPageScrape.Size = new System.Drawing.Size(565, 464);
            this.tabPageScrape.TabIndex = 3;
            this.tabPageScrape.Text = "Custom Scrape";
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(159, 64);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(227, 73);
            this.button3.TabIndex = 13;
            this.button3.Text = "Scrape Pages from List of URLs";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // listBoxGatherMethod
            // 
            this.listBoxGatherMethod.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxGatherMethod.FormattingEnabled = true;
            this.listBoxGatherMethod.ItemHeight = 24;
            this.listBoxGatherMethod.Items.AddRange(new object[] {
            "Contact Information",
            "Workshop Information",
            "Custom"});
            this.listBoxGatherMethod.Location = new System.Drawing.Point(439, 131);
            this.listBoxGatherMethod.Name = "listBoxGatherMethod";
            this.listBoxGatherMethod.Size = new System.Drawing.Size(288, 28);
            this.listBoxGatherMethod.TabIndex = 24;
            // 
            // buttonWriteContacts
            // 
            this.buttonWriteContacts.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonWriteContacts.Location = new System.Drawing.Point(1030, 116);
            this.buttonWriteContacts.Name = "buttonWriteContacts";
            this.buttonWriteContacts.Size = new System.Drawing.Size(227, 84);
            this.buttonWriteContacts.TabIndex = 23;
            this.buttonWriteContacts.Text = "Write  Information to Excel Sheet";
            this.buttonWriteContacts.UseVisualStyleBackColor = true;
            // 
            // buttonGetURLs
            // 
            this.buttonGetURLs.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonGetURLs.Location = new System.Drawing.Point(12, 116);
            this.buttonGetURLs.Name = "buttonGetURLs";
            this.buttonGetURLs.Size = new System.Drawing.Size(227, 73);
            this.buttonGetURLs.TabIndex = 22;
            this.buttonGetURLs.Text = "Load Source URLs";
            this.buttonGetURLs.UseVisualStyleBackColor = true;
            // 
            // labelInfoToGather
            // 
            this.labelInfoToGather.AutoSize = true;
            this.labelInfoToGather.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInfoToGather.Location = new System.Drawing.Point(540, 73);
            this.labelInfoToGather.Name = "labelInfoToGather";
            this.labelInfoToGather.Size = new System.Drawing.Size(363, 31);
            this.labelInfoToGather.TabIndex = 21;
            this.labelInfoToGather.Text = "Step 2: Information to Gather";
            // 
            // title1
            // 
            this.title1.AutoSize = true;
            this.title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title1.Location = new System.Drawing.Point(452, 0);
            this.title1.Name = "title1";
            this.title1.Size = new System.Drawing.Size(522, 37);
            this.title1.TabIndex = 20;
            this.title1.Text = "Information Gathering Web Crawler";
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1531, 762);
            this.Controls.Add(this.pageControl);
            this.Name = "GUI";
            this.Text = "Form1";
            this.pageControl.ResumeLayout(false);
            this.pageMain.ResumeLayout(false);
            this.pageMain.PerformLayout();
            this.tabControlGatherMethod.ResumeLayout(false);
            this.tabPageContactInfo.ResumeLayout(false);
            this.tabPageContactInfo.PerformLayout();
            this.tabPageScraping.ResumeLayout(false);
            this.tabPageScraping.PerformLayout();
            this.tabPageCustom.ResumeLayout(false);
            this.tabControlCustom.ResumeLayout(false);
            this.tabPageKeywords.ResumeLayout(false);
            this.tabPageKeywords.PerformLayout();
            this.tabPageBasicQuestions.ResumeLayout(false);
            this.tabPageBasicQuestions.PerformLayout();
            this.tabPageCrawl.ResumeLayout(false);
            this.tabPageCrawl.PerformLayout();
            this.tabPageScrape.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void buttonWriteContacts_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void page1_Click(object sender, EventArgs e)
        {

        }

        private void labelInfoToGather_Click(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
