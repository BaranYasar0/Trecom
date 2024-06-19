namespace Trecom.ServiceBus.Domain
{
    public class ServiceBusConfig
    {
        public int ConnectionRetryCount { get; set; } = 5;

        public string DefaultTopicName { get; set; } = "TrecomDefaultServiceBus";

        public string ServiceBusConnectionString { get; set; } = String.Empty;

        public string SubscriberClientAppName { get; set; } = String.Empty;

        public string EventNamePrefix { get; set; } = String.Empty;

        public string EventNameSuffix { get; set; } = "IntegrationEvent";

        public object Connection { get; set; }

        public bool DeleteEventPrefix => !String.IsNullOrEmpty(EventNamePrefix);
        public bool DeleteEventSuffix => !String.IsNullOrEmpty(EventNameSuffix);


    }
}
