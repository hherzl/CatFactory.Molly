using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace CatFactory.UI.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        // todo: Set port for API from config
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .UseUrls("http://localhost:8400/")
            .Build();
    }
}
