using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Aquaculture.API.Data
{
    public class Worker
    {
        [Key]
        public long WorkerId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Column(TypeName="date")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        [Column(TypeName="date")]
        public DateTime CertifiedUntil { get; set; }

        public string ImageUrl { get; set; }

        [ForeignKey(nameof(Farm))]
        public long FarmId { get; set; }

        public virtual Farm Farm { get; set; }
    }
}
