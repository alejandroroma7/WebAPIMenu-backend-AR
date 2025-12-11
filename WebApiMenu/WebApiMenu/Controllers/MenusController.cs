using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiRestaurant.Contracts;
using WebApiRestaurant.Models;
using WebApiRestaurant.Models.DTO;

namespace WebApiRestaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenusController : ControllerBase
    {
        private readonly IMenuService _service;

        public MenusController(IMenuService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "User, Admin")]
        public async Task<ActionResult<IEnumerable<MenuDto>>> GetAll()
        {
            return Ok(await _service.GetAll());
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "User, Admin")]
        public async Task<ActionResult<MenuDto>> GetById(int id)
        {
            return Ok(await _service.GetById(id));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateUpdateMenuDto createDto)
        {
            var product = await _service.Create(createDto);

            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateUpdateMenuDto updateDto)
        {
            var updated = await _service.Update(id, updateDto);
            return updated ? NoContent() : NotFound();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.Delete(id);
            return deleted ? NoContent() : NotFound();
        }

    }
}
