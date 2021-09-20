
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
            this.lblResults = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtContainer = new System.Windows.Forms.TextBox();
            this.lblContainer = new System.Windows.Forms.Label();
            this.txtManifestRoot = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(659, 78);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(112, 34);
            this.btnTest.TabIndex = 0;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Account Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Account Key";
            // 
            // txtAccountName
            // 
            this.txtAccountName.Location = new System.Drawing.Point(186, 40);
            this.txtAccountName.Name = "txtAccountName";
            this.txtAccountName.Size = new System.Drawing.Size(150, 31);
            this.txtAccountName.TabIndex = 3;
            // 
            // txtAccountKey
            // 
            this.txtAccountKey.Location = new System.Drawing.Point(186, 79);
            this.txtAccountKey.Name = "txtAccountKey";
            this.txtAccountKey.Size = new System.Drawing.Size(457, 31);
            this.txtAccountKey.TabIndex = 4;
            // 
            // btnOpenManifest
            // 
            this.btnOpenManifest.Location = new System.Drawing.Point(659, 118);
            this.btnOpenManifest.Name = "btnOpenManifest";
            this.btnOpenManifest.Size = new System.Drawing.Size(112, 34);
            this.btnOpenManifest.TabIndex = 5;
            this.btnOpenManifest.Text = "Open";
            this.btnOpenManifest.UseVisualStyleBackColor = true;
            this.btnOpenManifest.Click += new System.EventHandler(this.btnOpenManifest_Click);
            // 
            // lblManifestFile
            // 
            this.lblManifestFile.AutoSize = true;
            this.lblManifestFile.Location = new System.Drawing.Point(24, 121);
            this.lblManifestFile.Name = "lblManifestFile";
            this.lblManifestFile.Size = new System.Drawing.Size(111, 25);
            this.lblManifestFile.TabIndex = 6;
            this.lblManifestFile.Text = "Manifest File";
            // 
            // txtManifestPath
            // 
            this.txtManifestPath.Location = new System.Drawing.Point(186, 118);
            this.txtManifestPath.Name = "txtManifestPath";
            this.txtManifestPath.Size = new System.Drawing.Size(457, 31);
            this.txtManifestPath.TabIndex = 7;
            // 
            // txtManifestContent
            // 
            this.txtManifestContent.Location = new System.Drawing.Point(186, 207);
            this.txtManifestContent.Multiline = true;
            this.txtManifestContent.Name = "txtManifestContent";
            this.txtManifestContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtManifestContent.Size = new System.Drawing.Size(585, 279);
            this.txtManifestContent.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 210);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(148, 25);
            this.label3.TabIndex = 9;
            this.label3.Text = "Manifest Content";
            // 
            // btnValidate
            // 
            this.btnValidate.Location = new System.Drawing.Point(659, 492);
            this.btnValidate.Name = "btnValidate";
            this.btnValidate.Size = new System.Drawing.Size(112, 34);
            this.btnValidate.TabIndex = 10;
            this.btnValidate.Text = "Validate";
            this.btnValidate.UseVisualStyleBackColor = true;
            this.btnValidate.Click += new System.EventHandler(this.btnValidate_Click);
            // 
            // txtResults
            // 
            this.txtResults.Location = new System.Drawing.Point(186, 541);
            this.txtResults.Multiline = true;
            this.txtResults.Name = "txtResults";
            this.txtResults.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResults.Size = new System.Drawing.Size(585, 362);
            this.txtResults.TabIndex = 11;
            // 
            // lblResults
            // 
            this.lblResults.AutoSize = true;
            this.lblResults.Location = new System.Drawing.Point(24, 541);
            this.lblResults.Name = "lblResults";
            this.lblResults.Size = new System.Drawing.Size(150, 25);
            this.lblResults.TabIndex = 12;
            this.lblResults.Text = "Validation Results";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(659, 909);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(112, 34);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // txtContainer
            // 
            this.txtContainer.Location = new System.Drawing.Point(493, 40);
            this.txtContainer.Name = "txtContainer";
            this.txtContainer.Size = new System.Drawing.Size(150, 31);
            this.txtContainer.TabIndex = 15;
            // 
            // lblContainer
            // 
            this.lblContainer.AutoSize = true;
            this.lblContainer.Location = new System.Drawing.Point(399, 41);
            this.lblContainer.Name = "lblContainer";
            this.lblContainer.Size = new System.Drawing.Size(88, 25);
            this.lblContainer.TabIndex = 14;
            this.lblContainer.Text = "Container";
            // 
            // txtManifestRoot
            // 
            this.txtManifestRoot.Location = new System.Drawing.Point(225, 165);
            this.txtManifestRoot.Name = "txtManifestRoot";
            this.txtManifestRoot.Size = new System.Drawing.Size(418, 31);
            this.txtManifestRoot.TabIndex = 17;
            this.txtManifestRoot.Text = "/AccountDetails/";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 165);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(195, 25);
            this.label4.TabIndex = 16;
            this.label4.Text = "Manifest Root Location";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 953);
            this.Controls.Add(this.txtManifestRoot);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtContainer);
            this.Controls.Add(this.lblContainer);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblResults);
            this.Controls.Add(this.txtResults);
            this.Controls.Add(this.btnValidate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtManifestContent);
            this.Controls.Add(this.txtManifestPath);
            this.Controls.Add(this.lblManifestFile);
            this.Controls.Add(this.btnOpenManifest);
            this.Controls.Add(this.txtAccountKey);
            this.Controls.Add(this.txtAccountName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnTest);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BYOSA Utility";
            this.Load += new System.EventHandler(this.Main_Load);
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
        private System.Windows.Forms.Label lblResults;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtContainer;
        private System.Windows.Forms.Label lblContainer;
        private System.Windows.Forms.TextBox txtManifestRoot;
        private System.Windows.Forms.Label label4;
    }
}

