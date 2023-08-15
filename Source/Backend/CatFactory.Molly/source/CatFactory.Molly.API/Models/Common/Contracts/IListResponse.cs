namespace CatFactory.Molly.API.Models.Common.Contracts;

public interface IListResponse<TModel> : IResponse
{
    List<TModel> Model { get; set; }
}
