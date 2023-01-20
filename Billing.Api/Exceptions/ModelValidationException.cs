using System.Runtime.Serialization;

namespace Billing.Api.Exceptions
{
    [DataContract]
    public class ModelValidationException : Exception
    {
        public Dictionary<string, IEnumerable<string>> Errors { get; set; }

        public ModelValidationException(string message, Dictionary<string, IEnumerable<string>> error) : base(message)
        {
            Errors = error;
        }
    }
}
