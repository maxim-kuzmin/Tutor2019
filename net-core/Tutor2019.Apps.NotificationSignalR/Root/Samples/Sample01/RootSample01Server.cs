//Author Maxim Kuzmin//makc//

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using Tutor2019.Core.Base;

namespace Tutor2019.Apps.NotificationSignalR.Root.Samples.Sample01
{
    /// <summary>
    /// Корень. Пример 01. Сервер.
    /// </summary>
    public class RootSample01Server : CoreBaseSample
    {
        #region Properties

        /// <summary>
        /// Аргументы.
        /// </summary>
        public string[] Args { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="name">Имя.</param>
        /// <param name="logger">Регистратор.</param>
        public RootSample01Server(string name, ILogger logger)
            : base(name, logger)
        {
        }

        #endregion Constructors

        #region Public methods

        /// <inheritdoc/>
        public sealed override void Run(CancellationToken cancellationToken)
        {
            WebHost.CreateDefaultBuilder(Args).UseStartup<Startup>().Build().Run();
        }

        #endregion Public methods
    }
}