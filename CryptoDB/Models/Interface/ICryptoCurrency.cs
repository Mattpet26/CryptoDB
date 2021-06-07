using CryptoDB.Models.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoDB.Models.Interface
{
    public interface ICryptoCurrency
    {
        Task<CryptoCurrency> CreateCrypto(CryptoDTO crytoDTO);
        Task<CryptoDTO> GetCrypto(int Id);
        Task<List<CryptoDTO>> GetAllCrypto();
        Task<CryptoCurrency> UpdateCrypto(CryptoDTO cryptoDTO);
    }
}
