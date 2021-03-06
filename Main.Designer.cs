
namespace BYOSA_Utility
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnTest = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAccountName = new System.Windows.Forms.TextBox();
            this.txtAccountKey = new System.Windows.Forms.TextBox();
            this.btnOpenManifest = new System.Windows.Forms.Button();
            this.lblManifestFile = new System.Windows.Forms.Label();
            this.txtManifestPath = new System.Windows.Forms.TextBox();
            this.txtManifestContent = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnValidate = new System.Windows.Forms.Button();
            this.txtResults = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtContainer = new System.Windows.Forms.TextBox();
            this.lblContainer = new System.Windows.Forms.Label();
            this.txtManifestRoot = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.gridResults = new System.Windows.Forms.DataGridView();
            this.Entity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Regex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblResults = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkRoot = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbQuote = new System.Windows.Forms.ComboBox();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkHasHeaders = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.gridResults)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(851, 94);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(112, 34);
            this.btnTest.TabIndex = 4;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Account Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Account Key";
            // 
            // txtAccountName
            // 
            this.txtAccountName.Location = new System.Drawing.Point(152, 52);
            this.txtAccountName.Name = "txtAccountName";
            this.txtAccountName.Size = new System.Drawing.Size(150, 31);
            this.txtAccountName.TabIndex = 0;
            // 
            // txtAccountKey
            // 
            this.txtAccountKey.Location = new System.Drawing.Point(152, 94);
            this.txtAccountKey.Name = "txtAccountKey";
            this.txtAccountKey.Size = new System.Drawing.Size(686, 31);
            this.txtAccountKey.TabIndex = 3;
            // 
            // btnOpenManifest
            // 
            this.btnOpenManifest.Location = new System.Drawing.Point(851, 77);
            this.btnOpenManifest.Name = "btnOpenManifest";
            this.btnOpenManifest.Size = new System.Drawing.Size(112, 34);
            this.btnOpenManifest.TabIndex = 6;
            this.btnOpenManifest.Text = "Browse";
            this.btnOpenManifest.UseVisualStyleBackColor = true;
            this.btnOpenManifest.Click += new System.EventHandler(this.btnOpenManifest_Click);
            // 
            // lblManifestFile
            // 
            this.lblManifestFile.AutoSize = true;
            this.lblManifestFile.Location = new System.Drawing.Point(2, 82);
            this.lblManifestFile.Name = "lblManifestFile";
            this.lblManifestFile.Size = new System.Drawing.Size(38, 25);
            this.lblManifestFile.TabIndex = 6;
            this.lblManifestFile.Text = "File";
            // 
            // txtManifestPath
            // 
            this.txtManifestPath.Location = new System.Drawing.Point(159, 80);
            this.txtManifestPath.Name = "txtManifestPath";
            this.txtManifestPath.Size = new System.Drawing.Size(679, 31);
            this.txtManifestPath.TabIndex = 7;
            this.txtManifestPath.TabStop = false;
            // 
            // txtManifestContent
            // 
            this.txtManifestContent.Location = new System.Drawing.Point(159, 131);
            this.txtManifestContent.Multiline = true;
            this.txtManifestContent.Name = "txtManifestContent";
            this.txtManifestContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtManifestContent.Size = new System.Drawing.Size(812, 130);
            this.txtManifestContent.TabIndex = 8;
            this.txtManifestContent.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(148, 25);
            this.label3.TabIndex = 9;
            this.label3.Text = "Manifest Content";
            // 
            // btnValidate
            // 
            this.btnValidate.Location = new System.Drawing.Point(851, 25);
            this.btnValidate.Name = "btnValidate";
            this.btnValidate.Size = new System.Drawing.Size(112, 34);
            this.btnValidate.TabIndex = 7;
            this.btnValidate.Text = "Validate";
            this.btnValidate.UseVisualStyleBackColor = true;
            this.btnValidate.Click += new System.EventHandler(this.btnValidate_Click);
            // 
            // txtResults
            // 
            this.txtResults.Location = new System.Drawing.Point(183, 610);
            this.txtResults.Multiline = true;
            this.txtResults.Name = "txtResults";
            this.txtResults.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResults.Size = new System.Drawing.Size(837, 170);
            this.txtResults.TabIndex = 11;
            this.txtResults.TabStop = false;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(39, 590);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(81, 25);
            this.lblStatus.TabIndex = 12;
            this.lblStatus.Text = "Progress";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(908, 1136);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(112, 34);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtContainer
            // 
            this.txtContainer.Location = new System.Drawing.Point(418, 49);
            this.txtContainer.Name = "txtContainer";
            this.txtContainer.Size = new System.Drawing.Size(150, 31);
            this.txtContainer.TabIndex = 1;
            // 
            // lblContainer
            // 
            this.lblContainer.AutoSize = true;
            this.lblContainer.Location = new System.Drawing.Point(324, 50);
            this.lblContainer.Name = "lblContainer";
            this.lblContainer.Size = new System.Drawing.Size(88, 25);
            this.lblContainer.TabIndex = 14;
            this.lblContainer.Text = "Container";
            // 
            // txtManifestRoot
            // 
            this.txtManifestRoot.Location = new System.Drawing.Point(159, 41);
            this.txtManifestRoot.Name = "txtManifestRoot";
            this.txtManifestRoot.Size = new System.Drawing.Size(327, 31);
            this.txtManifestRoot.TabIndex = 5;
            this.txtManifestRoot.Text = "/sampleFolder/sampleChildFolder/";
            this.txtManifestRoot.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtManifestRoot_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 25);
            this.label4.TabIndex = 16;
            this.label4.Text = "Root Folder";
            // 
            // gridResults
            // 
            this.gridResults.AllowUserToAddRows = false;
            this.gridResults.AllowUserToDeleteRows = false;
            this.gridResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Entity,
            this.Regex,
            this.FileName,
            this.FileCount});
            this.gridResults.Location = new System.Drawing.Point(183, 804);
            this.gridResults.Name = "gridResults";
            this.gridResults.ReadOnly = true;
            this.gridResults.RowHeadersWidth = 62;
            this.gridResults.RowTemplate.Height = 33;
            this.gridResults.Size = new System.Drawing.Size(837, 326);
            this.gridResults.TabIndex = 18;
            this.gridResults.TabStop = false;
            // 
            // Entity
            // 
            this.Entity.FillWeight = 85.22729F;
            this.Entity.HeaderText = "Entity";
            this.Entity.MinimumWidth = 8;
            this.Entity.Name = "Entity";
            this.Entity.ReadOnly = true;
            // 
            // Regex
            // 
            this.Regex.FillWeight = 48.81197F;
            this.Regex.HeaderText = "Validate Regex";
            this.Regex.MinimumWidth = 8;
            this.Regex.Name = "Regex";
            this.Regex.ReadOnly = true;
            // 
            // FileName
            // 
            this.FileName.FillWeight = 165.9608F;
            this.FileName.HeaderText = "File Path";
            this.FileName.MinimumWidth = 8;
            this.FileName.Name = "FileName";
            this.FileName.ReadOnly = true;
            // 
            // FileCount
            // 
            this.FileCount.HeaderText = "FileCount";
            this.FileCount.MinimumWidth = 8;
            this.FileCount.Name = "FileCount";
            this.FileCount.ReadOnly = true;
            // 
            // lblResults
            // 
            this.lblResults.AutoSize = true;
            this.lblResults.Location = new System.Drawing.Point(35, 804);
            this.lblResults.Name = "lblResults";
            this.lblResults.Size = new System.Drawing.Size(140, 25);
            this.lblResults.TabIndex = 19;
            this.lblResults.Text = "Manifest Results";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtContainer);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtAccountName);
            this.groupBox1.Controls.Add(this.txtAccountKey);
            this.groupBox1.Controls.Add(this.lblContainer);
            this.groupBox1.Controls.Add(this.btnTest);
            this.groupBox1.Location = new System.Drawing.Point(24, 33);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(996, 142);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data Lake Details";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkRoot);
            this.groupBox2.Controls.Add(this.btnOpenManifest);
            this.groupBox2.Controls.Add(this.lblManifestFile);
            this.groupBox2.Controls.Add(this.txtManifestPath);
            this.groupBox2.Controls.Add(this.txtManifestRoot);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtManifestContent);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(24, 191);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(996, 299);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Default Manifest ";
            // 
            // chkRoot
            // 
            this.chkRoot.AutoSize = true;
            this.chkRoot.Location = new System.Drawing.Point(510, 41);
            this.chkRoot.Name = "chkRoot";
            this.chkRoot.Size = new System.Drawing.Size(76, 29);
            this.chkRoot.TabIndex = 17;
            this.chkRoot.Text = "Root";
            this.chkRoot.UseVisualStyleBackColor = true;
            this.chkRoot.CheckedChanged += new System.EventHandler(this.chkRoot_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(154, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 25);
            this.label5.TabIndex = 18;
            this.label5.Text = "Quote Style";
            // 
            // cmbQuote
            // 
            this.cmbQuote.FormattingEnabled = true;
            this.cmbQuote.Items.AddRange(new object[] {
            "None",
            "Double Quotes"});
            this.cmbQuote.Location = new System.Drawing.Point(279, 30);
            this.cmbQuote.Name = "cmbQuote";
            this.cmbQuote.Size = new System.Drawing.Size(182, 33);
            this.cmbQuote.TabIndex = 17;
            this.cmbQuote.Text = "None";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkHasHeaders);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.cmbQuote);
            this.groupBox3.Controls.Add(this.btnValidate);
            this.groupBox3.Location = new System.Drawing.Point(26, 509);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(994, 78);
            this.groupBox3.TabIndex = 22;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Entity Options";
            // 
            // chkHasHeaders
            // 
            this.chkHasHeaders.AutoSize = true;
            this.chkHasHeaders.Checked = true;
            this.chkHasHeaders.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHasHeaders.Location = new System.Drawing.Point(496, 32);
            this.chkHasHeaders.Name = "chkHasHeaders";
            this.chkHasHeaders.Size = new System.Drawing.Size(138, 29);
            this.chkHasHeaders.TabIndex = 19;
            this.chkHasHeaders.Text = "Has Headers";
            this.chkHasHeaders.UseVisualStyleBackColor = true;
            this.chkHasHeaders.CheckedChanged += new System.EventHandler(this.chkHasHeaders_CheckedChanged);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1044, 1184);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblResults);
            this.Controls.Add(this.gridResults);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.txtResults);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CDM Manifest Utility";
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridResults)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAccountName;
        private System.Windows.Forms.TextBox txtAccountKey;
        private System.Windows.Forms.Button btnOpenManifest;
        private System.Windows.Forms.Label lblManifestFile;
        private System.Windows.Forms.TextBox txtManifestPath;
        private System.Windows.Forms.TextBox txtManifestContent;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnValidate;
        private System.Windows.Forms.TextBox txtResults;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtContainer;
        private System.Windows.Forms.Label lblContainer;
        private System.Windows.Forms.TextBox txtManifestRoot;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView gridResults;
        private System.Windows.Forms.Label lblResults;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.DataGridViewTextBoxColumn Entity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Regex;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileCount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbQuote;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkHasHeaders;
        private System.Windows.Forms.CheckBox chkRoot;
    }
}

