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
    public class CryptoRepository : ICryptoCurrency
    {
        private readonly CryptoDbContext _context;
        public CryptoRepository(CryptoDbContext context)
        {
            _context = context;
        }
        public async Task<CryptoCurrency> CreateCrypto(CryptoDTO crytoDTO)
        {
            CryptoCurrency crypto = new CryptoCurrency()
            {
                Id = crytoDTO.Id,
                DayPrice = crytoDTO.DayPrice,
                HourPrice = crytoDTO.HourPrice,
                Name = crytoDTO.Name,
                Price = crytoDTO.Price,
                WeekPrice = crytoDTO.WeekPrice,
            };
            _context.Entry(crypto).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return crypto;
        }

        public async Task<List<CryptoDTO>> GetAllCrypto()
        {
            var crypto = await _context.CryptoCurrencies.ToListAsync();
            var cryptoList = new List<CryptoDTO>();

            foreach (var item in crypto)
            {
                cryptoList.Add(await GetCrypto(item.Id));
            }
            return cryptoList;
        }

        public async Task<CryptoDTO> GetCrypto(int Id)
        {
            CryptoCurrency crypto = await _context.CryptoCurrencies.FindAsync(Id);

            CryptoDTO dto = new CryptoDTO()
            {
                Id = crypto.Id,
                DayPrice = crypto.DayPrice,
                HourPrice = crypto.HourPrice,
                Name = crypto.Name,
                Price = crypto.Price,
                WeekPrice = crypto.WeekPrice,
            };

            return dto;
        }

        public async Task<CryptoCurrency> UpdateCrypto(CryptoDTO cryptoDTO)
        {
            CryptoCurrency crypto = new CryptoCurrency()
            {
                Id = cryptoDTO.Id,
                DayPrice = cryptoDTO.DayPrice,
                HourPrice = cryptoDTO.HourPrice,
                Name = cryptoDTO.Name,
                Price = cryptoDTO.Price,
                WeekPrice = cryptoDTO.WeekPrice,
            };
            _context.Entry(crypto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return crypto;
        }
    }
}
