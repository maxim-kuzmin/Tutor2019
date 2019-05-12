//Author Maxim Kuzmin//makc//

namespace Tutor2019.Apps.MessageBrokerRabbitMQ.Root.Samples.Sample04.EmitLogDirect
{
    /// <summary>
    /// Корень. Пример 04. Испускание лога по конкретному направлению. Уровни серьёзности.
    /// </summary>
    public enum RootSample04EmitLogDirectSeverities
    {
        /// <summary>
        /// Информация.
        /// </summary>
        Info = 1,
        /// <summary>
        /// Предупреждение.
        /// </summary>
        Warn = 2,
        /// <summary>
        /// Ошибка.
        /// </summary>
        Error = 3
    }
}
