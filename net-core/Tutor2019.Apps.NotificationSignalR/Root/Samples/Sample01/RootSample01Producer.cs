//Author Maxim Kuzmin//makc//

using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Tutor2019.Apps.NotificationSignalR.Root.Samples.Sample01.Server;
using Tutor2019.Apps.NotificationSignalR.Root.Samples.Sample01.Server.Jobs.SendMessage;
using Tutor2019.Core.Base;

namespace Tutor2019.Apps.NotificationSignalR.Root.Samples.Sample01
{
    /// <summary>
    /// Корень. Пример 01. Поставщик.
    /// </summary>
    public class RootSample01Producer : CoreBaseSample
    {
        #region Properties

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="name">Имя.</param>
        /// <param name="logger">Регистратор.</param>
        /// <param name="bootstrapServers">Серверы начальной загрузки.</param>
        /// <param name="topic">Тема.</param>
        public RootSample01Producer(string name, ILogger logger)
            : base(name, logger)
        {
        }

        #endregion Constructors

        #region Public methods

        /// <inheritdoc/>
        public sealed override void Run(CancellationToken cancellationToken)
        {
            var connection = new HubConnectionBuilder()
                .WithUrl(Startup.URL_Sample01)
                .Build();

            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };

            try
            {
                connection.StartAsync().Wait();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Connection start is failed");
            }

            while (!cancellationToken.IsCancellationRequested)
            {
                var methodName = nameof(RootSample01ServerHub.SendMessage);

                var input = new RootSample01ServerJobSendMessageInput
                {
                    Message = Guid.NewGuid().ToString("N")
                };

                try
                {
                    connection.InvokeAsync(methodName, input);
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex, "SendMessage is failed");
                }

                Logger.LogInformation($"Message '{input.Message}' produced");

                Task.Delay(1000).Wait();
            }
        }

        #endregion Public methods
    }
}