//Author Maxim Kuzmin//makc//

using Tutor2019.Apps.MessageBrokerKafka.Root;

namespace Tutor2019.Apps.MessageBrokerKafka
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
