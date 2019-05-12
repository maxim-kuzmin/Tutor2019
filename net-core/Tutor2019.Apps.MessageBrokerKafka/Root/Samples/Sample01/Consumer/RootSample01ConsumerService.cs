//Author Maxim Kuzmin//makc//

using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Tutor2019.Apps.MessageBrokerKafka.Root.Samples.Sample01.Consumer
{
    /// <summary>
    /// Корень. Пример 01. Потребитель. Сервис.
    /// </summary>
    public class RootSample01ConsumerService
    {
        #region Properties

        private string BootstrapServers { get; set; }

        private string GroupId { get; set; }

        private ILogger Logger { get; set; }

        private IEnumerable<string> Topics { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="logger">Регистратор.</param>
        /// <param name="bootstrapServers">Серверы начальной загрузки.</param>
        /// <param name="groupId">Идентификатор группы.</param>
        /// <param name="topics">Темы.</param>
        public RootSample01ConsumerService(
            ILogger logger,
            string bootstrapServers,
            string groupId,
            IEnumerable<string> topics
            )
        {
            Logger = logger;
            BootstrapServers = bootstrapServers;
            GroupId = !string.IsNullOrEmpty(groupId) ? groupId : Guid.NewGuid().ToString();
            Topics = topics;
        }

        #endregion Constructors

        #region Public methods

        /// <summary>
        /// Получить данные из очереди по подписке.
        ///     In this example
        ///         - consumer group functionality (Subscribe) is used.
        ///         - offsets are manually committed.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены.</param>
        public void ReceiveDataFromQueueBySubscription(CancellationToken cancellationToken)
        {
            var config = CreateConfig();

            config.EnableAutoCommit = false;
            config.StatisticsIntervalMs = 5000;
            config.SessionTimeoutMs = 6000;
            config.AutoOffsetReset = AutoOffsetReset.Earliest;
            config.EnablePartitionEof = true;

            Task.Factory.StartNew(
                () => StartSubscription(cancellationToken, config),
                cancellationToken,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Default
                ).ConfigureAwait(false);
        }

        /// <summary>
        /// Получить данные из очереди, назначив разделы вручную.
        ///     In this example
        ///         - consumer group functionality (i.e. .Subscribe + offset commits) is not used.
        ///         - the consumer is manually assigned to a partition and always starts consumption
        ///           from a specific offset (0).
        /// </summary>
        /// <param name="cancellationToken">Токен отмены.</param>
        public void ReceiveDataFromQueueByManualAssign(CancellationToken cancellationToken)
        {
            var config = CreateConfig();

            // partition offsets can be committed to a group even by consumers not
            // subscribed to the group. in this example, auto commit is disabled
            // to prevent this from occuring.
            config.EnableAutoCommit = true;

            Task.Factory.StartNew(
                () => StartManualAssign(cancellationToken, config),
                cancellationToken,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Default
                ).ConfigureAwait(false);
        }

        #endregion Public methods

        #region Private methods

        private ConsumerConfig CreateConfig()
        {
            return new ConsumerConfig
            {
#if DEBUG
                Debug = "msg",
#endif
                // the group.id property must be specified when creating a consumer, even 
                // if you do not intend to use any consumer group functionality.
                GroupId = GroupId,
                BootstrapServers = BootstrapServers,
                // Note: The AutoOffsetReset property determines the start offset in the event
                // there are not yet any committed offsets for the consumer group for the
                // topic/partitions of interest. By default, offsets are committed
                // automatically, so in this example, consumption will only start from the
                // earliest message in the topic 'my-topic' the first time you run the program.
                AutoOffsetReset = AutoOffsetReset.Earliest
                //{ "api.version.request", true },
                //{ "group.id", !string.IsNullOrEmpty(groupId) ? groupId : Guid.NewGuid().ToString() },
                //{ "socket.blocking.max.ms", 1 },
                //{ "enable.auto.commit", false },
                //{ "fetch.wait.max.ms", 5 },
                //{ "fetch.error.backoff.ms", 5 },
                //{ "fetch.message.max.bytes", 10240 },
                //{ "queued.min.messages", 1000 },
            };
        }

        private IConsumer<Ignore, string> CreateConsumer(ConsumerConfig config)
        {
            return new ConsumerBuilder<Ignore, string>(config)
                .SetLogHandler(OnLog)
                .SetErrorHandler(OnError)
                .Build();
        }

        private void StartSubscription(CancellationToken cancellationToken, ConsumerConfig config)
        {
            using (var consumer = CreateConsumer(config))
            {
                const int commitPeriod = 5;

                consumer.Subscribe(Topics);

                try
                {
                    while (true)
                    {
                        try
                        {
                            var res = consumer.Consume(cancellationToken);

                            if (res.IsPartitionEOF)
                            {
                                Logger.LogInformation(
                                    $"Reached end of topic {res.Topic}, partition {res.Partition}, offset {res.Offset}."
                                    );

                                continue;
                            }

                            Logger.LogInformation($"Received message at {res.TopicPartitionOffset}: {res.Value}");

                            if (res.Offset % commitPeriod == 0)
                            {
                                // The Commit method sends a "commit offsets" request to the Kafka
                                // cluster and synchronously waits for the response. This is very
                                // slow compared to the rate at which the consumer is capable of
                                // consuming messages. A high performance application will typically
                                // commit offsets relatively infrequently and be designed handle
                                // duplicate messages in the event of failure.
                                try
                                {
                                    consumer.Commit(res);
                                }
                                catch (KafkaException e)
                                {
                                    Logger.LogInformation($"Commit error: {e.Error.Reason}");
                                }
                            }
                        }
                        catch (ConsumeException e)
                        {
                            Logger.LogInformation($"Consume error: {e.Error.Reason}");
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    Logger.LogInformation("Closing consumer.");

                    consumer.Close();
                }
            }
        }

        private void StartManualAssign(CancellationToken cancellationToken, ConsumerConfig config)
        {
            using (var consumer = CreateConsumer(config))
            {
                var partitions = Topics.Select(x => new TopicPartitionOffset(x, 0, Offset.Beginning)).ToList();

                consumer.Assign(partitions);

                try
                {
                    while (true)
                    {
                        try
                        {
                            var consumeResult = consumer.Consume(cancellationToken);
                            // Note: End of partition notification has not been enabled, so
                            // it is guaranteed that the ConsumeResult instance corresponds
                            // to a Message, and not a PartitionEOF event.
                            Console.WriteLine($"Received message at {consumeResult.TopicPartitionOffset}: ${consumeResult.Value}");
                        }
                        catch (ConsumeException e)
                        {
                            Console.WriteLine($"Consume error: {e.Error.Reason}");
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    Logger.LogInformation("Closing consumer.");

                    consumer.Close();
                }
            }
        }

        private void OnLog(object sender, LogMessage log)
        {
            Logger.LogInformation(
                $"Consuming from Kafka. Client: '{log.Name}', syslog level: '{log.Level}', message: '{log.Message}'."
                );
        }

        private void OnError(object sender, Error error)
        {
            Logger.LogInformation($"Consumer error: {error}. No action required.");
        }

        #endregion Private methods
    }
}
