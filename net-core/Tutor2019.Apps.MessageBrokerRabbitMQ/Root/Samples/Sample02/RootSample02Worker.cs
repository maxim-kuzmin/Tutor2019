//Author Maxim Kuzmin//makc//

using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading;
using Tutor2019.Core.Base;

namespace Tutor2019.Apps.MessageBrokerRabbitMQ.Root.Samples.Sample02
{
    /// <summary>
    /// Корень. Пример 02. Работник.
    /// </summary>
    public class RootSample02Worker : CoreBaseSample
    {
        #region Constructors

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="name">Имя.</param>
        /// <param name="logger">Регистратор.</param>
        public RootSample02Worker(string name, ILogger logger)
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
                    channel.QueueDeclare(
                        queue: "task_queue",
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                        );

                    channel.BasicQos(0, 1, false);

                    Logger.LogInformation(" [*] Waiting for messages.");

                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;

                        var message = Encoding.UTF8.GetString(body);

                        Logger.LogInformation($" [x] Received {message}");

                        int dots = message.Split('.').Length - 1;

                        Thread.Sleep(dots * 1000);

                        Logger.LogInformation(" [x] Done");

                        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    };

                    channel.BasicConsume(queue: "task_queue", autoAck: false, consumer: consumer);

                    cancellationToken.WaitHandle.WaitOne();
                }
            }
        }

        #endregion Public methods
    }
}
