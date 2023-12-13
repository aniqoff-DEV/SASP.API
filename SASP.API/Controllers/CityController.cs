using Microsoft.AspNetCore.Mvc;
using SASP.API.Entities;
using SASP.API.Repositories.Contracts;

namespace SASP.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IRepository<City> _cityRepository;

        public CityController(IRepository<City> cityRepository)
        {
            _cityRepository = cityRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> GetCities()
        {
            try
            {
                var cities = await _cityRepository.GetItems();

                if (cities == null)
                {
                    return NotFound();
                }
                return Ok(cities);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");

            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(City), StatusCodes.Status201Created)]
        public async Task<IActionResult> PostCity(City city)
        {
            try
            {
                var newCity = await _cityRepository.CreateItem(city);

                if (newCity == null)
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
