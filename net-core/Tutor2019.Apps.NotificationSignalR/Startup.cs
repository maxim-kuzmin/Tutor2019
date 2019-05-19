//Author Maxim Kuzmin//makc//

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Tutor2019.Apps.NotificationSignalR.Root.Samples.Sample01.Server;

namespace Tutor2019.Apps.NotificationSignalR
{
    /// <summary>
    /// Пуск.
    /// </summary>
    public class Startup
    {
        #region Constants

        private const string CORS_POLICY_NAME = "MyPolicy";

        public const string PATH_Sample01 = "/sample01";

        public const string URL = "http://localhost:5000";

        public const string URL_Sample01 = URL + PATH_Sample01;

        #endregion Constants

        #region Public methods

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        /// </summary>
        /// <param name="services">Сервисы.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy(CORS_POLICY_NAME, builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials();
            }));

            services.AddSignalR();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">Приложение.</param>
        /// <param name="env">Окружение.</param>
        // 
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(CORS_POLICY_NAME);

            app.UseSignalR(route =>
            {
                route.MapHub<RootSample01ServerHub>(PATH_Sample01);
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }

        #endregion Public methods
    }
}
