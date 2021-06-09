using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoDB.Models.API
{
    public class CryptoDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string HourPrice { get; set; }
        public string DayPrice { get; set; }
        public string WeekPrice { get; set; }

    }
}
