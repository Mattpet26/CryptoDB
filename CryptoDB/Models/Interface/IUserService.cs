using CryptoDB.Models.API;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CryptoDB.Models.Interface
{
    public interface IUserService
    {
        /// <summary>
        /// Registers a new user
        /// </summary>
        /// <param name="data"></param>
        /// <param name="modelState"></param>
        /// <returns>userDTO that registered</returns>
        public Task<AppUserDTO> Register(RegisterUser data, ModelStateDictionary modelState);

        /// <summary>
        /// authenticates a user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>authed userDTO</returns>
        public Task<AppUserDTO> Authenticate(string username, string password);

        /// <summary>
        /// Gets a user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>userDTO</returns>
        public Task<AppUserDTO> GetUser(ClaimsPrincipal user);
    }
}
