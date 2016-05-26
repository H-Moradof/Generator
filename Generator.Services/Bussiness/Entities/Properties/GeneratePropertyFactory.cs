using Generator.Settings.Entities;
using System;

namespace Generator.Services.Bussiness.Entities.Properties
{
    public static class GeneratePropertyFactory
    {
        public static IGeneratePropertyStrategy GetStrategy(VirtualizePropertiesMode mode)
        {
            switch (mode)
            {
                case VirtualizePropertiesMode.PutForAll:
                    return new PutVirtualInProperties();
                case VirtualizePropertiesMode.IgnoreForAll:
                    return new IgnoreVirtualInProperties();
                default:
                    throw new ArgumentOutOfRangeException("mode");
            }
        }
    }
}
