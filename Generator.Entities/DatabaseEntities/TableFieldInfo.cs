namespace Generator.Entities.DatabaseEntities
{
    /// <summary>
    /// جهت نگهداری اطلاعات فیلدهای دیتابیس هدف
    /// </summary>
    public class TableFieldInfo
    {
        public string Name { get; set; }
        public string Nullable { get; set; }
        public string Type { get; set; }
        public int? Length { get; set; }
    }
}
