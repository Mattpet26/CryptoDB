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
    public class UserController : Controller
    {
        public IUserService UserService { get; }
        private readonly IBook _book;

        public UserController(IBook book, IUserService service)
        {
            _book = book;
            UserService = service;
        }

        //write routes for book
        //get book
        //create book
        //add+remove crypto from book

        //write routes for user
        //authenticate
        //register
    }
}
