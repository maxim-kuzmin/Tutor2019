//Author Maxim Kuzmin//makc//

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Tutor2019.Apps.DockerWebMvc.Root.Ext
{
    /// <summary>
    /// Корень. Расширение. Веб. 
    /// </summary>
    public static class RootExtDatabase
    {
        #region Public methods

        /// <summary>
        /// Корень. Расширение. База данных. Провести миграцию.
        /// </summary>
        /// <typeparam name="T">Тип контекста базы данных.</typeparam>
        /// <param name="appBuilder">Построитель приложения.</param>
        /// <returns>Построитель приложения.</returns>
        public static IApplicationBuilder RootExtDatabaseMigrate<T>(this IApplicationBuilder appBuilder) where T : DbContext
        {
            using (var scope = appBuilder.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var db = services.GetRequiredService<T>();

                    db.Database.Migrate();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();

                    logger.LogError(ex, "An error occurred while migrating the database.");
                }
            }
            return appBuilder;
        }

        #endregion Public methods
    }
}
