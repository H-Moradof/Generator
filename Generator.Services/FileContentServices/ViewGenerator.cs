using Generator.Entities.DatabaseEntities;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using Generator.Services.Interfaces;

namespace Generator.Services.FileContentServices
{
    /// <summary>
    /// ساخت ویوها
    /// </summary>
    public  class ViewGenerator : IViewGenerator
    {

        private  IList<TableFieldInfo> _tableProperties { get; set; }
        private IList<RelationSchemaInfo> _relations { get; set; }


        public void SetTableProperties(IList<TableFieldInfo> tableProperties)
        {
            _tableProperties = tableProperties;
        }

        public IList<TableFieldInfo> GetTableProperties()
        {
            return _tableProperties;
        }

        public void SetTableRelations(IList<RelationSchemaInfo> relations)
        {
            _relations = relations;
        }

        public IList<RelationSchemaInfo> GetTableRelations()
        {
            return _relations;
        }


        //-----------------------------------------------------

        #region Index

        public  void Create_IndexView(string singleTableName, TableInfo _Tables, string fileAddress)
        {

            string index = @"
<script src=""~/[Script]/script.js""></script>


<div id=""FormContainer"" class=""row popup-container"">
    <!---------- Start Create Form ----------------->
    @Html.Action(""_Create"", """ + singleTableName + @""")
    <!---------- End Create Form ----------------->
</div>


<div id=""FormList"" class=""row"">
    <!------------- Start Table ----------------------->
    @Html.Action(""_List"", """ + singleTableName + @""")
    <!------------- End Table ----------------------->
</div>

";

            index = index.Replace("[Script]", "Areas/" + _Tables.TableSchema + "/Views/" + singleTableName);

            Directory.CreateDirectory(fileAddress + "\\Areas\\" + _Tables.TableSchema + "\\Views\\" + singleTableName);
            File.WriteAllText(fileAddress + "\\Areas\\" + _Tables.TableSchema + "\\Views\\" + singleTableName + "\\" + "_Index.cshtml", index);

        }

        #endregion


        //-----------------------------------------------------

        #region Create

