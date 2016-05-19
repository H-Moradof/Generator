namespace Generator
{
    partial class MainForm
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
            this.btnBuild = new System.Windows.Forms.Button();
            this.btnAddress = new System.Windows.Forms.Button();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbDbName = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbGenerateAreaMode = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbAttributesContentType = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnBuild
            // 
            this.btnBuild.Location = new System.Drawing.Point(29, 211);
            this.btnBuild.Margin = new System.Windows.Forms.Padding(4);
            this.btnBuild.Name = "btnBuild";
            this.btnBuild.Size = new System.Drawing.Size(466, 50);
            this.btnBuild.TabIndex = 0;
            this.btnBuild.Text = "Generate";
            this.btnBuild.UseVisualStyleBackColor = true;
            this.btnBuild.Click += new System.EventHandler(this.btnBuild_Click);
            // 
            // btnAddress
            // 
            this.btnAddress.Location = new System.Drawing.Point(418, 98);
            this.btnAddress.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddress.Name = "btnAddress";
            this.btnAddress.Size = new System.Drawing.Size(80, 29);
            this.btnAddress.TabIndex = 4;
            this.btnAddress.Text = "Choose";
            this.btnAddress.UseVisualStyleBackColor = true;
            this.btnAddress.Click += new System.EventHandler(this.Address_Click);
            // 
            // txtAddress
            // 
            this.txtAddress.Enabled = false;
            this.txtAddress.Location = new System.Drawing.Point(29, 105);
            this.txtAddress.Margin = new System.Windows.Forms.Padding(4);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(381, 22);
            this.txtAddress.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Target Database";
            // 
            // cmbDbName
            // 
            this.cmbDbName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDbName.FormattingEnabled = true;
            this.cmbDbName.Location = new System.Drawing.Point(29, 41);
            this.cmbDbName.Margin = new System.Windows.Forms.Padding(4);
            this.cmbDbName.Name = "cmbDbName";
            this.cmbDbName.Size = new System.Drawing.Size(469, 24);
            this.cmbDbName.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "Destination Address";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label6.ForeColor = System.Drawing.Color.Gray;
            this.label6.Location = new System.Drawing.Point(26, 288);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 15);
            this.label6.TabIndex = 14;
            this.label6.Text = "Version 11.4";
            // 
            // cmbGenerateAreaMode
            // 
            this.cmbGenerateAreaMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGenerateAreaMode.FormattingEnabled = true;
            this.cmbGenerateAreaMode.Location = new System.Drawing.Point(29, 157);
            this.cmbGenerateAreaMode.Margin = new System.Windows.Forms.Padding(4);
            this.cmbGenerateAreaMode.Name = "cmbGenerateAreaMode";
            this.cmbGenerateAreaMode.Size = new System.Drawing.Size(197, 24);
            this.cmbGenerateAreaMode.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(141, 17);
            this.label4.TabIndex = 15;
            this.label4.Text = "Generate Area Mode";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(292, 136);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(157, 17);
            this.label5.TabIndex = 17;
            this.label5.Text = "Attributes Content Type";
            // 
            // cmbAttributesContentType
            // 
            this.cmbAttributesContentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAttributesContentType.FormattingEnabled = true;
            this.cmbAttributesContentType.Location = new System.Drawing.Point(295, 157);
            this.cmbAttributesContentType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbAttributesContentType.Name = "cmbAttributesContentType";
            this.cmbAttributesContentType.Size = new System.Drawing.Size(200, 24);
            this.cmbAttributesContentType.TabIndex = 18;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 322);
            this.Controls.Add(this.cmbAttributesContentType);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbGenerateAreaMode);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbDbName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.btnAddress);
            this.Controls.Add(this.btnBuild);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "Code Generator";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBuild;
        private System.Windows.Forms.Button btnAddress;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbDbName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbGenerateAreaMode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbAttributesContentType;
    }
}

