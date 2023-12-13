using System.ComponentModel.DataAnnotations;

namespace SASP.API.Entities
{
    public class Subscription
    {
        [Key] 
        public int SubscriptionId { get; set; }

        [Required]
        public int IssueId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime StartSub { get; set; }

        [Required]
        public DateTime EndSub { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
