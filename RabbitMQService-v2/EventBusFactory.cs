using RabbitMQService_v2.EventBusBase;
using RabbitMQService_v2.EventBusBase.Abstraction;

namespace RabbitMQService_v2
{
    public static class EventBusFactory
    {
        public static IEventBus Create(EventBusConfig config, IServiceProvider serviceProvider)
        {
            return new EventBusRabbitMQ(config, serviceProvider);
        }
    }
}
