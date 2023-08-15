namespace CatFactory.Molly.API.Models.Common.Contracts;

public interface ISingleResponse<TModel> : IResponse
{
    TModel Model { get; set; }
}
