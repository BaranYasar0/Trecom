//using Trecom.Api.Services.Catalog.Constants;
//using Trecom.Api.Services.Catalog.Models.Entities;
//using Trecom.Api.Services.Catalog.Persistance.DataSeeding;
//using Trecom.Api.Services.Catalog.Persistance.EntityFramework;
//using Trecom.Shared.CCS.GlobalException;

//namespace Trecom.Api.Services.Catalog.Application.Features.Rules
//{
//    public class ProductBusinessRules
//    {
//        private readonly AppDbContext context;

//        public ProductBusinessRules(AppDbContext context)
//        {
//            this.context = context;
//        }

//        public Task CheckProductIsNullOrNot(List<Product>? products = null, Product? product = null)
//        {
//            if (products is null && product == null)
//            {
//                throw new BusinessException(BusinessResponseConstants.NullItem);
//            }

//            return Task.CompletedTask;
//        }
//    }
//}
