using AutoMapper;
using MediatR;
using Trecom.Api.Services.Catalog.Application.Features.Queries;
using Trecom.Api.Services.Catalog.Models.Dtos;
using Trecom.Api.Services.Catalog.Models.ViewModels;
using Trecom.Api.Services.Catalog.Persistance.Repository;
using Trecom.Shared.Models;

namespace Trecom.Api.Services.Catalog.Application.Features.Handlers
{
    public class CategoryQueryHandlers :
        IRequestHandler<GetCategoryListQuery, ApiResponse<PaginationViewModel<CategoryResponseDto>>>
        //IRequestHandler<GetSpecificCategoriesByTypeCategoryIdQuery, PaginationViewModel<CategoryResponseDto>>,
        //IRequestHandler<GetTypeCategoriesByBaseCategoryIdQuery, PaginationViewModel<CategoryResponseDto>>
    {
        private readonly IMapper mapper;
        private readonly ILogger<CategoryQueryHandlers> logger;
        private readonly ProductRepository productRepository;
        private readonly CategoryRepository categoryRepository;
        public CategoryQueryHandlers(IMapper mapper, ILogger<CategoryQueryHandlers> logger, ProductRepository productRepository, CategoryRepository categoryRepository)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
        }

        public async Task<ApiResponse<PaginationViewModel<CategoryResponseDto>>> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
        {
            var categories= await categoryRepository.GetAllCategoriesAsync(request.Pagination);

            return ApiResponse<PaginationViewModel<CategoryResponseDto>>
                .Success(mapper.Map<PaginationViewModel<CategoryResponseDto>>(categories));
        }

        //public async Task<PaginationViewModel<CategoryResponseDto>> Handle(GetSpecificCategoriesByTypeCategoryIdQuery request, CancellationToken cancellationToken)
        //{

        //}

        //public async Task<PaginationViewModel<CategoryResponseDto>> Handle(GetTypeCategoriesByBaseCategoryIdQuery request, CancellationToken cancellationToken)
        //{

        //}
    }
}
