using AutoMapper;
using GeekShopping.ProductAPI.DTOs;
using GeekShopping.ProductAPI.Model;

namespace GeekShopping.ProductAPI.AutoMapperconfig
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mapConfig = new MapperConfiguration(config => {
                config.CreateMap<ProductDTO, Product>().ForMember(x => x.CategoryName, x => x.NullSubstitute(""))
                .ForMember(x => x.Description, x => x.NullSubstitute(""))
                .ForMember(x => x.ImageUrl, x => x.NullSubstitute(""))
                .ForMember(x => x.Name, x => x.NullSubstitute(""));
                config.CreateMap<Product, ProductDTO>()
                .ForMember(x => x.CategoryName, x => x.NullSubstitute(""))
                .ForMember(x => x.Description, x => x.NullSubstitute(""))
                .ForMember(x => x.ImageUrl, x => x.NullSubstitute(""))
                .ForMember(x => x.Name, x => x.NullSubstitute(""));
                });
            return mapConfig;
        }
    }
}
