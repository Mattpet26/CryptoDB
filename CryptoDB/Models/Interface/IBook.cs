using CryptoDB.Models.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoDB.Models.Interface
{
    public interface IBook
    {
        Task CreateBook(BookDTO bookdto);
        Task<Book> GetBook(string Id);
        Task RemoveCryptoFromBook(int bookId, int cryptoId);
        Task AddCryptoToBook(int bookId, int cryptoId);
    }
}
