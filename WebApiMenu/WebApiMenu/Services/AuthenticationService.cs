using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiRestaurant.Contracts;
using WebApiRestaurant.Custom;
using WebApiRestaurant.Models;
using WebApiRestaurant.Models.DTO;

namespace WebApiRestaurant.Services
{
    public class AuthenticationService : IAuthService
    {
        private readonly AppDbContext _dbContext;
        private readonly Utilities _utilities;

        public AuthenticationService(AppDbContext context, Utilities utilities)
        {
            _dbContext = context;
            _utilities = utilities;
        }

        public async Task<User> Register(UserDto user)
        {
            var userModel = new User
            {
                Username = user.Username,
                Mail = user.Mail,
                Password = _utilities.encryptSHA256(user.Password),
                Role = user.Role
            };

            await _dbContext.Users.AddAsync(userModel);
            await _dbContext.SaveChangesAsync();
            return userModel;
        }

        public async Task<User?> Login(LoginDto login)
        {
            var foundedUser = await _dbContext.Users
                                                    .Where(u =>
                                                        u.Username == login.Username &&
                                                        u.Password == _utilities.encryptSHA256(login.Password)
                                                      ).FirstOrDefaultAsync();

            return foundedUser;    
        }
    }
}
