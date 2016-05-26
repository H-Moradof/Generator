using Generator.Settings.Entities;
using System;

namespace Generator.Services.Bussiness.Entities.TableAttribute
{
    public static class GenerateTableAttributeFactory
    {
        public static IGenerateTableAttributeStrategy GetStrategy(TableAttributeGenerateMode mode)
        {
            switch (mode)
            {
                case TableAttributeGenerateMode.Put:
                    return new PutGenerateTableAttributeStrategy();
                case TableAttributeGenerateMode.Ignore:
                    return new IgnoreGenerateTableAttributeStrategy();
                default:
                    throw new ArgumentOutOfRangeException("mode");
            }
        }
    }
}
