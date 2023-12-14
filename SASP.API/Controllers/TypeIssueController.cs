using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SASP.API.Entities;
using SASP.API.Repositories.Contracts;

namespace SASP.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TypeIssueController : ControllerBase
    {
        private readonly IRepository<TypeIssue> _typeIssueRepository;

        public TypeIssueController(IRepository<TypeIssue> typeIssueRepository)
        {
            _typeIssueRepository = typeIssueRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeIssue>>> GetTypeIssues()
        {
            try
            {
                var types = await _typeIssueRepository.GetItems();

                if (types == null)
                {
                    return NotFound();
                }
                return Ok(types);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");

            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(TypeIssue), StatusCodes.Status201Created)]
        public async Task<IActionResult> PostTypeIssue(TypeIssue typeIssue)
        {
            try
            {
                var newTypeIssue = await _typeIssueRepository.CreateItem(typeIssue);

                if (newTypeIssue == null)
                {
                    return NoContent();
                }

                return Ok(newTypeIssue);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<TypeIssue>> DeleteTypeIssue(int id)
        {
            try
            {
                var type = await _typeIssueRepository.DeleteItem(id);

                if (type == null) return NotFound();

                return Ok(type);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPatch("{id:int}")]
        public async Task<ActionResult<TypeIssue>> UpdateTypeIssue(int id, TypeIssue typeIssue)
        {
            try
            {
                var typeToUpdate = await _typeIssueRepository.UpdateItem(id, typeIssue);

                if (typeToUpdate == null) return NotFound();

                return Ok(typeToUpdate);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
    }
}
