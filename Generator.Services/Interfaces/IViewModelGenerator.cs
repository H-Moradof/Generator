using System;

namespace Generator.Services.Interfaces
{
    public interface IViewModelGenerator
    {
        string Create(Generator.Entities.DatabaseEntities.TableInfo _Tables, string singleName, string fileAddress, string connection);
    }
}
