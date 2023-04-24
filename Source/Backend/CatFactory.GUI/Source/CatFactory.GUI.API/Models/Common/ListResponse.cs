namespace CatFactory.GUI.API.Models.Common
{
    public class ListResponse<TModel> : IListResponse<TModel>
    {
        public ListResponse()
        {
        }

        public ListResponse(IEnumerable<TModel> model)
        {
            Model = model;
        }

        public string Message { get; set; }
        public IEnumerable<TModel> Model { get; set; }
    }
}
