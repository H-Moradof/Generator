using Generator.Settings.Entities;
using System;

namespace Generator.Services.Bussiness.Entities.AnnotationAttributes
{
    public class GenerateAnnotationAttributesFactory
    {
        public static IGenerateAnnotationAttributesStrategy GetStrategy(DataAnnotationAttributeGenerateMode mode)
        {
            switch (mode)
            {
                case DataAnnotationAttributeGenerateMode.PutDataAnnotationAttributes:
                    return new PutAllGenerateAnnotationAttributesStrategy();
                case DataAnnotationAttributeGenerateMode.IgnoreDataAnnotationAttributes:
                    return new IgnoreAllGenerateAnnotationAttributesStrategy();
                default:
                    throw new ArgumentOutOfRangeException("mode");
            }
        }
    }
}
