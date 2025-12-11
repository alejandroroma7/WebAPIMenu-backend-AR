using WebApiRestaurant.Models;
using WebApiRestaurant.Models.DTO;

namespace WebApiRestaurant.Contracts
{
    public interface IAuthService
    {
        Task<User> Register(UserDto user);
        Task<User?> Login(LoginDto login);
    }
}
