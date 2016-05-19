using System.IO;
using System.Linq;
using System.Windows.Forms;
using Generator.Base;
using Generator.Services.Interfaces;
using Generator.Entities.Enums;
using Generator.Services.FileContentServices;
using Generator.Entities.DatabaseEntities;
using Generator.Services.Database;
using Generator.Settings;
using System.Collections.Generic;
using System;


namespace Generator.Main
{

    /// <summary>
    /// کلاس اصلی جنریتور
    /// </summary>
    public class CodeGenerator : BaseGenerator
    {

        #region ctor

        private readonly IEntityGenerator _entityGenerator;
        private readonly IServiceGenerator _serviceGenerator;
        private readonly IViewGenerator _viewGenerator;
        private readonly IControllerGenerator _controllerGenerator;

        public CodeGenerator()
            : this(new EntityGenerator(), new ServiceGenerator(), new ViewGenerator(), new ControllerGenerator())
        { }

        public CodeGenerator(
            IEntityGenerator entityGenerator,
            IServiceGenerator serviceGenerator,
            IViewGenerator viewGenerator,
            IControllerGenerator controllerGenerator)
        {
            _entityGenerator = entityGenerator;
            _serviceGenerator = serviceGenerator;
            _viewGenerator = viewGenerator;
            _controllerGenerator = controllerGenerator;
        }

        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetDatabaseName"></param>
        /// <param name="destinationPath"></param>
        /// <param name="attributesContentType"></param>
        /// <param name="generateAreaMode"></param>
        public void Run(string targetDatabaseName, string destinationPath, AttributesContentType attributesContentType, GenerateAreaMode generateAreaMode)
        {
            base.Run(targetDatabaseName, destinationPath, generateAreaMode, attributesContentType);
        }


        #region -GetSingleAndPluralTableName

        private void GetSingleAndPluralTableName(ref TableNameInfo tableName, ref string singleTableName, ref string tablePluralName, TableInfo tableInfo)
        {
            tableName = GeneratorDatabase.Titles.Where(c => c.Single == tableInfo.TableName || c.Plural == tableInfo.TableName).FirstOrDefault();
            singleTableName = ((tableName != null) ? tableName.Single : tableInfo.TableName);
            tablePluralName = ((tableName != null) ? tableName.Plural : tableInfo.TableName);
        }
       
        #endregion



        // TODO
        /// <summary>
        /// ایجاد دیتابیس جنریتور
        /// </summary>
        protected override void CreateGeneratorDatabase()
        {
            // We create db manually now
            throw new NotImplementedException();
        }

      
        /// <summary>
        /// تکمیل داده های دیتابیس جنریتور از طریق ثبت نام فیلدهای جدید
        /// </summary>
        protected override void CompleteGeneratorDatabase()
        {
            GeneratorDatabase.CompleteGeneratorDatabase();
        }


        // -----------------------------------------layers-----------------------------------------

        /// <summary>
        /// ایجاد پوشه ها
        /// </summary>
        /// <param name="folder"></param>
        protected override void CreateFolders(LayerFolder folder)
        {
            // ساخت پوشه های مدل، اینترفیس، سرویس، دیزاین، کنترلر
            var targetDatabaseSchemasInfos = GeneratorSettingsManager.TARGET_DATABASE_TABLES_INFORMATIONS.GroupBy(c => c.TableSchema).SelectMany(c => c.Take(1)).ToList();

            CreateRootFolders(folder);
            CreateSubFolders(folder, targetDatabaseSchemasInfos);
        }

        #region -create folders & AreaRegistrationRoute File

        private void CreateAreaRegistrationRouteFile(TableInfo table)
        {
            string areaRegistrationFileContent = @"using System.Web.Mvc;"
            + "namespace Web.Areas." + table.TableSchema + @"
                {
                    public class " + table.TableSchema + @"AreaRegistration : AreaRegistration 
                    {
                        public override string AreaName 
                        {
                            get 
                            {
                                return """ + table.TableSchema + @""";
                            }
                        }

                        public override void RegisterArea(AreaRegistrationContext context) 
                        {
                            context.MapRoute(
                                """ + table.TableSchema + @"_default"",
                                """ + table.TableSchema + @"/{controller}/{action}/{id}"",
                                new { action = ""Index"", id = UrlParameter.Optional }
                            );
                        }
                    }
                }";


            File.WriteAllText(string.Format("{0}\\{1}\\{2}\\AreaRegistration.cs", GeneratorSettingsManager.DESTINATION_PATH, GeneratorSettingsManager.AREAS_FOLDER_NAME, table.TableSchema, table.TableSchema), areaRegistrationFileContent);
        }


