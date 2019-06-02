//Author Maxim Kuzmin//makc//

using Microsoft.Extensions.CommandLineUtils;
using Tutor2019.Apps.MessageBrokerRabbitMQ.Root.Samples.Sample02;
using Tutor2019.Apps.MessageBrokerRabbitMQ.Root.Samples.Sample03;
using Tutor2019.Apps.MessageBrokerRabbitMQ.Root.Samples.Sample04;
using Tutor2019.Apps.MessageBrokerRabbitMQ.Root.Samples.Sample05;
using Tutor2019.Apps.MessageBrokerRabbitMQ.Root.Samples.Sample06;
using Tutor2019.Core.Base;

namespace Tutor2019.Apps.MessageBrokerRabbitMQ.Root
{
    /// <summary>
    /// Корень. Контекст.
    /// </summary>
    public class RootContext
    {
        #region Properties

        /// <summary>
        /// Сообщение.
        /// </summary>
        public CommandArgument Message { get; private set; }

        /// <summary>
        /// Пример.
        /// </summary>
        public CoreBaseSample Sample { get; private set; }

        /// <summary>
        /// Пример 02. Новая задача.
        /// </summary>
        public RootSample02NewTask Sample02NewTask { get; private set; }

        /// <summary>
        /// Пример 03. Испускание лога веерно (по всем направлениям).
        /// </summary>
        public RootSample03EmitLogFanout Sample03EmitLogFanout { get; private set; }

        /// <summary>
        /// Пример 04. Испускание лога по конкретному направлению.
        /// </summary>
        public RootSample04EmitLogDirect Sample04EmitLogDirect { get; private set; }

        /// <summary>
        /// Пример 04. Получение логов по конкретному направлению.
        /// </summary>
        public RootSample04ReceiveLogsDirect Sample04ReceiveLogsDirect { get; private set; }

        /// <summary>
        /// Пример 05. Испускание лога по теме.
        /// </summary>
        public RootSample05EmitLogTopic Sample05EmitLogTopic { get; private set; }

        /// <summary>
        /// Пример 05. Получение логов по теме.
        /// </summary>
        public RootSample05ReceiveLogsTopic Sample05ReceiveLogsTopic { get; private set; }

        /// <summary>
        /// Пример 06. Клиент.
        /// </summary>
        public RootSample06Client Sample06Client { get; private set; }

        /// <summary>
        /// Маршрут.
        /// </summary>
        public CommandOption Route { get; private set; }

        /// <summary>
        /// Маршруты.
        /// </summary>
        public CommandOption Routes { get; private set; }

        /// <summary>
        /// Уровень серьёзности.
        /// </summary>
        public CommandOption Severity { get; private set; }

        /// <summary>
        /// Уровни серьёзности.
        /// </summary>
        public CommandOption Severities { get; private set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="sample">Пример.</param>   
        /// <param name="commandLine">Командная строка.</param>
        public RootContext(CoreBaseSample sample, CommandLineApplication commandLine)
        {
            Sample = sample;

            Sample02NewTask = sample as RootSample02NewTask;
            Sample03EmitLogFanout = sample as RootSample03EmitLogFanout;
            Sample04EmitLogDirect = sample as RootSample04EmitLogDirect;
            Sample04ReceiveLogsDirect = sample as RootSample04ReceiveLogsDirect;
            Sample05EmitLogTopic = sample as RootSample05EmitLogTopic;
            Sample05ReceiveLogsTopic = sample as RootSample05ReceiveLogsTopic;
            Sample06Client = sample as RootSample06Client;

            bool isMessageNeeded = Sample02NewTask != null 
                || Sample03EmitLogFanout != null
                || Sample04EmitLogDirect != null
                || Sample05EmitLogTopic != null
                || Sample06Client != null;

            if (isMessageNeeded)
            {
                Message = commandLine.Argument(
                    "[message]",
                    "The message which should be sent."
                    );
            }

            bool isSeverityNeeded = Sample04EmitLogDirect != null;

            if (isSeverityNeeded)
            {
                Severity = commandLine.Option(
                    "-s|--severity <severity>",
                    "The severity of the message: 1 - info, 2 - warn, 3 - error",
                    CommandOptionType.SingleValue
                    );
            }

            bool isSeveritiesNeeded = Sample04ReceiveLogsDirect != null;

            if (isSeveritiesNeeded)
            {
                Severities = commandLine.Option(
                    "-ss|--severities <severities>",
                    "Severities of the messages: 1 - info, 2 - warn, 3 - error",
                    CommandOptionType.MultipleValue
                    );
            }

            bool isRouteNeeded = Sample05EmitLogTopic != null;

            if (isRouteNeeded)
            {
                Route = commandLine.Option(
                    "-r|--route <severity>",
                    "The route of the message",
                    CommandOptionType.SingleValue
                    );
            }

            bool isRoutesNeeded = Sample05ReceiveLogsTopic != null;

            if (isRoutesNeeded)
            {
                Routes = commandLine.Option(
                    "-rr|--routes <routes>",
                    "Routes of the messages",
                    CommandOptionType.MultipleValue
                    );
            }
        }

        #endregion Constructors
    }
}
