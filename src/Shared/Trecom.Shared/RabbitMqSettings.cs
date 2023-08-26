using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trecom.Shared
{
    public  class RabbitMqSettings
    {
        public const string OrderSaga = "order-saga-queue";
        public const string StockNotReservedQueueName = "Stock-order-not-reserved-queue";
        public const string UpdateBrandAndSupplierForCreateProductEvent = "update-brand-and-supplier-for-create-product-event-queue";
        public const string OrderFailedQueueName = "order-failed-queue";
        public const string OrderCreatedEventQueueName = "order-created-event-queue";
        public const string StockRollBackEventQueueName = "stock-rollback-event-queue";
        public const string OrderCompletedEventQueueName = "order-completed-event-queue";
        public const string StockReservedQueueName = "stock-reserved-queue";
    }
}
