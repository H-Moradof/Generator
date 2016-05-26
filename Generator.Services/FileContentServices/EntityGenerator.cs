using Generator.Services.Interfaces;
using Generator.Entities.DatabaseEntities;
using Generator.Entities.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Generator.Services.Database;
using Generator.Settings.Entities;
using Generator.Services.Bussiness.Entities.AnnotationAttributes;
using Generator.Services.Bussiness.Entities.Properties;
using Generator.Services.Bussiness.Entities.TableAttribute;

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
        private AttributesLanguageMode AttributesLanguageMode;
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
            AttributesLanguageMode attributesLanguageMode)
        {
            this.AttributesLanguageMode = attributesLanguageMode;

            // گرفتن خصوصیات جدول جاری
            this.TargetTableProperties = TargetDatabaseDataReceiver.GetTargetTableProperties(targetDbName, targetTableName, targetTableConnectionString);

            // گرفتن ریلیشن های جدول جاری
            this.TargetTableRelations = TargetDatabaseDataReceiver.GetTargetTableRelations(targetTableName, targetTableConnectionString);
            
            // add using clause
            AppendUsingsAndClassName(entitiesNameSpaceName, TableSchemaName, tableSingleName, tablePluralName, "");

            //-----------------------------------------------------------------------------------------
            IPrimaryKeyNameStrategy pkNameStrategy = PrimaryKeyNameModeFactory.GetStrategy(EntitiesCodeGeneratorSettings.PrimaryKeyNameMode);
            string pkName = string.Empty;
            
            // تخصیص خصوصیت به جدول
            foreach (var targetTableProperty in this.TargetTableProperties)
            {
                pkName = pkNameStrategy.GetTablePrimaryKeyName(tableSingleName);
                AppendProperties(targetTableProperty, pkName);
            }

            AppendProjectNameSpacesUsings(targetSchemaName);

            if (EntitiesCodeGeneratorSettings.NavigatePropertyGenerateMode == NavigatePropertyGenerateMode.PutNavigateProperties)
                AppendNavigationProperties(targetTableName);

            this.NewEntityClassContent.Append("\n\t}\n}");

            string result = NewEntityClassContent.ToString();
            this.NewEntityClassContent = new StringBuilder(string.Empty);

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
            var columnMeaning = GeneratorDatabaseProcessor.Titles.FirstOrDefault(c => c.Single == targetTableProperty.Name || c.Plural == targetTableProperty.Name);
            if (columnMeaning != null) columnTranslatedName = columnMeaning.Meaning;

            // append properties
            bool isPropertyNullable = false;
            IGenerateAnnotationAttributesStrategy annotationAttributesStategy = GenerateAnnotationAttributesFactory.GetStrategy(EntitiesCodeGeneratorSettings.DataAnnotationAttributeGenerateMode);
            annotationAttributesStategy.AppendAttributes(ref NewEntityClassContent, ref isPropertyNullable, columnTranslatedName, targetTableProperty);

            // create property
            IGeneratePropertyStrategy generatePropertyStrategy = GeneratePropertyFactory.GetStrategy(EntitiesCodeGeneratorSettings.VirtualizePropertiesMode);
            generatePropertyStrategy.AppendProperty(ref NewEntityClassContent, TargetDatabaseDataReceiver.GetColumnType(targetTableProperty.Type), targetTableProperty.Name, isPropertyNullable);
        }

        #endregion


        #region -Append Navigation Properties

        private void AppendNavigationProperties(string targetTableName)
        {
            if(EntitiesCodeGeneratorSettings.RegionGenerateMode == RegionGenerateMode.PutRegions)
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

            if (EntitiesCodeGeneratorSettings.RegionGenerateMode == RegionGenerateMode.PutRegions)
                NewEntityClassContent.Append("\t\t#endregion");
        }

        #endregion


        #region -Append Usings And ClassName

        private void AppendUsingsAndClassName(string entitiesNameSpaceName, string TableSchemaName, string tableSingleName, string tablePluralName, string createTablesMode)
        {

            string tableNameInDatabase = tableSingleName;
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("using System;");
            builder.AppendLine("using System.Collections.Generic;");

            if (EntitiesCodeGeneratorSettings.DataAnnotationAttributeGenerateMode == DataAnnotationAttributeGenerateMode.PutDataAnnotationAttributes
                || EntitiesCodeGeneratorSettings.TableAttributeGenerateMode == TableAttributeGenerateMode.Put)
            {
                builder.AppendLine("using System.ComponentModel;");
                builder.AppendLine("using System.ComponentModel.DataAnnotations;");
                builder.AppendLine("using System.ComponentModel.DataAnnotations.Schema;");
            }

            builder.Append(string.Format("\n\nnamespace {0}.{1} \n{{\n", entitiesNameSpaceName, TableSchemaName));

            IGenerateTableAttributeStrategy strategy = GenerateTableAttributeFactory.GetStrategy(EntitiesCodeGeneratorSettings.TableAttributeGenerateMode);
            strategy.AppendTableAttribute(ref builder, tableNameInDatabase, TableSchemaName);

            if (EntitiesCodeGeneratorSettings.InheritFromBaseEntityMode == InheritFromBaseEntityMode.Inherit)
            {
                builder.AppendLine(string.Format("\tpublic class {0} : BaseEntity \n\t{{\n", tableSingleName));
            }
            else if (EntitiesCodeGeneratorSettings.InheritFromBaseEntityMode == InheritFromBaseEntityMode.Ignore)
            {
                builder.AppendLine(string.Format("\tpublic class {0} \n\t{{\n", tableSingleName));
            }

            

            NewEntityClassContent.Append(builder.ToString());
        }

        #endregion

    }

}