        // ساخت پارشیال ویو _Create
        public  void Create_CreateView(string singleTableName, TableInfo tableInfo, string destinationPath)
        {
            string viewHtmlTags = string.Empty;


            foreach (var item in this._tableProperties)
            {
                // ساخت هیدن فیلد برای آی دی
                if (item.Name == singleTableName + "ID")
                {
                    viewHtmlTags += 
@"@Html.HiddenFor(model => model.[ModelName].[PropertyName], new { Value = (@ViewBag.Id != null) ? @ViewBag.Id : Model.[ModelName].[PropertyName], @id = ""[PropertyName]"" })

";

                    viewHtmlTags = viewHtmlTags.Replace("[ModelName]", singleTableName);
                    viewHtmlTags = viewHtmlTags.Replace("[PropertyName]", item.Name);

                    continue;
                }

                // ساخت دراپ داون 
                if (_relations.Any(c => c.ColumnName == item.Name))
                {
                    viewHtmlTags += 
@"    <div class=""form-group col-lg-6"">
        @Html.LabelFor(model => model.[ModelName].[PropertyName], new { @class = ""label-txt"" })
        @Html.ValidationMessageFor(model => model.[ModelName].[PropertyName], String.Empty, new { @class = ""label-validation"" })
        @Html.DropDownListFor(model => model.[ModelName].[PropertyName], new SelectList(Model.[PropertyNameWithoutID]s, ""[PropertyName]"", ""Title""), new { @class = ""form-control txtbox"" })
    </div>

";

                    viewHtmlTags = viewHtmlTags.Replace("[ModelName]", singleTableName);
                    viewHtmlTags = viewHtmlTags.Replace("[PropertyName]", item.Name);
                    viewHtmlTags = viewHtmlTags.Replace("[PropertyNameWithoutID]", item.Name.Replace("ID" , ""));

                    continue;
                }


                // ساخت دراپ داون برای فیلدهای از نوع  بیت 
                if (item.Type == "bit")
                {

                    viewHtmlTags += 
@"    <div class=""form-group col-lg-6"">
        @Html.LabelFor(model => model.[ModelName].[PropertyName], new { @class = ""label-txt"" })
        @Html.ValidationMessageFor(model => model.[ModelName].[PropertyName], String.Empty, new { @class = ""label-validation"" })
        @Html.DropDownListFor(model => model.[ModelName].[PropertyName], new List<SelectListItem> {
            new SelectListItem{ Text=""مرد"", Value=""true""},
            new SelectListItem{ Text=""زن"", Value=""false""}
        }, new { @class = ""form-control txtbox"" })
    </div>

";

                    viewHtmlTags = viewHtmlTags.Replace("[ModelName]", singleTableName);
                    viewHtmlTags = viewHtmlTags.Replace("[PropertyName]", item.Name);

                    continue;
                }


                // ساخت Textbox
                if (item.Type == "int" || item.Type == "bigint" || item.Type == "nvarchar" || item.Type == "varchar")
                {
                    viewHtmlTags += 
@"    <div class=""form-group col-lg-6"">
        @Html.LabelFor(model => model.[ModelName].[PropertyName], new { @class = ""label-txt"" })
        @Html.ValidationMessageFor(model => model.[ModelName].[PropertyName], String.Empty, new { @class = ""label-validation"" })
        @Html.TextBoxFor(model => model.[ModelName].[PropertyName], new { @class = ""form-control txtbox"" })
    </div>

";

                    viewHtmlTags = viewHtmlTags.Replace("[ModelName]", singleTableName);
                    viewHtmlTags = viewHtmlTags.Replace("[PropertyName]", item.Name);
                }
            }

            string applicationRootPath = Application.StartupPath.Replace("\\bin\\Debug", "");
            string viewTemplateFilePath = string.Format("{0}\\CodeTemplates\\CreateViewTemplate.txt", applicationRootPath);
            string viewTemplate = System.IO.File.ReadAllText(viewTemplateFilePath);

            viewTemplate = viewTemplate.Replace("[CreateViewModel]", string.Format("ViewModels.{0}.{1}VM.Create{1}_VM", tableInfo.TableSchema, singleTableName));
            viewTemplate = viewTemplate.Replace("[ModelName]", singleTableName);
            viewTemplate = viewTemplate.Replace("[Code]", viewHtmlTags);

            File.WriteAllText(destinationPath + "\\Areas\\" + tableInfo.TableSchema + "\\Views\\" + singleTableName + "\\" + "_Create.cshtml", viewTemplate);

        }

        #endregion


        //-----------------------------------------------------


        #region List

