using System.ComponentModel.DataAnnotations;

namespace Billing.Api.Models.Customer
{
    public class CustomerLoginViewModel
    {
        [Required]
        [MaxLength(100)]
        public string? Email { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Password { get; set; }
    }
}
