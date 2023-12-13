using System.ComponentModel.DataAnnotations;

namespace SASP.API.Entities
{
    public class Country
    {
        [Key]
        public int CountryId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
