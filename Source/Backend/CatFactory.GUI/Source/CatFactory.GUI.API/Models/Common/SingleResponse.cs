namespace CatFactory.GUI.API.Models.Common
{
    public class SingleResponse<TModel> : ISingleResponse<TModel>
    {
        public SingleResponse()
        {
        }

        public SingleResponse(TModel model)
        {
            Model = model;
        }

        public string Message { get; set; }
        public TModel Model { get; set; }
    }
}
