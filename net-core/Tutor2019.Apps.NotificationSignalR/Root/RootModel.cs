//Author Maxim Kuzmin//makc//

using Microsoft.Extensions.CommandLineUtils;
using Tutor2019.Apps.NotificationSignalR.Root.Samples.Sample01;
using Tutor2019.Core.Base;

namespace Tutor2019.Apps.NotificationSignalR.Root
{
    /// <summary>
    /// Корень. Модель.
    /// </summary>
    public class RootModel
    {
        #region Properties

        /// <summary>
        /// Пример.
        /// </summary>
        public CoreBaseSample Sample { get; private set; }

        /// <summary>
        /// Пример 01. Потребитель.
        /// </summary>
        public RootSample01Consumer Sample01Consumer { get; private set; }

        /// <summary>
        /// Пример 01. Поставщик.
        /// </summary>
        public RootSample01Producer Sample01Producer { get; private set; }

        /// <summary>
        /// Пример 01. Сервер.
        /// </summary>
        public RootSample01Server Sample01Server { get; private set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="sample">Пример.</param>   
        /// <param name="commandLine">Командная строка.</param>
        public RootModel(CoreBaseSample sample, CommandLineApplication commandLine)
        {
            Sample = sample;

            Sample01Consumer = sample as RootSample01Consumer;

            Sample01Producer = sample as RootSample01Producer;

            Sample01Server = sample as RootSample01Server;
        }

        #endregion Constructors
    }
}
