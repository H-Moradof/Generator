using Generator.Entities.DatabaseEntities;
using Generator.Services.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Generator.Services.FileContentServices
{
    /// <summary>
    /// دریافت اطلاعات جداول دیتابیس هدف
    /// </summary>
    public static class TargetDatabaseDataReceiver
    {
        /// <summary>
        /// گرفتن خصوصیات جدول
        /// </summary>
        public static IList<TableFieldInfo> GetTargetTableProperties(string dbName, string tableName, string connectionString)
        {
            return new DbContext(connectionString).Database.SqlQuery<TableFieldInfo>("use " + dbName + " select COLUMN_NAME AS Name, IS_NULLABLE AS Nullable , DATA_TYPE AS [Type],CHARACTER_MAXIMUM_LENGTH AS [Length] from INFORMATION_SCHEMA.COLUMNS where Table_Name = '" + tableName + "' ").ToList();
        }


        /// <summary>
        /// گرفتن ریلیشن های جدول
        /// </summary>
        public static IList<RelationSchemaInfo> GetTargetTableRelations(string tableName, string connectionString)
        {
            var tableRelations = new DbContext(connectionString).Database
                .SqlQuery<RelationSchemaInfo>(@"
SELECT * FROM
	(SELECT
		SCHEMA_NAME(f.SCHEMA_ID) SchemaName,
		OBJECT_NAME(f.parent_object_id) AS TableName,
		COL_NAME(fc.parent_object_id,fc.parent_column_id) AS ColumnName,
		SCHEMA_NAME(o.SCHEMA_ID) ReferenceSchemaName,
		OBJECT_NAME (f.referenced_object_id) AS ReferenceTableName
	FROM sys.foreign_keys AS f
		INNER JOIN sys.foreign_key_columns AS fc ON f.OBJECT_ID = fc.constraint_object_id
		INNER JOIN sys.objects AS o ON o.OBJECT_ID = fc.referenced_object_id
	) T
WHERE T.TableName = '" + tableName + "' OR T.ReferenceTableName = '" + tableName + "'").ToList();


            return tableRelations;
        }


        /// <summary>
        /// دریافت نوع پراپرتی های معادل فیلدهای جداول دیتابیس هدف
        /// </summary>
        public static string GetColumnType(string propertyName)
        {
            switch (propertyName)
            {
                case "tinyint"  : return "byte";
                case "smallint" : return "short";
                case "bigint"   : return "long";
                case "int"      : return "int";
                case "decimal"  : return "decimal";
                case "time":
                case "datetime" :
                case "date"     : return "DateTime";
                case "bit"      : return "bool";
                case "uniqueidentifier": return "Guid";
                case "varbinary": return "bytr[]";
                case "nvarchar" :
                case "varchar"  :
                case "ntext"    :
                case "nchar"    :
                case "char"     : return "string";
                case "geography": return "DbGeography";
                
                default:
                    throw new Exception("propertyName is invalid. value is " + propertyName);
            }
        }


        /// <summary>
        /// دریافت نام مفرد اسامی جداول دیتابیس هدف
        /// </summary>
        public static string GetTableSingleName(string tableName)
        {
            var tableSingleName = GeneratorDatabaseProcessor.Titles.Where(c => c.Single == tableName || c.Plural == tableName).FirstOrDefault();

            if (tableSingleName != null)
                return tableSingleName.Single;
            else
                return tableName;
        }

    }
}
