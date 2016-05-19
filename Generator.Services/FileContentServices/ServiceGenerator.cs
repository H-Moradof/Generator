using Generator.Entities.DatabaseEntities;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using Generator.Services.Interfaces;
using Generator.Settings;

namespace Generator.Services.FileContentServices
{
    /// <summary>
    /// ساخت محتوای کلاس ها و اینترفیس های لایه سرویس
    /// </summary>
    public class ServiceGenerator : IServiceGenerator
    {
        #region ctor

        private IList<RelationSchemaInfo> TargetDatabaseRelations;

        public ServiceGenerator()
        {
            TargetDatabaseRelations = new List<RelationSchemaInfo>();
        }

        #endregion


        // ========================================================CLASS========================================================
        /// <summary>
        /// ساخت استرینگ محتوای فایل کلاس های لایه سرویس
        /// </summary>
        public string CreateClass(string tableName, string tableSingleName, string schemaName, string applicationRootPath)
        {
            string serviceClassTemplateFilePath = string.Format("{0}\\CodeTemplates\\{1}", applicationRootPath, GeneratorSettingsManager.SERVICE_CLASS_TEMPLATE_FILENAME);

            var reader = new StreamReader(serviceClassTemplateFilePath);
            string template = reader.ReadToEnd();
            ReplaceServiceClassTemplateTags(ref template, tableName, tableSingleName, schemaName);

            return template;
        }


        /// <summary>
        /// جایگزینی تگ های داخل تمپلیت کلاس لایه سرویس
        /// </summary>
        private void ReplaceServiceClassTemplateTags(ref string template, string tableName, string tableSingleName, string schemaName)
        {
            template = template.Replace("[DATABASECONTEXT_CONTEXT_NAMESPACE_NAME]", GeneratorSettingsManager.DATABASECONTEXT_CONTEXT_NAMESPACE_NAME);
            template = template.Replace("[ENTITIES_NAMESPACE_NAME]", GeneratorSettingsManager.ENTITIES_NAMESPACE_NAME);
            template = template.Replace("[SERVICES_CLASS_NAMESPACE_NAME]", GeneratorSettingsManager.SERVICES_CLASS_NAMESPACE_NAME);
            template = template.Replace("[SERVICES_INTERFACES_NAMESPACE_NAME]", GeneratorSettingsManager.SERVICES_INTERFACES_NAMESPACE_NAME);
            template = template.Replace("[VIEWMODEL_LAYER_NAME]", GeneratorSettingsManager.VIEWMODEL_LAYER_NAME);
            template = template.Replace("[SchemaName]", schemaName);
            template = template.Replace("[TableSingleName]", tableSingleName);
            template = template.Replace("[TableSingleNameCamelCase]", tableSingleName.ToCamelCaseFormat());

            TargetDatabaseRelations = TargetDatabaseDataReceiver.GetTargetTableRelations(tableName, GeneratorSettingsManager.TARGET_DATABASE_CONNECTION_STRING);
            ReplaceGetAllJoinClause(ref template, tableName, tableSingleName);
            ReplaceGetAllSelectClause(ref template, tableName);
        }


        /// <summary>
        /// جایگزینی کدهای جوین متد گت-آل کلاس لایه سرویس
        /// </summary>
        /// <param name="template"></param>
        /// <param name="tableName">نام جدول مورد نظر ما</param>
        /// <param name="tableSingleName">نام مفرد جدول مورد نظر ما</param>
        private void ReplaceGetAllJoinClause(ref string template, string tableName, string tableSingleName)
        {
            var joinClause = new StringBuilder();
            string newJoin = string.Empty, beforeColumnNames = tableSingleName, usingJoinEntities = string.Empty;
            List<string> beforeTables = new List<string> { tableSingleName };
            var tableRelationQuery = TargetDatabaseRelations.Where(c => c.ReferenceTableName != tableName);

            if (tableRelationQuery.Count() == 0)
            {
                joinClause.Append("pg._orderColumn = pg._orderColumn.Substring(pg._orderColumn.IndexOf('.') + 1);");
                
                joinClause.Append("\r\n\t\t\t");
                joinClause.Append("pg._filter = pg._filter.Substring(pg._filter.IndexOf('.') + 1);");
                joinClause.Append("\r\n\t\t\t");
                joinClause.Append(string.Format("var result = _uow.Set<{0}>().AsQueryable<{0}>();", tableSingleName));
                //joinClause.Append("\r\n\r\n\t\t\t");
                //joinClause.Append("if (pg._filter != string.Empty)");
                //joinClause.Append("\r\n\r\n\t\t\t");
                //joinClause.Append("result = result.Where(pg._filter, pg._values.ToArray());");
                //joinClause.Append("\r\n\t\t\t");
                //joinClause.Append("pg._rowCount = result.Count();");
                //joinClause.Append("\r\n\t\t\t");
                //joinClause.Append("result = result.OrderBy(pg._orderColumn).Skip((pg._pageNumber - 1) * pg._pageSize).Take(pg._pageSize);");
                joinClause.Append("\r\n");
            }
            else
            {
                
                joinClause.Append(string.Format("var result = _uow.Set<{0}>()", tableSingleName));

                foreach (var relationInfo in tableRelationQuery.ToList())
                {
                    beforeTables.Add(relationInfo.ReferenceTableName);

                    if (beforeColumnNames == tableSingleName) // is first join
                    {
                        newJoin = string.Format(".Join(_uow.Set<{0}>(), {3} => {1}.{2}, {0} => {0}.{2}, ({3}, {0}) => new {{ ",
                            relationInfo.ReferenceTableName,
                            tableName,
                            relationInfo.ColumnName,
                            beforeColumnNames);
                    }
                    else
                    {
                        newJoin = string.Format(".Join(_uow.Set<{0}>(), {3} => {3}.{1}.{2}, {0} => {0}.{2}, ({3}, {0}) => new {{ ",
                            relationInfo.ReferenceTableName,
                            tableName,
                            relationInfo.ColumnName,
                            beforeColumnNames);
                    }

                    foreach (var beforTable in beforeTables)
                    {
                        if (beforeColumnNames == tableSingleName) // is first join
                            newJoin += string.Format("{0}, ", beforTable);
                        else
                            if (beforTable != relationInfo.ReferenceTableName)
                                newJoin += string.Format("{0}.{1}, ", beforeColumnNames, beforTable);
                            else
                                newJoin += string.Format("{0}, ", beforTable); // is last column (it's relationInfo.ReferenceTableName)
                    }

                    newJoin = newJoin.Substring(0, newJoin.Length - 2); // remove ", "

                    joinClause.Append(string.Format("{0} }})\r\n\t\t\t\t\t", newJoin));

                    beforeColumnNames += relationInfo.ReferenceTableName;

                    usingJoinEntities += string.Format("using {0}.{1};\r\n", GeneratorSettingsManager.ENTITIES_NAMESPACE_NAME, relationInfo.ReferenceSchemaName);
                }
            }

            template = template.Replace("[GetAllJoinClause]", joinClause.ToString().TrimEnd() + ";");
            template = template.Replace("[Using_Join_Entities]", usingJoinEntities);
        }


