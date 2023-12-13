using System.ComponentModel.DataAnnotations;

namespace SASP.API.Entities
{
    public class Catalog
    {
        [Key]
        public int CatalogId { get; set; }

        [Required]
        public string CatalogName { get; set; }
    }
}
