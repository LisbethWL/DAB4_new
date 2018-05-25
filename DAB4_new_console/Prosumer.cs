using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAB4.Prosumers
{
    class Prosumer
    {
        public Prosumer(int proId, string proType)
        {
            Type = proType;
            Id = proId;
        }

        public int Id { get; set; }
        public int ProducedkW { get; set; }
        public int ConsumedkW { get; set; }
        public string Type { get; set; }
        public int DifferencekW { get; set; }
    }
}
