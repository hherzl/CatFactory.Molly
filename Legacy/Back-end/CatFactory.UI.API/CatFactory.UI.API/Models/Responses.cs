using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CatFactory.UI.API.Models
{
    public interface IResponse
    {
        string Message { get; set; }

        bool DidError { get; set; }

        string ErrorMessage { get; set; }
    }

    public interface IListResponse<TModel> : IResponse
    {
        IEnumerable<TModel> Model { get; set; }
    }

    public interface ISingleResponse<TModel> : IResponse
    {
        TModel Model { get; set; }
    }

    public class ImportDatabaseResponse : IResponse
    {
        public string Message { get; set; }

        public bool DidError { get; set; }

        public string ErrorMessage { get; set; }
    }

    public class ListResponse<TModel> : IListResponse<TModel>
    {
        public string Message { get; set; }

        public bool DidError { get; set; }

        public string ErrorMessage { get; set; }

        public IEnumerable<TModel> Model { get; set; }
    }

    public class SingleResponse<TModel> : ISingleResponse<TModel>
    {
        public string Message { get; set; }

        public bool DidError { get; set; }

        public string ErrorMessage { get; set; }

        public TModel Model { get; set; }
    }

    public static class ResponsesExtensions
    {
        public static void SetError(this IResponse response, Exception ex, ILogger logger)
        {
            logger?.LogCritical(ex.ToString());

            response.DidError = true;
            response.ErrorMessage = ex.Message;
        }

        public static IActionResult ToHttpResponse(this IResponse response)
        {
            var status = HttpStatusCode.OK;

            if (response.DidError)
                status = HttpStatusCode.InternalServerError;

            return new ObjectResult(response)
            {
                StatusCode = (int)status
            };
        }
    }

    public class EditDescription
    {
    }

    public class ImportedDatabase
    {
        public string Name { get; set; }

        public string Dbms { get; set; } = "SQL Server";

        public int TablesCount { get; set; }

        public int ViewsCount { get; set; }

        public string Details { get; } = "Details";
    }
}
