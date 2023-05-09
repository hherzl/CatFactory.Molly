namespace CatFactory.Molly.API.Models.Common
{
    public interface IListResponse<TModel> : IResponse
    {
        List<TModel> Model { get; set; }
    }
}
