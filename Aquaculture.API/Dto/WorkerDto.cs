using Swashbuckle.AspNetCore.Annotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace Aquaculture.API.Dto
{
    public class WorkerDto
    {
        [Required]
        [SwaggerSchema(ReadOnly = true)]
        public long WorkerId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public DateTime CertifiedUntil { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public long FarmId { get; set; }

        [SwaggerSchema(ReadOnly = true)]
        public FarmDto Farm { get; set; }
    }
}
