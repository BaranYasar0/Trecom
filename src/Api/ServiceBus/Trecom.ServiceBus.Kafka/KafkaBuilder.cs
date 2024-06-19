using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trecom.ServiceBus.Domain;
using Trecom.ServiceBus.Kafka.Configuration;
using Trecom.ServiceBus.Kafka.CustomSerializers;

namespace Trecom.ServiceBus.Kafka
{
    public class KafkaBuilder<TKey,TValue>(IOptions<ServiceBusConfig> options,IConfiguration configuration) where TValue : IntegrationEvent
    {
        private ServiceBusConfig serviceBusConfig = options.Value ?? throw new ArgumentNullException("ServiceBusConfig cannot be null");

        public IAdminClient BuildAdminClient()
        {
            AdminClientConfig adminClientConfig = GetAdminClientConfig();

            ArgumentNullException.ThrowIfNull(adminClientConfig.BootstrapServers);

            return new AdminClientBuilder(adminClientConfig).Build();
        }

        public IConsumer<TKey, TValue> BuildKafkaConsumer(string groupId = null)
        {
            ConsumerConfig consumerConfig = GetConsumerConfig(groupId);

            ArgumentNullException.ThrowIfNull(consumerConfig.BootstrapServers);

            return new ConsumerBuilder<TKey, TValue>(consumerConfig)
                .SetKeyDeserializer(new CustomKafkaDeserializer<TKey>())
                .SetValueDeserializer(new CustomKafkaDeserializer<TValue>())
                .Build();
        }
        public IProducer<TKey, TValue> BuildKafkaProducer()
        {
            ProducerConfig producerConfig = GetProducerConfig();

            ArgumentNullException.ThrowIfNull(producerConfig.BootstrapServers);

            return new ProducerBuilder<TKey, TValue>(producerConfig)
                .SetKeySerializer(new CustomKafkaSerializer<TKey>())
                .SetValueSerializer(new CustomKafkaSerializer<TValue>())
                .Build();
        }

        #region Private Methods
        private AdminClientConfig GetAdminClientConfig()
        {
            return new AdminClientConfig()
            {
                BootstrapServers = serviceBusConfig.ServiceBusConnectionString,
                SaslMechanism = SaslMechanism.ScramSha512,
                SecurityProtocol = SecurityProtocol.SaslSsl,
                //SslCaLocation = serviceBusConfig.CertificatePath,
                //SaslPassword = serviceBusConfig.Password,
                //SaslUsername = serviceBusConfig.UserName,
            };
        }
        private ProducerConfig GetProducerConfig()
        {
            return new ProducerConfig()
            {
                BootstrapServers = serviceBusConfig.ServiceBusConnectionString,
                SaslMechanism = SaslMechanism.ScramSha512,
                SecurityProtocol = SecurityProtocol.SaslSsl,
                //SslCaLocation = serviceBusConfig.CertificatePath,
                //SaslPassword = serviceBusConfig.Password,
                //SaslUsername = serviceBusConfig.UserName,
                Acks = Acks.All,
                AllowAutoCreateTopics = true,
                MessageTimeoutMs = 6000,
                //MessageSendMaxRetries =6,
                //RetryBackoffMaxMs = 2000,
                //RetryBackoffMs = 2000
            };
        }
        private ConsumerConfig GetConsumerConfig(string groupId)
        {
            return new ConsumerConfig()
            {
                BootstrapServers = serviceBusConfig.ServiceBusConnectionString,
                SaslMechanism = SaslMechanism.ScramSha512,
                SecurityProtocol = SecurityProtocol.SaslSsl,
                //SslCaLocation = _serverConfiguration.CertificatePath,
                //SaslPassword = _serverConfiguration.Password,
                //SaslUsername = _serverConfiguration.UserName,
                EnableAutoCommit = false,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                GroupId = string.IsNullOrEmpty(groupId)
                        ? configuration["GroupId"]
                        : groupId,
            };
        }
        #endregion



    }
}
