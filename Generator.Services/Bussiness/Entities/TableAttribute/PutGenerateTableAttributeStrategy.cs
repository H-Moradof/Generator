using System.Text;

namespace Generator.Services.Bussiness.Entities.TableAttribute
{
    public class PutGenerateTableAttributeStrategy : IGenerateTableAttributeStrategy
    {
        public void AppendTableAttribute(ref StringBuilder output, string tableName, string tableSchemaName)
        {
            output.AppendLine(string.Format("\t[Table(\"{0}\", Schema = \"{1}\")]", tableName, tableSchemaName));
        }
    }
}
