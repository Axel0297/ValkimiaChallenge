
namespace Billing.Api.Models
{
    public class TokenResponseModel
    {
        public Guid CustomerId { get; set; }

        public string AccessToken { get; set; }

        public int ExpiresIn { get; set; }
    }
}
