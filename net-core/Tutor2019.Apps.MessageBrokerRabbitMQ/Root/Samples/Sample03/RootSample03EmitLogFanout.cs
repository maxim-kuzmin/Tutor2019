//Author Maxim Kuzmin//makc//

using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Text;
using System.Threading;
using Tutor2019.Core.Base;

namespace Tutor2019.Apps.MessageBrokerRabbitMQ.Root.Samples.Sample03
{
    /// <summary>
    /// Корень. Пример 03. Испускание лога веерно (по всем направлениям).
    /// </summary>
    public class RootSample03EmitLogFanout : CoreBaseSample
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
        public RootSample03EmitLogFanout(string name, ILogger logger)
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
                    channel.ExchangeDeclare(exchange: "logs", type: "fanout");

                    var message = Message ?? "info: Hello World!";

                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(
                        exchange: "logs",
                        routingKey: "",
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
