//Author Maxim Kuzmin//makc//

using Microsoft.Extensions.CommandLineUtils;
using Tutor2019.Apps.MessageBrokerKafka.Root.Samples.Sample01;
using Tutor2019.Core.Base;

namespace Tutor2019.Apps.MessageBrokerKafka.Root
{
    /// <summary>
    /// Корень. Модель.
    /// </summary>
    public class RootModel
    {
        #region Properties

        /// <summary>
        /// Действие.
        /// </summary>
        public CommandArgument Action { get; private set; }

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
        public RootSample01Producer RootSample01Producer { get; private set; }

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

            RootSample01Producer = sample as RootSample01Producer;

            bool isActionNeeded = Sample01Consumer != null;

            if (isActionNeeded)
            {
                Action = commandLine.Argument(
                    "[action]",
                    "The action which should be done: 1 - ReceiveByManualAssign, 2 - ReceiveBySubscription."
                    );
            }
        }

        #endregion Constructors
    }
}
