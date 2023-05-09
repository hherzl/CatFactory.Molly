namespace CatFactory.Molly.API.Models.Common
{
    public class Response : IResponse
    {
        public Response()
        {
        }

        public Response(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}
