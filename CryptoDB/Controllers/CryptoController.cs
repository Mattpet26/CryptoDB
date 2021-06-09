using CryptoDB.Models;
using CryptoDB.Models.API;
using CryptoDB.Models.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CryptoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptoController : ControllerBase
    {
        private readonly ICryptoCurrency _crypto;
        public CryptoController(ICryptoCurrency crypto)
        {
            _crypto = crypto;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CryptoDTO>>> GetAllCrypto()
        {
            return Ok(await _crypto.GetAllCrypto());
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<CryptoDTO>> GetCrypto(int Id)
        {
            var result = await _crypto.GetCrypto(Id);

            if (result == null)
            {
                return NotFound();
            }
            return result;
        }

        [HttpPost]
        public async Task<ActionResult<CryptoCurrency>> CreateCrypto(CryptoDTO dto)
        {
            await _crypto.CreateCrypto(dto);
            return CreatedAtAction("GetCrypto", new { Id = dto.Id}, dto);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<CryptoCurrency>> UpdateCrypto(CryptoDTO dto)
        {
            var updateCrypto = await _crypto.UpdateCrypto(dto);
            return Ok(updateCrypto);
        }
    }
}
