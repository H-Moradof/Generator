using Generator.DatabaseContext;
using Generator.Entities.DatabaseEntities;
using Generator.Services.FileContentServices;
using Generator.Settings.Core;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Generator.Services.Database
{
    /// <summary>
    /// تکمیل کننده اطلاعات اسامی فیلدهای جدید در دیتابیس جنریتور
    /// </summary>
    public static class GeneratorDatabaseProcessor
    {
        /// <summary>
        /// اطلاعات نام فیلدهای دیتابیس هدف جهت جلوگیری از اتصال مکرر به دیتابیس جنریتور
        /// </summary>
        public static List<TableNameInfo> Titles;


        /// <summary>
        /// ثبت اسامی جداولی که معنی شان در دیتابیس جنریتور نیست در داخل دیتابیس جنریتور
        /// </summary>
        public static void CompleteGeneratorDatabase()
        {
            GeneratorDatabaseProcessor.SetTableNames();

            DbContext en = new DbContext(GeneratorSettingsManager.TARGET_DATABASE_CONNECTION_STRING);
            var targetDatabaseTables = en.Database
                .SqlQuery<TableInfo>("USE " + GeneratorSettingsManager.TARGET_DATABASE_NAME + 
                " SELECT TABLE_SCHEMA AS TableSchema, TABLE_NAME AS TableName" + 
                " FROM INFORMATION_SCHEMA.TABLES" +
                " WHERE TABLE_NAME != 'sysdiagrams' AND" +
                " TABLE_NAME != '__MigrationHistory'")
                .ToList();


            using (GeneratorEntities enGenerator = new GeneratorEntities())
            {
                var dd = enGenerator.Titles.ToList();


                foreach (var item in targetDatabaseTables)
                {
                    if (!GeneratorDatabaseProcessor.Titles.Any(c => c.Plural.ToLower() == item.TableName.ToLower() || c.Single.ToLower() == item.TableName.ToLower()))
                    {
                        enGenerator.Titles.Add(new Title { Single = item.TableName, Plural = item.TableName });
                        GeneratorDatabaseProcessor.Titles.Add(new TableNameInfo { Single = item.TableName, Plural = item.TableName });
                    }

                    // گرفتن خصوصیت جدول
                    var _Property = TargetDatabaseDataReceiver.GetTargetTableProperties(GeneratorSettingsManager.TARGET_DATABASE_NAME, item.TableName, GeneratorSettingsManager.TARGET_DATABASE_CONNECTION_STRING);

                    foreach (var prop in _Property)
                    {
                        if (!GeneratorDatabaseProcessor.Titles.Any(c => c.Single.ToLower() == prop.Name.ToLower()))
                        {
                            enGenerator.Titles.Add(new Title { Single = prop.Name, Plural = prop.Name });
                            GeneratorDatabaseProcessor.Titles.Add(new TableNameInfo { Single = prop.Name, Plural = prop.Name });
                        }
                    }

                    enGenerator.SaveChanges();

                }
            }

        }


        /// <summary>
        /// دریافت اطلاعات نام فیلدهای دیتابیس هدف از دیتابیس جنریتور و ریختن آنها در پراپرتی تایتل همین کلاس
        /// </summary>
        private static void SetTableNames()
        {
            // گرفتن نام جداول جنریتور جمع و مفرد و معنی فیلد ها
            Titles = new DbContext(GeneratorSettingsManager.GENERATOR_DATABASE_CONNECTION_STRING)
                .Database
                .SqlQuery<TableNameInfo>("SELECT Single, Plural, Meaning FROM Titles")
                .ToList();
        }


    }
}
