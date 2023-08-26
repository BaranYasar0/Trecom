using Ardalis.SmartEnum;
using AutoMapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Trecom.Api.Services.Catalog.Models.Dtos;
using Trecom.Api.Services.Catalog.Models.Entities;
using Trecom.Api.Services.Catalog.Models.Enums;

namespace Trecom.Api.Services.Catalog.Application.Features
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductResponseDto>()
                .ForMember(x => x.Supplier, y => y.MapFrom(x => x.Supplier))
                .ForMember(x => x.Brand, y => y.MapFrom(x => x.Brand))
                ;

            CreateMap<CreateProductDto, Product>();
            CreateMap<Product, CreateProductResponseDto>();
            
            CreateMap<Brand, BrandResponseDto>();
            CreateMap<Supplier, SupplierResponseDto>();
            CreateMap<SpecificCategory, SpecificCategoryResponseDto>();
            CreateMap<TypeCategory, TypeCategoryResponseDto>();
            CreateMap<BaseCategory, BaseCategoryResponseDto>();
        }
    }
}
