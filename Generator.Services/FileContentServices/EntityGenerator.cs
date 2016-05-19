using Generator.Services.Interfaces;
using Generator.Entities.DatabaseEntities;
using Generator.Entities.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Generator.Services.Database;
using Generator.Services.Attributes;

namespace Generator.Services.FileContentServices
{
    /// <summary>
    /// ساخت محتوای کلاس های لایه ان-تی-تی
    /// </summary>
    public class EntityGenerator : IEntityGenerator
    {
        #region ctor

        private IEnumerable<RelationSchemaInfo> TargetTableRelations;
        private IEnumerable<TableFieldInfo> TargetTableProperties;
        private AttributesContentType AttributesContentType;
        private StringBuilder NewEntityClassContent;

        public EntityGenerator()
        {
            // initialize NewEntityClassContent
            NewEntityClassContent = new StringBuilder(string.Empty);
        }

        #endregion


        public string Create(
            string targetDbName, 
            string targetTableName, 
            string targetSchemaName, 
            TableNameInfo targetTableNameInfo,
            string targetTableConnectionString, 
            string entitiesNameSpaceName, 
            string TableSchemaName, 
            string tableSingleName, 
            string tablePluralName,
            AttributesContentType attributesContentType)
        {
            this.AttributesContentType = attributesContentType;

            // گرفتن خصوصیات جدول جاری
            this.TargetTableProperties = TargetDatabaseDataReceiver.GetTargetTableProperties(targetDbName, targetTableName, targetTableConnectionString);

            // گرفتن ریلیشن های جدول جاری
            this.TargetTableRelations = TargetDatabaseDataReceiver.GetTargetTableRelations(targetTableName, targetTableConnectionString);
            
            // add using clause
            AppendUsingsAndClassName(entitiesNameSpaceName, TableSchemaName, tableSingleName, tablePluralName, "");

            //-----------------------------------------------------------------------------------------
            // تخصیص خصوصیت به جدول
            foreach (var targetTableProperty in this.TargetTableProperties)
            {
                AppendProperties(targetTableProperty, tableSingleName + "ID");
            }

            // project usings
            AppendProjectNameSpacesUsings(targetSchemaName);

            // Navigation Properties
            AppendNavigationProperties(targetTableName);

            NewEntityClassContent.Append("\n\t}\n}");

            string result = NewEntityClassContent.ToString();
            NewEntityClassContent = new StringBuilder(string.Empty);

            return result;
        }


        #region -Append Project NameSpaces Usings

        private void AppendProjectNameSpacesUsings(string targetSchemaName)
        {
            StringBuilder entityClassUsings = new StringBuilder();

            var schemaGroup = this.TargetTableRelations.Select(c => new { Schema = c.SchemaName }).ToList();
            schemaGroup.AddRange(this.TargetTableRelations.Select(c => new { Schema = c.ReferenceSchemaName }).ToList());
            var targetTableSchemas = schemaGroup.GroupBy(c => c.Schema).SelectMany(c => c.Take(1));

            foreach (var item in targetTableSchemas)
            {
                if (item.Schema != targetSchemaName)
                    entityClassUsings.Append(string.Format("using DomainModels.Entities.{0};\n", item.Schema));
            }

            // add BaseEntity namespace
            entityClassUsings.Append("using DomainModels.Entities.Base;\n");

            NewEntityClassContent.Insert(0, entityClassUsings.ToString());
        }

        #endregion


        #region -Append Properties

