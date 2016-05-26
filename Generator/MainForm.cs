using Generator.Entities.Enums;
using Generator.Main;
using Generator.Settings.Entities;
using System;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace Generator
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Form Cunstructor
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        const string DEFAULT_DESTINATION_FOLDER_PATH = @"D:\test generator\";

        private void MainForm_Load(object sender, EventArgs e)
        {
            txtAddress.Text = DEFAULT_DESTINATION_FOLDER_PATH;

            string[] invalidDbNames = new string[] { "master", "model", "msdb", "tempdb", "Generator" };

            // fill cmbDbName
            string connectionDb = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=master;Data Source=.";
            DbContext enDb = new DbContext(connectionDb);
            cmbDbName.DataSource = enDb.Database.SqlQuery<string>("Select name From sys.databases").Where(c => !invalidDbNames.Contains(c)).ToList();
            cmbDbName.DisplayMember = "name";

            // fill cmbAttributesContentType
            cmbAttributesContentType.Items.Add(AttributesLanguageMode.MultiLanguage.ToString());
            cmbAttributesContentType.Items.Add(AttributesLanguageMode.Persian.ToString());
            cmbAttributesContentType.SelectedIndex = 0;
            cmbAttributesContentType.Enabled = false;

            // fill cmbGenerateAreaMode
            cmbGenerateAreaMode.Items.Add(GenerateAreaMode.OneAreas.ToString());
            cmbGenerateAreaMode.Items.Add(GenerateAreaMode.SeperatedAreas.ToString());
            cmbGenerateAreaMode.SelectedIndex = 0;
            cmbGenerateAreaMode.Enabled = false;

            // fill cmbPrimaryKeyMode
            cmbPrimaryKeyMode.Items.Add(PrimaryKeyNameMode.Id.ToString());
            cmbPrimaryKeyMode.Items.Add(PrimaryKeyNameMode.ID.ToString());
            cmbPrimaryKeyMode.Items.Add(PrimaryKeyNameMode.TableNameWithId.ToString());
            cmbPrimaryKeyMode.Items.Add(PrimaryKeyNameMode.TableNameWithID.ToString());
            cmbPrimaryKeyMode.SelectedIndex = 0;

        }


        /// <summary>
        /// رویداد تولید کدها
        /// </summary>
        private void btnBuild_Click(object sender, EventArgs e)
        {
            btnBuild.Enabled = false;
            if (txtAddress.Text.Trim() == "")
            {
                MessageBox.Show("لطفا آدرس مقصد را انتخاب نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (cmbDbName.Items.Count == 0)
            {
                MessageBox.Show("دیتابیسِ انتخاب نشده است", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            // generate codes
            var generator = new EntitiesCodeGenerator();
            var attributesContentType = (AttributesLanguageMode)Enum.Parse(typeof(AttributesLanguageMode), cmbAttributesContentType.SelectedItem.ToString(), true);
            var generateAreaMode = (GenerateAreaMode)Enum.Parse(typeof(GenerateAreaMode), cmbGenerateAreaMode.SelectedItem.ToString(), true);

            // set setttings
            SetEntitiesCodeGenerateSettings(
                chkIgnoreAnnotationAttributes.Checked, 
                chkIgnoreNavigareProperties.Checked,
                chkIgnoreRegions.Checked,
                chkIgnoreVirtualizePropertiesMode.Checked,
                chkIgnoreTableAttribute.Checked,
                chkIgnoreInheritFromBaseEntity.Checked);

            generator.Run(cmbDbName.SelectedItem.ToString(), txtAddress.Text.Trim(), attributesContentType, generateAreaMode);


            MessageBox.Show("کد ها با موفقیت تولید گردید. با تشکر", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnBuild.Enabled = true;
        }

        private void SetEntitiesCodeGenerateSettings(
            bool ignoreAnnotationAttributes,
            bool ignoreNavigareProperties,
            bool ignoreRegions,
            bool ignoreVirtualizePropertiesMode,
            bool ignoreTableAttributeGenerateMode,
            bool ignoreInheritFromBaseEntity)
        {
            EntitiesCodeGeneratorSettings.DataAnnotationAttributeGenerateMode =
                ignoreAnnotationAttributes ?
                DataAnnotationAttributeGenerateMode.IgnoreDataAnnotationAttributes :
                DataAnnotationAttributeGenerateMode.PutDataAnnotationAttributes;

            EntitiesCodeGeneratorSettings.NavigatePropertyGenerateMode =
                ignoreNavigareProperties ?
                NavigatePropertyGenerateMode.IgnoreNavigateProperties :
                NavigatePropertyGenerateMode.PutNavigateProperties;

            EntitiesCodeGeneratorSettings.RegionGenerateMode =
                ignoreRegions?
                RegionGenerateMode.IgnoreRegions :
                RegionGenerateMode.PutRegions;

            EntitiesCodeGeneratorSettings.VirtualizePropertiesMode =
                ignoreVirtualizePropertiesMode ?
                VirtualizePropertiesMode.IgnoreForAll :
                VirtualizePropertiesMode.PutForAll;

            EntitiesCodeGeneratorSettings.TableAttributeGenerateMode =
                ignoreTableAttributeGenerateMode ?
                TableAttributeGenerateMode.Ignore :
                TableAttributeGenerateMode.Put;

            EntitiesCodeGeneratorSettings.InheritFromBaseEntityMode =
                ignoreInheritFromBaseEntity ?
                InheritFromBaseEntityMode.Ignore :
                InheritFromBaseEntityMode.Inherit;

            EntitiesCodeGeneratorSettings.PrimaryKeyNameMode = (PrimaryKeyNameMode)Enum.Parse(typeof(PrimaryKeyNameMode), cmbPrimaryKeyMode.SelectedItem.ToString(), true);
        }


        /// <summary>
        /// رویداد دریافت مسیر پوشه مقصد فایل های جنریت شده
        /// </summary>
        private void Address_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            txtAddress.Text = folderBrowserDialog1.SelectedPath;
        }


    }
}
