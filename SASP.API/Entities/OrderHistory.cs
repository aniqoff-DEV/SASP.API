using System.ComponentModel.DataAnnotations;

namespace SASP.API.Entities
{
    public class OrderHistory
    {
        [Key] 
        public int OrderId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int IssueId { get; set; }

        [Required]
        public DateTime CreatedDate { get; set;}
    }
}
