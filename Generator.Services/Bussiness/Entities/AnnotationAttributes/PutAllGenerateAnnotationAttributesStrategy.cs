using System.Text;
using Generator.Entities.DatabaseEntities;
using Generator.Services.Attributes;

namespace Generator.Services.Bussiness.Entities.AnnotationAttributes
{
    public class PutAllGenerateAnnotationAttributesStrategy : IGenerateAnnotationAttributesStrategy
    {
        public void AppendAttributes(ref StringBuilder NewEntityClassContent, ref bool isPropertyNullable, string columnTranslatedName, TableFieldInfo targetTableProperty)
        {
            AttributeAppender.AppendDisplayNameAttribute(ref NewEntityClassContent, columnTranslatedName);
            AttributeAppender.AppendRequiredAttribute(ref NewEntityClassContent, ref isPropertyNullable, targetTableProperty);
            AttributeAppender.AppendStringLengthAttribute(ref NewEntityClassContent, targetTableProperty);
            AttributeAppender.AppendScaffoldColumnAttribute(ref NewEntityClassContent, targetTableProperty);
            AttributeAppender.AppendEmailAddressAttribute(ref NewEntityClassContent, targetTableProperty);
            AttributeAppender.AppendScaffoldColumnAttribute(ref NewEntityClassContent, targetTableProperty);
            AttributeAppender.AppendDataTypeAttribute(ref NewEntityClassContent, targetTableProperty);
        }
    }

}
