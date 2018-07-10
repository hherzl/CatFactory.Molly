using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;

namespace CatFactory.UI.API.UnitTests
{
    public class MockHostingEnvironment : IHostingEnvironment
    {
        public MockHostingEnvironment()
        {
            EnvironmentName = "Development";
            ApplicationName = "CatFactory.UI";
            WebRootPath = "wwwroot";
            ContentRootPath = Environment.CurrentDirectory.Replace(Path.GetRelativePath("..\\..\\..", Environment.CurrentDirectory), "");
        }

        public string EnvironmentName { get; set; }

        public string ApplicationName { get; set; }

        public string WebRootPath { get; set; }

        public IFileProvider WebRootFileProvider { get; set; }

        public string ContentRootPath { get; set; }

        public IFileProvider ContentRootFileProvider { get; set; }
    }
}
