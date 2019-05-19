//Author Maxim Kuzmin//makc//

using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Tutor2019.Apps.NotificationSignalR.Root.Samples.Sample01.Client.Jobs.ReceiveMessage;
using Tutor2019.Core.Base;

namespace Tutor2019.Apps.NotificationSignalR.Root.Samples.Sample01
{
    /// <summary>
    /// Корень. Пример 01. Потребитель.
    /// </summary>
    public class RootSample01Consumer : CoreBaseSample
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
        public RootSample01Consumer(string name, ILogger logger)
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

            Connection.Closed += OnConnectionClosed;

            var methodName = nameof(IRootSample01Client.ReceiveMessage);

            Connection.On<RootSample01ClientJobReceiveMessageInput>(methodName, OnReceiveMessage);

            try
            {
                Connection.StartAsync().Wait();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Connection start is failed");
            }
        }

        #endregion Public methods

        #region Private methods

        private async Task OnConnectionClosed(Exception ex)
        {
            await Task.Delay(new Random().Next(0, 5) * 1000);

            await Connection.StartAsync();
        }

        private void OnReceiveMessage(RootSample01ClientJobReceiveMessageInput input)
        {
            Logger.LogInformation($"Message '{input.Message}' consumed");
        }

        #endregion Private methods
    }
}