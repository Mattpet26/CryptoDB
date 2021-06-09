using CryptoDB.Models.API;
using CryptoDB.Models.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        public IUserService Userservice { get; }
        private readonly IBook _book;

        public UserController(IBook book, IUserService _context)
        {
            _book = book;
            Userservice = _context;
        }

        //write routes for book
        //get book
        //create book
        //add+remove crypto from book

        [HttpPost("Register")]

        //this.ModelState ------- comes directly in from MVC / Models

        //Either we get a good user OR we throw a modelstate error.
        public async Task<ActionResult<AppUserDTO>> Register(RegisterUser data)
        {
            var user = await Userservice.Register(data, this.ModelState);
            if (ModelState.IsValid)
            {
                return user;
            }
            return BadRequest(new ValidationProblemDetails(ModelState));
        }

        [HttpPost("Login")]
        public async Task<ActionResult<AppUserDTO>> Login(LoginData data)
        {
            //authentication verifies who you are
            //authorization says what you can do
            var user = await Userservice.Authenticate(data.Username, data.Password);

            if (user != null)
            {
                return user;
            }
            return Unauthorized();
        }

        //[Authorize(Policy = "create")]
        [HttpGet("me")]
        public async Task<ActionResult<AppUserDTO>> Me()
        {
            return await Userservice.GetUser(this.User);
        }
    }
}
