using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace CatFactory.UI.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost
                .CreateDefaultBuilder(args)
                .UseUrls("http://localhost:8400")
                .UseStartup<Startup>();
    }
}