        public  void Create_ListView(string singleTableName, TableInfo tableInfo, string destinationPath)
        {
            string headRow = string.Empty,
            bodyRow = string.Empty,
            inputs = string.Empty,
            sort = string.Empty;

            foreach (var item in this._tableProperties)
            {

                // اگر شناسه جدول بود این کد اجرا شود
                if (item.Name == singleTableName + "ID" || item.Name == "Id")
                {
                    // مرتب سازی بر اساس فیلد آی دی
                    sort = @"

<input type=""hidden"" id=""sort-[ModelName]"" value=""[ModelName].[PropertyName] descending"" />

";
                    sort = sort.Replace("[ModelName]", singleTableName);
                    sort = sort.Replace("[PropertyName]", item.Name);

                    // -------------

                    headRow += @"
                                <th style=""width:80px;""><a onmousedown=""Sort('[ModelName].[PropertyName]', '[ModelName]')"">شناسه <span class=""sort-arrow arrow-sortable""></span></a></th>";

                    headRow = headRow.Replace("[ModelName]", singleTableName);
                    headRow = headRow.Replace("[PropertyName]", item.Name);

                    // -------------

                    bodyRow += @"

                            <td data-th='شناسه'>
                                <input type=""text"" class=""form-control txtbox"" name=""[ModelName].[PropertyName]"">
                            </td>";

                    bodyRow = bodyRow.Replace("[ModelName]", singleTableName);
                    bodyRow = bodyRow.Replace("[PropertyName]", item.Name);

                    continue;
                }


                // اگر پراپرتی ریلیشن داشت نام مدل باید از جدول ریلیشن خوانده شود
                if (_relations.Any(c => c.ColumnName == item.Name))
                {
                    string refrenceTableName = _relations.Where(c => c.ColumnName == item.Name).First().ReferenceTableName;

                    headRow += @"
                                <th><a onmousedown=""Sort('[ModelNameRelation].Title', '[ModelName]')"">@Html.DisplayNameFor(model => model.[ModelNameRelation].Title) <span class=""sort-arrow arrow-sortable""></span></a></th>";

                    headRow = headRow.Replace("[ModelNameRelation]", refrenceTableName);
                    headRow = headRow.Replace("[ModelName]", singleTableName);

                    //-------------

                    bodyRow += @"

                            <td data-th=""@Html.DisplayNameFor(model => model.[ModelNameRelation].Title)"">
                                <input type=""text"" class=""form-control txtbox"" name=""[ModelNameRelation].Title"">
                            </td>";

                    bodyRow = bodyRow.Replace("[ModelNameRelation]", refrenceTableName);

                    continue;
                }



                // اگر نوع بولین بود یک دراپ دان ساخته می شود
                if (item.Type == "bit")
                {
                    headRow += @"
                                <th><a onmousedown=""Sort('[ModelName].[PropertyName]', '[ModelName]')"">@Html.DisplayNameFor(model => model.[ModelName].[PropertyName]) <span class=""sort-arrow arrow-sortable""></span></a></th>";

                    headRow = headRow.Replace("[ModelName]", singleTableName);
                    headRow = headRow.Replace("[PropertyName]", item.Name);

                    // -------------

                    bodyRow += @"

                            <td data-th=""@Html.DisplayNameFor(model => model.[ModelName].[PropertyName])"">
                                <select name=""[ModelName].[PropertyName]"">
                                    <option value="""">نمایش همه</option>
                                    <option value=""true"">فعال</option>
                                    <option value=""false"">غیر فعال</option>
                                </select>
                            </td>";

                    bodyRow = bodyRow.Replace("[ModelName]", singleTableName);
                    bodyRow = bodyRow.Replace("[PropertyName]", item.Name);

                    continue;
                }



                headRow += @"
                                <th><a onmousedown=""Sort('[ModelName].[PropertyName]', '[ModelName]')"">@Html.DisplayNameFor(model => model.[ModelName].[PropertyName]) <span class=""sort-arrow arrow-sortable""></span></a></th>";

                headRow = headRow.Replace("[ModelName]", singleTableName);
                headRow = headRow.Replace("[PropertyName]", item.Name);

                // -------------

                bodyRow += @"

                            <td data-th=""@Html.DisplayNameFor(model => model.[ModelName].[PropertyName])"">
                                <input type=""text"" class=""form-control txtbox"" name=""[ModelName].[PropertyName]"">
                            </td>";

                bodyRow = bodyRow.Replace("[ModelName]", singleTableName);
                bodyRow = bodyRow.Replace("[PropertyName]", item.Name);

            }


            string applicationRootPath = Application.StartupPath.Replace("\\bin\\Debug", "");
            string ListTemplateFilePath = string.Format("{0}\\CodeTemplates\\{1}", applicationRootPath, "ListViewTemplate.txt");
            inputs = System.IO.File.ReadAllText(ListTemplateFilePath);


            inputs = inputs.Replace("[ListViewModel]", string.Format("ViewModels.{0}.{1}VM.List{1}", tableInfo.TableSchema, singleTableName));
            inputs = inputs.Replace("[ModelName]", singleTableName);
            inputs = inputs.Replace("[SortColumn]", sort);


            inputs = inputs.Replace("[HeadRow]", headRow);
            inputs = inputs.Replace("[BodyRow]", bodyRow);
            inputs = inputs.Replace("[CountRow]", string.Format(@"<td colspan=""{0}""></td>", _tableProperties.Count + 1));


            File.WriteAllText(destinationPath + "\\Areas\\" + tableInfo.TableSchema + "\\Views\\" + singleTableName + "\\" + "_List.cshtml", inputs);

        }

        #endregion


        //-----------------------------------------------------


        #region Script

        public void Create_Script(string singleTableName, TableInfo tableInfo, string destinationPath)
        {
            string bodyRow = "";
            string inputs = "";
            string temp = "";
            string idName = "";

            foreach (var item in this._tableProperties)
            {

                // اگر شناسه بود این کد اجرا شود
                if (item.Name == singleTableName + "ID" || item.Name == "Id")
                {
                    temp += @"
                tr.append(""<td data-th='شناسه'>"" + json[i].[ModelName].[PropertyName] + ""</td>"");";

                    temp = temp.Replace("[ModelName]", singleTableName);
                    temp = temp.Replace("[PropertyName]", item.Name);
                    bodyRow += temp;

                    idName = item.Name;

                    continue;
                }



                // اگر پراپرتی ریلیشن داشت نام مدل باید از جدول ریلیشن خوانده شود
                if (_relations.Any(c => c.ColumnName == item.Name))
                {
                    string refrenceTable = _relations.Where(c => c.ColumnName == item.Name).First().ReferenceTableName;

                    temp = @"
                tr.append(""<td data-th='@Html.DisplayNameFor(model => model.[ModelNameRelation].Title)'>"" + json[i].[ModelNameRelation].Title + ""</td>"");";

                    temp = temp.Replace("[ModelNameRelation]", refrenceTable);
                    bodyRow += temp;

                    continue;
                }


                // اگر نوع بولین بود یک دراپ دان ساخته می شود
                if (item.Type == "bit")
                {
                    temp = @"  

                if (json[i].[ModelName].[PropertyName] == true) 
                    tr.append(""<td style='text-align:center' data-th='@Html.DisplayNameFor(model => model.[ModelName].[PropertyName])'><input type='checkbox' checked disabled></td>"");
                else
                    tr.append(""<td style='text-align:center' data-th='@Html.DisplayNameFor(model => model.[ModelName].[PropertyName])'><input type='checkbox' disabled></td>"");";


                    temp = temp.Replace("[ModelName]", singleTableName);
                    temp = temp.Replace("[PropertyName]", item.Name);
                    bodyRow += temp;

                    continue;
                }



                temp = @"
                tr.append(""<td data-th='@Html.DisplayNameFor(model => model.[ModelName].[PropertyName])'>"" + json[i].[ModelName].[PropertyName] + ""</td>"");";

                temp = temp.Replace("[ModelName]", singleTableName);
                temp = temp.Replace("[PropertyName]", item.Name);
                bodyRow += temp;

            }



            string applicationRootPath = Application.StartupPath.Replace("\\bin\\Debug", "");
            string ListTemplateFilePath = string.Format("{0}\\CodeTemplates\\{1}", applicationRootPath, "ScriptView.txt");
            inputs = System.IO.File.ReadAllText(ListTemplateFilePath);

            inputs = inputs.Replace("[ModelName]", singleTableName);
            inputs = inputs.Replace("[PropertyName]", idName);
            inputs = inputs.Replace("[BodyRow]", bodyRow);


            File.WriteAllText(destinationPath + "\\Areas\\" + tableInfo.TableSchema + "\\Views\\" + singleTableName + "\\" + "script.js", inputs);


        }

        #endregion


    }
}
