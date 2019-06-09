//Author Maxim Kuzmin//makc//

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Tutor2019.Apps.DockerWebMvc.Data.Entity.Db;
using Tutor2019.Apps.DockerWebMvc.Root.Ext;

namespace Tutor2019.Apps.DockerWebMvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
