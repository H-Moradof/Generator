using Generator.Entities.DatabaseEntities;
using Generator.Services.Interfaces;
using Generator.Settings.Core;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Generator.Services.FileContentServices
{
    /// <summary>
    /// ساخت محتوای کلاس های کنترلرهای لایه وب
    /// </summary>
    public  class ControllerGenerator : IControllerGenerator
    {
        #region ctor

        private  IList<RelationSchemaInfo> TargetDatabaseRelations;

        public ControllerGenerator()
        {
            TargetDatabaseRelations = new List<RelationSchemaInfo>();
        }

        #endregion


        /// <summary>
        /// ساخت محتوای فایل کلاس های کنترلرها
        /// </summary>
        public string Create(string tableName, string tableSingleName, string schemaName, string applicationRootPath)
        {
            string serviceClassTemplateFilePath = string.Format("{0}\\CodeTemplates\\{1}", applicationRootPath, GeneratorSettingsManager.CONTROLLER_TEMPLATE_FILENAME);

            var reader = new StreamReader(serviceClassTemplateFilePath);
            string template = reader.ReadToEnd();
            ReplaceControllerTemplateTags(ref template, tableName, tableSingleName, schemaName);

            return template;
        }



        #region -Replace Controller Template Tags

        /// <summary>
        /// جایگزینی تگ های داخل تمپلیت کلاس کنترلر
        /// </summary>
        private  void ReplaceControllerTemplateTags(ref string template, string tableName, string tableSingleName, string schemaName)
        {
            template = template.Replace("[DATABASECONTEXT_CONTEXT_NAMESPACE_NAME]", GeneratorSettingsManager.DATABASECONTEXT_CONTEXT_NAMESPACE_NAME);
            template = template.Replace("[ENTITIES_NAMESPACE_NAME]", GeneratorSettingsManager.ENTITIES_NAMESPACE_NAME);
            template = template.Replace("[SERVICES_INTERFACES_NAMESPACE_NAME]", GeneratorSettingsManager.SERVICES_INTERFACES_NAMESPACE_NAME);
            template = template.Replace("[VIEWMODEL_LAYER_NAME]", GeneratorSettingsManager.VIEWMODEL_LAYER_NAME);
            template = template.Replace("[SchemaName]", schemaName);
            template = template.Replace("[TableSingleName]", tableSingleName);
            template = template.Replace("[TableSingleNameCamelCase]", tableSingleName.ToCamelCaseFormat());

            this.TargetDatabaseRelations = TargetDatabaseDataReceiver.GetTargetTableRelations(tableName, GeneratorSettingsManager.TARGET_DATABASE_CONNECTION_STRING);
            ReplaceControllerDependencyPropsAndCunstructorClause(ref template, tableSingleName);
            LoadRelationViewModelEntitiesClause(ref template, tableName, tableSingleName);
        }

        #endregion


        #region -Replace Controller DependencyProps And Cunstructor Clause

        /// <summary>
        /// جایگذاری کدهای پراپرتی های دپندنسی اینجکشن و کانستراکتور کنترلر
        /// </summary>
        private  void ReplaceControllerDependencyPropsAndCunstructorClause(ref string template, string tableSingleName)
        {
            var dependencyProps = new StringBuilder("#region ctor\r\n\r\n\t\tprivate readonly IUnitOfWork _uow;");
            var cunstructor = new StringBuilder(string.Format("public {0}Controller(", tableSingleName));
            cunstructor.Append("\r\n\t\t\tIUnitOfWork uow,");
            var cunstructorBody = new StringBuilder("\r\n\t\t\t_uow = uow;");
            int i = 1;


            // سرویس خود جدول
            dependencyProps.Append(string.Format("\r\n\t\tprivate readonly I{0}Service _{1}Service;", tableSingleName, tableSingleName.ToCamelCaseFormat()));

            cunstructor.Append(string.Format("\r\n\t\t\tI{0}Service {1}Service", tableSingleName, tableSingleName.ToCamelCaseFormat()));

            cunstructorBody.Append(string.Format("\r\n\t\t\t_{0}Service = {0}Service;", tableSingleName.ToCamelCaseFormat()));

            if (TargetDatabaseRelations.Count(c => c.ReferenceTableName != tableSingleName) > 0)
                cunstructor.Append(",");

            foreach (var tableInfo in TargetDatabaseRelations.Where(c => c.ReferenceTableName != tableSingleName).ToList())
            {
                dependencyProps.Append(string.Format("\r\n\t\tprivate readonly I{0}Service _{1}Service;", tableInfo.ReferenceTableName, tableInfo.ReferenceTableName.ToCamelCaseFormat()));

                cunstructor.Append(string.Format("\r\n\t\t\tI{0}Service {1}Service", tableInfo.ReferenceTableName, tableInfo.ReferenceTableName.ToCamelCaseFormat()));
                
                if (i < TargetDatabaseRelations.Count)
                    cunstructor.Append(",");

                cunstructorBody.Append(string.Format("\r\n\t\t\t_{0}Service = {0}Service;", tableInfo.ReferenceTableName.ToCamelCaseFormat()));
                i++;
            }

            cunstructor.Append(")\r\n\t\t{");
            cunstructorBody.Append("\r\n\t\t}\r\n\r\n\t\t#endregion");

            string propsAndCunstructorTotalContent = string.Format("{0}\r\n\r\n\t\t{1}{2}", dependencyProps.ToString(), cunstructor.ToString(), cunstructorBody.ToString());
            template = template.Replace("[ControllerDependencyPropsAndCunstructorClause]", propsAndCunstructorTotalContent);
        }

        #endregion


        #region -Load Relation ViewModel EntitiesClause

        /// <summary>
        /// جایگذاری کدهای لود ویومدل های کنترلر
        /// </summary>
        private  void LoadRelationViewModelEntitiesClause(ref string template, string tableName, string tableSingleName)
        {
            var loadRelationViewModelEntitiesClause = new StringBuilder();
            int i = 1;

            foreach (var tableInfo in this.TargetDatabaseRelations.Where(c => c.ReferenceTableName != tableName).ToList())
            {
                loadRelationViewModelEntitiesClause.Append(string.Format("[Tab]{0}.{1} = _{2}Service.Get().ToList();", tableSingleName.ToCamelCaseFormat(), tableInfo.ReferenceTableName, tableInfo.ReferenceTableName.ToCamelCaseFormat()));
                if (i == 1)
                    loadRelationViewModelEntitiesClause.Replace("[Tab]" , ""); // first row not need tab
                
                if (i < TargetDatabaseRelations.Count(c => c.ReferenceTableName != tableName))
                    loadRelationViewModelEntitiesClause.Append("\r\n");
                i++;
            }

            template = template.Replace("[LoadRelationViewModelEntitiesClause_3Tab]", loadRelationViewModelEntitiesClause.ToString().Replace("[Tab]", "\t\t\t"));
            template = template.Replace("[LoadRelationViewModelEntitiesClause_4Tab]", loadRelationViewModelEntitiesClause.ToString().Replace("[Tab]", "\t\t\t\t"));
        }

        #endregion

    }
}
