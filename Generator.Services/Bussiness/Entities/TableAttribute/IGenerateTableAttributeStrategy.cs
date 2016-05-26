using System.Text;

namespace Generator.Services.Bussiness.Entities.TableAttribute
{
    public interface IGenerateTableAttributeStrategy
    {
        void AppendTableAttribute(ref StringBuilder output, string tableName, string tableSchemaName);
    }
}
