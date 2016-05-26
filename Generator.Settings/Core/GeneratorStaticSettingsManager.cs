using Generator.Entities.DatabaseEntities;
using Generator.Entities.Enums;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Generator.Settings.Core
{
    public static partial class GeneratorSettingsManager
    {
        // settings
        public static GenerateAreaMode GENERATE_AREA_MODE { get; private set; }
        public static AttributesLanguageMode ATTRIBUTE_CONTENT_TYPE { get; private set; }
        public static string TARGET_DATABASE_NAME { get; private set; }
        public static string TARGET_DATABASE_CONNECTION_STRING { get; private set; }
        public static string DESTINATION_PATH { get; private set; }
        public static IEnumerable<TableInfo> TARGET_DATABASE_TABLES_INFORMATIONS { get; private set; }

        //
        public static void SetGeneratorInformations(
            string targetDatabaseName,
            string destinationPath,
            GenerateAreaMode generateAreaMode,
            AttributesLanguageMode attributesContentType)
        {
            ATTRIBUTE_CONTENT_TYPE = attributesContentType;
            GENERATE_AREA_MODE = generateAreaMode;
            DESTINATION_PATH = destinationPath;
            TARGET_DATABASE_NAME = targetDatabaseName;
            TARGET_DATABASE_CONNECTION_STRING = string.Format("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog={0};Data Source=.", targetDatabaseName);

            TARGET_DATABASE_TABLES_INFORMATIONS = GetTargetDatabaseTableInformations();
        }



        #region -GetTargetDatabaseTableInformations

        /// <summary>
        /// getting table name and schema name from target database
        /// </summary>
        private static List<TableInfo> GetTargetDatabaseTableInformations()
        {
            DbContext en = new DbContext(GeneratorSettingsManager.TARGET_DATABASE_CONNECTION_STRING);
            var _Tabels = en.Database
                .SqlQuery<TableInfo>("use " + GeneratorSettingsManager.TARGET_DATABASE_NAME +
                " SELECT TABLE_SCHEMA AS TableSchema, TABLE_NAME AS TableName" +
                " FROM INFORMATION_SCHEMA.TABLES" +
                " WHERE TABLE_NAME != 'sysdiagrams' AND TABLE_NAME != '__MigrationHistory'")
                .ToList();

            return _Tabels;
        }

        #endregion
    }
}
