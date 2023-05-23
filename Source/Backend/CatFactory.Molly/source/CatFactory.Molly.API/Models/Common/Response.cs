using CatFactory.Molly.API.Models.Common.Contracts;

namespace CatFactory.Molly.API.Models.Common
{
    public record Response : IResponse
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
