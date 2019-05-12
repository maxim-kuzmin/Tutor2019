//Author Maxim Kuzmin//makc//

using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Text;
using System.Threading;
using Tutor2019.Core.Base;

namespace Tutor2019.Apps.MessageBrokerRabbitMQ.Root.Samples.Sample02
{
    /// <summary>
    /// Корень. Пример 02. Новая задача.
    /// </summary>
    public class RootSample02NewTask : CoreBaseSample
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
        public RootSample02NewTask(string name, ILogger logger)
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

                    var message = Message ?? "Hello World!";

                    var body = Encoding.UTF8.GetBytes(message);

                    var properties = channel.CreateBasicProperties();

                    properties.Persistent = true;

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: "task_queue",
                        basicProperties: properties,
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
