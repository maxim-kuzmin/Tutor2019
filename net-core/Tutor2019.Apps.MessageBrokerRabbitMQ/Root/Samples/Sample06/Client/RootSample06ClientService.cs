//Author Maxim Kuzmin//makc//

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Concurrent;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tutor2019.Apps.MessageBrokerRabbitMQ.Root.Samples.Sample06.Client
{
    /// <summary>
    /// Корень. Пример 06. Клиент. Сервис.
    /// </summary>
    public class RootSample06ClientService
    {
        #region Properties

        private IConnection Сonnection { get; set; }

        private IModel Channel { get; set; }

        private string ReplyQueueName { get; set; }

        private EventingBasicConsumer Consumer { get; set; }

        private ConcurrentDictionary<string, TaskCompletionSource<string>> CallbackMapper { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Конструктор.
        /// </summary>
        public RootSample06ClientService()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            CallbackMapper = new ConcurrentDictionary<string, TaskCompletionSource<string>>();

            Сonnection = factory.CreateConnection();

            Channel = Сonnection.CreateModel();

            ReplyQueueName = Channel.QueueDeclare().QueueName;

            Consumer = new EventingBasicConsumer(Channel);

            Consumer.Received += (model, ea) =>
            {
                if (!CallbackMapper.TryRemove(ea.BasicProperties.CorrelationId, out TaskCompletionSource<string> tcs))
                {
                    return;
                }

                var body = ea.Body;

                var response = Encoding.UTF8.GetString(body);

                tcs.TrySetResult(response);
            };
        }

        #endregion Constructors

        #region Public methods

        /// <summary>
        /// Запустить.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Задача на получение ответного сообщения.</returns>
        public Task<string> Run(string message, CancellationToken cancellationToken = default)
        {
            IBasicProperties props = Channel.CreateBasicProperties();

            var correlationId = Guid.NewGuid().ToString();

            props.CorrelationId = correlationId;
            props.ReplyTo = ReplyQueueName;

            var messageBytes = Encoding.UTF8.GetBytes(message);

            var tcs = new TaskCompletionSource<string>();

            CallbackMapper.TryAdd(correlationId, tcs);

            Channel.BasicPublish(
                exchange: "",
                routingKey: "rpc_queue",
                basicProperties: props,
                body: messageBytes
                );

            Channel.BasicConsume(
                consumer: Consumer,
                queue: ReplyQueueName,
                autoAck: true
                );

            cancellationToken.Register(() => CallbackMapper.TryRemove(correlationId, out var tmp));

            return tcs.Task;
        }

        /// <summary>
        /// Закрыть.
        /// </summary>
        public void Close()
        {
            Сonnection.Close();
        }

        #endregion Public methods
    }
}
