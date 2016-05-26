using Generator.Settings.Entities;
using System;

namespace Generator.Services.Bussiness.Entities.Properties
{
    public static class PrimaryKeyNameModeFactory
    {
        public static IPrimaryKeyNameStrategy GetStrategy(PrimaryKeyNameMode pkMode)
        {
            switch (pkMode)
            {
                case Settings.Entities.PrimaryKeyNameMode.Id:
                    return new JustCamelCaseIdPrimaryKeyNameStrategy();
                case Settings.Entities.PrimaryKeyNameMode.ID:
                    return new JustPascalCaseIdPrimaryKeyNameStrategy();
                case Settings.Entities.PrimaryKeyNameMode.TableNameWithId:
                    return new TableNameWithCamelCaseIdPrimaryKeyNameStrategy();
                case Settings.Entities.PrimaryKeyNameMode.TableNameWithID:
                    return new TableNameWithPascalCaseIdPrimaryKeyNameStrategy();
                default:
                    throw new ArgumentOutOfRangeException("pkMode");
            }
        }
    }
}
