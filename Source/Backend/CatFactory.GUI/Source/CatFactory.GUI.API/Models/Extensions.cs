using CatFactory.GUI.API.Models.Common;
using Microsoft.AspNetCore.Mvc;

namespace CatFactory.GUI.API.Models
{
    public static class Extensions
    {
        public static IActionResult ToOkResult(this IResponse response)
            => new OkObjectResult(response);

        public static IActionResult ToOkResult<TModel>(this IListResponse<TModel> response)
            => new OkObjectResult(response);

        public static IActionResult ToOkResult<TModel>(this ISingleResponse<TModel> response)
            => new OkObjectResult(response);
    }
}
