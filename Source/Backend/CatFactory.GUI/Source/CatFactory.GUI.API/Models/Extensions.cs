using CatFactory.GUI.API.Models.Common;
using Microsoft.AspNetCore.Mvc;

namespace CatFactory.GUI.API.Models
{
    public static class Extensions
    {
        public static IActionResult ToOkResult(this Response response)
            => new OkObjectResult(response);

        public static IActionResult ToOkResult<TModel>(this ListResponse<TModel> response)
            => new OkObjectResult(response);

        public static IActionResult ToOkResult<TModel>(this SingleResponse<TModel> response)
            => new OkObjectResult(response);
    }
}
