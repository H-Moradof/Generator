using Generator.Entities.DatabaseEntities;
using Generator.Entities.Enums;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Generator.Settings.Core
{
    public static partial class GeneratorSettingsManager
    {
        // Layers Name
        public const string DATABASECONTEXT_LAYER_NAME = "DatabaseContext";
        public const string DOMAINCLASSESS_LAYER_NAME = "DomainModels";
        public const string SERVICE_LAYER_NAME = "Services";
        public const string VIEWMODEL_LAYER_NAME = "ViewModels";
        public const string WEB_LAYER_NAME = "Web";


        // Folders Name
        public const string DATABASECONTEXT_LAYER_CONTEXT_FOLDER_NAME = "DatabaseContext";
        public const string ENTITIES_LAYER_FOLDER_NAME = "Entities";
        public const string SERVICE_LAYER_SERVICE_CLASSES_FOLDER_NAME = "EfServices";
        public const string SERVICE_LAYER_SERVICE_INTERFACES_FOLDER_NAME = "Interfaces";
        public const string SERVICE_LAYER_GENERICSERVICE_FOLDER_NAME = "GenericService";
        public const string VIEWMODEL_LAYER_FOLDER_NAME = "ViewModels";
        public const string AREAS_FOLDER_NAME = "Areas";
        public const string CONTROLLERS_FOLDER_NAME = "Controllers";
        public const string VIEWSS_FOLDER_NAME = "Views";

        // Constant Project Files Element Name
        public const string SERVICE_LAYER_GENERICSERVICE_CLASS_NAME = "BaseGenericService";
        public const string SERVICE_LAYER_GENERICSERVICE_INTERFACE_NAME = "IBaseGenericService";


        // Postfix
        public const string SERVICE_LAYER_SERVICE_CLASSES_POSTFIX = "Service";
        public const string ADMIN_PANEL_VIEWMODELS_POSTFIX = "Admin";
        public const string MEMBER_PANEL_VIEWMODELS_POSTFIX = "Member";
        

        // NameSpaces Name
        public static readonly string DATABASECONTEXT_CONTEXT_NAMESPACE_NAME = string.Format("{0}.{1}", DATABASECONTEXT_LAYER_NAME, DATABASECONTEXT_LAYER_CONTEXT_FOLDER_NAME);
        public static readonly string ENTITIES_NAMESPACE_NAME = string.Format("{0}.{1}", DOMAINCLASSESS_LAYER_NAME, ENTITIES_LAYER_FOLDER_NAME);
        public static readonly string SERVICES_CLASS_NAMESPACE_NAME = string.Format("{0}.{1}", SERVICE_LAYER_NAME, SERVICE_LAYER_SERVICE_CLASSES_FOLDER_NAME);
        public static readonly string SERVICES_INTERFACES_NAMESPACE_NAME = string.Format("{0}.{1}", SERVICE_LAYER_NAME, SERVICE_LAYER_SERVICE_INTERFACES_FOLDER_NAME);


        // Areas Name [Separate Areas Mode]
        public const string ADMIN_PANEL_AREA_NAME = "Admin";
        public const string MEMBER_PANEL_AREA_NAME = "Member";
        


        // Template Files Name
        public const string DATABASECONTEXT_TEMPLATE_FILENAME = "DbContextTemplate.txt";
        public const string SERVICE_CLASS_TEMPLATE_FILENAME = "ServiceClassTemplate.txt";
        public const string SERVICE_INTERFACE_TEMPLATE_FILENAME = "ServiceInterfaceTemplate.txt";
        public const string CONTROLLER_TEMPLATE_FILENAME = "ControllerTemplate.txt";


        // Attribute Persian Messages
        public const string REQUIRED_ATTRIBUTE_PERSIAN_MESSAGE = "{0} الزامی است";
        public const string STRING_LENGTH_ATTRIBUTE_PERSIAN_MESSAGE = "حداکثر طول مجاز برای {0} {1} کاراکتر می باشد";
        public const string EMAIL_ADDRESS_ATTRIBUTE_PERSIAN_MESSAGE = "فرمت آدرس ایمیل نادرست است";


        // Connection String
        public const string GENERATOR_DATABASE_CONNECTION_STRING = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Generator;Data Source=.";
        

        // Resource File
        public const string RESOURCE_FILE_PROPS_NAME_PATTERN = "WebResources.Props.{0}";
    }
}
