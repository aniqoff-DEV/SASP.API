using System.ComponentModel.DataAnnotations;

namespace SASP.API.Entities
{
    public class TypeIssue
    {
        [Key]
        public int TypeIssueId { get; set; }

        [Required]
        public string TypeName { get; set; }
    }
}
