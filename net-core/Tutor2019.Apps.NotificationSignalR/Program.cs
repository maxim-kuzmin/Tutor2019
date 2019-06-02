//Author Maxim Kuzmin//makc//

using Tutor2019.Apps.NotificationSignalR.Root;

namespace Tutor2019.Apps.NotificationSignalR
{
    /// <summary>
    /// Программа.
    /// </summary>
    public class Program
    {
        #region Public methods

        /// <summary>
        /// Точка входа.
        /// </summary>
        /// <param name="args">Аргументы.</param>
        public static void Main(string[] args)
        {
            RootApp.Instance.Run(args);
        }

        #endregion Public methods
    }
}
