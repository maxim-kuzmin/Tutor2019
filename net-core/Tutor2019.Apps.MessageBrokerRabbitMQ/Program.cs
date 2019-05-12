//Author Maxim Kuzmin//makc//

using Tutor2019.Apps.MessageBrokerRabbitMQ.Root;

namespace Tutor2019.Apps.MessageBrokerRabbitMQ
{
    /// <summary>
    /// Программа.
    /// </summary>
    class Program
    {
        #region Private methods

        private static void Main(string[] args)
        {
            RootApp.Instance.Run(args);
        }

        #endregion Private methods
    }
}
