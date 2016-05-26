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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkIgnoreTableAttribute = new System.Windows.Forms.CheckBox();
            this.chkIgnoreVirtualizePropertiesMode = new System.Windows.Forms.CheckBox();
            this.chkIgnoreRegions = new System.Windows.Forms.CheckBox();
            this.chkIgnoreNavigareProperties = new System.Windows.Forms.CheckBox();
            this.chkIgnoreAnnotationAttributes = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbPrimaryKeyMode = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbAttributesContentType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbGenerateAreaMode = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddress = new System.Windows.Forms.Button();
            this.btnBuild = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbDbName = new System.Windows.Forms.ComboBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.chkIgnoreInheritFromBaseEntity = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.cmbPrimaryKeyMode);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.cmbAttributesContentType);
            this.splitContainer1.Panel2.Controls.Add(this.label5);
            this.splitContainer1.Panel2.Controls.Add(this.cmbGenerateAreaMode);
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.btnAddress);
            this.splitContainer1.Panel2.Controls.Add(this.btnBuild);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.cmbDbName);
            this.splitContainer1.Panel2.Controls.Add(this.txtAddress);
            this.splitContainer1.Size = new System.Drawing.Size(816, 380);
            this.splitContainer1.SplitterDistance = 257;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkIgnoreInheritFromBaseEntity);
            this.groupBox2.Controls.Add(this.chkIgnoreTableAttribute);
            this.groupBox2.Controls.Add(this.chkIgnoreVirtualizePropertiesMode);
            this.groupBox2.Controls.Add(this.chkIgnoreRegions);
            this.groupBox2.Controls.Add(this.chkIgnoreNavigareProperties);
            this.groupBox2.Controls.Add(this.chkIgnoreAnnotationAttributes);
            this.groupBox2.Location = new System.Drawing.Point(3, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(251, 209);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Entities Settings";
            // 
            // chkIgnoreTableAttribute
            // 
            this.chkIgnoreTableAttribute.AutoSize = true;
            this.chkIgnoreTableAttribute.Location = new System.Drawing.Point(12, 55);
            this.chkIgnoreTableAttribute.Name = "chkIgnoreTableAttribute";
            this.chkIgnoreTableAttribute.Size = new System.Drawing.Size(174, 21);
            this.chkIgnoreTableAttribute.TabIndex = 9;
            this.chkIgnoreTableAttribute.Text = "Ignore Table Attributes";
            this.chkIgnoreTableAttribute.UseVisualStyleBackColor = true;
            // 
            // chkIgnoreVirtualizePropertiesMode
            // 
            this.chkIgnoreVirtualizePropertiesMode.AutoSize = true;
            this.chkIgnoreVirtualizePropertiesMode.Location = new System.Drawing.Point(12, 135);
            this.chkIgnoreVirtualizePropertiesMode.Name = "chkIgnoreVirtualizePropertiesMode";
            this.chkIgnoreVirtualizePropertiesMode.Size = new System.Drawing.Size(201, 21);
            this.chkIgnoreVirtualizePropertiesMode.TabIndex = 8;
            this.chkIgnoreVirtualizePropertiesMode.Text = "Ignore Virtualize Properties";
            this.chkIgnoreVirtualizePropertiesMode.UseVisualStyleBackColor = true;
            // 
            // chkIgnoreRegions
            // 
            this.chkIgnoreRegions.AutoSize = true;
            this.chkIgnoreRegions.Location = new System.Drawing.Point(12, 108);
            this.chkIgnoreRegions.Name = "chkIgnoreRegions";
            this.chkIgnoreRegions.Size = new System.Drawing.Size(126, 21);
            this.chkIgnoreRegions.TabIndex = 7;
            this.chkIgnoreRegions.Text = "Ignore Regions";
            this.chkIgnoreRegions.UseVisualStyleBackColor = true;
            // 
            // chkIgnoreNavigareProperties
            // 
            this.chkIgnoreNavigareProperties.AutoSize = true;
            this.chkIgnoreNavigareProperties.Location = new System.Drawing.Point(12, 81);
            this.chkIgnoreNavigareProperties.Name = "chkIgnoreNavigareProperties";
            this.chkIgnoreNavigareProperties.Size = new System.Drawing.Size(200, 21);
            this.chkIgnoreNavigareProperties.TabIndex = 6;
            this.chkIgnoreNavigareProperties.Text = "Ignore Navigare Properties";
            this.chkIgnoreNavigareProperties.UseVisualStyleBackColor = true;
            // 
            // chkIgnoreAnnotationAttributes
            // 
            this.chkIgnoreAnnotationAttributes.AutoSize = true;
            this.chkIgnoreAnnotationAttributes.Location = new System.Drawing.Point(12, 28);
            this.chkIgnoreAnnotationAttributes.Name = "chkIgnoreAnnotationAttributes";
            this.chkIgnoreAnnotationAttributes.Size = new System.Drawing.Size(206, 21);
            this.chkIgnoreAnnotationAttributes.TabIndex = 5;
            this.chkIgnoreAnnotationAttributes.Text = "Ignore Annotation Attributes";
            this.chkIgnoreAnnotationAttributes.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label6.ForeColor = System.Drawing.Color.Gray;
            this.label6.Location = new System.Drawing.Point(12, 356);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 15);
            this.label6.TabIndex = 25;
            this.label6.Text = "Version 12.0";
            // 
            // cmbPrimaryKeyMode
            // 
            this.cmbPrimaryKeyMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPrimaryKeyMode.FormattingEnabled = true;
            this.cmbPrimaryKeyMode.Location = new System.Drawing.Point(44, 225);
            this.cmbPrimaryKeyMode.Margin = new System.Windows.Forms.Padding(4);
            this.cmbPrimaryKeyMode.Name = "cmbPrimaryKeyMode";
            this.cmbPrimaryKeyMode.Size = new System.Drawing.Size(197, 24);
            this.cmbPrimaryKeyMode.TabIndex = 31;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(41, 204);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 17);
            this.label3.TabIndex = 30;
            this.label3.Text = "PrimaryKey Mode";
            // 
            // cmbAttributesContentType
            // 
            this.cmbAttributesContentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAttributesContentType.FormattingEnabled = true;
            this.cmbAttributesContentType.Location = new System.Drawing.Point(310, 171);
            this.cmbAttributesContentType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbAttributesContentType.Name = "cmbAttributesContentType";
            this.cmbAttributesContentType.Size = new System.Drawing.Size(200, 24);
            this.cmbAttributesContentType.TabIndex = 29;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(307, 150);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(157, 17);
            this.label5.TabIndex = 28;
            this.label5.Text = "Attributes Content Type";
            // 
            // cmbGenerateAreaMode
            // 
            this.cmbGenerateAreaMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGenerateAreaMode.FormattingEnabled = true;
            this.cmbGenerateAreaMode.Location = new System.Drawing.Point(44, 171);
            this.cmbGenerateAreaMode.Margin = new System.Windows.Forms.Padding(4);
            this.cmbGenerateAreaMode.Name = "cmbGenerateAreaMode";
            this.cmbGenerateAreaMode.Size = new System.Drawing.Size(197, 24);
            this.cmbGenerateAreaMode.TabIndex = 27;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(41, 150);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(141, 17);
            this.label4.TabIndex = 26;
            this.label4.Text = "Generate Area Mode";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 34);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 17);
            this.label1.TabIndex = 22;
            this.label1.Text = "Target Database";
            // 
            // btnAddress
            // 
            this.btnAddress.Location = new System.Drawing.Point(433, 113);
            this.btnAddress.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddress.Name = "btnAddress";
            this.btnAddress.Size = new System.Drawing.Size(80, 28);
            this.btnAddress.TabIndex = 20;
            this.btnAddress.Text = "Choose";
            this.btnAddress.UseVisualStyleBackColor = true;
            this.btnAddress.Click += new System.EventHandler(this.Address_Click);
            // 
            // btnBuild
            // 
            this.btnBuild.Location = new System.Drawing.Point(44, 292);
            this.btnBuild.Margin = new System.Windows.Forms.Padding(4);
            this.btnBuild.Name = "btnBuild";
            this.btnBuild.Size = new System.Drawing.Size(466, 50);
            this.btnBuild.TabIndex = 19;
            this.btnBuild.Text = "Generate";
            this.btnBuild.UseVisualStyleBackColor = true;
            this.btnBuild.Click += new System.EventHandler(this.btnBuild_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 17);
            this.label2.TabIndex = 24;
            this.label2.Text = "Destination Address";
            // 
            // cmbDbName
            // 
            this.cmbDbName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDbName.FormattingEnabled = true;
            this.cmbDbName.Location = new System.Drawing.Point(44, 55);
            this.cmbDbName.Margin = new System.Windows.Forms.Padding(4);
            this.cmbDbName.Name = "cmbDbName";
            this.cmbDbName.Size = new System.Drawing.Size(469, 24);
            this.cmbDbName.TabIndex = 23;
            // 
            // txtAddress
            // 
            this.txtAddress.Enabled = false;
            this.txtAddress.Location = new System.Drawing.Point(44, 119);
            this.txtAddress.Margin = new System.Windows.Forms.Padding(4);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(381, 22);
            this.txtAddress.TabIndex = 21;
            // 
            // chkIgnoreInheritFromBaseEntity
            // 
            this.chkIgnoreInheritFromBaseEntity.AutoSize = true;
            this.chkIgnoreInheritFromBaseEntity.Location = new System.Drawing.Point(12, 165);
            this.chkIgnoreInheritFromBaseEntity.Name = "chkIgnoreInheritFromBaseEntity";
            this.chkIgnoreInheritFromBaseEntity.Size = new System.Drawing.Size(220, 21);
            this.chkIgnoreInheritFromBaseEntity.TabIndex = 10;
            this.chkIgnoreInheritFromBaseEntity.Text = "Ignore Inherit From BaseEntity";
            this.chkIgnoreInheritFromBaseEntity.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(816, 380);
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "Code Generator";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ComboBox cmbAttributesContentType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbGenerateAreaMode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddress;
        private System.Windows.Forms.Button btnBuild;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbDbName;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkIgnoreVirtualizePropertiesMode;
        private System.Windows.Forms.CheckBox chkIgnoreRegions;
        private System.Windows.Forms.CheckBox chkIgnoreNavigareProperties;
        private System.Windows.Forms.CheckBox chkIgnoreAnnotationAttributes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbPrimaryKeyMode;
        private System.Windows.Forms.CheckBox chkIgnoreTableAttribute;
        private System.Windows.Forms.CheckBox chkIgnoreInheritFromBaseEntity;
    }
}

