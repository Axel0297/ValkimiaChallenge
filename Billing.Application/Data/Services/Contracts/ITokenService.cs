using Billing.Application.Data.Models;

namespace Billing.Application.Data.Services.Contracts
{
    public interface ITokenService
    {
        Task<TokenModel> GetToken();
        Task RemoveToken();
        Task SetToken(TokenModel tokenDTO);
    }
}
