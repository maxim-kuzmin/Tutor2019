//Author Maxim Kuzmin//makc//

using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Tutor2019.Core.Base;

namespace Tutor2019.Apps.MessageBrokerRabbitMQ.Root.Samples.Sample05
{
    /// <summary>
    /// Корень. Пример 05. Получение логов по теме.
    /// </summary>
    public class RootSample05ReceiveLogsTopic : CoreBaseSample
    {
        #region Properties

        /// <summary>
        /// Сообщение.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Маршруты.
        /// </summary>
        public List<string> Routes { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="name">Имя.</param>
        /// <param name="logger">Регистратор.</param>
        public RootSample05ReceiveLogsTopic(string name, ILogger logger)
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
                    channel.ExchangeDeclare(exchange: "topic_logs", type: "topic");

                    var queueName = channel.QueueDeclare().QueueName;

                    foreach (var route in Routes)
                    {
                        channel.QueueBind(
                            queue: queueName,
                            exchange: "topic_logs",
                            routingKey: route
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
