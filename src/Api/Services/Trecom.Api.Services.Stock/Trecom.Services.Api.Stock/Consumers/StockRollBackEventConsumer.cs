using MassTransit;
using Microsoft.EntityFrameworkCore;
using Trecom.Services.Api.Stock.Models;
using Trecom.Shared.Events.Interfaces;
using Trecom.Shared.Extensions;

namespace Trecom.Services.Api.Stock.Consumers;

public class StockRollBackEventConsumer:IConsumer<IStockRollBackEvent>
{
    private readonly StockDbContext dbContext;
    private readonly ILogger<StockRollBackEventConsumer> logger;

    public StockRollBackEventConsumer(StockDbContext context, ILogger<StockRollBackEventConsumer> logger)
    {
        this.dbContext = context;
        this.logger = logger;
    }

    public async Task Consume(ConsumeContext<IStockRollBackEvent> context)
    {
        var products = new List<Models.Stock>();

        foreach (var item in context.Message.OrderItems)
        {
            var product = await dbContext.Stocks.FirstOrDefaultAsync(x => x.ProductId == item.ProductId);
                
            if (product.ValidateNullBool())
            {
                product.Amount += item.Quantity;
                dbContext.Update(product);
                logger.LogInformation($"{product.Id} is rolled back");
            }
            else
            {
                product = new Models.Stock 
                {
                    ProductId = item.ProductId,
                    Amount = item.Quantity
                };
                dbContext.Add(product);
            }
        }

        await dbContext.SaveChangesAsync();
    }
}