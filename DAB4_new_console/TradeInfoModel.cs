using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAB4_new_console
{
    class TradeInfoModel
    {
        public int Id { get; set; }
        public int Token { get; set; }
        public int BitCoin { get; set; }
        public int sellerId { get; set; }
        public int buyerId { get; set; }
        public int amount { get; set; }
    }
}
