//Author Maxim Kuzmin//makc//

namespace Tutor2019.Apps.DockerWebMvc.Data.Base.Objects
{
    /// <summary>
    /// Данные. Основа. Объекты. Сущность "Product".
    /// </summary>
    public class DataBaseObjectProduct
    {
        #region Properties

        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Категория.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Цена.
        /// </summary>
        public decimal Price { get; set; }

        #endregion Properties
    }
}
