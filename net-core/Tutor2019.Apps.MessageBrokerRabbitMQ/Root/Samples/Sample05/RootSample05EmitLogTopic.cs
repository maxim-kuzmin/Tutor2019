//Author Maxim Kuzmin//makc//

using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Text;
using System.Threading;
using Tutor2019.Core.Base;

namespace Tutor2019.Apps.MessageBrokerRabbitMQ.Root.Samples.Sample05
{
    /// <summary>
    /// Корень. Пример 05. Испускание лога по теме.
    /// </summary>
    public class RootSample05EmitLogTopic : CoreBaseSample
    {
        #region Properties

        /// <summary>
        /// Сообщение.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Маршрут.
        /// </summary>
        public string Route { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="name">Имя.</param>
        /// <param name="logger">Регистратор.</param>
        public RootSample05EmitLogTopic(string name, ILogger logger)
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

                    var message = Message ?? "Hello World!";
                    var route = Route ?? "anonymous.info";

                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(
                        exchange: "topic_logs",
                        routingKey: route,
                        basicProperties: null,
                        body: body
                        );

                    Logger.LogInformation($" [x] Sent {route}: {message}");
                }
            }

            cancellationToken.WaitHandle.WaitOne();
        }

        #endregion Public methods
    }
}
