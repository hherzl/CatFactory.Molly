namespace CatFactory.GUI.API.Models.Common
{
    public class ListResponse<TModel> : Response, IListResponse<TModel>
    {
        public ListResponse()
        {
        }

        public ListResponse(IEnumerable<TModel> model)
        {
            Model = model;
        }

        public IEnumerable<TModel> Model { get; set; }
    }
}
