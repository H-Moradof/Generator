public static class StringExtension
{
    /// <summary>
    /// متد الحاقی کلاس استرینگ جهت تبدیل کلمه به کمل-کیس
    /// </summary>
    public static string ToCamelCaseFormat(this string value)
    {
        string firstChar = value.Substring(0, 1);
        return string.Format("{0}{1}", firstChar.ToLowerInvariant(), value.Substring(1));
    }
}