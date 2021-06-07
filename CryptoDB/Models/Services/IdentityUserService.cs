using CryptoDB.Models.API;
using CryptoDB.Models.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CryptoDB.Models.Services
{
    public class IdentityUserService : IUserService
    {
        private UserManager<AppUser> UserManager;
        private JwtTokenService tokenService;
        public IdentityUserService(UserManager<AppUser> manager, JwtTokenService jwtTokenService)
        {
            UserManager = manager;
            tokenService = jwtTokenService;
        }

        /// <summary>
        /// registers a new user
        /// </summary>
        /// <param name="data"></param>
        /// <param name="modelState"></param>
        /// <returns>AppUserDTO created</returns>
        public async Task<AppUserDTO> Register(RegisterUser data, ModelStateDictionary modelState)
        {
            var user = new AppUser
            {
                UserName = data.Username,
                PhoneNumber = data.PhoneNumber,
                Email = data.Email
            };

            var result = await UserManager.CreateAsync(user, data.Password);

            //if the result succeeds, we create a dto user and return it
            if (result.Succeeded)
            {
                await UserManager.AddToRolesAsync(user, data.Roles);
                AppUserDTO newuser = new AppUserDTO
                {
                    Username = user.UserName,
                    Id = user.Id,
                    Token = await tokenService.GetToken(user, System.TimeSpan.FromMinutes(5))
                };
                return newuser;
            }

            foreach (var error in result.Errors)
            {
                var errorKey =
                    error.Code.Contains("Password") ? nameof(data.Password) :
                    error.Code.Contains("Email") ? nameof(data.Email) :
                    error.Code.Contains("UserName") ? nameof(data.Username) :
                     "";
                modelState.AddModelError(errorKey, error.Description);
            }

            return null;
        }

        /// <summary>
        /// authenticates a user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>returns an AppUserDTO if auth'd</returns>
        public async Task<AppUserDTO> Authenticate(string username, string password)
        {
            var user = await UserManager.FindByNameAsync(username);
            if (await UserManager.CheckPasswordAsync(user, password))
            {
                return new AppUserDTO
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Token = await tokenService.GetToken(user, TimeSpan.FromMinutes(5))
                };
            }
            return null;
        }

        /// <summary>
        /// returns a user
        /// </summary>
        /// <param name="principal"></param>
        /// <returns>returns an AppUserDTO</returns>
        public async Task<AppUserDTO> GetUser(ClaimsPrincipal principal)
        {
            var user = await UserManager.GetUserAsync(principal);
            return new AppUserDTO
            {
                Id = user.Id,
                Username = user.UserName,
                Token = await tokenService.GetToken(user, System.TimeSpan.FromMinutes(5))

            };
        }
    }
}
