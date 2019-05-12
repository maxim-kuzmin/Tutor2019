//Author Maxim Kuzmin//makc//

using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Tutor2019.Apps.MessageBrokerRabbitMQ.Root.Samples.Sample04.EmitLogDirect;
using Tutor2019.Core.Base;

namespace Tutor2019.Apps.MessageBrokerRabbitMQ.Root.Samples.Sample04
{
    /// <summary>
    /// Корень. Пример 04. Получение логов по конкретному направлению.
    /// </summary>
    public class RootSample04ReceiveLogsDirect : CoreBaseSample
    {
        #region Properties

        /// <summary>
        /// Сообщение.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Уровни серьёзности.
        /// </summary>
        public List<RootSample04EmitLogDirectSeverities> Severities { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="name">Имя.</param>
        /// <param name="logger">Регистратор.</param>
        public RootSample04ReceiveLogsDirect(string name, ILogger logger)
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

                    var queueName = channel.QueueDeclare().QueueName;

                    foreach (var severity in Severities)
                    {
                        var severityString = severity.ToString().ToLower();

                        channel.QueueBind(
                            queue: queueName,
                            exchange: "direct_logs",
                            routingKey: severityString
                            );
                    }

                    Logger.LogInformation(" [*] Waiting for messages.");

                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;

                        var message = Encoding.UTF8.GetString(body);

                        var routingKey = ea.RoutingKey;

                        Logger.LogInformation($" [x] Received '{routingKey}: {message}'");
                    };

                    channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

                    cancellationToken.WaitHandle.WaitOne();
                }
            }
        }

        #endregion Public methods
    }
}
