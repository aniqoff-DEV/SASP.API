using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SASP.API.Entities;
using SASP.API.Repositories.Contracts;

namespace SASP.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IRepository<Catalog> _catalogRepository;

        public CatalogController(IRepository<Catalog> catalogRepository)
        {
            _catalogRepository = catalogRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Catalog>>> GetCatalogItems()
        {
            try
            {
                var catalogItems = await _catalogRepository.GetItems();

                if (catalogItems == null)
                {
                    return NotFound();
                }
                return Ok(catalogItems);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");

            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(Catalog), StatusCodes.Status201Created)]

        public async Task<IActionResult> PostCatalogItem(Catalog catalogItem)
        {
            try
            {
                var newCatalogItem = await _catalogRepository.CreateItem(catalogItem);

                if (newCatalogItem == null)
                {
                    return NoContent();
                }

                return Ok(newCatalogItem);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Catalog>> DeleteCatalogItem(int id)
        {
            try
            {
                var catalogItem = await _catalogRepository.DeleteItem(id);

                if (catalogItem == null) return NotFound();

                return Ok(catalogItem);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPatch("{id:int}")]
        public async Task<ActionResult<Catalog>> UpdateCatalogItem(int id, Catalog catalogItem)
        {
            try
            {
                var catalogItemToUpdate = await _catalogRepository.UpdateItem(id, catalogItem);

                if (catalogItemToUpdate == null) return NotFound();

                return Ok(catalogItemToUpdate);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
    }
}
