using System.ComponentModel.DataAnnotations;

namespace SASP.API.Dtos
{
    public class OrderHistoryDto
    {
        public int OrderId { get; set; }

        public string User { get; set; }

        public string Issue { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
