using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;

namespace Billing.Api.Controllers
{
    public abstract class ApiControllerBase : Controller
    {
        protected static Dictionary<string, IEnumerable<string>> GetModelErrors(ModelStateDictionary modelState)
        {
            var errors = modelState.ToDictionary(k => k.Key, v => v.Value.Errors.Select(s => s.ErrorMessage));

            return errors;
        }
    }
}
