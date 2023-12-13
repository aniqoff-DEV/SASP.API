using Microsoft.AspNetCore.Mvc;
using SASP.API.Entities;
using SASP.API.Repositories.Contracts;

namespace SASP.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IRepository<Country> _countryRepository;

        public CountryController(IRepository<Country> countryRepository)
        {
            _countryRepository = countryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountry()
        {
            try
            {
                var country = await _countryRepository.GetItems();

                if (country == null)
                {
                    return NotFound();
                }
                return Ok(country);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");

            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(Country), StatusCodes.Status201Created)]
        public async Task<IActionResult> PostCountry(Country country)
        {
            try
            {
                var newCountry = await _countryRepository.CreateItem(country);

                if (newCountry == null)
                {
                    return NoContent();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
