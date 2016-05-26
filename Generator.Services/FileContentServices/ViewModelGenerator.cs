using Generator.Entities.DatabaseEntities;
using Generator.Services.Database;
using Generator.Services.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;

namespace Generator.Services.FileContentServices
{

    /// <summary>
    /// ساخت ویومدل ها
    /// </summary>
    public class ViewModelGenerator : IViewModelGenerator
    {

        public string Create(TableInfo _Tables, string singleName, string fileAddress, string connection)
        {
            var _CreateVM = new StringBuilder();
            var _ListVM = new StringBuilder();

            const string nameSpaceSystem = "using System.Collections.Generic;\n"; // ثابت
            string nameSpaceEntity = "using DomainModels.Entities." + _Tables.TableSchema + ";\n"; // نیم اسپیس ان تی تی

            string folderName = singleName + "VM";
            string nameSpaceClass = "namespace ViewModels." + _Tables.TableSchema + "." + folderName + "\n";
            
            string className = singleName + "_VM";

            string nameSpaceDesign = "ViewModels." + _Tables.TableSchema + "." + folderName + "." + className;


            // گرفتن ارتباط جداول با جدول فعلی
            List<RelationSchemaInfo> _Relation = GetRelations(_Tables, connection);


            string createProp = "";
            string listProp = "";

            foreach (var item in _Relation)
            {
                TableNameInfo tableName = GeneratorDatabaseProcessor.Titles.Where(c => c.Single == item.ReferenceTableName || c.Plural == item.ReferenceTableName).FirstOrDefault();
                string singleRelationName = ((tableName != null) ? tableName.Single : item.ReferenceTableName);
                string pluralNameTable = ((tableName != null) ? tableName.Plural : item.ReferenceTableName);

                createProp += "\t\tpublic ICollection<" + singleRelationName + "> " + pluralNameTable + " { get; set; }\n";
                listProp += "\t\tpublic " + singleRelationName + " " + singleRelationName + " { get; set; }\n";

                if (_Tables.TableSchema != item.ReferenceSchemaName)
                    nameSpaceEntity += "using DomainModels.Entities." + item.ReferenceSchemaName + ";\n";

            }


            _CreateVM.Append(nameSpaceSystem);
            _CreateVM.Append(nameSpaceEntity + "\n");
            _CreateVM.Append(nameSpaceClass);
            _CreateVM.Append("{\n\tpublic class Create" + className + "\n\t{\n");
            _CreateVM.Append("\t\tpublic " + singleName + " " + singleName + " { get; set; }\n");

            _ListVM.Append(_CreateVM.ToString().Replace("Create" + className, "List" + className));

            _CreateVM.Append(createProp);
            _CreateVM.Append("\t}\n}");


            _ListVM.Append(listProp);
            _ListVM.Append("\t}\n}");


            Directory.CreateDirectory(fileAddress + "\\ViewModels\\" + _Tables.TableSchema + "\\" + folderName);
            File.WriteAllText(fileAddress + "\\ViewModels\\" + _Tables.TableSchema + "\\" + folderName + "\\Create" + className + ".cs", _CreateVM.ToString());
            File.WriteAllText(fileAddress + "\\ViewModels\\" + _Tables.TableSchema + "\\" + folderName + "\\List" + className + ".cs", _ListVM.ToString());

            return nameSpaceDesign;
        }


        #region -Get Relations

        private List<RelationSchemaInfo> GetRelations(TableInfo _Tables, string connection)
        {
            List<RelationSchemaInfo> relations = new DbContext(connection).Database.SqlQuery<RelationSchemaInfo>(
            @"SELECT * FROM
	            (SELECT
		            SCHEMA_NAME(f.SCHEMA_ID) SchemaName,
		            OBJECT_NAME(f.parent_object_id) AS TableName,
		            COL_NAME(fc.parent_object_id,fc.parent_column_id) AS ColumnName,
		            SCHEMA_NAME(o.SCHEMA_ID) ReferenceSchemaName,
		            OBJECT_NAME (f.referenced_object_id) AS ReferenceTableName
	            FROM sys.foreign_keys AS f
		            INNER JOIN sys.foreign_key_columns AS fc ON f.OBJECT_ID = fc.constraint_object_id
		            INNER JOIN sys.objects AS o ON o.OBJECT_ID = fc.referenced_object_id
	            ) T
            WHERE T.TableName = '" + _Tables.TableName + "' AND T.ReferenceTableName != '" + _Tables.TableName + "'").ToList();

            return relations;
        }

        #endregion

    }
}

