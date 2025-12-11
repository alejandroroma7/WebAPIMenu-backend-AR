using Microsoft.AspNetCore.Mvc;
using WebApiRestaurant.Models;
using WebApiRestaurant.Models.DTO;

namespace WebApiRestaurant.Contracts
{
    public interface IMenuService
    {
        Task<IEnumerable<MenuDto>> GetAll();
        Task<MenuDto?> GetById(int id);
        Task<MenuDto> Create(CreateUpdateMenuDto createDto);
        Task<bool> Update(int id, CreateUpdateMenuDto updateDto);
        Task<bool> Delete(int id);
    }
}
