using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Services.Bussiness.Entities.Properties
{
    public interface IGeneratePropertyStrategy
    {
        void AppendProperty(ref StringBuilder output, string propertyType, string propertyName, bool isPropertyNullable);
    }
}
