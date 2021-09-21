
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
            this.lblResults = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.gridResults)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(859, 94);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(112, 34);
            this.btnTest.TabIndex = 4;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnConnect_Click);
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
            this.txtAccountName.TabIndex = 1;
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
            this.btnOpenManifest.Location = new System.Drawing.Point(859, 77);
            this.btnOpenManifest.Name = "btnOpenManifest";
            this.btnOpenManifest.Size = new System.Drawing.Size(112, 34);
            this.btnOpenManifest.TabIndex = 6;
            this.btnOpenManifest.Text = "Open";
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
            this.txtManifestContent.Location = new System.Drawing.Point(159, 121);
            this.txtManifestContent.Multiline = true;
            this.txtManifestContent.Name = "txtManifestContent";
            this.txtManifestContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtManifestContent.Size = new System.Drawing.Size(812, 279);
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
            this.btnValidate.Location = new System.Drawing.Point(765, 1136);
            this.btnValidate.Name = "btnValidate";
            this.btnValidate.Size = new System.Drawing.Size(112, 34);
            this.btnValidate.TabIndex = 7;
            this.btnValidate.Text = "Validate";
            this.btnValidate.UseVisualStyleBackColor = true;
            this.btnValidate.Click += new System.EventHandler(this.btnValidate_Click);
            // 
            // txtResults
            // 
            this.txtResults.Location = new System.Drawing.Point(183, 619);
            this.txtResults.Multiline = true;
            this.txtResults.Name = "txtResults";
            this.txtResults.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResults.Size = new System.Drawing.Size(812, 161);
            this.txtResults.TabIndex = 11;
            this.txtResults.TabStop = false;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(35, 622);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(60, 25);
            this.lblStatus.TabIndex = 12;
            this.lblStatus.Text = "Status";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(883, 1136);
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
            this.txtContainer.TabIndex = 2;
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
            this.txtManifestRoot.Size = new System.Drawing.Size(409, 31);
            this.txtManifestRoot.TabIndex = 5;
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
            this.FileName});
            this.gridResults.Location = new System.Drawing.Point(183, 804);
            this.gridResults.Name = "gridResults";
            this.gridResults.ReadOnly = true;
            this.gridResults.RowHeadersWidth = 62;
            this.gridResults.RowTemplate.Height = 33;
            this.gridResults.Size = new System.Drawing.Size(812, 326);
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
            // lblResults
            // 
            this.lblResults.AutoSize = true;
            this.lblResults.Location = new System.Drawing.Point(35, 804);
            this.lblResults.Name = "lblResults";
            this.lblResults.Size = new System.Drawing.Size(67, 25);
            this.lblResults.TabIndex = 19;
            this.lblResults.Text = "Results";
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
            this.groupBox2.Controls.Add(this.btnOpenManifest);
            this.groupBox2.Controls.Add(this.lblManifestFile);
            this.groupBox2.Controls.Add(this.txtManifestPath);
            this.groupBox2.Controls.Add(this.txtManifestRoot);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtManifestContent);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(24, 191);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(996, 422);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Default Manifest ";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1044, 1184);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblResults);
            this.Controls.Add(this.gridResults);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.txtResults);
            this.Controls.Add(this.btnValidate);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CDM Manifest Utility";
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridResults)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
        private System.Windows.Forms.DataGridViewTextBoxColumn Entity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Regex;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileName;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
    }
}

