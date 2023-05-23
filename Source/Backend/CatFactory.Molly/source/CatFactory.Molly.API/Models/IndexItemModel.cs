namespace CatFactory.Molly.API.Models
{
    public record IndexItemModel
    {
        public IndexItemModel(ObjectRelationalMapping.Index index)
        {
            Name = index.IndexName;
            Description = index.IndexDescription;
            Keys = index.IndexKeys;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Keys { get; set; }
    }
}
