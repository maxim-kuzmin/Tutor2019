//Author Maxim Kuzmin//makc//

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tutor2019.Apps.DockerWebMvc.Data.Entity.Db;
using Tutor2019.Apps.DockerWebMvc.Root;
using Tutor2019.Apps.DockerWebMvc.Root.Ext;

namespace Tutor2019.Apps.DockerWebMvc
{
    /// <summary>
    /// Пуск.
    /// </summary>
    public class Startup
    {
        #region Properties

        private IConfiguration Configuration { get; }

        #endregion Properties

        #region Constructors

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #endregion Constructors

        #region Public methods

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(logging =>
            {
                logging.AddConsole();
                logging.AddDebug();
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            var host = Configuration["DBHOST"] ?? "localhost";
            var port = Configuration["DBPORT"] ?? "3306";
            var password = Configuration["DBPASSWORD"] ?? "mysecret";

            services.AddDbContext<DataEntityDbContext>(options =>
                options.UseMySql($"server={host};userid=root;pwd={password};port={port};database=products")
                );

            services.AddTransient<IRootRepository, RootRepository>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseMvcWithDefaultRoute();

            app.RootExtDatabaseMigrate<DataEntityDbContext>();
        }

        #endregion Public methods
    }
}
