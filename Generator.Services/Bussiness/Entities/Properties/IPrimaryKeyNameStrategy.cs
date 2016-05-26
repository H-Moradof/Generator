namespace Generator.Services.Bussiness.Entities.Properties
{
    public interface IPrimaryKeyNameStrategy
    {
        string GetTablePrimaryKeyName(string tableName);
    }
}
