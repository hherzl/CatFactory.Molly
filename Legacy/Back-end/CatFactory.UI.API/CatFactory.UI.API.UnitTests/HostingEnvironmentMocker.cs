using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace CatFactory.UI.API.UnitTests
{
    public static class HostingEnvironmentMocker
    {
        public static IHostingEnvironment GetMockHostingEnvironment()
            => new HostingEnvironment
            {
                EnvironmentName = "Development",
                ApplicationName = "CatFactory.UI",
                WebRootPath = "wwwroot",
                ContentRootPath = Environment.CurrentDirectory.Replace(Path.GetRelativePath("..\\..\\..", Environment.CurrentDirectory), "")
            };
    }
}
