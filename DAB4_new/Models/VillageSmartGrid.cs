using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DAB4_new.Models
{
    /// <summary>
    /// Author - Tue Jensen
    /// </summary>
    [Table("VillageSmartGrid")]
    public partial class VillageSmartGrid
    {

        /// <summary>
        /// Id of the element
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        /// <summary>
        /// Type of the element
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Typen af produktet
        /// </summary>
        public string ProductType { get; set; }
        /// <summary>
        /// SmartMeter
        /// </summary>
        public string SmartMeter { get; set; }
    }
}