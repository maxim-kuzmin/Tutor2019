﻿//Author Maxim Kuzmin//makc//

using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Text;
using System.Threading;
using Tutor2019.Apps.MessageBrokerRabbitMQ.Root.Samples.Sample04.EmitLogDirect;
using Tutor2019.Core.Base;

namespace Tutor2019.Apps.MessageBrokerRabbitMQ.Root.Samples.Sample04
{
    /// <summary>
    /// Корень. Пример 04. Испускание лога по конкретному направлению.
    /// </summary>
    public class RootSample04EmitLogDirect : CoreBaseSample
    {
        #region Properties

        /// <summary>
        /// Сообщение.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Уровень серьёзности.
        /// </summary>
        public RootSample04EmitLogDirectSeverities Severity { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="name">Имя.</param>
        /// <param name="logger">Регистратор.</param>
        public RootSample04EmitLogDirect(string name, ILogger logger)
            : base(name, logger)
        {
        }

        #endregion Constructors

        #region Public methods

        /// <inheritdoc/>
        public sealed override void Run(CancellationToken cancellationToken)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: "direct_logs", type: "direct");

                    var message = Message ?? "Hello World!";
                    var severityString = Severity.ToString().ToLower();

                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(
                        exchange: "direct_logs",
                        routingKey: severityString,
                        basicProperties: null,
                        body: body
                        );

                    Logger.LogInformation($" [x] Sent {severityString}: {message}");
                }
            }

            cancellationToken.WaitHandle.WaitOne();
        }

        #endregion Public methods
    }
}
