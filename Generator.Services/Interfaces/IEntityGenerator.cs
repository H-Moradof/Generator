using Generator.Entities.DatabaseEntities;
using Generator.Entities.Enums;

namespace Generator.Services.Interfaces
{
    public interface IEntityGenerator
    {
        /// <summary>
        /// ساخت محتوای کلاس ان-تی-تی
        /// </summary>
        /// <param name="targetDbName">نام دیتابیس هدف</param>
        /// <param name="targetTableName">نام جدول هدف</param>
        /// <param name="targetSchemaName">نام اسکیمای جدول هدف</param>
        /// <param name="targetTableNameInfo">اطلاعات نام جدول هدف</param>
        /// <param name="targetTableConnectionString">کانکشن استرینگ دیتابیس هدف</param>
        /// <param name="entitiesNameSpaceName">نام نیم-اسپس کلاس هدف جاری</param>
        /// <param name="TableSchemaName"></param>
        /// <param name="tableSingleName"></param>
        /// <param name="tablePluralName"></param>
        /// <param name="attributesContentType"></param>
        /// <returns></returns>
        string Create(
            string targetDbName,
            string targetTableName,
            string targetSchemaName,
            TableNameInfo targetTableNameInfo,
            string targetTableConnectionString,
            string entitiesNameSpaceName,
            string TableSchemaName,
            string tableSingleName,
            string tablePluralName,
            AttributesLanguageMode attributesContentType);
        
    }
}