        private void CreateRootFolders(LayerFolder folder)
        {
            const string FOLDER_PATH_PATTERN = @"{0}\{1}";

            switch (folder)
            {
                case LayerFolder.Entities:
                    Directory.CreateDirectory(string.Format(FOLDER_PATH_PATTERN, GeneratorSettingsManager.DESTINATION_PATH, GeneratorSettingsManager.ENTITIES_LAYER_FOLDER_NAME));
                    break;
                case LayerFolder.Services:
                    Directory.CreateDirectory(string.Format(FOLDER_PATH_PATTERN, GeneratorSettingsManager.DESTINATION_PATH, GeneratorSettingsManager.SERVICE_LAYER_SERVICE_CLASSES_FOLDER_NAME));
                    Directory.CreateDirectory(string.Format(FOLDER_PATH_PATTERN, GeneratorSettingsManager.DESTINATION_PATH, GeneratorSettingsManager.SERVICE_LAYER_SERVICE_INTERFACES_FOLDER_NAME));
                    break;
                case LayerFolder.ViewModels:
                    Directory.CreateDirectory(string.Format(FOLDER_PATH_PATTERN, GeneratorSettingsManager.DESTINATION_PATH, GeneratorSettingsManager.VIEWMODEL_LAYER_FOLDER_NAME));
                    break;
                case LayerFolder.Areas:
                    Directory.CreateDirectory(string.Format(FOLDER_PATH_PATTERN, GeneratorSettingsManager.DESTINATION_PATH, GeneratorSettingsManager.AREAS_FOLDER_NAME));
                    break;
            }
            
        }

        private void CreateSubFolders(LayerFolder folder, IEnumerable<TableInfo> targetDatabaseSchemasInfos)
        {
            foreach (var tableInfoItem in targetDatabaseSchemasInfos)
            {
                switch (folder)
                {
                    case LayerFolder.Entities:
                        Directory.CreateDirectory(string.Format("{0}\\{1}\\{2}", GeneratorSettingsManager.DESTINATION_PATH, GeneratorSettingsManager.ENTITIES_LAYER_FOLDER_NAME, tableInfoItem.TableSchema));
                        break;
                    case LayerFolder.Services:
                        Directory.CreateDirectory(string.Format("{0}\\{1}\\{2}", GeneratorSettingsManager.DESTINATION_PATH, GeneratorSettingsManager.SERVICE_LAYER_SERVICE_CLASSES_FOLDER_NAME, tableInfoItem.TableSchema));
                        Directory.CreateDirectory(string.Format("{0}\\{1}\\{2}", GeneratorSettingsManager.DESTINATION_PATH, GeneratorSettingsManager.SERVICE_LAYER_SERVICE_INTERFACES_FOLDER_NAME, tableInfoItem.TableSchema));
                        break;
                    case LayerFolder.ViewModels:
                        Directory.CreateDirectory(string.Format("{0}\\{1}\\{2}", GeneratorSettingsManager.DESTINATION_PATH, GeneratorSettingsManager.VIEWMODEL_LAYER_FOLDER_NAME, tableInfoItem.TableSchema));
                        break;
                    case LayerFolder.Areas:
                        Directory.CreateDirectory(string.Format("{0}\\{1}\\{2}", GeneratorSettingsManager.DESTINATION_PATH, GeneratorSettingsManager.AREAS_FOLDER_NAME, tableInfoItem.TableSchema));
                        Directory.CreateDirectory(string.Format("{0}\\{1}\\{2}\\{3}",GeneratorSettingsManager.DESTINATION_PATH, GeneratorSettingsManager.AREAS_FOLDER_NAME, tableInfoItem.TableSchema, GeneratorSettingsManager.CONTROLLERS_FOLDER_NAME));
                        Directory.CreateDirectory(string.Format("{0}\\{1}\\{2}\\{3}",GeneratorSettingsManager.DESTINATION_PATH, GeneratorSettingsManager.AREAS_FOLDER_NAME, tableInfoItem.TableSchema, GeneratorSettingsManager.VIEWSS_FOLDER_NAME));
                        CreateAreaRegistrationRouteFile(tableInfoItem);
                        break;
                }
            }
        }

