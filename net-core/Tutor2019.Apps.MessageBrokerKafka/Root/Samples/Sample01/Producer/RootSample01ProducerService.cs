//Author Maxim Kuzmin//makc//

using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Tutor2019.Apps.MessageBrokerKafka.Root.Samples.Sample01.Producer
{
    /// <summary>
    /// Корень. Пример 01. Поставщик. Сервис.
    /// </summary>
    public class RootSample01ProducerService : IDisposable
    {
        #region Properties

        private ILogger Logger { get; set; }

        private IProducer<Null, string> Producer { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="logger">Регистратор.</param>
        /// <param name="bootstrapServers">Серверы начальной загрузки.</param>
        public RootSample01ProducerService(ILogger logger, string bootstrapServers)
        {
            Logger = logger;

            ProducerConfig config = new ProducerConfig
            {
#if DEBUG
                Debug = "msg",
#endif
                Acks = Acks.All,
                ApiVersionRequest = true,
                BootstrapServers = bootstrapServers,
                MessageTimeoutMs = 3000,
                QueueBufferingMaxKbytes = 1048576
                //{ "socket.blocking.max.ms", 1 },
                //{ "queue.buffering.max.ms", 5 },
            };


            Producer = new ProducerBuilder<Null, string>(config)
                .SetLogHandler(OnLog)
                .SetErrorHandler(OnError)
                .Build();
        }

        #endregion Constructors

        #region Public methods

        /// <summary>
        /// Отправить данные в очередь.
        /// </summary>
        /// <param name="data">Данные.</param>
        /// <param name="topic">Тема.</param>
        /// <param name="partition">Раздел.</param>
        /// <returns>Результат доставки.</returns>
        public async Task<DeliveryResult<Null, string>> SendDataToQueue(
            string data,
            string topic,
            int partition = -1
            )
        {
            DeliveryResult<Null, string> result;

            var message = new Message<Null, string>
            {
                Value = data
            };

            try
            {
                if (partition < 0)
                {
                    result = await Producer.ProduceAsync(topic, message);
                }
                else
                {
                    var topicPartition = new TopicPartition(topic, new Partition(partition));

                    result = await Producer.ProduceAsync(topicPartition, message);
                }
            }
            catch (Exception ex)
            {
                var msg = new StringBuilder()
                    .Append($"Error producing to Kafka. Topic/partition: '{topic}/{partition}'. Message: '")
                    .Append(message?.Value ?? "N/A")
                    .Append("'.");

                if (ex is ProduceException<Null, string> produceException)
                {
                    msg.Append($" Reason: '{produceException.Error.Reason}'.");
                }

                Logger.LogError(new EventId(), ex, msg.ToString());

                throw;
            }

            return result;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            if (Producer != null)
            {
                Producer.Dispose();
            }
        }

        #endregion Public methods

        #region Private methods

        private void OnLog(object sender, LogMessage log)
        {
            Logger.LogInformation(
                $"Producing to Kafka. Client: {log.Name}, syslog level: '{log.Level}', message: {log.Message}."
                );
        }

        private void OnError(object sender, Error error)
        {
            Logger.LogInformation($"Producer error: {error}. No action required.");
        }

        #endregion Private methods
    }
}