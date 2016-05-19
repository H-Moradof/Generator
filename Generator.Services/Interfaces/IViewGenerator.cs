using Generator.Entities.DatabaseEntities;
using System;
using System.Collections.Generic;

namespace Generator.Services.Interfaces
{
    public interface IViewGenerator
    {
        void SetTableProperties(IList<TableFieldInfo> tableProperties);
        IList<TableFieldInfo> GetTableProperties();
        void SetTableRelations(IList<RelationSchemaInfo> Relations);
        IList<RelationSchemaInfo> GetTableRelations();


        void Create_CreateView(string singleTableName, Generator.Entities.DatabaseEntities.TableInfo tableInfo, string destinationPath);
        void Create_IndexView(string singleTableName, Generator.Entities.DatabaseEntities.TableInfo _Tables, string fileAddress);
        void Create_ListView(string singleTableName, Generator.Entities.DatabaseEntities.TableInfo tableInfo, string destinationPath);
        void Create_Script(string singleTableName, Generator.Entities.DatabaseEntities.TableInfo tableInfo, string destinationPath);
    }
}
