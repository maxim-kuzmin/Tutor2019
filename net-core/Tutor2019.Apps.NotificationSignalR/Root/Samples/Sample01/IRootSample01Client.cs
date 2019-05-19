//Author Maxim Kuzmin//makc//

using System.Threading.Tasks;
using Tutor2019.Apps.NotificationSignalR.Root.Samples.Sample01.Client.Jobs.ReceiveMessage;

namespace Tutor2019.Apps.NotificationSignalR.Root.Samples.Sample01
{
    /// <summary>
    /// Корень. Пример 01. Клиент.
    /// </summary>
    public interface IRootSample01Client
    {
        /// <summary>
        /// Получить сообщение.
        /// </summary>
        /// <param name="input">Ввод.</param>
        /// <returns>Задача.</returns>
        Task ReceiveMessage(RootSample01ClientJobReceiveMessageInput input);
    }
}
