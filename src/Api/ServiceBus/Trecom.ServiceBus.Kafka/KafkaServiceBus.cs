using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trecom.ServiceBus.BusinessAction.Abstraction;
using Trecom.ServiceBus.Domain;
using static Confluent.Kafka.ConfigPropertyNames;

namespace Trecom.ServiceBus.Kafka;

public class KafkaServiceBus : BaseServiceBus
{
    public ServiceBusConfig config;
    private readonly ILogger<KafkaServiceBus> logger;
    private readonly IConfiguration configuration;
    private readonly IOptions<ServiceBusConfig> options;
    private bool initialized = false;
    private bool disposed = false;
    public KafkaServiceBus(ServiceBusConfig config, IEventManager eventManager, IServiceProvider serviceProvider, ILogger<KafkaServiceBus> logger, IConfiguration configuration, IOptions<ServiceBusConfig> options) : base(config, eventManager, serviceProvider)
    {
        this.logger = logger;
        this.configuration = configuration;
        this.options = options;
    }

    public override void CallEsbService<T>(T message)
    {
        //Do nothing
    }

    public override void Publish<TEvent>(string key, TEvent @event)
    {
        var eventName = @event.GetType().Name;
        var message = GetMessage(key, @event);
        var builder = new KafkaBuilder<string, TEvent>(options, configuration);
        using IProducer<string, TEvent> producer = builder.BuildKafkaProducer();
        try
        {
            producer.ProduceAsync(eventName, message).GetAwaiter().GetResult();
            logger.LogInformation($"Event {eventName} published successfully");
        }
        catch (Exception ex)
        {
            producer.Flush(TimeSpan.FromSeconds(5));
            producer.Dispose();
            logger.LogError(ex, $"Error while publishing event {@event.GetType().Name}");
        }
    }

    public override Task Subscribe<T, TH>(CancellationToken cancellationToken = default)
    {
        string eventName = typeof(T).Name;
        using IConsumer<string, T> consumer = InitializeConsumer<T>();
        eventManager.AddSubscription<T, TH>();
        consumer.Subscribe(eventName);
        Task.Factory.StartNew(async () =>
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    ConsumeResult<string, T> result = null!;
                    try
                    {
                        result = consumer.Consume(cancellationToken);
                    }
                    catch (ConsumeException e)
                    {
                        consumer.StoreOffset(result);
                        logger.LogError(e, $"Error while consuming message {result.Message.Value}");
                        continue;
                    }
                    if (result.Message == null)
                    {
                        consumer.StoreOffset(result);
                        logger.LogError($"Message is null while consuming message {result.Message.Value}");
                        continue;
                    }
                    try
                    {
                        await ProcessEvent<T>(result.Message.Value);
                        consumer.Commit();
                    }
                    catch (Exception ex)
                    {
                        consumer.StoreOffset(result);
                        logger.LogError(ex, $"Error while processing message {result.Message.Value}");
                        continue;
                    }

                }
                catch (Exception e)
                {
                    logger.LogError($"Error occured on consuming message", e);
                }
                finally
                {
                    consumer.Close();
                    disposed = true;
                }
            }
        }, cancellationToken);
        return Task.CompletedTask;
    }

    public override void UnSubscribe<T, TH>()
    {
        eventManager.RemoveSubscription<T, TH>();
    }

    #region Private Methods

    private Message<TKey, TValue> GetMessage<TKey, TValue>(TKey key, TValue value) where TValue : IntegrationEvent
    {
        return new Message<TKey, TValue>
        {
            Headers = GetKafkaHeader(value.Header),
            Key = key,
            Value = value,
        };
    }

    private Headers GetKafkaHeader(Dictionary<string, string> eventHeaders)
    {
        Headers headers = new();
        foreach (var header in eventHeaders)
        {
            headers.Add(header.Key, Encoding.UTF8.GetBytes(header.Value));
        }
        return headers;
    }

    private IConsumer<string, TEvent> InitializeConsumer<TEvent>(string groupId = null) where TEvent : IntegrationEvent
    {
        var builder = new KafkaBuilder<string, TEvent>(options, configuration);
        initialized = true;
        return builder.BuildKafkaConsumer(groupId);
    }

    private async Task<bool> ProcessEvent<T>(T @event) where T : IntegrationEvent
    {
        var processed = false;

        if (eventManager.HasSubscriptionsForEvent<T>())
        {
            using var scope = serviceProvider.CreateScope();
            IEnumerable<SubscriptionInfo> subscriptions = eventManager.GetHandlersForEvent<T>();
            foreach (var subscription in subscriptions)
            {
                var handler = serviceProvider.GetService(subscription.HandlerType);
                if (handler == null) continue;
                var concreteEventType = typeof(IIntegrationEventHandler<>).MakeGenericType(typeof(T));
                await (Task)concreteEventType.GetMethod("HandleEvent").Invoke(handler, new object[] { @event });
            }
            processed = true;
        }
        return processed;
    }


    #endregion
}