//Author Maxim Kuzmin//makc//

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq;
using Tutor2019.Apps.DockerWebMvc.Data.Entity.Objects;

namespace Tutor2019.Apps.DockerWebMvc.Data.Entity.Schemas
{
    /// <summary>
    /// Данные. Entity Framework. Схемы. Сущность "Product".
    /// </summary>
    public class DataEntitySchemaProduct : IEntityTypeConfiguration<DataEntityObjectProduct>
    {
        #region Constants

        private const string TABLE_NAME = "Product";

        #endregion Constants

        #region Public methods

        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<DataEntityObjectProduct> builder)
        {
            builder.ToTable(TABLE_NAME);

            builder.HasKey(x => x.Id).HasName($"PK_{TABLE_NAME}");

            builder.Property(x => x.Name).IsRequired().IsUnicode().HasMaxLength(50);
            builder.Property(x => x.Category).IsRequired().IsUnicode().HasMaxLength(50);
            builder.Property(x => x.Price).IsRequired();
        }

        /// <summary>
        /// Засеять тестовые данные.
        /// </summary>
        /// <param name="modelBuilder">Построитель модели.</param>
        public static void SeedTestData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DataEntityObjectProduct>().HasData(
                Enumerable.Range(1, 100).Select(id => CreateTestDataItem(id)).ToArray()
                );
        }

        #endregion Public methods

        #region Private methods

        private static DataEntityObjectProduct CreateTestDataItem(int id)
        {
            return new DataEntityObjectProduct
            {
                Id = id,
                Name = $"Name-{id}",
                Category = $"Category-{new Random(Guid.NewGuid().GetHashCode()).Next(1, 10)}",
                Price = 1000M + id + (id / 100M)
            };
        }

        #endregion Private methods
    }
}
