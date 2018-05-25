using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DAB4_new.Models
{
    public class ProsumerInfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }

        public int ProducedkW { get; set; }
        public int ConsumedkW { get; set; }

        public string Type { get; set; }
        public int DifferencekW { get; set; }

    }
}