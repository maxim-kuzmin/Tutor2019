//Author Maxim Kuzmin//makc//

using Microsoft.Extensions.Logging;
using System.Threading;
using Tutor2019.Apps.MessageBrokerRabbitMQ.Root.Samples.Sample06.Client;
using Tutor2019.Core.Base;

namespace Tutor2019.Apps.MessageBrokerRabbitMQ.Root.Samples.Sample06
{
    /// <summary>
    /// Корень. Пример 06. Клиент.
    /// </summary>
    public class RootSample06Client : CoreBaseSample
    {
        #region Properties

        public string Message { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="name">Имя.</param>
        /// <param name="logger">Регистратор.</param>
        public RootSample06Client(string name, ILogger logger)
            : base(name, logger)
        {
        }

        #endregion Constructors

        #region Public methods

        /// <inheritdoc/>
        public sealed override void Run(CancellationToken cancellationToken)
        {
            var n = Message ?? "30";

            var procedure = new RootSample06ClientProcedure();

            Logger.LogInformation(" [x] Requesting fib({0})", n);

            var response = procedure.CallAsync(n).Result;

            Logger.LogInformation(" [.] Got '{0}'", response);

            procedure.Close();

            cancellationToken.WaitHandle.WaitOne();
        }

        #endregion Public methods
    }
}
