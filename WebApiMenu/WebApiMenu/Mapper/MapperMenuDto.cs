
using AutoMapper;
using WebApiRestaurant.Models;
using WebApiRestaurant.Models.DTO;

namespace WebApiRestaurant.Mapper
{
    public class MapperMenuDto : Profile
    {
        public MapperMenuDto()
        {
            CreateMap<Menu, MenuDto>();
            CreateMap<CreateUpdateMenuDto, Menu>();
        }
    }
}
