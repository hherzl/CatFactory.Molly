namespace CatFactory.Molly.API.Models.Common
{
    public interface ISingleResponse<TModel> : IResponse
    {
        TModel Model { get; set; }
    }
}
