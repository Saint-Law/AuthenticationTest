namespace AuthTest.Api.Extensions;

public class ProfileMapper : Profile
{
    public ProfileMapper()
    {
        CreateMap<Product, CreateProductDto>();
        CreateMap<CreateProductDto, Product>();
        CreateMap<Product, UpdateProductDto>();
        CreateMap<UpdateProductDto, Product>();

        CreateMap<Category, CreateCategoryDto>();
        CreateMap<CreateCategoryDto, Category>();
        CreateMap<Category, UpdateCategoryDto>();
        CreateMap<UpdateCategoryDto, Category>();

        CreateMap<Sales, CreateSalesDto>();
        CreateMap<CreateCategoryDto, Sales>();
        CreateMap<Sales, UpdateSalesDto>(); 
        CreateMap<UpdateSalesDto, Sales>();

        CreateMap<User, UserDto>();
        CreateMap<User, RegistrationDto>();
    }
}
