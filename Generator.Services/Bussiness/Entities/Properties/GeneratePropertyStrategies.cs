using System.Text;

namespace Generator.Services.Bussiness.Entities.Properties
{
    public class PutVirtualInProperties : IGeneratePropertyStrategy
    {
        public void AppendProperty(ref StringBuilder output, string propertyType, string propertyName, bool isPropertyNullable)
        {
            string nullableChar = isPropertyNullable ? "?" : string.Empty;
            string propContent = string.Format("\t\tpublic virtual {0}{1} {2} {{ get; set; }}\n\n", propertyType, nullableChar, propertyName);
            output.Append(propContent);
        }
    }

    public class IgnoreVirtualInProperties : IGeneratePropertyStrategy
    {
        public void AppendProperty(ref StringBuilder output, string propertyType, string propertyName, bool isPropertyNullable)
        {
            string nullableChar = isPropertyNullable ? "?" : string.Empty;
            string propContent = string.Format("\t\tpublic {0}{1} {2} {{ get; set; }}\n\n", propertyType, nullableChar, propertyName);
            output.Append(propContent);
        }
    }
}
