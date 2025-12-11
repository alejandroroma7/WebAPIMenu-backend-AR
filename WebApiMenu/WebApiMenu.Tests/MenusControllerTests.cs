using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiRestaurant.Contracts;
using WebApiRestaurant.Controllers;
using WebApiRestaurant.Models.DTO;
using Xunit;

public class MenusControllerTests
{
    private readonly Mock<IMenuService> _mockService;
    private readonly MenusController _controller;
    public MenusControllerTests()
    {
        _mockService = new Mock<IMenuService>();
        _controller = new MenusController(_mockService.Object);
    }

    [Fact]
    public async Task GetAll_MustReturnOk()
    {
        var menus = new List<MenuDto>
        {
            new MenuDto { Id = 1, Name = "Menu 1" },
            new MenuDto { Id = 2, Name = "Menu 2" },
            new MenuDto { Id = 4, Name = "Menu 3" },
        };

        _mockService.Setup(s => s.GetAll()).ReturnsAsync(menus);

        var result = await _controller.GetAll();

        var ok = result.Result as OkObjectResult;
        ok.Should().NotBeNull();
        (ok!.Value as IEnumerable<MenuDto>).Should().HaveCount(3);
    }

    [Fact]
    public async Task GetById_MustReturnOk_IfExists()
    {
        var menu = new MenuDto { Id = 10, Name = "TestMenu" };
        _mockService.Setup(s => s.GetById(10)).ReturnsAsync(menu);

        var result = await _controller.GetById(10);

        var ok = result.Result as OkObjectResult;
        ok.Should().NotBeNull();
        (ok!.Value as MenuDto)!.Id.Should().Be(10);
    }

    [Fact]
    public async Task Create_MustReturnACreated()
    {
        var dto = new CreateUpdateMenuDto { Name = "Menu1", Price = 10 };
        var created = new MenuDto { Id = 5, Name = "Menu1", Price = 10 };
        _mockService.Setup(s => s.Create(dto)).ReturnsAsync(created);
        var result = await _controller.Create(dto);
        var createdResult = result as CreatedAtActionResult;
        createdResult.Should().NotBeNull();
        (createdResult!.Value as MenuDto)!.Id.Should().Be(5);
        (createdResult!.Value as MenuDto)!.Price.Should().Be(10);
    }

    [Fact]
    public async Task Update_MustReturnNoContent_IfIsUpdated()
    {
        _mockService.Setup(s => s.Update(1, It.IsAny<CreateUpdateMenuDto>())).ReturnsAsync(true);
        var result = await _controller.Update(1, new CreateUpdateMenuDto());
        result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task Update_MustReturnNotFound_IfNotUpdated()
    {
        _mockService.Setup(s => s.Update(1, It.IsAny<CreateUpdateMenuDto>())).ReturnsAsync(false);
        var result = await _controller.Update(1, new CreateUpdateMenuDto());
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task Delete_MustReturnNoContent_IfIsDeleted()
    {
        _mockService.Setup(s => s.Delete(1)).ReturnsAsync(true);
        var result = await _controller.Delete(1);
        result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task Delete_MustReturnNotFound_IfNotDeleted()
    {
        _mockService.Setup(s => s.Delete(1)).ReturnsAsync(false);
        var result = await _controller.Delete(1);
        result.Should().BeOfType<NotFoundResult>();
    }
}
