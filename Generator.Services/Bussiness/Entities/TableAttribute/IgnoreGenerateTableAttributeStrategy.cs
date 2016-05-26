using System.Text;

namespace Generator.Services.Bussiness.Entities.TableAttribute
{
    public class IgnoreGenerateTableAttributeStrategy : IGenerateTableAttributeStrategy
    {
        public void AppendTableAttribute(ref StringBuilder output, string tableName, string tableSchemaName)
        {
            // do nothing
        }
    }
}
