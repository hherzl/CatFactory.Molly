namespace CatFactory.GUI.API.Models.Common
{
    public interface ISingleResponse<TModel> : IResponse
    {
        TModel Model { get; set; }
    }
}
