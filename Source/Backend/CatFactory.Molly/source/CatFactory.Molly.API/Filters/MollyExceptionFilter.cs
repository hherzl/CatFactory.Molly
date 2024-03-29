﻿using System.Net;
using CatFactory.Molly.API.Models.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CatFactory.Molly.API.Filters;

public class MollyExceptionFilter : IExceptionFilter
{
    private readonly IHostEnvironment _hostEnvironment;

    public MollyExceptionFilter(IHostEnvironment hostEnvironment) =>
        _hostEnvironment = hostEnvironment;

    public void OnException(ExceptionContext context)
    {
        if (!_hostEnvironment.IsDevelopment())
        {
            // Don't display exception details unless running in Development.
            return;
        }

        context.Result ??= new JsonResult(new Response("There was an error"))
        {
            StatusCode = (int)HttpStatusCode.InternalServerError
        };
    }
}
