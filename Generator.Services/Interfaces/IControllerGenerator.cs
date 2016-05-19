using System;

namespace Generator.Services.Interfaces
{
    public interface IControllerGenerator
    {
        string Create(string tableName, string tableSingleName, string schemaName, string applicationRootPath);
    }
}
