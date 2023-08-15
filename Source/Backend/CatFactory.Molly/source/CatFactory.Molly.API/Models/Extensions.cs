using CatFactory.Molly.API.Models.Common.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CatFactory.Molly.API.Models;

public static class Extensions
{
    public static IActionResult ToOkResult(this IResponse response)
        => new OkObjectResult(response);

    public static IActionResult ToOkResult<TModel>(this IListResponse<TModel> response)
        => new OkObjectResult(response);

    public static IActionResult ToOkResult<TModel>(this ISingleResponse<TModel> response)
        => new OkObjectResult(response);
}
