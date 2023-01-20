using System.Text;

namespace Billing.Business.Extensions
{
    public static class StringExtensions
    {
        public static string Base64Encode(this string str)
        {
            var textBytes = Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(textBytes);
        }
    }
}