        /// <summary>
        /// جایگزینی کدهای جوین متد سلکت-آل کلاس لایه سرویس
        /// </summary>
        private void ReplaceGetAllSelectClause(ref string template, string tableName)
        {
            string selectClause = string.Empty;
            var selectClauseQuery = TargetDatabaseRelations;

            int i = 1;

            if (selectClauseQuery.Count() != 0)
                foreach (var relationInfo in selectClauseQuery.ToList())
                {
                    AddSelectClause(ref selectClause, relationInfo.ReferenceTableName, true);

                    if (i < TargetDatabaseRelations.Count)
                        selectClause += ",\r\n\t\t\t\t";

                    i++;
                }
            else
                AddSelectClause(ref selectClause, tableName, false);

            template = template.Replace("[GetAllSelectClause]", selectClause);
            
        }

        private void AddSelectClause(ref string selectClause, string tableName, bool tableHaveRelation)
        {
            IList<TableFieldInfo> tableFields = new List<TableFieldInfo>();
            string formatPattern = tableHaveRelation ? "{0} = Result.{1}.{0}, " : "{0} = Result.{0}, ";

            selectClause += string.Format(@"{0} = new {0}() {{ ", tableName);

            tableFields = TargetDatabaseDataReceiver.GetTargetTableProperties(GeneratorSettingsManager.TARGET_DATABASE_NAME, tableName, GeneratorSettingsManager.TARGET_DATABASE_CONNECTION_STRING);

            foreach (var tableField in tableFields)
                selectClause += string.Format(formatPattern, tableField.Name, tableName);

            selectClause = selectClause.Substring(0, selectClause.Length - 2) + " }";
        }



        // ========================================================INTERFACE========================================================
        /// <summary>
        /// ساخت استرینگ محتوای فایل اینترفیس های لایه سرویس
        /// </summary>
        public string CreateInterface(string tableSingleName, string schemaName, string applicationRootPath)
        {
            string serviceInterfaceTemplateFilePath = string.Format("{0}\\CodeTemplates\\{1}", applicationRootPath, GeneratorSettingsManager.SERVICE_INTERFACE_TEMPLATE_FILENAME);

            var reader = new StreamReader(serviceInterfaceTemplateFilePath);
            string template = reader.ReadToEnd();
            ReplaceServiceInterfaceTemplateTags(ref template, tableSingleName, schemaName);

            return template;
        }


        /// <summary>
        /// جایگزینی تگ های داخل تمپلیت اینترفیس لایه سرویس
        /// </summary>
        private void ReplaceServiceInterfaceTemplateTags(ref string template, string tableSingleName, string schemaName)
        {
            template = template.Replace("[ENTITIES_NAMESPACE_NAME]", GeneratorSettingsManager.ENTITIES_NAMESPACE_NAME);
            template = template.Replace("[SERVICES_INTERFACES_NAMESPACE_NAME]", GeneratorSettingsManager.SERVICES_INTERFACES_NAMESPACE_NAME);
            template = template.Replace("[VIEWMODEL_LAYER_NAME]", GeneratorSettingsManager.VIEWMODEL_LAYER_NAME);
            template = template.Replace("[SchemaName]", schemaName);
            template = template.Replace("[TableSingleName]", tableSingleName);
            template = template.Replace("[TableSingleNameLowerCase]", tableSingleName.ToLowerInvariant());
        }


    }
}
