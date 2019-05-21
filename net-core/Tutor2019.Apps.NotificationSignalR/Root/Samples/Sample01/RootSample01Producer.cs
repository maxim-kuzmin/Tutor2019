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

        private HubConnection Connection { get; set; }

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
            Connection = new HubConnectionBuilder()
                .WithUrl(Startup.URL_Sample01)
                .Build();

            Connection.Closed += ex => OnConnectionClosed(ex, cancellationToken);

            StartConnection(cancellationToken);

            while (!cancellationToken.IsCancellationRequested)
            {
                var methodName = nameof(RootSample01ServerHub.SendMessage);

                if (Connection.State == HubConnectionState.Connected)
                {
                    try
                    {
                        var input = new RootSample01ServerJobSendMessageInput
                        {
                            Message = Guid.NewGuid().ToString("N")
                        };

                        Connection.InvokeAsync(methodName, input, cancellationToken);

                        Logger.LogInformation($"Message '{input.Message}' produced");
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError(ex, "SendMessage is failed");
                    }
                }

                Task.Delay(1000).Wait();
            }
        }

        #endregion Public methods

        #region Private methods

        private void StartConnection(CancellationToken cancellationToken)
        {
            Logger.LogInformation("Connection is starting");

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    Connection.StartAsync(cancellationToken).Wait();

                    Logger.LogInformation("Connection is started");

                    break;
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex, "Connection start is failed");
                }

                Task.Delay(1000).Wait();
            }
        }

        private async Task OnConnectionClosed(Exception error, CancellationToken cancellationToken)
        {
            Logger.LogError(error, "Connection is closed");

            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);

                Logger.LogInformation("Connection is restarting");

                try
                {
                    await Connection.StartAsync(cancellationToken);

                    Logger.LogInformation("Connection is restarted");

                    break;
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex, "Connection restart is failed");
                }
            }
        }

        #endregion Private methods
    }
}