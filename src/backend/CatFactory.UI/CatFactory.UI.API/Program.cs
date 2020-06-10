using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace CatFactory.UI.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseUrls("http://localhost:8400")
                        .UseStartup<Startup>();
                });
    }
}
