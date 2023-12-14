using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SASP.API.Dtos;
using SASP.API.Entities;
using SASP.API.Extensions;
using SASP.API.Repositories.Contracts;

namespace SASP.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        private readonly IRepository<Issue> _issueRepository;
        private readonly IRepository<Catalog> _catalogRepository;
        private readonly IRepository<TypeIssue> _typeIssueRepository;

        public IssueController(IRepository<Issue> issueRepository, IRepository<Catalog> catalogRepository, IRepository<TypeIssue> typeIssueRepository)
        {
            _issueRepository = issueRepository;
            _catalogRepository = catalogRepository;
            _typeIssueRepository = typeIssueRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IssueDto>>> GetIssues()
        {
            try
            {
                var issues = await _issueRepository.GetItems();
                var catalog = await _catalogRepository.GetItems();
                var type = await _typeIssueRepository.GetItems();

                if (issues == null)
                {
                    return NotFound();
                }
                else
                {
                    var issueDtos = issues.ConvertIssueToDto(type, catalog);

                    return Ok(issueDtos);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");

            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<IssueDto>> GetIssue(int id)
        {
            try
            {
                var issue = await _issueRepository.GetItemById(id);

                if (issue == null)
                {
                    return NotFound();
                }
                var catalog = await _catalogRepository.GetItemById(issue.CatalogId);
                var type = await _typeIssueRepository.GetItemById(issue.TypeIssueId);

                if (catalog == null || type == null)
                    return NotFound();

                var issueDto = issue.ConvertIssueToDto(type, catalog);

                return Ok(issueDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<IssueDto>> PostIssue([FromBody] Issue issue)
        {
            try
            {
                var newIssue = await _issueRepository.CreateItem(issue);

                if (newIssue == null)
                {
                    return NoContent();
                }

                var type = await _typeIssueRepository.GetItemById(issue.TypeIssueId);
                var catalog = await _catalogRepository.GetItemById(issue.CatalogId);

                if (catalog == null || type == null)
                    return NotFound();

                var issueDto = newIssue.ConvertIssueToDto(type, catalog);

                return Ok(issueDto);


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<IssueDto>> DeleteIssue(int id)
        {
            try
            {
                var issue = await _issueRepository.DeleteItem(id);

                if (issue == null)
                {
                    return NotFound();
                }

                var type = await _typeIssueRepository.GetItemById(issue.TypeIssueId);
                var catalog = await _catalogRepository.GetItemById(issue.CatalogId);

                if (catalog == null || type == null)
                    return NotFound();

                var issueDto = issue.ConvertIssueToDto(type, catalog);

                return Ok(issueDto);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPatch("{id:int}")]
        public async Task<ActionResult<IssueDto>> UpdateIssue(int id, Issue issue)
        {
            try
            {
                var issueToUpdate = await _issueRepository.UpdateItem(id, issue);
                if (issueToUpdate == null)
                {
                    return NotFound();
                }

                var type = await _typeIssueRepository.GetItemById(issue.TypeIssueId);
                var catalog = await _catalogRepository.GetItemById(issue.CatalogId);

                if (catalog == null || type == null)
                    return NotFound();

                var issueDto = issue.ConvertIssueToDto(type, catalog);

                return Ok(issueDto);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
    }
}
