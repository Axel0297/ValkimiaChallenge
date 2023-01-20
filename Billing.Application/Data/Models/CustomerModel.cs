using System.ComponentModel.DataAnnotations;

namespace Billing.Application.Data.Models
{
    public class CustomerModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "El Nombre es obligatorio")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "El Apellido es obligatorio")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "La Direccion es obligatorio")]
        public string? Address { get; set; }

        [RegularExpression("^[^@\\s]+@[^@\\s]+\\.(com|net|org|gov)$", ErrorMessage = "Debe indicar un email valido")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatorio")]
        public string? Password { get; set; }

        public bool Enabled { get; set; } = true;

        [Required(ErrorMessage = "El cliente debe pertenecer a una ciudad")]
        public Guid CityId { get; set; }
    }
}
