using System;

namespace Generator.Services.Interfaces
{
    public interface IServiceGenerator
    {
        string CreateClass(string tableName, string tableSingleName, string schemaName, string applicationRootPath);
        string CreateInterface(string tableSingleName, string schemaName, string applicationRootPath);
    }
}