        private void AppendProperties(TableFieldInfo targetTableProperty, string currentTablePrimaryKey)
        {
            // پراپرتی های ارتباطی رو داخل رجیون باید بیارم
            var relationProperty = this.TargetTableRelations.FirstOrDefault(c => c.ColumnName == targetTableProperty.Name && c.ColumnName != currentTablePrimaryKey);
            if (relationProperty != null)
                return;

            // گرفتن معنی پراپرتی
            string columnTranslatedName = string.Empty;
            var columnMeaning = GeneratorDatabase.Titles.FirstOrDefault(c => c.Single == targetTableProperty.Name || c.Plural == targetTableProperty.Name);
            if (columnMeaning != null) columnTranslatedName = columnMeaning.Meaning;

            string nullPropAdditionalCharacter = string.Empty;

            AttributeAppender.AppendDisplayNameAttribute(ref NewEntityClassContent, columnTranslatedName);
            AttributeAppender.AppendRequiredAttribute(ref NewEntityClassContent, ref nullPropAdditionalCharacter, targetTableProperty);
            AttributeAppender.AppendStringLengthAttribute(ref NewEntityClassContent, targetTableProperty);
            AttributeAppender.AppendScaffoldColumnAttribute(ref NewEntityClassContent, targetTableProperty);
            AttributeAppender.AppendEmailAddressAttribute(ref NewEntityClassContent, targetTableProperty);
            AttributeAppender.AppendScaffoldColumnAttribute(ref NewEntityClassContent, targetTableProperty);
            AttributeAppender.AppendDataTypeAttribute(ref NewEntityClassContent, targetTableProperty);

            NewEntityClassContent.Append("\t\tpublic virtual " + TargetDatabaseDataReceiver.GetColumnType(targetTableProperty.Type) + nullPropAdditionalCharacter + " " + targetTableProperty.Name + " { get; set; }\n\n");
        }

        #endregion


        #region -Append Navigation Properties

        private void AppendNavigationProperties(string targetTableName)
        {
            NewEntityClassContent.Append("\n\t\t#region Navigation Properties\n\n");

            foreach (var targetTableRelation in this.TargetTableRelations)
            {
                string refrencedTable = TargetDatabaseDataReceiver.GetTableSingleName(targetTableRelation.ReferenceTableName);
                string childTable = TargetDatabaseDataReceiver.GetTableSingleName(targetTableRelation.TableName);

                NewEntityClassContent.Append("\t\t// ----- " + refrencedTable + "\n");

                if (targetTableRelation.TableName == targetTableName)
                {
                    NewEntityClassContent.Append("\t\tpublic virtual long " + targetTableRelation.ColumnName + " { get; set; }\n\n");
                 
                    NewEntityClassContent.Append("\t\t[ForeignKey(\"" + targetTableRelation.ColumnName + "\")]\n");

                    if (targetTableRelation.ReferenceTableName == targetTableName)
                    {
                        NewEntityClassContent.Append("\t\tpublic virtual " + refrencedTable + " " + refrencedTable + "Parent { get; set; }\n\n");
                    }
                    else
                    {
                        NewEntityClassContent.Append("\t\tpublic virtual " + refrencedTable + " " + refrencedTable + " { get; set; }\n\n");
                    }
                }

                if (targetTableRelation.TableName != targetTableName)
                {
                    // TODO
                    // باید درست بشه تا اسم جمع جدول رو بذاره
                    NewEntityClassContent.Append("\t\tpublic virtual ICollection<" + childTable + "> " + targetTableRelation.TableName + " { get; set; }\n\n");
                }

                NewEntityClassContent.Append("\n");
            }

            NewEntityClassContent.Append("\t\t#endregion");
        }

        #endregion


        #region -Append Usings And ClassName

        private void AppendUsingsAndClassName(string entitiesNameSpaceName, string TableSchemaName, string tableSingleName, string tablePluralName, string createTablesMode)
        {
            string tableNameInDatabase = tableSingleName;
            string classEntity = "using System;\nusing System.Collections.Generic;\nusing System.ComponentModel;\nusing System.ComponentModel.DataAnnotations;\nusing System.ComponentModel.DataAnnotations.Schema;";
            classEntity += "\n\nnamespace " + entitiesNameSpaceName + "." + TableSchemaName + "\n{";
            classEntity += "\n\t[Table(\"" + tableNameInDatabase + "\", Schema = \"" + TableSchemaName + "\")]";
            classEntity += "\n\tpublic class " + tableSingleName + " : BaseEntity \n\t{\n";

            NewEntityClassContent.Append(classEntity);
        }

        #endregion

    }

}
