//Author Maxim Kuzmin//makc//

using Microsoft.Extensions.Logging;
using System.Threading;

namespace Tutor2019.Core.Base
{
    public abstract class CoreBaseSample
    {
        #region Properties

        /// <summary>
        /// Регистратор.
        /// </summary>
        protected ILogger Logger { get; private set; }

        /// <summary>
        /// Имя.
        /// </summary>
        public string Name { get; private set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="name">Имя.</param>
        /// <param name="logger">Регистратор.</param>
        public CoreBaseSample(string name, ILogger logger)
        {
            Name = name;
            Logger = logger;
        }

        #endregion Constructors

        #region Public methods

        /// <summary>
        /// Запустить.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены.</param>
        public abstract void Run(CancellationToken cancellationToken);

        #endregion Public methods
    }
}