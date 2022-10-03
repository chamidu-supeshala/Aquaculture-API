using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Aquaculture.API.Data
{
    public class Farm
    {
        [Key]
        public long FarmId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal GpsPosition { get; set; }

        [Required]
        public int NoOfCages { get; set; }

        [Required]
        public bool HasBarge { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public virtual ICollection<Worker> Workers { get; set; }
    }
}
