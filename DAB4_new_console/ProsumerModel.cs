using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAB4_new_console
{
    class ProsumerModel
    {
        public int Id { get; set; }
        public int ProducedkW { get; set; }
        public int ConsumedkW { get; set; }
        public string Type { get; set; }
        public int DifferencekW { get; set; }
    }
}
