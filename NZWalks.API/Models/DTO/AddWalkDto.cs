using NZWalks.API.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class AddWalkDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000, ErrorMessage = "Description has to be a maximum of 1000 characters")]
        public string Description { get; set; }

        [Required]
        [Range(0, 100)]
        public double LengthInKm { get; set; }

        public string? WalkImageUrl { get; set; }

        [Required]
        public Guid DifficultyId { get; set; }

        [Required]
        public Guid RegionId { get; set; }

    }
}
