using System.ComponentModel.DataAnnotations;

namespace Billing.Api.Models.Customer
{
    public class CustomerPayloadViewModel
    {
        public Guid? Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Lastname { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Address { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Email { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Password { get; set; }

        public bool Enabled { get; set; } = true;

        [Required]
        public Guid CityId { get; set; }
    }
}
