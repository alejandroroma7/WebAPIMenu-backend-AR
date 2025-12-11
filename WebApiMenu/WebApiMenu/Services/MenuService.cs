using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiRestaurant.Contracts;
using WebApiRestaurant.Models;
using WebApiRestaurant.Models.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace WebApiRestaurant.Services
{
    public class MenuService : IMenuService
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public MenuService(AppDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MenuDto>> GetAll()
        {
            var products = await _dbContext.Menus.ToListAsync();
            return _mapper.Map<IEnumerable<MenuDto>>(products);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "User, Admin")]
        public async Task<MenuDto?> GetById(int id)
        {
            var product = await _dbContext.Menus.FindAsync(id);
            return product == null ? null : _mapper.Map<MenuDto>(product);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<MenuDto> Create(CreateUpdateMenuDto createDto)
        {
            var product = _mapper.Map<Menu>(createDto);
            _dbContext.Menus.Add(product);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<MenuDto>(product);

        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<bool> Update(int id, CreateUpdateMenuDto updateDto)
        {
            try
            {
                var product = await _dbContext.Menus.FindAsync(id);
                if (product == null)
                {
                    return false;
                }

                _mapper.Map(updateDto, product);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            var menu = await _dbContext.Menus.FindAsync(id);
            if (menu == null)
            {
                return false;
            }

            _dbContext.Menus.Remove(menu);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        private bool MenuExists(int id)
        {
            return _dbContext.Menus.Any(e => e.Id == id);
        }
    }
}
