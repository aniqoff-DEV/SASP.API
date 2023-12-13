using System.ComponentModel.DataAnnotations;

namespace SASP.API.Entities
{
    public class Admin
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
