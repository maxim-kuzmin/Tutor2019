//Author Maxim Kuzmin//makc//

using System.Linq;
using Tutor2019.Apps.DockerWebMvc.Data.Entity.Db;
using Tutor2019.Apps.DockerWebMvc.Data.Entity.Objects;

namespace Tutor2019.Apps.DockerWebMvc.Root
{
    /// <summary>
    /// Корень. Репозиторий.
    /// </summary>
    public class RootRepository : IRootRepository
    {
        #region Properties

        private DataEntityDbContext DbContext { get; set; }

        /// <inheritdoc/>
        public IQueryable<DataEntityObjectProduct> Products => DbContext.Product;

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="dbContext">Контекст базы данных.</param>
        public RootRepository(DataEntityDbContext dbContext)
        {
            DbContext = dbContext;
        }

        #endregion Constructors
    }
}
