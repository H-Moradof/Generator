using Generator.Entities.DatabaseEntities;
using System.Text;

namespace Generator.Services.Bussiness.Entities.AnnotationAttributes
{
    public interface IGenerateAnnotationAttributesStrategy
    {
        void AppendAttributes(ref StringBuilder NewEntityClassContent, ref bool isPropertyNullable, string columnTranslatedName, TableFieldInfo targetTableProperty);
    }
}
