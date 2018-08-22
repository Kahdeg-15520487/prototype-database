using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.Reflection;

namespace prototype_database.Startup
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var assemblyName = typeof(Startup).GetTypeInfo().Assembly.FullName;

            return
            WebHost.CreateDefaultBuilder(args)
                //.UseStartup<Startup>()
                .UseStartup(assemblyName)
                ;
        }
    }
}
