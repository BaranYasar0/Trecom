using Trecom.Shared.Models;

namespace Trecom.Services.Api.Stock.Models
{
    public class Stock:BaseEntity
    {
        public Guid ProductId { get; set; }
        public int Amount { get; set; }

    }
}
