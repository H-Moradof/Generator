using System;
using System.Text;
using Generator.Entities.DatabaseEntities;

namespace Generator.Services.Bussiness.Entities.AnnotationAttributes
{
    public class IgnoreAllGenerateAnnotationAttributesStrategy : IGenerateAnnotationAttributesStrategy
    {
        public void AppendAttributes(ref StringBuilder NewEntityClassContent, ref bool isPropertyNullable, string columnTranslatedName, TableFieldInfo targetTableProperty)
        {
            // do nothing
        }
    }
}
