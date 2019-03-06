using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace CatFactory.UI.WebAPI.UnitTests
{
    public static class HostingEnvironmentMocker
    {
        public static IHostingEnvironment GetHostingEnvironment()
            => new HostingEnvironment
            {
                EnvironmentName = "Development",
                ApplicationName = "CatFactory.UI",
                WebRootPath = "wwwroot",
                ContentRootPath = Environment.CurrentDirectory.Replace(Path.GetRelativePath("..\\..\\..", Environment.CurrentDirectory), "")
            };
    }
}
