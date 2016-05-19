using Generator.Entities.DatabaseEntities;
using Generator.Entities.Enums;
using Generator.Services.FileContentServices;
using Generator.Settings;
using System.Text;

namespace Generator.Services.Attributes
{
    /// <summary>
    /// ایجاد اتریبیوت های پراپرتی ها
    /// </summary>
    public static class AttributeAppender
    {
        /// <summary>
        /// Append [Required] attribute
        /// </summary>
        /// <param name="newEntityClassContent"></param>
        /// <param name="nullPropAdditionalCharacter"></param>
        /// <param name="targetTableProperty"></param>
        public static void AppendRequiredAttribute(ref StringBuilder newEntityClassContent, ref string nullPropAdditionalCharacter, TableFieldInfo targetTableProperty)
        {
            if (targetTableProperty.Nullable == "NO")
            {
                newEntityClassContent.Append(string.Format("\t\t[Required(ErrorMessage= \"{0}\")]\n", GeneratorSettingsManager.REQUIRED_ATTRIBUTE_PERSIAN_MESSAGE));
            }
            else if (TargetDatabaseDataReceiver.GetColumnType(targetTableProperty.Type) != "string")
            {
                nullPropAdditionalCharacter = "?";
            }
        }

        /// <summary>
        /// Append [StringLength] attribute
        /// </summary>
        /// <param name="newEntityClassContent"></param>
        /// <param name="targetTableProperty"></param>
        public static void AppendStringLengthAttribute(ref StringBuilder newEntityClassContent, TableFieldInfo targetTableProperty)
        {
            if (targetTableProperty.Length != null && targetTableProperty.Length != -1)
                newEntityClassContent.Append(string.Format("\t\t[StringLength({0}, ErrorMessage=\"{1}\")]\n", targetTableProperty.Length, GeneratorSettingsManager.STRING_LENGTH_ATTRIBUTE_PERSIAN_MESSAGE));
        }


        /// <summary>
        /// Append [DataType] attribute
        /// </summary>
        /// <param name="newEntityClassContent"></param>
        /// <param name="targetTableProperty"></param>
        public static void AppendDataTypeAttribute(ref StringBuilder newEntityClassContent, TableFieldInfo targetTableProperty)
        {
            if (targetTableProperty.Name == "Password")
                newEntityClassContent.Append("\t\t[DataType(DataType.Password)]\n");
        }

        /// <summary>
        /// Append [EmailAddress] attribute
        /// </summary>
        /// <param name="newEntityClassContent"></param>
        /// <param name="targetTableProperty"></param>
        public static void AppendEmailAddressAttribute(ref StringBuilder newEntityClassContent, TableFieldInfo targetTableProperty)
        {
            if (targetTableProperty.Name == "Email" || targetTableProperty.Name == "EmailAddress")
                newEntityClassContent.Append(string.Format("\t\t [EmailAddress(ErrorMessage = \"{0}\")]\n", GeneratorSettingsManager.EMAIL_ADDRESS_ATTRIBUTE_PERSIAN_MESSAGE));
        }

        /// <summary>
        /// Append [ScaffoldColumn] attribute
        /// </summary>
        /// <param name="newEntityClassContent"></param>
        /// <param name="targetTableProperty"></param>
        public static void AppendScaffoldColumnAttribute(ref StringBuilder newEntityClassContent, TableFieldInfo targetTableProperty)
        {
            if (targetTableProperty.Name == "IsActive" || targetTableProperty.Name == "IsDeleted")
                if (targetTableProperty.Type == "datetime" || targetTableProperty.Type == "date")
                    newEntityClassContent.Append("\t\t[ScaffoldColumn(false)]\n");
        }

        /// <summary>
        /// Append [DisplayName] attribute
        /// </summary>
        /// <param name="newEntityClassContent"></param>
        /// <param name="columnTranslatedName"></param>
        public static void AppendDisplayNameAttribute(ref StringBuilder newEntityClassContent, string columnTranslatedName)
        {
            newEntityClassContent.Append("\t\t[DisplayName(\"" + columnTranslatedName + "\")]\n");
        }
    }
}
