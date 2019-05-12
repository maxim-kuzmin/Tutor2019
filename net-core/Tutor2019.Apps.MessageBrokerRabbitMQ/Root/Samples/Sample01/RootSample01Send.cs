//Author Maxim Kuzmin//makc//

using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Text;
using System.Threading;
using Tutor2019.Core.Base;

namespace Tutor2019.Apps.MessageBrokerRabbitMQ.Root.Samples.Sample01
{
    /// <summary>
    /// Корень. Пример 01. Отправка.
    /// </summary>
    public class RootSample01Send : CoreBaseSample
    {
        #region Constructors

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="name">Имя.</param>
        /// <param name="logger">Регистратор.</param>
        public RootSample01Send(string name, ILogger logger)
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
                        queue: "hello",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                        );

                    string message = "Hello World!";

                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: "hello",
                        basicProperties: null,
                        body: body
                        );

                    Logger.LogInformation($" [x] Sent {message}");
                }
            }

            cancellationToken.WaitHandle.WaitOne();
        }

        #endregion Public methods
    }
}
