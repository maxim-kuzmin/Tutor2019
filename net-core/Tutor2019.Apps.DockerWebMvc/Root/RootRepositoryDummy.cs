//Author Maxim Kuzmin//makc//

using System.Linq;
using Tutor2019.Apps.DockerWebMvc.Data.Entity.Objects;

namespace Tutor2019.Apps.DockerWebMvc.Root
{
    /// <summary>
    /// Корень. Репозиторий. Заглушка.
    /// </summary>
    public class RootRepositoryDummy : IRootRepository
    {
        #region Properties

        private static DataEntityObjectProduct[] DummyData { get; } = new []
        {
            new DataEntityObjectProduct { Name = "Prod1",  Category = "Cat1", Price = 100 },
            new DataEntityObjectProduct { Name = "Prod2",  Category = "Cat1", Price = 100 },
            new DataEntityObjectProduct { Name = "Prod3",  Category = "Cat2", Price = 100 },
            new DataEntityObjectProduct { Name = "Prod4",  Category = "Cat2", Price = 100 },
        };

        /// <inheritdoc/>
        public IQueryable<DataEntityObjectProduct> Products => DummyData.AsQueryable();

        #endregion Properties
    }
}
