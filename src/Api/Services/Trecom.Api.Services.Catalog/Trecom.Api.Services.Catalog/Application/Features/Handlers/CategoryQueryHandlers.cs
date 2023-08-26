using AutoMapper;
using MediatR;
using Trecom.Api.Services.Catalog.Application.Features.Queries;
using Trecom.Api.Services.Catalog.Models.Dtos;
using Trecom.Api.Services.Catalog.Models.ViewModels;
using Trecom.Api.Services.Catalog.Persistance.Repository;

namespace Trecom.Api.Services.Catalog.Application.Features.Handlers
{
    public class CategoryQueryHandlers :
        IRequestHandler<GetBaseCategoryListQuery, PaginationViewModel<BaseCategoryResponseDto>>
        //IRequestHandler<GetSpecificCategoriesByTypeCategoryIdQuery, PaginationViewModel<BaseCategoryResponseDto>>,
        //IRequestHandler<GetTypeCategoriesByBaseCategoryIdQuery, PaginationViewModel<BaseCategoryResponseDto>>
    {
        private readonly IMapper mapper;
        private readonly ILogger<CategoryQueryHandlers> logger;
        private readonly ProductRepository productRepository;
        public CategoryQueryHandlers(IMapper mapper, ILogger<CategoryQueryHandlers> logger, ProductRepository productRepository)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.productRepository = productRepository;
        }

        public async Task<PaginationViewModel<BaseCategoryResponseDto>> Handle(GetBaseCategoryListQuery request, CancellationToken cancellationToken)
        {
            return await productRepository.GetBaseCategoriesAsync(request.Pagination);
        }

        //public async Task<PaginationViewModel<BaseCategoryResponseDto>> Handle(GetSpecificCategoriesByTypeCategoryIdQuery request, CancellationToken cancellationToken)
        //{

        //}

        //public async Task<PaginationViewModel<BaseCategoryResponseDto>> Handle(GetTypeCategoriesByBaseCategoryIdQuery request, CancellationToken cancellationToken)
        //{

        //}
    }
}
