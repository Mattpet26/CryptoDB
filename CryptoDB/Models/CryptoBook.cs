using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoDB.Models
{
    public class CryptoBook
    {
        public int CryptoId { get; set; }
        public int BookId { get; set; }

        public CryptoCurrency CryptoCurrency { get; set; }
        public Book Book { get; set; }
    }
}
