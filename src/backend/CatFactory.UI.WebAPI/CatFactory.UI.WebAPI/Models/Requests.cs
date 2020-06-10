namespace CatFactory.UI.WebAPI.Models
{
    public class ImportDatabaseRequest
    {
        public string Name { get; set; }

        public string ConnectionString { get; set; }

        public bool ImportTables { get; set; }

        public bool ImportViews { get; set; }
    }

    public class DbRequest
    {
        public string Name { get; set; }

        public string ConnectionString { get; set; }

        public string Type { get; set; }

        public string Table { get; set; }

        public string View { get; set; }

        public string Column { get; set; }

        public string Description { get; set; }

        public string FixedDescription
            => string.IsNullOrEmpty(Description) ? string.Empty : Description.Contains("'") ? Description.Replace("'", "''").Trim() : Description.Trim();
    }
}
