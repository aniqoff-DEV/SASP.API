using System.ComponentModel.DataAnnotations;

namespace SASP.API.Entities
{
    public class Issue
    {
        [Key]
        public int IssueId { get; set; }

        [Required]
        public int CatalogId { get; set; }

        [Required]
        public int TypeIssueId { get; set; }

        [Required]
        public string Title { get; set; }

        public string Photo { get; set; }

        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
