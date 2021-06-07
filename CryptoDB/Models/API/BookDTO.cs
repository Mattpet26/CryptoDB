using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoDB.Models.API
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [BindProperty]
        public List<CryptoBook> CryptoBooks { get; set; }
        public List<CryptoBook> WatchList { get; set; }
    }
}
