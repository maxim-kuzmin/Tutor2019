//Author Maxim Kuzmin//makc//

using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Tutor2019.Apps.MessageBrokerKafka.Root.Samples.Sample01.Producer;
using Tutor2019.Core.Base;

namespace Tutor2019.Apps.MessageBrokerKafka.Root.Samples.Sample01
{
    /// <summary>
    /// Корень. Пример 01. Поставщик.
    /// </summary>
    public class RootSample01Producer : CoreBaseSample
    {
        #region Properties

        /// <summary>
        /// Серверы начальной загрузки.
        /// </summary>
        public string BootstrapServers { get; set; }

        /// <summary>
        /// Тема.
        /// </summary>
        public string Topic { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="name">Имя.</param>
        /// <param name="logger">Регистратор.</param>
        /// <param name="bootstrapServers">Серверы начальной загрузки.</param>
        /// <param name="topic">Тема.</param>
        public RootSample01Producer(string name, ILogger logger)
            : base(name, logger)
        {
        }

        #endregion Constructors

        #region Public methods

        /// <inheritdoc/>
        public sealed override void Run(CancellationToken cancellationToken)
        {            
            using (var service = new RootSample01ProducerService(Logger, BootstrapServers))
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var data = Guid.NewGuid().ToString();

                    var res = service.SendDataToQueue(data, Topic).Result;

                    Logger.LogInformation(
                        $"Message '{res.Value}' produced to '{res.Topic}/{res.Partition} @{res.Offset}'"
                        );

                    Task.Delay(1000).Wait();
                }
            }
        }

        #endregion Public methods
    }
}