using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Aquaculture.API.Dto
{
    public class FarmDto
    {
        [Required]
        public long FarmId { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        public decimal GpsPosition { get; set; }

        [Required]
        public int NoOfCages { get; set; }

        [Required]
        public bool HasBarge { get; set; }
        
        public IFormFile file { get; set; }

        public string ImageUrl { get; set; }

        public List<WorkerDto> Workers { get; set; }
    }
}
