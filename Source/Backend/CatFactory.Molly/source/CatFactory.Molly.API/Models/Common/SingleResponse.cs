namespace CatFactory.Molly.API.Models.Common
{
    public class SingleResponse<TModel> : Response, ISingleResponse<TModel>
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
}
