//Author Maxim Kuzmin//makc//

using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using Tutor2019.Apps.MessageBrokerKafka.Root.Samples.Sample01.Consumer;
using Tutor2019.Apps.MessageBrokerKafka.Root.Samples.Sample01.Consumer.Enums;
using Tutor2019.Core.Base;

namespace Tutor2019.Apps.MessageBrokerKafka.Root.Samples.Sample01
{
    /// <summary>
    /// Корень. Пример 01. Потребитель.
    /// </summary>
    public class RootSample01Consumer : CoreBaseSample
    {
        #region Properties

        /// <summary>
        /// Действие.
        /// </summary>
        public RootSample01ConsumerActions Action { get; set; }

        /// <summary>
        /// Серверы начальной загрузки.
        /// </summary>
        public string BootstrapServers { get; set; }

        /// <summary>
        /// Темы.
        /// </summary>
        public IEnumerable<string> Topics { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="name">Имя.</param>
        /// <param name="logger">Регистратор.</param>
        public RootSample01Consumer(string name, ILogger logger)
            : base(name, logger)
        {
        }

        #endregion Constructors

        #region Public methods

        /// <inheritdoc/>
        public sealed override void Run(CancellationToken cancellationToken)
        {
            var service = new RootSample01ConsumerService(
                Logger,
                BootstrapServers,
                "sample01-consumer",
                Topics
                );

            switch (Action)
            {
                case RootSample01ConsumerActions.ReceiveByManualAssign:
                    service.ReceiveDataFromQueueBySubscription(cancellationToken);
                    break;
                case RootSample01ConsumerActions.ReceiveBySubscription:
                    service.ReceiveDataFromQueueBySubscription(cancellationToken);
                    break;
            }            
        }

        #endregion Public methods
    }
}