namespace NoAsthmaClientApp
{
    partial class MasterForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MasterForm));
            this.MasterForm_MainPic = new System.Windows.Forms.PictureBox();
            this.buttonGetAll = new System.Windows.Forms.Button();
            this.buttonAddNew = new System.Windows.Forms.Button();
            this.textBoxStreet = new System.Windows.Forms.TextBox();
            this.textBoxCity = new System.Windows.Forms.TextBox();
            this.textBoxState = new System.Windows.Forms.TextBox();
            this.textBoxZip = new System.Windows.Forms.TextBox();
            this.labelAddNew = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.comboBoxSearchMode = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonSearchFor = new System.Windows.Forms.Button();
            this.textBoxResultSearch = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.MasterForm_MainPic)).BeginInit();
            this.SuspendLayout();
            // 
            // MasterForm_MainPic
            // 
            this.MasterForm_MainPic.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MasterForm_MainPic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.MasterForm_MainPic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MasterForm_MainPic.Image = global::NoAsthmaClientApp.Properties.Resources.AppIcon;
            this.MasterForm_MainPic.Location = new System.Drawing.Point(600, 8);
            this.MasterForm_MainPic.Name = "MasterForm_MainPic";
            this.MasterForm_MainPic.Size = new System.Drawing.Size(204, 449);
            this.MasterForm_MainPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.MasterForm_MainPic.TabIndex = 0;
            this.MasterForm_MainPic.TabStop = false;
            // 
            // buttonGetAll
            // 
            this.buttonGetAll.Location = new System.Drawing.Point(50, 65);
            this.buttonGetAll.Margin = new System.Windows.Forms.Padding(2);
            this.buttonGetAll.Name = "buttonGetAll";
            this.buttonGetAll.Size = new System.Drawing.Size(62, 35);
            this.buttonGetAll.TabIndex = 1;
            this.buttonGetAll.Text = "Get All Address";
            this.buttonGetAll.UseVisualStyleBackColor = true;
            this.buttonGetAll.Click += new System.EventHandler(this.ButtonGetAll_Click);
            // 
            // buttonAddNew
            // 
            this.buttonAddNew.Location = new System.Drawing.Point(402, 178);
            this.buttonAddNew.Name = "buttonAddNew";
            this.buttonAddNew.Size = new System.Drawing.Size(75, 43);
            this.buttonAddNew.TabIndex = 2;
            this.buttonAddNew.Text = "Add New Address";
            this.buttonAddNew.UseVisualStyleBackColor = true;
            this.buttonAddNew.Click += new System.EventHandler(this.buttonAddNew_Click);
            // 
            // textBoxStreet
            // 
            this.textBoxStreet.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxStreet.Location = new System.Drawing.Point(373, 47);
            this.textBoxStreet.Name = "textBoxStreet";
            this.textBoxStreet.Size = new System.Drawing.Size(131, 26);
            this.textBoxStreet.TabIndex = 3;
            // 
            // textBoxCity
            // 
            this.textBoxCity.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCity.Location = new System.Drawing.Point(373, 79);
            this.textBoxCity.Name = "textBoxCity";
            this.textBoxCity.Size = new System.Drawing.Size(131, 26);
            this.textBoxCity.TabIndex = 4;
            // 
            // textBoxState
            // 
            this.textBoxState.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxState.Location = new System.Drawing.Point(373, 111);
            this.textBoxState.Name = "textBoxState";
            this.textBoxState.Size = new System.Drawing.Size(131, 26);
            this.textBoxState.TabIndex = 5;
            // 
            // textBoxZip
            // 
            this.textBoxZip.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxZip.Location = new System.Drawing.Point(373, 146);
            this.textBoxZip.Name = "textBoxZip";
            this.textBoxZip.Size = new System.Drawing.Size(131, 26);
            this.textBoxZip.TabIndex = 6;
            // 
            // labelAddNew
            // 
            this.labelAddNew.AutoSize = true;
            this.labelAddNew.Location = new System.Drawing.Point(320, 55);
            this.labelAddNew.Name = "labelAddNew";
            this.labelAddNew.Size = new System.Drawing.Size(35, 13);
            this.labelAddNew.TabIndex = 7;
            this.labelAddNew.Text = "Street";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(322, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "City";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(320, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "State";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(322, 154);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Zip";
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Location = new System.Drawing.Point(28, 234);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(239, 20);
            this.textBoxSearch.TabIndex = 11;
            // 
            // comboBoxSearchMode
            // 
            this.comboBoxSearchMode.FormattingEnabled = true;
            this.comboBoxSearchMode.Items.AddRange(new object[] {
            "ID",
            "FIRSTNAME",
            "LASTNAME",
            "EMAIL"});
            this.comboBoxSearchMode.Location = new System.Drawing.Point(159, 265);
            this.comboBoxSearchMode.Name = "comboBoxSearchMode";
            this.comboBoxSearchMode.Size = new System.Drawing.Size(105, 21);
            this.comboBoxSearchMode.TabIndex = 12;
            this.comboBoxSearchMode.SelectedIndexChanged += new System.EventHandler(this.comboBoxSearchMode_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 268);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Search For Customer By:";
            // 
            // buttonSearchFor
            // 
            this.buttonSearchFor.Location = new System.Drawing.Point(289, 265);
            this.buttonSearchFor.Name = "buttonSearchFor";
            this.buttonSearchFor.Size = new System.Drawing.Size(75, 23);
            this.buttonSearchFor.TabIndex = 14;
            this.buttonSearchFor.Text = "Search";
            this.buttonSearchFor.UseVisualStyleBackColor = true;
            this.buttonSearchFor.Click += new System.EventHandler(this.buttonSearchFor_Click);
            // 
            // textBoxResultSearch
            // 
            this.textBoxResultSearch.Enabled = false;
            this.textBoxResultSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxResultSearch.ForeColor = System.Drawing.Color.MidnightBlue;
            this.textBoxResultSearch.Location = new System.Drawing.Point(28, 316);
            this.textBoxResultSearch.Multiline = true;
            this.textBoxResultSearch.Name = "textBoxResultSearch";
            this.textBoxResultSearch.Size = new System.Drawing.Size(359, 108);
            this.textBoxResultSearch.TabIndex = 15;
            this.textBoxResultSearch.Text = "test\r\ntest2\r\n";
            this.textBoxResultSearch.WordWrap = false;
            // 
            // MasterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 464);
            this.Controls.Add(this.textBoxResultSearch);
            this.Controls.Add(this.buttonSearchFor);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBoxSearchMode);
            this.Controls.Add(this.textBoxSearch);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelAddNew);
            this.Controls.Add(this.textBoxZip);
            this.Controls.Add(this.textBoxState);
            this.Controls.Add(this.textBoxCity);
            this.Controls.Add(this.textBoxStreet);
            this.Controls.Add(this.buttonAddNew);
            this.Controls.Add(this.buttonGetAll);
            this.Controls.Add(this.MasterForm_MainPic);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MasterForm";
            this.Text = "NoAsthma Client";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MasterForm_FormClosing);
            this.Load += new System.EventHandler(this.MasterForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MasterForm_MainPic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox MasterForm_MainPic;
        private System.Windows.Forms.Button buttonGetAll;
        private System.Windows.Forms.Button buttonAddNew;
        private System.Windows.Forms.TextBox textBoxStreet;
        private System.Windows.Forms.TextBox textBoxCity;
        private System.Windows.Forms.TextBox textBoxState;
        private System.Windows.Forms.TextBox textBoxZip;
        private System.Windows.Forms.Label labelAddNew;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.ComboBox comboBoxSearchMode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonSearchFor;
        private System.Windows.Forms.TextBox textBoxResultSearch;
    }
}

