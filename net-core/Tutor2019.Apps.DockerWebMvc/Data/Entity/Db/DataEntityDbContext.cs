//Author Maxim Kuzmin//makc//

using Microsoft.EntityFrameworkCore;
using Tutor2019.Apps.DockerWebMvc.Data.Entity.Objects;
using Tutor2019.Apps.DockerWebMvc.Data.Entity.Schemas;

namespace Tutor2019.Apps.DockerWebMvc.Data.Entity.Db
{
    /// <summary>
    /// Данные. Entity Framework. База данных. Контекст.
    /// </summary>
    public class DataEntityDbContext : DbContext
    {
        #region Properties

        /// <summary>
        /// Данные сущности "Product".
        /// </summary>
        public DbSet<DataEntityObjectProduct> Product { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="options">Опции.</param>
        public DataEntityDbContext(DbContextOptions<DataEntityDbContext> options)
            : base(options)
        {
        }

        #endregion Constructors

        #region Protected methods

        /// <summary>
        /// Обработать событие создания модели.
        /// </summary>
        /// <param name="modelBuilder">Построитель модели.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new DataEntitySchemaProduct());

#if TEST || DEBUG
            DataEntitySchemaProduct.SeedTestData(modelBuilder);
#endif
        }

        #endregion Protected methods
    }
}
