//Author Maxim Kuzmin//makc//

using System.Linq;
using Tutor2019.Apps.DockerWebMvc.Data.Entity.Objects;

namespace Tutor2019.Apps.DockerWebMvc.Root
{
    /// <summary>
    /// Корень. Репозиторий. Интерфейс.
    /// </summary>
    public interface IRootRepository
    {
        #region Properties

        /// <summary>
        /// Продукты.
        /// </summary>
        IQueryable<DataEntityObjectProduct> Products { get; }

        #endregion Properties
    }
}
