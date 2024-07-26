using AutoMapper;
using TodoApi.Models;

namespace TodoApi.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductCreation>().ReverseMap();
        }
    }
}
