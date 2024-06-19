using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trecom.ServiceBus.Kafka.Configuration
{
    public class KafkaConfiguration
    {
        public string TopicName { get; init; }
        public int MaxRetryCount { get; init; }
        public const int DefaultMaxRetryCount = 3;
    }
}
