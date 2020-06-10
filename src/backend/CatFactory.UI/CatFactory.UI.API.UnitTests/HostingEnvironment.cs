using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;

namespace CatFactory.UI.API.UnitTests
{
    class HostingEnvironment : IHostingEnvironment
    {
        public string EnvironmentName { get; set; }

        public string ApplicationName { get; set; }

        public string WebRootPath { get; set; }

        public IFileProvider WebRootFileProvider { get; set; }

        public string ContentRootPath { get; set; }

        public IFileProvider ContentRootFileProvider { get; set; }
    }
}
