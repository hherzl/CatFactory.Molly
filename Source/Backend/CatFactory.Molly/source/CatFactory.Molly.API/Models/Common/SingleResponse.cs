using CatFactory.Molly.API.Models.Common.Contracts;

namespace CatFactory.Molly.API.Models.Common;

public record SingleResponse<TModel> : Response, ISingleResponse<TModel>
{
    public SingleResponse()
    {
    }

    public SingleResponse(TModel model)
    {
        Model = model;
    }

    public TModel Model { get; set; }
}
