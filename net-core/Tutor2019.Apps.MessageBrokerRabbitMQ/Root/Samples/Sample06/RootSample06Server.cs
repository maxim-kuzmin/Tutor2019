//Author Maxim Kuzmin//makc//

using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using Tutor2019.Core.Base;

namespace Tutor2019.Apps.MessageBrokerRabbitMQ.Root.Samples.Sample06
{
    /// <summary>
    /// Корень. Пример 06. Сервер.
    /// </summary>
    public class RootSample06Server : CoreBaseSample
    {
        #region Constructors

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="name">Имя.</param>
        /// <param name="logger">Регистратор.</param>
        public RootSample06Server(string name, ILogger logger)
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
                        queue: "rpc_queue",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                        );

                    channel.BasicQos(0, 1, false);

                    Logger.LogInformation(" [x] Awaiting RPC requests");

                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (model, ea) =>
                    {
                        string response = null;

                        var body = ea.Body;

                        var props = ea.BasicProperties;

                        var replyProps = channel.CreateBasicProperties();

                        replyProps.CorrelationId = props.CorrelationId;

                        try
                        {
                            var message = Encoding.UTF8.GetString(body);

                            int n = int.Parse(message);

                            Logger.LogInformation($" [.] fib({message})");

                            response = Fib(n).ToString();
                        }
                        catch (Exception e)
                        {
                            Logger.LogInformation($" [.] {e.Message}");

                            response = "";
                        }
                        finally
                        {
                            var responseBytes = Encoding.UTF8.GetBytes(response);

                            channel.BasicPublish(
                                exchange: "",
                                routingKey: props.ReplyTo,
                                basicProperties: replyProps,
                                body: responseBytes
                                );

                            channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                        }
                    };

                    channel.BasicConsume(queue: "rpc_queue", autoAck: false, consumer: consumer);

                    cancellationToken.WaitHandle.WaitOne();
                }
            }
        }

        #endregion Public methods

        #region Private methods

        private int Fib(int n)
        {
            if (n == 0 || n == 1)
            {
                return n;
            }

            return Fib(n - 1) + Fib(n - 2);
        }

        #endregion Private methods
    }
}
