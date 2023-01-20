using System.Text.Json.Serialization;

namespace Billing.Application.Data.Models
{
    public class TokenModel
    {
        public Guid CustomerId { get; set; }

        public string AccessToken { get; set; }

        public int ExpiresIn { get; set; }
    }
}
