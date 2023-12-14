using SASP.API.Dtos;
using SASP.API.Entities;

namespace SASP.API.Extensions
{
    public static class IssueDtoConversions
    {
        public static IEnumerable<IssueDto> ConvertIssueToDto(this IEnumerable<Issue> issues, IEnumerable<TypeIssue> typeIssues, IEnumerable<Catalog> catalogs)
        {
            return (from issue in issues
                    join type in typeIssues
                    on issue.TypeIssueId equals type.TypeIssueId
                    join catalog in catalogs
                    on issue.CatalogId equals catalog.CatalogId
                    select new IssueDto
                    {
                        IssueId = issue.IssueId,
                        Catalog = catalog.CatalogName,
                        TypeIssue = type.TypeName,
                        Price = issue.Price,
                        Title = issue.Title
                    }).ToList();
        }

        public static IssueDto ConvertIssueToDto(this Issue issues, TypeIssue typeIssues, Catalog catalogs)
        {
            return new IssueDto
            {
                IssueId = issues.IssueId,
                Catalog = catalogs.CatalogName,
                TypeIssue = typeIssues.TypeName,
                Price = issues.Price,
                Title = issues.Title
            };
        }
    }
}