        #endregion


        // ------------------------------------------------ Entities Layer
        protected override void CreateEntitiesLayer()
        {
            TableNameInfo tableName = null;
            string singleTableName = string.Empty, tablePluralName = string.Empty;


            foreach (var tableInfo in GeneratorSettingsManager.TARGET_DATABASE_TABLES_INFORMATIONS)
            {
                // گرفتن اسم جمع و مفرد جدول
                GetSingleAndPluralTableName(ref tableName, ref singleTableName, ref tablePluralName, tableInfo);

                // دریافت کدهای کلاس های ان تــی تــی ها
                string entitiesLayerClassContent = _entityGenerator.Create(
                                                    GeneratorSettingsManager.TARGET_DATABASE_NAME,
                                                    tableInfo.TableName,
                                                    tableInfo.TableSchema,
                                                    tableName,
                                                    GeneratorSettingsManager.TARGET_DATABASE_CONNECTION_STRING,
                                                    GeneratorSettingsManager.ENTITIES_NAMESPACE_NAME,
                                                    tableInfo.TableSchema, singleTableName,
                                                    tablePluralName,
                                                    GeneratorSettingsManager.ATTRIBUTE_CONTENT_TYPE);


                // ساخت فایل کلاس های ان تی تی ها
                CreateEntitiesClassFile(GeneratorSettingsManager.DESTINATION_PATH, singleTableName, tableInfo, entitiesLayerClassContent);
            }
        }

        #region -Entities

        /// <summary>
        /// creating entity class file
        /// </summary>
        private void CreateEntitiesClassFile(string destinationPath, string tableSingleName, TableInfo item, string entityClassContent)
        {
            File.WriteAllText(destinationPath + "\\" + GeneratorSettingsManager.ENTITIES_LAYER_FOLDER_NAME + "\\" + item.TableSchema + "\\" + tableSingleName + ".cs", entityClassContent);
        }


        #endregion


        // ------------------------------------------------ Services Layer
        protected override void CreateServicesLayer()
        {
            TableNameInfo tableName = null;
            string singleTableName = string.Empty, tablePluralName = string.Empty;


            foreach (var tableInfo in GeneratorSettingsManager.TARGET_DATABASE_TABLES_INFORMATIONS)
            {
                // گرفتن اسم جمع و مفرد جدول
                GetSingleAndPluralTableName(ref tableName, ref singleTableName, ref tablePluralName, tableInfo);

                // ایجاد کلاس و اینترفیس های لایه ســــرویـــــس 
                CreateServiceClassAndInterfaceFile(GeneratorSettingsManager.DESTINATION_PATH, singleTableName, tableInfo);
            }
        }

        #region -service

        /// <summary>
        /// creating Service Class And Interfaces
        /// </summary>
        private void CreateServiceClassAndInterfaceFile(string destinationPath, string tableSingleName, TableInfo tableInfo)
        {
            string applicationRootPath = Application.StartupPath.Replace("\\bin\\Debug", "");

            // class
            string classFileName = string.Format("{0}{1}.cs", tableSingleName, GeneratorSettingsManager.SERVICE_LAYER_SERVICE_CLASSES_POSTFIX);
            string classFilePath = string.Format("{0}\\{1}\\{2}\\{3}", destinationPath, GeneratorSettingsManager.SERVICE_LAYER_SERVICE_CLASSES_FOLDER_NAME, tableInfo.TableSchema, classFileName);

            File.WriteAllText(classFilePath, _serviceGenerator.CreateClass(tableInfo.TableName, tableSingleName, tableInfo.TableSchema, applicationRootPath));


            // interface
            string interfaceFileName = string.Format("I{0}{1}.cs", tableSingleName, GeneratorSettingsManager.SERVICE_LAYER_SERVICE_CLASSES_POSTFIX);
            string interfaceFilePath = string.Format("{0}\\{1}\\{2}\\{3}", destinationPath, GeneratorSettingsManager.SERVICE_LAYER_SERVICE_INTERFACES_FOLDER_NAME, tableInfo.TableSchema, interfaceFileName);

            File.WriteAllText(interfaceFilePath, _serviceGenerator.CreateInterface(tableSingleName, tableInfo.TableSchema, applicationRootPath));
        }

