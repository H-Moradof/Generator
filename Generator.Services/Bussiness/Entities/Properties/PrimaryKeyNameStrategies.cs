namespace Generator.Services.Bussiness.Entities.Properties
{
    public class JustCamelCaseIdPrimaryKeyNameStrategy : IPrimaryKeyNameStrategy
    {
        public string GetTablePrimaryKeyName(string tableName)
        {
            return "Id";
        }
    }

    public class JustPascalCaseIdPrimaryKeyNameStrategy : IPrimaryKeyNameStrategy
    {
        public string GetTablePrimaryKeyName(string tableName)
        {
            return "ID";
        }
    }

    public class TableNameWithCamelCaseIdPrimaryKeyNameStrategy : IPrimaryKeyNameStrategy
    {
        public string GetTablePrimaryKeyName(string tableName)
        {
            return string.Format("{0}Id", tableName);
        }
    }

    public class TableNameWithPascalCaseIdPrimaryKeyNameStrategy : IPrimaryKeyNameStrategy
    {
        public string GetTablePrimaryKeyName(string tableName)
        {
            return string.Format("{0}ID", tableName);
        }
    }
}
