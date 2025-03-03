using AutoMapper;
using SportsEcommerce.Models.Dtos.Categories.Requests;
using SportsEcommerce.Models.Dtos.Categories.Responses;
using SportsEcommerce.Models.Dtos.Products.Requests;
using SportsEcommerce.Models.Dtos.Products.Responses;
using SportsEcommerce.Models.Entities;

namespace SportsEcommerce.Service.Profiles;

public class MappingProfiles : Profile
{
  public MappingProfiles()
  {
    CreateMap<CreateCategoryRequest, Category>();
    CreateMap<UpdateCategoryRequest, Category>();
    CreateMap<Category, CategoryResponseDto>();

    CreateMap<CreateProductRequest, Product>();
    CreateMap<UpdateProductRequest, Product>();
    CreateMap<Product, CreatedProductResponseDto>();
    CreateMap<Product, ProductResponseDto>()
      .ForMember(prd => prd.Category, opt => opt.MapFrom(p => p.Category.Name));
  }
}
