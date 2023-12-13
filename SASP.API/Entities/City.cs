using System.ComponentModel.DataAnnotations;

namespace SASP.API.Entities
{
    public class City
    {
        [Key] 
        public int CityId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
