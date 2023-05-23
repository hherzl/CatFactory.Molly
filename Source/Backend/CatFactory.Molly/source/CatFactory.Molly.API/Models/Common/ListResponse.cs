using CatFactory.Molly.API.Models.Common.Contracts;

namespace CatFactory.Molly.API.Models.Common
{
    public record ListResponse<TModel> : Response, IListResponse<TModel>
    {
        public ListResponse()
        {
        }

        public ListResponse(IEnumerable<TModel> model)
        {
            Model = new List<TModel>(model);
        }

        public List<TModel> Model { get; set; }
    }
}
