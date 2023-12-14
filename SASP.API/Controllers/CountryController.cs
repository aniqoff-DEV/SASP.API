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

                return Ok(newCountry);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Country>> DeleteCountry(int id)
        {
            try
            {
                var country = await _countryRepository.DeleteItem(id);

                if (country == null) return NotFound();

                return Ok(country);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPatch("{id:int}")]
        public async Task<ActionResult<Country>> UpdateCountry(int id, Country country)
        {
            try
            {
                var countryToUpdate = await _countryRepository.UpdateItem(id, country);

                if (countryToUpdate == null) return NotFound();

                return Ok(countryToUpdate);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
    }
}
