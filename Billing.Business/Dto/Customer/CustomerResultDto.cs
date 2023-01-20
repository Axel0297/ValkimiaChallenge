using Billing.Business.Dto.City;
using Billing.Business.Dto.Invoice;

namespace Billing.Business.Dto.Customer
{
    public class CustomerResultDto
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Lastname { get; set; }

        public string? Address { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public bool Enabled { get; set; }

        public Guid CityId { get; set; }

        public CityDto? City { get; set; }
    }
}