        #endregion


        // ------------------------------------------------ ViewModels Layer
        protected override void CreateViewModelsLayer()
        {
            TableNameInfo tableName = null;
            string singleTableName = string.Empty, tablePluralName = string.Empty;


            foreach (var tableInfo in GeneratorSettingsManager.TARGET_DATABASE_TABLES_INFORMATIONS)
            {
                // گرفتن اسم جمع و مفرد جدول
                GetSingleAndPluralTableName(ref tableName, ref singleTableName, ref tablePluralName, tableInfo);

                // ساخت فایل های کلاس لایه ویـــو-مـــدل
                var vmGenerator = new ViewModelGenerator();
                string nameSpaceDesign = vmGenerator.Create(tableInfo, singleTableName, GeneratorSettingsManager.DESTINATION_PATH, GeneratorSettingsManager.TARGET_DATABASE_CONNECTION_STRING);
            }
        }


        // ------------------------------------------------ Web Layer
        protected override void CreateWebLayer()
        {
            TableNameInfo tableName = null;
            string singleTableName = string.Empty, tablePluralName = string.Empty;


            foreach (var tableInfo in GeneratorSettingsManager.TARGET_DATABASE_TABLES_INFORMATIONS)
            {
                // گرفتن اسم جمع و مفرد جدول
                GetSingleAndPluralTableName(ref tableName, ref singleTableName, ref tablePluralName, tableInfo);

                // ساخـــت فایل های ویـــو
                CreateViewFile(singleTableName, tableInfo);

                // ساخت کلاس کـنـتـرلــــــرها
                CreateControllerClassFile(GeneratorSettingsManager.DESTINATION_PATH, singleTableName, tableInfo, GeneratorSettingsManager.GENERATE_AREA_MODE);
            }
        }

        #region -web

        // TODO
        private void CreateViewFile(string singleTableName, TableInfo tableInfo)
        {
            _viewGenerator.SetTableProperties(TargetDatabaseDataReceiver.GetTargetTableProperties(GeneratorSettingsManager.TARGET_DATABASE_NAME, tableInfo.TableName, GeneratorSettingsManager.TARGET_DATABASE_CONNECTION_STRING));
            _viewGenerator.SetTableRelations(TargetDatabaseDataReceiver.GetTargetTableRelations(tableInfo.TableName, GeneratorSettingsManager.TARGET_DATABASE_CONNECTION_STRING));
            _viewGenerator.Create_IndexView(singleTableName, tableInfo, GeneratorSettingsManager.DESTINATION_PATH);
            _viewGenerator.Create_CreateView(singleTableName, tableInfo, GeneratorSettingsManager.DESTINATION_PATH);
            _viewGenerator.Create_ListView(singleTableName, tableInfo, GeneratorSettingsManager.DESTINATION_PATH);
            _viewGenerator.Create_Script(singleTableName, tableInfo, GeneratorSettingsManager.DESTINATION_PATH);
        }


        /// <summary>
        /// creating Controller Class
        /// </summary>
        private void CreateControllerClassFile(string destinationPath, string tableSingleName, TableInfo tableInfo, GenerateAreaMode generateAreaMode)
        {
            // TODO : i can use generateAreaMode

            string applicationRootPath = Application.StartupPath.Replace("\\bin\\Debug", "");

            string controllerFileName = string.Format("{0}Controller.cs", tableSingleName);
            string controllerFilePath = string.Format("{0}\\Areas\\{1}\\Controllers\\{2}", destinationPath, tableInfo.TableSchema, controllerFileName);
            File.WriteAllText(controllerFilePath, _controllerGenerator.Create(tableInfo.TableName, tableSingleName, tableInfo.TableSchema, applicationRootPath));
        }


        #endregion


        // ------------------------------------------------ UnitTests Layer
        // TODO
        protected override void CreateUnitTestsLayer()
        {
            throw new NotImplementedException();
        }

        // ------------------------------------------------ IntegrationTests Layer
        // TODO
        protected override void CreateIntegrationTestsLayer()
        {
            throw new NotImplementedException();
        }

    }
}
