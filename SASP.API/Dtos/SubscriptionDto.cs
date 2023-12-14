namespace SASP.API.Dtos
{
    public class SubscriptionDto
    {
        public int SubscriptionId { get; set; }

        public string Issue { get; set; }

        public string User { get; set; }

        public DateTime StartSub { get; set; }

        public DateTime EndSub { get; set; }

        public decimal Price { get; set; }
    }
}
