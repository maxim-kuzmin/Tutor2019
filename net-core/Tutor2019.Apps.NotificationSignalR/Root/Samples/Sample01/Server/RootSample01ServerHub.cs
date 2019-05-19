//Author Maxim Kuzmin//makc//

using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Tutor2019.Apps.NotificationSignalR.Root.Samples.Sample01.Client.Jobs.ReceiveMessage;
using Tutor2019.Apps.NotificationSignalR.Root.Samples.Sample01.Server.Jobs.SendMessage;

namespace Tutor2019.Apps.NotificationSignalR.Root.Samples.Sample01.Server
{
    /// <summary>
    /// Корень. Пример 01. Сервер. Хаб.
    /// </summary>
    public class RootSample01ServerHub : Hub<IRootSample01Client>
    {
        /// <summary>
        /// Отправить сообщение.
        /// </summary>
        /// <param name="input">Ввод.</param>
        /// <returns>Задача.</returns>
        public async Task SendMessage(RootSample01ServerJobSendMessageInput input)
        {
            var clientInput = new RootSample01ClientJobReceiveMessageInput
            {
                Message = input.Message
            };

            await Clients.All.ReceiveMessage(clientInput);
        }
    }
}
