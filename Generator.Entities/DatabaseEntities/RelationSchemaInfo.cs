namespace Generator.Entities.DatabaseEntities
{
    /// <summary>
    /// جهت نگهداری اطلاعات ریلیشن های جداول دیتابیس هدف
    /// </summary>
    public class RelationSchemaInfo
    {
        public string SchemaName { get; set; }
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string ReferenceSchemaName { get; set; }
        public string ReferenceTableName { get; set; }

    }
}
