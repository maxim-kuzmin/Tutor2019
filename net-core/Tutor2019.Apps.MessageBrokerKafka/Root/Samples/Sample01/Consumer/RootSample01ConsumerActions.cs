//Author Maxim Kuzmin//makc//

namespace Tutor2019.Apps.MessageBrokerKafka.Root.Samples.Sample01.Consumer.Enums
{
    /// <summary>
    /// Корень. Пример 01. Потребитель. Действия.
    /// </summary>
    public enum RootSample01ConsumerActions
    {
        /// <summary>
        /// Получить путём назначения разделов вручную.
        /// </summary>
        ReceiveByManualAssign = 1,
        /// <summary>
        /// Получить по подписке.
        /// </summary>
        ReceiveBySubscription = 2    
    }
}
