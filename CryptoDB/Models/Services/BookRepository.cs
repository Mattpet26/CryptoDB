using CryptoDB.Data;
using CryptoDB.Models.API;
using CryptoDB.Models.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoDB.Models.Services
{
    public class BookRepository : IBook
    {
        private readonly CryptoDbContext _context;
        public BookRepository(CryptoDbContext context)
        {
            _context = context;
        }

        public async Task AddCryptoToBook(int bookId, int cryptoId)
        {
            CryptoBook book = new CryptoBook()
            {
                BookId = bookId,
                CryptoId = cryptoId
            };
            _context.Entry(book).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            await _context.SaveChangesAsync();
        }

        public async Task CreateBook(BookDTO bookdto)
        {
            Book book = new Book()
            {
                Id = bookdto.Id,
                UserId = bookdto.UserId,
                CryptoBooks = new List<CryptoBook>()
            };
            _context.Entry(book).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            await _context.SaveChangesAsync();
        }

        public async Task<Book> GetBook(string Id)
        {
            return await _context.Books
                .Where(x => x.UserId == Id)
                .Include(x => x.CryptoBooks)
                .ThenInclude(x => x.CryptoCurrency)
                .Select(x => new Book
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    CryptoBooks = x.CryptoBooks
                }).FirstOrDefaultAsync();
        }

        public async Task RemoveCryptoFromBook(int bookId, int cryptoId)
        {
            var result = await _context.CryptoBooks.FirstOrDefaultAsync(x => x.CryptoId == cryptoId && x.BookId == bookId);
            _context.Entry(result).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
