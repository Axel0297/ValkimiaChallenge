using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Billing.Data.Entities
{
    [Table("City")]
    public class City
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("Nombre")]
        public string? Name { get; set; }
    }
}
