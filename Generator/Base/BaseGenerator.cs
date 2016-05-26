using Generator.Entities.Enums;
using Generator.Settings.Core;

namespace Generator.Base
{
    /// <summary>
    /// کلاس بیس کنترل کننده ترتیب و نحوه اجرای متدهای جنریتور
    /// </summary>
    public abstract class BaseGenerator
    {
        private readonly BaseGeneratorDatabaseCreator _baseGeneratorDatabaseCreator;

        public BaseGenerator(BaseGeneratorDatabaseCreator baseGeneratorDatabaseCreator)
        {
            _baseGeneratorDatabaseCreator = baseGeneratorDatabaseCreator;
        }

        /// <summary>
        /// متد اصلی اجرای جنریتور
        /// </summary>
        /// <param name="targetDatabaseName"></param>
        /// <param name="destinationPath"></param>
        /// <param name="generateAreaMode"></param>
        /// <param name="attributesContentType"></param>
        protected void Run(string targetDatabaseName, string destinationPath, GenerateAreaMode generateAreaMode, AttributesLanguageMode attributesContentType)
        {
            // تنظیم اطلاعات پایه
             GeneratorSettingsManager.SetGeneratorInformations(targetDatabaseName, destinationPath, generateAreaMode, attributesContentType);

            // ساخت دیتابیس جنریتور
            _baseGeneratorDatabaseCreator.CreateGeneratorDatabase();

            // تکمیل اسامی جداول دیتابیس جنریتور
            CompleteGeneratorDatabase();

            // ----- ساخت کدهای لایه های مختلف -----
            CreateFolders(LayerFolder.Entities);
            CreateEntitiesLayer();


            //CreateFolders(LayerFolder.Services);
            //CreateServicesLayer();
            //CreateFolders(LayerFolder.ViewModels);
            //CreateViewModelsLayer();
            //CreateFolders(LayerFolder.Web);
            //CreateWebLayer();
            //CreateFolders(LayerFolder.UnitTests);
            //CreateUnitTestsLayer();
            //CreateFolders(LayerFolder.IntegrationTests);
            //CreateIntegrationTestsLayer();
        }

        /// <summary>
        /// تکمیل دیتابیس جنریتور از روی پراپرتی های دیتابیس هدف
        /// </summary>
        protected abstract void CompleteGeneratorDatabase();

        /// <summary>
        /// ایجاد پوشه های بخش های مختلف پروژه
        /// </summary>
        /// <param name="folder">بخش مورد نظر که باید پوشه اش ساخته شود</param>
        protected abstract void CreateFolders(LayerFolder folder);

        /// <summary>
        /// ایجاد فایل کلاس های لایه ان تی تی
        /// </summary>
        protected abstract void CreateEntitiesLayer();

        /// <summary>
        /// ایجاد فایل کلاس های لایه سرویس
        /// </summary>
        protected abstract void CreateServicesLayer();

        /// <summary>
        /// ایجاد فایل کلاس های لایه ویو-مدل 
        /// </summary>
        protected abstract void CreateViewModelsLayer();

        /// <summary>
        /// ایجاد فایل کلاس های لایه وب
        /// اعم از کنترلرها و ویوها
        /// </summary>
        protected abstract void CreateWebLayer();

        /// <summary>
        /// ایجاد فایل کلاس های لایه یونیت-تست
        /// </summary>
        protected abstract void CreateUnitTestsLayer();

        /// <summary>
        /// ایجاد فایل کلاس های لایه اینتگریشن-تست
        /// </summary>
        protected abstract void CreateIntegrationTestsLayer();

    }
}
