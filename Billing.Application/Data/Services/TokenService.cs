using Billing.Application.Data.Models;
using Billing.Application.Data.Services.Contracts;
using Blazored.LocalStorage;

namespace Billing.Application.Data.Services
{
    public class TokenService : ITokenService
    {
        private readonly ILocalStorageService localStorageService;

        public TokenService(ILocalStorageService localStorageService)
        {
            this.localStorageService = localStorageService;
        }

        public async Task SetToken(TokenModel tokenModel)
        {
            await localStorageService.SetItemAsync("token", tokenModel);
        }

        public async Task<TokenModel> GetToken()
        {
            try
            {
                return await localStorageService.GetItemAsync<TokenModel>("token");
            }
            catch { return new(); }
        }

        public async Task RemoveToken()
        {
            await localStorageService.RemoveItemAsync("token");
        }
    }
}
