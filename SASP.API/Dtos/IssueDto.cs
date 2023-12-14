namespace SASP.API.Dtos
{
    public class IssueDto
    {
        public int IssueId { get; set; }

        public string Catalog { get; set; }

        public string TypeIssue { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }
    }
}
