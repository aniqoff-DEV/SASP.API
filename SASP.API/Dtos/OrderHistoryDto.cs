namespace SASP.API.Dtos
{
    public class OrderHistoryDto
    {
        public int OrderId { get; set; }

        public string User { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Issue { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Status { get; set; }

    }
}
