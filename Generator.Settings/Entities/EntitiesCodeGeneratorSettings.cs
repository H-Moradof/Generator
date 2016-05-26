
namespace Generator.Settings.Entities
{
    public sealed class EntitiesCodeGeneratorSettings
    {
        private static EntitiesCodeGeneratorSettings _instance;
        private static readonly object _lock = new object();

        private EntitiesCodeGeneratorSettings() { }

        public static EntitiesCodeGeneratorSettings Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        { 
                            _instance = new EntitiesCodeGeneratorSettings();
                        }
                    }
                }

                return _instance;
            }
        }

        public static DataAnnotationAttributeGenerateMode DataAnnotationAttributeGenerateMode;
        public static NavigatePropertyGenerateMode NavigatePropertyGenerateMode;
        public static RegionGenerateMode RegionGenerateMode;
        public static VirtualizePropertiesMode VirtualizePropertiesMode;
        public static PrimaryKeyNameMode PrimaryKeyNameMode;
        public static TableAttributeGenerateMode TableAttributeGenerateMode;
        public static InheritFromBaseEntityMode InheritFromBaseEntityMode;
    }

    public enum DataAnnotationAttributeGenerateMode
    {
        PutDataAnnotationAttributes,
        IgnoreDataAnnotationAttributes
    }

    public enum NavigatePropertyGenerateMode
    {
        PutNavigateProperties,
        IgnoreNavigateProperties
    }

    public enum RegionGenerateMode
    {
        PutRegions,
        IgnoreRegions
    }

    public enum VirtualizePropertiesMode
    {
        PutForAll,
        IgnoreForAll
    }

    public enum PrimaryKeyNameMode
    {
        Id,
        ID,
        TableNameWithId,
        TableNameWithID
    }

    public enum TableAttributeGenerateMode
    {
        Put,
        Ignore
    }

    public enum InheritFromBaseEntityMode
    {
        Inherit,
        Ignore
    }

}
