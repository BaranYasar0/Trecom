namespace Trecom.Api.Services.Order.Domain.Enums;

public enum OrderStatus
{
    Started,
    StockFailed,
    PaymentStarted,
    AwaitingPickUp,
    Cancelled,
    Completed,
    Shipped,

}