using AutoMapper;
using MyAPI.Domain.Dto;
using MyAPI.Domain.Entities;

namespace MyAPI.API.Helpers
{
    public class MappingHelper : Profile
    {
        public MappingHelper()
        {
            CreateMap<Product, GetProductDto>();
            CreateMap<GetProductDto, Product>();
            CreateMap<Product, PostProductDto>();
            CreateMap<PostProductDto, Product>();
            CreateMap<Order, PostOrderDto>();
            CreateMap<PostOrderDto, Order>();
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>();

        }
    }
}
