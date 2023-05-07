using CatFactory.SqlServer;

namespace CatFactory.GUI.API.Models
{
    public class EditDescriptionRequest
    {
        public string Database { get; set; }
        public string Type { get; set; }
        public string Table { get; set; }
        public string View { get; set; }
        public string Column { get; set; }
        public string Description { get; set; }

        public bool HasDescription
            => !string.IsNullOrEmpty(Description);

        public string FixedDescription
            => Description.Replace("'", "\'").Trim();

        public bool IsTable
            => Type == SqlServerToken.TABLE;

        public bool IsView
            => Type == SqlServerToken.VIEW;

        public bool IsColumn
            => !string.IsNullOrEmpty(Column);
    }
}
